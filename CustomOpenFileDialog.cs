using System;
using System.Windows.Forms;

namespace CustomOpenFileDialog
{
    public partial class CustomOpenFileDialog : UserControl
    {
        private string _openButtonCaption = "Open";

        public CustomOpenFileDialog()
        {
            InitializeComponent();
        }

        public OpenFileDialog OpenDialog
        {
            get { return dlgOpen; }
        }
        
        public string OpenButtonCaption
        {
            get { return _openButtonCaption; }
            set { _openButtonCaption = value; }
        }

        public DialogResult ShowDialog()
        {
            return ShowDialog(null);
        }

        public string FileName { get; set; }
        public DialogResult ShowDialog(Form owner)
        {
            var returnDialogResult = DialogResult.Cancel;
            try
            {
                returnDialogResult = dlgOpen.ShowDialog(owner);
                if (returnDialogResult == DialogResult.OK)
                {
                    FileName = dlgOpen.FileName;
                    dlgOpen.FileName = "";
                }
                else
                {
                    FileName = string.Empty;
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return returnDialogResult;
        }
    }

    public enum ControlsId
    {
        ButtonOpen = 0x1,
        ButtonCancel = 0x2,
        ButtonHelp = 0x40E,
        GroupFolder = 0x440,
        LabelFileType = 0x441,
        LabelFileName = 0x442,
        LabelLookIn = 0x443,
        DefaultView = 0x461,
        LeftToolBar = 0x4A0,
        ComboFileName = 0x47c,
        ComboFileType = 0x470,
        ComboFolder = 0x471,
        CheckBoxReadOnly = 0x410
    }
}