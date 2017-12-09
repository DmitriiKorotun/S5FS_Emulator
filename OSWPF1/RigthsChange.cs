using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

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
            //if (DiagTools.IsFileLocked(new System.IO.FileInfo("FS"), false))
            //{
            //    throw new System.IO.IOException();
            //}
            Thread.Sleep(200);
            using (System.IO.FileStream fs = System.IO.File.Open("FS", System.IO.FileMode.Open,
System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite))
            {
                var node = DataExtractor.GetINode(fs, nodeNum);
                //node.
            }
        }
    }
}
