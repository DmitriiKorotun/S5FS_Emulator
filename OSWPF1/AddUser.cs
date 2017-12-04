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
    public partial class AddUser : Form
    {
        Dictionary<short, string> groups;
        public AddUser()
        {
            InitializeComponent();
            groups = GroupPolicy.GetGroupList();
            foreach (KeyValuePair<short, string> entry in groups)
            {
                cb_group.Items.Add(entry.Value);
            }
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            WriteUser();
        }

        private void WriteUser()
        {
            GroupPolicy.WriteUser(tb_login.Text, tb_pass.Text, GroupPolicy.GetID(cb_group.SelectedText, true));
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
