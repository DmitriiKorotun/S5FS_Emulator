using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OSWPF1
{
    public partial class AddFile : Form
    {
        public AddFile()
        {
            InitializeComponent();
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            TryToWrite(tb_fullName.Text);
        }

        private void btn_openFile_Click(object sender, EventArgs e)
        {
            ChoseFile();
        }

        private void TryToWrite(string fullName)
        {
            if (fullName == "")
                throw new Exception("Файл не выбран");
            var iNode = new INode();
            byte[] ftype = Encoding.ASCII.GetBytes(DateWorker.GetDate(DateTime.Now.Date));
            iNode.CreationDate = BitConverter.ToInt64(ftype, 0);
            ftype = Encoding.ASCII.GetBytes(DateWorker.GetDate(DateTime.Now.Date));
            iNode.ChangeDate = BitConverter.ToInt64(ftype, 0);
            iNode.Flag.Hidden = chb_hidden.Checked;
            iNode.Flag.System = chb_system.Checked;
            iNode.Flag.Type = chb_dir.Checked;
            iNode.Size = (int)num_size.Value * 1024;
            iNode.GID = 1;
            iNode.UID = 1;
            iNode.Rights = 198;
            iNode.Name = tb_filename.Text;
            var handler = new FSHandler();
            if (iNode.Flag.Type == true)
                DirHandler.WriteDir(iNode, "FS");
            else
            {
                handler.AddFile(iNode, "FS", System.IO.File.ReadAllBytes(fullName), 1);
            }
        }

        private void ChoseFile()
        {
            if (ofd_addFile.ShowDialog() == DialogResult.OK && System.IO.File.Exists(ofd_addFile.FileName))
            {
                var fileInfo = new System.IO.FileInfo(ofd_addFile.FileName);

                if (fileInfo.Length / 1024 > num_size.Maximum)
                    MessageBox.Show("Размер файла слишком большой", "Ошибка");
                else
                {
                    SetValue(tb_filename, fileInfo.Name);
                    SetValue(num_size, fileInfo.Length / 1024);
                    SetValue(tb_fullName, fileInfo.FullName);
                }
            }
        }

        private bool isAllCompeted()
        {
            if (tb_filename.Text != "" && num_size.Value != 0 && cbox_user.Text != "")
                return true;
            else
                return false;
        }

        private void SetValue<T>(T control, string value) where T: Control
        {
            control.Text = value;
        }

        private void SetValue(NumericUpDown numeric, long value)
        {
            numeric.Value = value;
        }
    }
}
