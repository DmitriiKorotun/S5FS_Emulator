using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSWPF1
{
    class FSHandler
    {
        public FSHandler()
        {
            CheckForFile();
        }

        void CheckForFile()
        {
            if (!System.IO.File.Exists("FS"))
                CreateMainFile("FS");       
        }

        //It creates the FS file and wrytes the superblock info to this file
        //After that it will fill the rest of the file with '0'
        void CreateMainFile(string filepath)
        {
            using (var fs = System.IO.File.Create(filepath))
            {
                try
                {
                    var iniHandler = new IniFiles.IniHandler("data.ini");
                    FSPartsWriter.WriteSuperblock(fs, new Superblock(iniHandler));
                    ByteWriter.WriteJunk(fs, 63774720 - 4096); //4096 - is superblock size
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            DirHandler.WriteDir(new INode(SystemSigns.Signs.CREATEMAINDIR), "FS", 0); //Change 0 to INITDIR enum
            AddFile(new INode(SystemSigns.Signs.CREATEGROUPFILE), null, 1);
            AddFile(new INode(SystemSigns.Signs.CREATEUSERFILE), null, 1);
            AddFile(new INode(SystemSigns.Signs.CREATEBANNEDFILE), null, 1);
            GroupPolicy.WriteGroup("Admins");
            GroupPolicy.WriteUser("Admin", "root", 1);
        }

        public void AddFile(INode iNode, byte[] data, short dirNode)
        {
            var storage = DataExtractor.GetData("FS");
            DirHandler.AddFileToDir(iNode.Name, storage.Superblock.ClusterSize, dirNode,
                CommitFile(storage, iNode, "FS", data), iNode.Flag.Type);
        }

        public short CommitFile(FileDataStorage storage, INode iNode, string path, byte[] data)
        {
            var blocks = BlocksHandler.GetBlocksArr(storage.Bitmap, iNode.Size, storage.Superblock.ClusterSize);
            var nodeNum = CompleteFileInfo(ref storage, ref iNode, data, blocks);
            WriteFileInfo(storage, iNode, path, nodeNum);
            WriteFileBlocks(storage, GetDataDict(blocks, data, storage.Superblock.ClusterSize), path);
            return nodeNum;
        }

        private short CompleteFileInfo(ref FileDataStorage storage, ref INode iNode, byte[] data, short[] blocks)
        {
            if (iNode.Size / 1024 > 8240)
                throw new Exception("File size is too big");
            //Add exception for file is corrupted by checking size

            int nodeNum = DataExtractor.GetINodeNum(storage.INodeMap);
            for (int i = 0; i < blocks.Length; ++i)
            {
                storage.Bitmap.BitmapValue = BitWorker.TurnBitOn(storage.Bitmap.BitmapValue, blocks[i]);
            }
            storage.INodeMap.BitmapValue = BitWorker.TurnBitOn(storage.INodeMap.BitmapValue, nodeNum);
            iNode = FillDAddr(iNode, GetDataDict(blocks, data, storage.Superblock.ClusterSize));

            return (short)nodeNum;
        }

        private INode FillDAddr(INode node, Dictionary<int, byte[]> data)
        {
            var dataKeys = GetDataKeys(data);
            for (int index = 0; index < node.Di_addr.Length && index < data.Count; ++index)
            {
                node.Di_addr[index] = (short)dataKeys[index]; //To change short for int cause data can be lost
            }
            return node;
        }

        private void WriteFileInfo(FileDataStorage storage, INode iNode, string path, short nodeNum)
        {
            using (System.IO.FileStream fs = System.IO.File.OpenWrite(path))
            {
                fs.Position = OffsetHandbook.GetPos(OffsetHandbook.posGuide.BITMAP);
                FSPartsWriter.WriteBitmap(fs, storage.Bitmap, storage.Superblock.ClusterSize);
                FSPartsWriter.WriteBitmap(fs, storage.INodeMap, storage.Superblock.ClusterSize);
                fs.Position = OffsetHandbook.GetPos(OffsetHandbook.posGuide.INODES) +
                    OffsetHandbook.GetOffs(OffsetHandbook.sizeGuide.INODE) * (nodeNum - 1);
                FSPartsWriter.WriteINode(fs, iNode);

            }
        }

        private void WriteFileBlocks(FileDataStorage storage, Dictionary<int, byte[]> data, string path)
        {
            var dataKeys = GetDataKeys(data);
            using (System.IO.FileStream fs = System.IO.File.OpenWrite(path))
            {
                fs.Position = OffsetHandbook.GetPos(OffsetHandbook.posGuide.MAINDIR);
                for (int i = 1; i <= storage.Bitmap.BitmapValue.Length * 8; ++i)
                {
                    if (dataKeys.Contains(i))
                        ByteWriter.WriteBlock(fs, 
                            OffsetHandbook.GetPos(OffsetHandbook.posGuide.MAINDIR) + (i - 1) * 4096, //Make 4096 dynamic
                            storage.Superblock.ClusterSize, data[i]);
                }
            }
        }

        public static long GetBlockEnd(int blockSize, long position)
        {
            long blockEndAddr = position, remDiv = blockEndAddr % blockSize, diff = blockSize - remDiv;
            return blockEndAddr + diff;
        }

        private List<int> GetDataKeys(Dictionary<int, byte[]> data)
        {
            return new List<int>(data.Keys);
        }

        private Dictionary<int, byte[]> GetDataDict(short[] blocks, byte[] data, int clusterSize)
        {
            return BlocksHandler.GetDataArr(blocks, data, clusterSize);
        }



        public static void DelFS()
        {
            var path = "FS";
            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);
            else
                throw new System.IO.FileNotFoundException();
        }
    }
}
