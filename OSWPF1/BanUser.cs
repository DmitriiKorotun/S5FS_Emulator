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
    public partial class BanUser : Form
    {
        Dictionary<short, string[]> groups;

        public BanUser()
        {
            InitializeComponent();
            groups = GroupPolicy.GetUserList();
            foreach (KeyValuePair<short, string[]> entry in groups)
            {
                cb_users.Items.Add(entry.Value[0]);
            }
        }

        private void btn_ban_Click(object sender, EventArgs e)
        {
            var id = GroupPolicy.GetID(cb_users.SelectedText, false);
            GroupPolicy.BanUser(id);
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
