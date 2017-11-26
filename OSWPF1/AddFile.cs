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
            var iNode = new INode();
            byte[] ftype = Encoding.ASCII.GetBytes(GetDate(DateTime.Now.Date));
            iNode.CreationDate = BitConverter.ToInt64(ftype, 0);
            ftype = Encoding.ASCII.GetBytes(GetDate(DateTime.Now.Date));
            iNode.ChangeDate = BitConverter.ToInt64(ftype, 0);
            iNode.Flag.Hidden = chb_hidden.Checked;
            iNode.Flag.System = chb_system.Checked;
            iNode.Flag.Type = chb_dir.Checked;
            iNode.Size = (int)num_size.Value * 1024;
            iNode.GID = 1;
            iNode.UID = 1;
            iNode.Rights = 198;
            iNode.Name = tb_filename.Text;
            var handler = new FSHandler("D:\\Test\\test.txt");
            handler.AddFile(iNode);
        }

        private string GetDate(DateTime date)
        {
            string dateStr = date.ToString().Substring(0, 10);
            dateStr = dateStr.Remove(5, 1);
            dateStr = dateStr.Remove(2, 1);
            return dateStr;
        }

        private bool isAllCompeted()
        {
            if (tb_filename.Text != "" && num_size.Value != 0 && cbox_user.Text != "")
                return true;
            else
                return false;
        }
    }
}
