using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSWPF1
{
    class FSHandler
    {
        void CheckForFile()
        {
            if (!System.IO.File.Exists("FS"))
                CreateMainFile("FS");       
        }

        public FSHandler()
        {
            CheckForFile();
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
            DirHandler.WriteDir(new INode(), "FS");
        }

        public static long GetBlockEnd(int blockSize, long position)
        {
            long blockEndAddr = position, remDiv = blockEndAddr % blockSize, diff = blockSize - remDiv;
            return blockEndAddr + diff;
        }

        private bool WriteFile(FileDataStorage storage, INode iNode, string data, string path)
        {
            using (System.IO.FileStream fs = System.IO.File.OpenWrite(path))
            {
                if (iNode.Size / 1024 > 8240)
                    throw new Exception("File size is too big");
                //Add exception for file is corrupted by checking size

                int nodeNum = DataExtractor.GetINodeNum(storage.INodeMap);
                fs.Position = GetBlockEnd(OffsetHandbook.GetSuperblockStart(), fs.Position);
                var blocks = BlocksHandler.GetBlocksArr(storage.Bitmap, iNode.Size, storage.Superblock.ClusterSize);

                for (int i = 0; i < blocks.Length; ++i)
                {
                    storage.Bitmap.BitmapValue = BitWorker.TurnBitOn(storage.Bitmap.BitmapValue, blocks[i]);
                }
                FSPartsWriter.WriteBitmap(fs, storage.Bitmap, storage.Superblock.ClusterSize);
                storage.INodeMap.BitmapValue = BitWorker.TurnBitOn(storage.INodeMap.BitmapValue, nodeNum);
                FSPartsWriter.WriteBitmap(fs, storage.INodeMap, storage.Superblock.ClusterSize);
                
                var dataDict = BlocksHandler.GetDataArr(blocks, storage.Superblock.ClusterSize);
                List<int> dataKeys = new List<int>(dataDict.Keys);
                for (int index = 0; index < iNode.Di_addr.Length && index < dataDict.Count; ++index)
                {
                    iNode.Di_addr[index] = (short)dataKeys[index]; //To change short for int cause data can be lost
                }

                fs.Position = GetBlockEnd(OffsetHandbook.GetIMapStart(), fs.Position) + INode.Offset * (nodeNum - 1);
                FSPartsWriter.WriteINode(fs, iNode);
                fs.Position = GetBlockEnd(OffsetHandbook.GetMainDirStart(), fs.Position);
                for (int i = 0; i < storage.Bitmap.BitmapValue.Length * 8; ++i)
                {
                    if (dataKeys.Contains(i))
                        ByteWriter.WriteBlock(fs, storage.Superblock.ClusterSize, dataDict[i]);
                }
                return true;
            }
        }

        public void AddFile(INode iNode, string path, string data)
        {
            WriteFile(DataExtractor.GetData(path), iNode, data, path);
        }


    }
}
