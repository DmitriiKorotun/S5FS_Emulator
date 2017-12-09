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
        short uid;

        public AddFile(short id)
        {
            InitializeComponent();
            uid = id;
            check_uRead.Checked = true;
            check_uWrite.Checked = true;
            check_uEx.Checked = true;

            check_gRead.Checked = true;
            check_gWrite.Checked = true;
            check_gEx.Checked = true;

            check_aRead.Checked = true;
            check_aWrite.Checked = true;
            check_aEx.Checked = true;
        }


        public AddFile(short nodeNum, bool junk)
        {
            InitializeComponent();
            using (System.IO.FileStream fs = System.IO.File.Open("FS", System.IO.FileMode.Open,
                System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite))
            {
                var node = DataExtractor.GetINode(fs, nodeNum);
                for (var i = 0; i < node.Di_addr.Length - 1; ++i)
                {
                    if (node.Di_addr[i] == 0)
                        continue;
                    var block = BlocksHandler.GetBlock(fs, 4096, node.Di_addr[i]);
                    rtb_data.Text = Encoding.ASCII.GetString(block);
                }
                if (node.Size > 53248)
                {
                }
                tb_name.Text = node.Name;
            }
                
            //uid = id;
            check_uRead.Checked = true;
            check_uWrite.Checked = true;
            check_uEx.Checked = true;

            check_gRead.Checked = true;
            check_gWrite.Checked = true;
            check_gEx.Checked = true;

            check_aRead.Checked = true;
            check_aWrite.Checked = true;
            check_aEx.Checked = true;
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            TryToAddFile();
        }

        private void TryToAddFile()
        {
            if (!IsAllCompleted())
                MessageBox.Show("Вы заполнили не всю необходимую информацию");
            else
            {
                try
                {
                    this.btn_add.Enabled = false;
                    this.btn_exit.Enabled = false;
                    WriteFile();
                }
                catch (ArgumentOutOfRangeException exception)
                {
                    MessageBox.Show(exception.Message, "Значение слишком большое");
                }
                catch (NullReferenceException ex)
                {
                    if (ex.Message == "Директория не выбрана")
                        MessageBox.Show(ex.Message);
                    else
                        throw ex;
                }
                catch (ArgumentException ex)
                {
                    if (ex.Message == "Попытка записи в файл, а не папку")
                        MessageBox.Show(ex.Message);
                    else
                        throw ex;
                }
                finally
                {
                    this.btn_add.Enabled = true;
                    this.btn_exit.Enabled = true;
                }
            }
        }

        private bool IsAllCompleted()
        {
            var everythingIsOK = true;
            if (tb_name.Text == "")
                everythingIsOK = false;
            if (rtb_data.Text == "" && (tb_fullpath.Text == "" || num_size.Value == 0))
                everythingIsOK = false;
            return everythingIsOK;
        }

        private void WriteFile()
        {
            var tv_dirView = ((MainForm)this.Owner).TV_FilesView;
            if (!DirSeeker.IsDir(tv_dirView.SelectedNode, 4096)) //Make 4096 dynamic
                throw new ArgumentException("Попытка записи в файл, а не папку");
            if (tb_fullpath.Text != "")
                WriteExisting(tb_fullpath.Text, tv_dirView.SelectedNode);
            else
                WriteRaw(rtb_data.Text, tv_dirView.SelectedNode);
        }

        private void btn_openFile_Click(object sender, EventArgs e)
        {
            ChoseFile();
        }

        private void WriteExisting(string fullName, TreeNode tree)
        {
            if (fullName == "")
                throw new Exception("Файл не выбран");
            var iNode = GetINodeInfo();
            var handler = new FSHandler();
            handler.AddFile(iNode, System.IO.File.ReadAllBytes(fullName), DirSeeker.GetNeededFileNode(tree, 4096));
        }

        private void WriteRaw(string text, TreeNode tree)
        {
            var iNode = GetINodeInfo();
            if (text == "" && !iNode.Flag.Type)
                throw new Exception("Данные не введены");
            var handler = new FSHandler();
            if (iNode.Flag.Type == true)
                DirHandler.WriteDir(iNode, "FS", DirSeeker.GetNeededFileNode(tree, 4096)); //Make 4096 dynamic
            else
            {
                var data = Encoding.ASCII.GetBytes(text);
                iNode.Size = data.Length;
                handler.AddFile(iNode, data, DirSeeker.GetNeededFileNode(tree, 4096));
            }
                
        }

        private INode GetINodeInfo()
        {
            var iNode = new INode();
            byte[] ftype = Encoding.ASCII.GetBytes(DateWorker.GetDate(DateTime.Now.Date));
            iNode.CreationDate = BitConverter.ToInt64(ftype, 0);
            ftype = Encoding.ASCII.GetBytes(DateWorker.GetDate(DateTime.Now.Date));
            iNode.ChangeDate = BitConverter.ToInt64(ftype, 0);
            iNode.Flag.Hidden = chb_hidden.Checked;
            iNode.Flag.System = chb_system.Checked;
            iNode.Flag.Type = chb_dir.Checked;
            iNode.Size = (int)num_size.Value;
            iNode.GID = GroupPolicy.GetUserGID(uid);
            iNode.UID = uid;
            iNode.Rights = GetRights();
            iNode.Name = tb_name.Text;
            return iNode;
        }

        private short GetRights()
        {
            var rights = 0;
            var strRights = "";
            if (check_uRead.Checked)
                rights += 4;
            if (check_uWrite.Checked)
                rights += 2;
            if (check_uEx.Checked)
                rights += 1;
            strRights = rights.ToString();
            rights = 0;

            if (check_gRead.Checked)
                rights += 4;
            if (check_gWrite.Checked)
                rights += 2;
            if (check_gEx.Checked)
                rights += 1;
            strRights += rights.ToString();
            rights = 0;

            if (check_aRead.Checked)
                rights += 4;
            if (check_aWrite.Checked)
                rights += 2;
            if (check_gEx.Checked)
                rights += 1;
            strRights += rights.ToString();
            rights = 0;

            return Int16.Parse(strRights);
        }

        private void ChoseFile()
        {
            if (ofd_addFile.ShowDialog() == DialogResult.OK && System.IO.File.Exists(ofd_addFile.FileName))
            {
                var fileInfo = new System.IO.FileInfo(ofd_addFile.FileName);

                if (fileInfo.Length > num_size.Maximum)
                    MessageBox.Show("Размер файла слишком большой", "Ошибка");
                else
                {
                    SetValue(tb_name, fileInfo.Name);
                    SetValue(num_size, fileInfo.Length);
                    SetValue(tb_fullpath, fileInfo.FullName);
                }
            }
        }

        private void SetValue<T>(T control, string value) where T: Control
        {
            control.Text = value;
        }

        private void SetValue(NumericUpDown numeric, long value)
        {
            numeric.Value = value;
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            ResetExisting();
        }

        private void ResetExisting()
        {
            tb_fullpath.Text = "";
            num_size.Value = 0;
        }
    }
}
