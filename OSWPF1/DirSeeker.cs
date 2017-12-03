using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSWPF1
{
    class DirSeeker
    {
        public static System.Windows.Forms.TreeNode GetFileList(int nodeNum, int blockSize, string name)
        {
            var tree = new System.Windows.Forms.TreeNode(name);

            using (System.IO.FileStream fs = System.IO.File.OpenRead("FS"))
            {
                var node = DataExtractor.GetINode(fs, nodeNum);
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
                            if (file.Type)
                                tree.Nodes.Add(GetFileList(file.NodeNum, blockSize, file.Name));
                            else
                                tree.Nodes.Add(file.Name);
                        }
                    }
                }
            }
            return tree;
        }



        public static System.Windows.Forms.TreeNode GetExtendedFileList(int nodeNum, int blockSize, string name)
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
                            if (file.Type)
                                tree.Nodes.Add(GetExtendedFileList(file.NodeNum, blockSize, file.Name));
                            else
                                tree.Nodes.Add(GetTruncatedName(file.Name) + AddPropToName(DataExtractor.GetINode(fs, file.NodeNum)));
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

        public static string AddPropToName(INode node)
        {
            var props = " : ";
            props += node.Flag.Type == true ? "d" : "f";
            props += node.Flag.Hidden == true ? "h" : "v";
            props += node.Flag.System == true ? "s " : "u ";
            props += GetRights(node);
            return props;
        }

        public static string GetRights(INode node)
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

        public static FileInDir GetFile(byte[] block, int startIndex)
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
    }
}
