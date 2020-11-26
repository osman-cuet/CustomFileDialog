using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace CustomOpenFileDialog
{
    public partial class MainForm : Form
    {
        private readonly CustomOpenFileDialog _customOpenFileDialog;
        private OpenDialogNative _openNativeDialog;
        private IntPtr _openDialogHandle = IntPtr.Zero;

        public bool WatchForActivate { get; set; }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (_openNativeDialog != null)
                _openNativeDialog.Dispose();
            base.OnClosing(e);
        }

        protected override void WndProc(ref Message m)
        {
            if (WatchForActivate && m.Msg == (int)Msg.WM_ACTIVATE)
            {
                WatchForActivate = false;
                _openDialogHandle = m.LParam;
                _openNativeDialog = new OpenDialogNative(_openDialogHandle, _customOpenFileDialog);
            }
            base.WndProc(ref m);
        }

        public MainForm(CustomOpenFileDialog customOpenFileDialog)
        {
            WatchForActivate = false;
            _customOpenFileDialog = customOpenFileDialog;
            InitializeComponent();
        }

        private void OpenButtonClicked(object sender, EventArgs e)
        {
            _customOpenFileDialog.OpenDialog.AddExtension = true;
            _customOpenFileDialog.OpenDialog.CheckFileExists = false;
            _customOpenFileDialog.OpenDialog.Filter = @"Text files (*.txt)|*.txt";
            WatchForActivate = true;
            _customOpenFileDialog.ShowDialog();
            var filename = _customOpenFileDialog.FileName;
        }
    }
}