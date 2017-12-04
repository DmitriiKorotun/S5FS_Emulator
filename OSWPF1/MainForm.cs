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
    public partial class MainForm : Form
    {
        bool isTreeExtended = false;
        private bool IsTreeExtended
        {
            get { return isTreeExtended; }
            set { isTreeExtended = value; }
        }

        public MainForm(short uid)
        {
            InitializeComponent();
            lbl_name.Text = GroupPolicy.GetUser(uid);
            UpdateFSView(tv_dirView);
        }

        public TreeView TV_FilesView
        {
               get { return tv_dirView; }
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void addFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateFSView(tv_dirView);
            AddFile();
            UpdateFSView(tv_dirView);
        }

        private void createFSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateFS();
            UpdateFSView(tv_dirView);
        }

        private void delFSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            delFS();
            UpdateFSView(tv_dirView);
        }

        private void btn_getProps_Click(object sender, EventArgs e)
        {
            UpdateExtendedFSView(tv_dirView);
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            UpdateFSView(tv_dirView);
        }

        private void AddFile()
        {
            try
            {
                var addFileForm = new AddFile();
                addFileForm.ShowDialog(this);
            }
            catch (NullReferenceException e)
            {
                if (e.Message == "Директория не выбрана")
                    MessageBox.Show("Директория не выбрана");
                else
                    throw e;
            }
        }

        private void CreateFS()
        {
            var fsHandler = new FSHandler();
        }

        private void delFS()
        {
            try
            {
                FSHandler.DelFS();
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("Файл не существует");
            }
        }

        private void UpdateFSView(TreeView view)
        {
            try
            {
                var selected = view.SelectedNode;
                Update(view);
                if (selected != null)
                {
                    if (!IsTreeExtended)
                        ResetSelected(view, selected);
                    else
                        CastSelected(view, selected);
                }
                IsTreeExtended = false;
            }
            catch (System.IO.FileNotFoundException)
            {
                view.Nodes.Add("Файловая система не инициализирована");
            }
        }

        private void Update(TreeView view)
        {
            view.Nodes.Clear();
            view.Nodes.Add(DirSeeker.GetFileList(1, 4096, "\\"));
        }

        private void UpdateExtendedFSView(TreeView view)
        {
            try
            {
                var selected = view.SelectedNode;
                UpdateExtended(view);
                if (selected != null)
                    ResetSelected(view, selected);
                IsTreeExtended = true;
            }
            catch (System.IO.FileNotFoundException)
            {
                view.Nodes.Add("Файловая система не инициализирована");
            }
            catch (NullReferenceException ex)
            {
                if (ex.Message == "Дерево не инициализировано")
                    MessageBox.Show(ex.Message);
                else
                    throw ex;
            }
        }

        private void UpdateExtended(TreeView view)
        {
            view.Nodes.Clear();
            view.Nodes.Add(DirSeeker.GetExtendedFileList(1, 4096, "\\"));
        }

        private void ResetSelected(TreeView view, TreeNode node)
        {
                var nodeStack = DirSeeker.GetNodes(node);
                if (nodeStack.Count == 0)
                    view.SelectedNode = view.Nodes[view.Nodes.Count - 1];
                else
                    view.SelectedNode = GetPrevSelected(nodeStack, view.Nodes[view.Nodes.Count - 1]);
        }

        private TreeNode GetPrevSelected(Stack<TreeNode> nodeStack, TreeNode node)
        {
            TreeNode neededNode = null;
            var currNode = nodeStack.Pop();
            foreach (TreeNode child in node.Nodes)
            {
                if (child.Text == currNode.Text)
                {
                    if (nodeStack.Count == 0)
                        neededNode = child;
                    else
                        neededNode = GetPrevSelected(nodeStack, child);
                    break;
                }
            }
            return neededNode;
        }

        private void CastSelected(TreeView view, TreeNode node)
        {
            var nodeStack = DirSeeker.GetNodes(node);
            if (nodeStack.Count == 0)
                view.SelectedNode = view.Nodes[view.Nodes.Count - 1];
            else
                view.SelectedNode = FindSimilarCasted(nodeStack, view.Nodes[view.Nodes.Count - 1]);
        }

        private TreeNode FindSimilarCasted(Stack<TreeNode> nodeStack, TreeNode node)
        {
            TreeNode neededNode = null;
            var currNode = nodeStack.Pop();
            foreach (TreeNode child in node.Nodes)
            {
                var unifiedName = DirSeeker.GetTruncatedName(child.Text);
                if (currNode.Text.Length >= unifiedName.Length)
                {
                    currNode.Text = currNode.Text.Remove(unifiedName.Length, currNode.Text.Length - unifiedName.Length);
                    if (currNode.Text == unifiedName)
                    {
                        if (nodeStack.Count == 0)
                            neededNode = child;
                        else
                            neededNode = FindSimilarCasted(nodeStack, child);
                        break;
                    }
                }

            }
            return neededNode;
        }

        private void addUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var addUserForm = new AddUser();
            addUserForm.ShowDialog(this);
        }

        private void AddGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var addGroupForm = new AddGroup();
            addGroupForm.ShowDialog(this);
        }

        private void banUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var banUser = new BanUser();
            banUser.ShowDialog(this);
        }
    }
}