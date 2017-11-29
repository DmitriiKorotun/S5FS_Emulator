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
            var tree = new System.Windows.Forms.TreeNode();
            tree.Nodes.Add(name);
            using (System.IO.FileStream fs = System.IO.File.OpenRead("FS"))
            {
                var node = DataExtractor.GetINode(fs, nodeNum);
                for (int i = 0; i < node.Di_addr.Length; ++i)
                {
                    if (node.Di_addr[i] != 0)
                    {
                        var block = ByteReader.ReadBlock(fs, blockSize, OffsetHandbook.GetPos(OffsetHandbook.posGuide.MAINDIR) +
                            node.Di_addr[i] * blockSize);
                        for (int j = 0; j < block.Length; ++j)
                        {
                            var fileInDirName = ByteConverter.StringFromBytes(fs, 16);
                            var nextNodeNum = ByteConverter.ShortFromBytes(fs);
                            var type = Convert.ToBoolean(fs.ReadByte());
                            tree.Nodes[tree.Nodes.Count - 1].Nodes.Add(name);
                            if (type)
                            {
                                tree.Nodes[tree.Nodes.Count - 1].Nodes[tree.Nodes.Count - 1].Nodes.
                                    Insert(0, GetFileList(nextNodeNum, blockSize, fileInDirName));
                            }
                        }
                    }
                }
            }  
            return tree;
        }

        //public static System.Windows.Forms.TreeNode GetFileList(INode node, int blockSize)
        //{
        //    var tree = new System.Windows.Forms.TreeNode();
        //    tree.Nodes.Add("\\");
        //    for (int i = 0; i < node.Di_addr.Length; ++i)
        //    {
        //        if (node.Di_addr[i] != 0)
        //        {
        //            using (System.IO.FileStream fs = System.IO.File.OpenRead("FS"))
        //            {
        //                var block = ByteReader.ReadBlock(fs, blockSize, OffsetHandbook.GetMainDirStart() +
        //                    node.Di_addr[i] * blockSize);
        //                for (int j = 0; j < block.Length; ++j)
        //                {
        //                    var name = ByteConverter.StringFromBytes(fs, 16);
        //                    fs.Position += 2;
        //                    var type = Convert.ToBoolean(fs.ReadByte());
        //                    tree.Nodes[tree.Nodes.Count - 1].Nodes.Add(name);
        //                    if (type)
        //                        tree.Nodes[tree.Nodes.Count - 1].Nodes[tree.Nodes.Count].Nodes.Add("test");
        //                }
        //            }
        //        }
        //    }
        //    return tree;
        //}

        public static long FindPlaceInDir(int nodeNum)
        {
            long offset = 0;
            using (System.IO.FileStream fs = System.IO.File.OpenRead("FS"))
            {
                var node = DataExtractor.GetINode(fs, nodeNum);
                for (int i = 0; i < node.Di_addr.Length; ++i)
                {
                    if (node.Di_addr[i] != 0)
                    {
                        var block = ByteReader.ReadBlock(fs, 4096, 
                            OffsetHandbook.GetPos(OffsetHandbook.posGuide.MAINDIR) + node.Di_addr[i] * 4096); //To change 4096
                        bool isFree = true;
                        for (int k = 0; k < 4096; k += 20)
                        {
                            for (int j = 0; j < 20; ++j)
                            {
                                if (block[k + j] == 0)
                                    continue;
                                isFree = false;
                            }
                            if (isFree)
                            {
                                offset = OffsetHandbook.GetPos(OffsetHandbook.posGuide.MAINDIR)
                                    + (node.Di_addr[i] - 1) * 4096 + k;
                            }
                        }
                    }
                }
            }
            return offset;
        }
    }
}
