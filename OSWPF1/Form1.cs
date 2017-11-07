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
            saveFileDialog1.FileName = "D:\\Test\\test";
            saveFileDialog1.DefaultExt = ".txt";
            saveFileDialog1.ShowDialog();
            var filename = saveFileDialog1.FileName;
            var lol = new FSHandler(filename);
            lol.GetData(new FileDataStorage());
           // lol.CreateFile(filename);
        }
    }
}
