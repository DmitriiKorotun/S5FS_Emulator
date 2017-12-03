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
                            var fileInDirName = Encoding.ASCII.GetString(block, j, 16);
                            var nextNodeNum = BitConverter.ToInt16(block, j + 16);
                            var type = BitConverter.ToBoolean(block, j + 18);
                            if (nextNodeNum < 1)
                                continue;
                            if (type)
                            {
                                tree.Nodes.Add(GetFileList(nextNodeNum, blockSize, fileInDirName));
                                //var child = GetFileList(nextNodeNum, blockSize, fileInDirName);
                                //tree.Nodes[tree.Nodes.Count - 1].Nodes.
                                //    Insert(tree.Nodes[tree.Nodes.Count - 1].Nodes.Count,
                                //    GetFileList(nextNodeNum, blockSize, new System.Windows.Forms.TreeNode()));
                            }
                            else
                                tree.Nodes.Add(fileInDirName);
                        }
                    }
                }
            }
            return tree;
        }


        public static short GetNeededFileNode(System.Windows.Forms.TreeNode tree, int blockSize)
        {
            var child = tree;
            var treeStack = new Stack<System.Windows.Forms.TreeNode>();
            if (tree.Text == "\\")
                return (short)SystemSigns.Signs.MAINDIRNODE;
            else
            {
                while (child.Parent != null)
                {
                    treeStack.Push(child);
                    child = child.Parent;
                }
            }
            using (System.IO.FileStream fs = System.IO.File.OpenRead("FS"))
                return GetCurrentFileNode(fs, treeStack, (short)SystemSigns.Signs.MAINDIRNODE, blockSize);
        }
            
        private static short GetCurrentFileNode(System.IO.FileStream fs, Stack<System.Windows.Forms.TreeNode> treeStack, short nodeNum, int blockSize)
        {
            var branch = treeStack.Pop();
            var newNodeNum = CheckDirForFile(fs, branch.Name, nodeNum, blockSize);
            if (newNodeNum < 0)
                throw new OSException.FileNotFoundException("Файл с именем " + branch.Name + " не был найден");
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

                var block = ByteReader.ReadBlock(fs, blockSize);
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
    }
}
