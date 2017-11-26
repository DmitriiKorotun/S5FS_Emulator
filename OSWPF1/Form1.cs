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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_makeFile_Click(object sender, EventArgs e)
        {
            var lol = new FSHandler();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var addFileForm = new AddFile();
            addFileForm.ShowDialog(this);
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
    }
}
