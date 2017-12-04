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
    public partial class Authorization : Form
    {
        short id;
        public short ID
        {
            get { return id; }
        }

        public Authorization()
        {
            InitializeComponent();
            tb_login.Text = "Admin";
            tb_pass.Text = "root";
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            LogIn();
        }

        private void LogIn()
        {
            if (tb_pass.Text.Length < 1)
                MessageBox.Show("Введите логин");
            else if (tb_login.Text.Length < 1)
                MessageBox.Show("Введите пароль");
            else
            {
                try
                {
                    if (GroupPolicy.CheckLoginPass(tb_login.Text, tb_pass.Text))
                        CreateMain();
                    else
                        MessageBox.Show("Неверные логин или пароль");
                }
                catch (System.IO.FileNotFoundException)
                {
                    CreateFS();
                    if (GroupPolicy.CheckLoginPass(tb_login.Text, tb_pass.Text))
                        CreateMain();
                    else
                        MessageBox.Show("Неверные логин или пароль");
                }
            }
        }

        private void CreateMain()
        {
            this.Hide();
            SetId(tb_login.Text);
            var mainform = new MainForm(ID);
            mainform.Closed += (s, args) => this.Close();
            mainform.Show();
        }
 
        private void SetId(string login)
        {
            var list = GroupPolicy.GetUserList();
            foreach (KeyValuePair<short, string[]> entry in list)
            {
                if (DirSeeker.GetTruncatedName(entry.Value[0]) == login)
                    id = entry.Key;
            }
        }

        private void CreateFS()
        {
            var fsHandler = new FSHandler();
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
