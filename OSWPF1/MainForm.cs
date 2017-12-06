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

        short currUid;
        private short CurrUid
        {
            get { return currUid; }
        }

        public MainForm(short uid)
        {
            InitializeComponent();
            currUid = uid;
            lbl_name.Text = GroupPolicy.GetUser(currUid);
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
                var addFileForm = new AddFile(CurrUid);
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
            view.Nodes.Add(DirSeeker.GetFileList(1, 4096, "\\", CurrUid, GroupPolicy.GetUserGID(CurrUid)));
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
            view.Nodes.Add(DirSeeker.GetExtendedFileList(1, 4096, "\\", CurrUid, GroupPolicy.GetUserGID(CurrUid)));
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
                var tempNodeText = currNode.Text;
                if (tempNodeText.Length >= unifiedName.Length)
                {
                    tempNodeText = tempNodeText.Remove(unifiedName.Length, tempNodeText.Length - unifiedName.Length);
                    if (tempNodeText == unifiedName)
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

        private void delFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var storage = DataExtractor.GetData("FS");
                var nodeNum = DirSeeker.GetNeededFileNode(tv_dirView.SelectedNode, storage.Superblock.ClusterSize);
                if (DirSeeker.IsDir(tv_dirView.SelectedNode, storage.Superblock.ClusterSize))
                    FileCleaner.DelDir(storage, nodeNum, CurrUid, GroupPolicy.GetUserGID(currUid));
                else
                    FileCleaner.DelOneFile(storage, storage.Superblock.ClusterSize, nodeNum,
                        DirSeeker.GetNeededFileNode(tv_dirView.SelectedNode.Parent, storage.Superblock.ClusterSize),
                        CurrUid, GroupPolicy.GetUserGID(currUid));
                UpdateFSView(tv_dirView);
            }
            catch (OSException.AccessException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (OSException.SystemDeleteException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void getFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                GetFile();
            }
            catch (OSException.AccessException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GetFile()
        {
            UpdateFSView(tv_dirView);
            var name = DirSeeker.GetTruncatedName(tv_dirView.SelectedNode.Text);
            using (System.IO.FileStream output = System.IO.File.OpenWrite(name))
            {
                long bytesWritten = 0;
                if (DiagTools.IsFileLocked(new System.IO.FileInfo("FS")))
                {
                    throw new System.IO.IOException();
                }
                var nodeNum = DirSeeker.GetNeededFileNode(tv_dirView.SelectedNode, 4096);
                if (DiagTools.IsFileLocked(new System.IO.FileInfo("FS")))
                {
                    throw new System.IO.IOException();
                }
                using (System.IO.FileStream input = System.IO.File.Open("FS", System.IO.FileMode.Open))
                {
                    var node = DataExtractor.GetINode(input, nodeNum);
                    if (DirSeeker.IfCan(node, SystemSigns.Signs.READ, currUid, GroupPolicy.GetUserGID(currUid)))
                    {
                        for (var i = 0; i < node.Di_addr.Length - 1; ++i)
                        {
                            if (node.Di_addr[i] == 0)
                                continue;
                            var block = ByteReader.ReadBlock(input, 4096,
                                (long)OffsetHandbook.posGuide.MAINDIR + (node.Di_addr[i] - 1) * 4096);
                            bytesWritten += ByteWriter.WriteOutputBlock(output, block);
                        }
                        if (node.Di_addr[node.Di_addr.Length - 1] != 0)
                        {
                            var block = ByteReader.ReadBlock(input, 4096,
                                (long)OffsetHandbook.posGuide.MAINDIR +
                                (node.Di_addr[node.Di_addr.Length - 1] - 1) * 4096);
                            if (BlocksHandler.GetOverallBlocks(node.Size, 4096) > 13)
                            {
                                for (var i = 0; i < block.Length - 1; i += 2)
                                {
                                    var blockNum = BitConverter.ToInt16(block, i);
                                    if (blockNum == 0)
                                        continue;
                                    if (blockNum == 108)
                                    {
                                        var w = 0;
                                    }
                                    var dataBlock = ByteReader.ReadBlock(input, 4096,
                                        (long)OffsetHandbook.posGuide.MAINDIR + (blockNum - 1) * 4096);
                                    if (node.Size - bytesWritten < 4096)
                                    {
                                        var j = 0;
                                        while (bytesWritten < node.Size)
                                        {
                                            output.WriteByte(dataBlock[j]);
                                            ++j;
                                            ++bytesWritten;
                                        }
                                    }
                                    else
                                        bytesWritten += ByteWriter.WriteOutputBlock(output, dataBlock);
                                }
                            }
                            else
                            {
                                if (node.Size - bytesWritten < 4096)
                                {
                                    var j = 0;
                                    while (bytesWritten < node.Size)
                                    {
                                        output.WriteByte(block[j]);
                                        ++j;
                                        ++bytesWritten;
                                    }
                                }
                                else
                                    ByteWriter.WriteBlock(output, 4096, block);
                            }
                        }
                    }
                    else
                        throw new OSException.AccessException("У вас недостаточно прав для выполнения данной операции");
                }
            }
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void changeRightsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
    }
}