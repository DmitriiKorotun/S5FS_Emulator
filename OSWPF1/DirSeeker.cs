using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSWPF1
{
    class DirSeeker
    {
        private static bool IsOn(short rights, int byteNum, int bitNum)
        {
            var strRights = rights.ToString();
            var right = strRights[byteNum];
            return BitWorker.IsBitOn(Convert.ToByte(Char.GetNumericValue(right)), bitNum);
        }

        private static bool GetRightValue(SystemSigns.Signs entity, SystemSigns.Signs right, short rigths)
        {
            int bitNum = -1, byteNum = -1;
            switch (entity)
            {
                case SystemSigns.Signs.USER:
                    byteNum = 2;
                    break;
                case SystemSigns.Signs.GROUP:
                    byteNum = 1;
                    break;
                case SystemSigns.Signs.OTHER:
                    byteNum = 0;
                    break;
            }
            switch (right)
            {
                case SystemSigns.Signs.READ:
                    bitNum = 2;
                    break;
                case SystemSigns.Signs.WRITE:
                    bitNum = 1;
                    break;
                case SystemSigns.Signs.EX:
                    bitNum = 0;
                    break;
            }
            return IsOn(rigths, byteNum, bitNum);
        }

        public static bool DoNeedToHide(INode node, short uid, short gid)
        {
            bool toHide = false;
            if (node.Flag.Hidden)
            {
                if (node.UID == uid || node.GID == gid)
                    toHide = false;
                else
                    toHide = true;
            }
            return toHide;    
        }

        public static bool IfCan(INode node, SystemSigns.Signs whatToDo, short uid, short gid)
        {
            bool ifCanDo;
            if (node.UID == uid)
                ifCanDo = GetRightValue(SystemSigns.Signs.USER, whatToDo, node.Rights);
            else if (node.GID == gid)
                ifCanDo = GetRightValue(SystemSigns.Signs.GROUP, whatToDo, node.Rights);
            else
                ifCanDo = GetRightValue(SystemSigns.Signs.OTHER, whatToDo, node.Rights);
            return ifCanDo;
        }

        //public static System.Windows.Forms.TreeNode RemoveHidden(System.Windows.Forms.TreeNode tree, INode node,
        //    short uid, short gid)
        //{
        //    if (node.Rights = )
        //    foreach (System.Windows.Forms.TreeNode branch in tree.Nodes)
        //    {

        //    }
        //}


        public static System.Windows.Forms.TreeNode GetFileList(int nodeNum, int blockSize, string name,
            short uid, short gid)
        {
            var tree = new System.Windows.Forms.TreeNode(name);
            using (System.IO.FileStream fs = System.IO.File.OpenRead("FS"))
            {
                var node = DataExtractor.GetINode(fs, nodeNum);
                if (!DoNeedToHide(node, uid, gid))
                {
                    for (int i = 0; i < node.Di_addr.Length; ++i)
                    {
                        if (node.Di_addr[i] != 0)
                        {
                            var block = ByteReader.ReadBlock(fs, blockSize, OffsetHandbook.GetPos(OffsetHandbook.posGuide.MAINDIR) +
                                (node.Di_addr[i] - 1) * blockSize);
                            for (int j = 0; j < block.Length - 20;
                                j += OffsetHandbook.GetOffs(OffsetHandbook.sizeGuide.FILEINDIR))
                            {
                                var file = GetFile(block, j);
                                if (file.NodeNum < 1)
                                    continue;
                                if (!DoNeedToHide(DataExtractor.GetINode(fs, file.NodeNum), uid, gid))
                                {
                                    if (file.Type)
                                        tree.Nodes.Add(GetFileList(file.NodeNum, blockSize, file.Name, uid, gid));
                                    else
                                        tree.Nodes.Add(file.Name);
                                }
                            }
                        }
                    }
                }
            }
            return tree;
        }



        public static System.Windows.Forms.TreeNode GetExtendedFileList(int nodeNum, int blockSize, string name,
            short uid, short gid)
        {
            var tree = new System.Windows.Forms.TreeNode(GetTruncatedName(name));
            using (System.IO.FileStream fs = System.IO.File.OpenRead("FS"))
            {
                var node = DataExtractor.GetINode(fs, nodeNum);
                tree.Text += AddPropToName(node);

                for (int i = 0; i < node.Di_addr.Length; ++i)
                {
                    if (node.Di_addr[i] != 0)
                    {
                        var block = ByteReader.ReadBlock(fs, blockSize, OffsetHandbook.GetPos(OffsetHandbook.posGuide.MAINDIR) +
                            (node.Di_addr[i] - 1) * blockSize);
                        for (int j = 0; j < block.Length - 20;
                            j += OffsetHandbook.GetOffs(OffsetHandbook.sizeGuide.FILEINDIR))
                        {
                            var file = GetFile(block, j);
                            if (file.NodeNum < 1)
                                continue;
                            if (!DoNeedToHide(DataExtractor.GetINode(fs, file.NodeNum), uid, gid))
                            {
                                if (file.Type)
                                    tree.Nodes.Add(GetExtendedFileList(file.NodeNum, blockSize, file.Name, uid, gid));
                                else
                                    tree.Nodes.Add(GetTruncatedName(file.Name) +
                                        AddPropToName(DataExtractor.GetINode(fs, file.NodeNum)));
                            }
                        }
                    }
                }
            }
            return tree;
        }

        public static string GetTruncatedName(string name)
        {
            return name.Replace("\0", "");
        }

        public static string GetNormalizedName(string name)
        {
            if (name.Contains(':'))
            {
                var index = name.IndexOf(':');
                name = name.Remove(index - 1);
            }
            return name;
        }



    private static string AddPropToName(INode node)
        {
            var props = " : ";
            props += node.Flag.Type == true ? "d" : "f";
            props += node.Flag.Hidden == true ? "h" : "v";
            props += node.Flag.System == true ? "s " : "u ";
            props += GetRights(node);
            return props;
        }

        private static string GetRights(INode node)
        {
            var str = "";
            var rights = node.Rights.ToString();
            for (var i = 0; i < rights.Length; ++i)
            {
                var set = Convert.ToString((int)Char.GetNumericValue(rights[i]), 2);
                while (set.Length < 3)
                    set += "0";
                str += set[0] == '1' ? "r" : "-";
                str += set[1] == '1' ? "w" : "-";
                str += set[2] == '1' ? "x" : "-";
            }
            return str;
        }

        private static FileInDir GetFile(byte[] block, int startIndex)
        {
            var file = new FileInDir();
            file.Name = Encoding.ASCII.GetString(block, startIndex, 16);
            file.NodeNum = BitConverter.ToInt16(block, startIndex + 16);
            file.Type = BitConverter.ToBoolean(block, startIndex + 18);
            return file;
        }



        public static short GetNeededFileNode(System.Windows.Forms.TreeNode tree, int blockSize)
        {
            if (tree == null)
                throw new NullReferenceException("Директория не выбрана");
            var treeStack = new Stack<System.Windows.Forms.TreeNode>();
            if (tree.Text == "\\")
                return (short)SystemSigns.Signs.MAINDIRNODE;
            else
            {
                treeStack = GetNodes(tree);
                if (DiagTools.IsFileLocked(new System.IO.FileInfo("FS")))
                {
                    throw new System.IO.IOException();
                }
                using (System.IO.FileStream fs = System.IO.File.OpenRead("FS"))
                    return GetCurrentFileNode(fs, treeStack, (short)SystemSigns.Signs.MAINDIRNODE, blockSize);
            }
        }

        public static Stack<System.Windows.Forms.TreeNode> GetNodes(System.Windows.Forms.TreeNode tree)
        {
            if (tree == null)
                throw new NullReferenceException("Дерево не инициализировано");
            var treeStack = new Stack<System.Windows.Forms.TreeNode>();
            while (tree.Parent != null)
            {
                treeStack.Push(tree);
                tree = tree.Parent;
            }
            return treeStack;
        }

        private static short GetCurrentFileNode(System.IO.FileStream fs, Stack<System.Windows.Forms.TreeNode> treeStack, short nodeNum, int blockSize)
        {
            var branch = treeStack.Pop();
            var newNodeNum = CheckDirForFile(fs, branch.Text, nodeNum, blockSize);
            if (newNodeNum < 0)
                throw new OSException.FileNotFoundException("Файл с именем " + branch.Text + " не был найден");
            if (treeStack.Count == 0)
                return newNodeNum;
            else
                return GetCurrentFileNode(fs, treeStack, newNodeNum, blockSize);  
        }

        private static short CheckDirForFile(System.IO.FileStream fs, string name, short nodeNum, int blockSize)
        {
            var node = DataExtractor.GetINode(fs, nodeNum);
            var newNodeNum = -1;
            for (var i = 0; i < node.Di_addr.Length; ++i)
            {
                if (node.Di_addr[i] == 0)
                    continue;

                var block = ByteReader.ReadBlock(fs, blockSize,
                    OffsetHandbook.GetPos(OffsetHandbook.posGuide.MAINDIR) + (node.Di_addr[i] - 1) * blockSize);
                newNodeNum = FindFileNode(block, name);

                if (newNodeNum > -1)
                    break;
            }
            return (short)newNodeNum;
        }

        private static short FindFileNode(byte[] dirBlock, string name)
        {
            short fileNode = -1;
            for (var i = 0; i < dirBlock.Length / OffsetHandbook.GetOffs(OffsetHandbook.sizeGuide.FILEINDIR); i += 20)
            {
                var currFileName = Encoding.ASCII.GetString(dirBlock, i, 16);
                if (currFileName == name)
                {
                    fileNode = BitConverter.ToInt16(dirBlock, i + 16);
                    break;
                }
            }
            return fileNode;
        }



        public static bool IsDir(System.Windows.Forms.TreeNode tree, int blockSize)
        {

            using (System.IO.FileStream fs = System.IO.File.OpenRead("FS"))
                if (DataExtractor.GetINode(fs, GetNeededFileNode(tree, blockSize)).Flag.Type)
                    return true;
            return false;
        }


        public static int GetFileDirBlockNum(System.IO.FileStream fs, int blockSize, short dirNum, short fileNum)
        {
            var blockNum = -1;
            var node = DataExtractor.GetINode(fs, dirNum);
            for (var i = 0; i < node.Di_addr.Length; ++i)
            {
                if (node.Di_addr[i] == 0)
                    continue;
                if (GetFileDirOffset(BlocksHandler.GetBlock(fs, blockSize, node.Di_addr[i]), fileNum) < 0)
                    continue;
                blockNum = node.Di_addr[i];
                break;
            }
            return blockNum;
        }

        public static int GetFileDirOffset(byte[] block, short fileNum)
        {
            var offset = -1;
            for (int j = 16; j < block.Length / (int)OffsetHandbook.sizeGuide.FILEINDIR;
                  j += (int)OffsetHandbook.sizeGuide.FILEINDIR)
            {
                var nodeNum = BitConverter.ToInt16(block, j);
                if (nodeNum != fileNum)
                    continue;
                offset = j - 16;
                break;
            }
            return offset;
        }
    }


}
