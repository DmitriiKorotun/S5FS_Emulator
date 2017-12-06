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
    public partial class RigthsChange : Form
    {
        short nodeNum;
        public RigthsChange(short num, short rights)
        {
            InitializeComponent();
            nodeNum = num;

        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ActivateRights(short rights)
        {
            //check_aEx = BitWorker.IsBitOn()
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (DiagTools.IsFileLocked(new System.IO.FileInfo("FS")))
            {
                throw new System.IO.IOException();
            }
            using (System.IO.FileStream fs = System.IO.File.OpenWrite("FS"))
            {
                var node = DataExtractor.GetINode(fs, nodeNum);
                //node.
            }
        }
    }
}
