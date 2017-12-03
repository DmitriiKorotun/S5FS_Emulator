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

        public TreeView TV_FilesView
        {
            get { Update(tv_dirView); return tv_dirView; }
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            Update(tv_dirView);
        }

        private void Update(TreeView view)
        {
            var selectedText = view.SelectedNode == null ? "" : view.SelectedNode.Text;
            view.Nodes.Clear();
            view.Nodes.Add(DirSeeker.GetFileList(1, 4096, "\\"));
            view.SelectedNode = view.Nodes.Cast<TreeNode>().SingleOrDefault(n => n.Text == selectedText);
        }

        private void addFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var addFileForm = new AddFile();
            addFileForm.ShowDialog(this);
        }

        private void createFSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fsHandler = new FSHandler();
        }
    }
}
