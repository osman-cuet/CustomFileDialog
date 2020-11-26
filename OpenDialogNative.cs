using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace CustomOpenFileDialog
{
    public class OpenDialogNative : NativeWindow, IDisposable
    {
        private const SetWindowPosFlags UFLAGSZORDER =
            SetWindowPosFlags.SWP_NOACTIVATE |
            SetWindowPosFlags.SWP_NOMOVE |
            SetWindowPosFlags.SWP_NOSIZE;

        private readonly IntPtr _openDialogHandle;
        private IntPtr _listViewPtr;
        private WindowInfo _listViewInfo;
        private IntPtr _comboFolders;
        private WindowInfo _comboFoldersInfo;
        private IntPtr _groupButtons;
        private WindowInfo _groupButtonsInfo;
        private IntPtr _comboFileName;
        private WindowInfo _comboFileNameInfo;
        private IntPtr _comboExtensions;
        private WindowInfo _comboExtensionsInfo;
        private IntPtr _openButton;
        private WindowInfo _openButtonInfo;
        private IntPtr _cancelButton;
        private WindowInfo _cancelButtonInfo;
        private IntPtr _helpButton;
        private WindowInfo _helpButtonInfo;
        private readonly CustomOpenFileDialog _sourceControl;
        private IntPtr _toolBarFolders;
        private WindowInfo _toolBarFoldersInfo;
        private IntPtr _labelFileName;
        private WindowInfo _labelFileNameInfo;
        private IntPtr _labelLookIn;
        private WindowInfo _labelLookInInfo;
        private IntPtr _labelFileType;
        private WindowInfo _labelFileTypeInfo;
        private IntPtr _chkReadOnly;
        private WindowInfo _chkReadOnlyInfo;
        private Rect _openDialogWindowRect;
        private Rect _openDialogClientRect;

        public OpenDialogNative(IntPtr handle, CustomOpenFileDialog sourceControl)
        {
            _openDialogHandle = handle;
            _sourceControl = sourceControl;
            AssignHandle(_openDialogHandle);
        }

        public void Dispose()
        {
            ReleaseHandle();
        }

        private void PopulateWindowsHandlers()
        {
            Win32.EnumChildWindows(_openDialogHandle, new Win32.EnumWindowsCallBack(OpenFileDialogEnumWindowCallBack), 0);
        }

        private bool OpenFileDialogEnumWindowCallBack(IntPtr hwnd, int lParam)
        {
            var className = new StringBuilder(256);
            Win32.GetClassName(hwnd, className, className.Capacity);
            var controlId = Win32.GetDlgCtrlID(hwnd);
            WindowInfo windowInfo;
            Win32.GetWindowInfo(hwnd, out windowInfo);
            switch ((ControlsId)controlId)
            {
                case ControlsId.DefaultView:
                    _listViewPtr = hwnd;
                    _listViewInfo = windowInfo;
                    break;
                case ControlsId.ComboFolder:
                    _comboFolders = hwnd;
                    _comboFoldersInfo = windowInfo;
                    break;
                case ControlsId.ComboFileType:
                    _comboExtensions = hwnd;
                    _comboExtensionsInfo = windowInfo;
                    break;
                case ControlsId.ComboFileName:
                    if (className.ToString().ToLower() == "comboboxex32")
                    {
                        _comboFileName = hwnd;
                        _comboFileNameInfo = windowInfo;
                    }
                    break;
                case ControlsId.GroupFolder:
                    _groupButtons = hwnd;
                    _groupButtonsInfo = windowInfo;
                    break;
                case ControlsId.LeftToolBar:
                    _toolBarFolders = hwnd;
                    _toolBarFoldersInfo = windowInfo;
                    break;
                case ControlsId.ButtonOpen:
                    _openButton = hwnd;
                    _openButtonInfo = windowInfo;
                    break;
                case ControlsId.ButtonCancel:
                    _cancelButton = hwnd;
                    _cancelButtonInfo = windowInfo;
                    break;
                case ControlsId.ButtonHelp:
                    _helpButton = hwnd;
                    _helpButtonInfo = windowInfo;
                    break;
                case ControlsId.CheckBoxReadOnly:
                    _chkReadOnly = hwnd;
                    _chkReadOnlyInfo = windowInfo;
                    break;
                case ControlsId.LabelFileName:
                    _labelFileName = hwnd;
                    _labelFileNameInfo = windowInfo;
                    break;
                case ControlsId.LabelFileType:
                    _labelFileType = hwnd;
                    _labelFileTypeInfo = windowInfo;
                    break;
                case ControlsId.LabelLookIn:
                    _labelLookIn = hwnd;
                    _labelLookInInfo = windowInfo;
                    break;
            }
            return true;
        }

        private void InitControls()
        {
            Win32.GetClientRect(_openDialogHandle, ref _openDialogClientRect);
            Win32.GetWindowRect(_openDialogHandle, ref _openDialogWindowRect);
            PopulateWindowsHandlers();
            Win32.SetParent(_sourceControl.Handle, _openDialogHandle);
            Win32.SetWindowPos(_sourceControl.Handle, (IntPtr)ZOrderPos.HWND_BOTTOM, 0, 0, 0, 0, UFLAGSZORDER);
            Win32.SetWindowText(new HandleRef(this, _openButton), _sourceControl.OpenButtonCaption);// Set your desired text
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == (int)Msg.WM_SHOWWINDOW)
            {
                InitControls();
            }
            base.WndProc(ref m);
        }
    }
}