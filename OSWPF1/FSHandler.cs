using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSWPF1
{
    class FSHandler
    {
        public FSHandler(string filepath)
        {
            SetPath(filepath);
        }

        // Path to the FS file
        string path;
        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        void SetPath(string path)
        {
            if (System.IO.File.Exists(path))
            {
                using (Logger.GetInstance(path + ".log"))
                {
                    Logger.GetInstance(path + ".log").Log(DateTime.Now +
                        " - Logpath has been set to " + path + ".log" + Environment.NewLine);
                    Logger.GetInstance(null).Log(DateTime.Now +
                        " - FSHandle has set path to file. Path is " + path + Environment.NewLine);
                }
            }
            else
            {
                Path = path; //TO REMOVE
                CreateMainFile(path);
            }
            Path = path;
        }

        //It creates the FS file and wrytes the superblock info to this file
        //After that it will fill the rest of the file with '0'
        void CreateMainFile(string filepath)
        {
            using (Logger.GetInstance(path + ".log"))
            {
                using (var fs = System.IO.File.Create(path))
                {
                    try
                    {
                        Logger.GetInstance(null).Log(DateTime.Now + " - MainFile was created with name: " + path
                            + Environment.NewLine);

                        var iniHandler = new IniFiles.IniHandler("data.ini");
                        Logger.GetInstance(null).Log(DateTime.Now + " - IniHandler was initialized with" +
                            " filename: " + path + "_data.ini" + Environment.NewLine);

                        SuperblockWriter.WriteSuperblock(fs, new Superblock(iniHandler));
                        ByteWriter.WriteJunk(fs, 63774720 - Superblock.Offset); //14 is superblock size
                    }
                    catch (Exception e)
                    {
                        Logger.GetInstance(null).Log(DateTime.Now + " - There was an EXCEPTION: "
                            + e.Message + Environment.NewLine + "EXCEPTION with path: " + path);
                        throw e;
                    }
                }
            }
        }

        private long GetBlockEnd(int blockSize, long position)
        {
            long blockEndAddr = position, remDiv = blockEndAddr % blockSize, diff = blockSize - remDiv;
            return blockEndAddr + diff;
        }

        private bool WriteFile(FileDataStorage storage, INode iNode)
        {
            using (System.IO.FileStream fs = System.IO.File.OpenRead(Path),
                temp = new System.IO.FileStream(fs.Name + ".temp", System.IO.FileMode.Create))
            {

                if (iNode.Size / 1024 > 8240)
                    throw new Exception("File size is too big");
                temp.SetLength(63774720);
                int nodeNum = DataExtractor.GetINodeNum(storage.INodeMap);

                SuperblockWriter.WriteSuperblock(temp, storage.Superblock);
                ByteWriter.CopyData(fs, temp, temp.Position, 
                    GetBlockEnd(storage.Superblock.ClusterSize, temp.Position), storage.Superblock.ClusterSize);

                var blocks = BlocksHandler.GetBlocksArr(storage.Bitmap, iNode.Size, storage.Superblock.ClusterSize);
                for (int i = 0; i < blocks.Length; ++i)
                {
                    storage.Bitmap.BitmapValue = BitWorker.TurnBitOn(storage.Bitmap.BitmapValue, blocks[i]);
                }

                BitmapWriter.WriteBitmap(temp, storage.Bitmap);
                ByteWriter.CopyData(fs, temp, temp.Position, GetBlockEnd((Superblock.UsedBlock +
                    Bitmap.UsedBlock) * storage.Superblock.ClusterSize, temp.Position), storage.Superblock.ClusterSize);

                storage.INodeMap.BitmapValue = BitWorker.TurnBitOn(storage.INodeMap.BitmapValue, nodeNum);

                BitmapWriter.WriteBitmap(temp, storage.INodeMap);
                ByteWriter.CopyData(fs, temp, temp.Position, GetBlockEnd((Superblock.UsedBlock +
                    Bitmap.UsedBlock + Bitmap.UsedBlock) * storage.Superblock.ClusterSize,
                    temp.Position), storage.Superblock.ClusterSize);

                var dataDict = BlocksHandler.GetDataArr(blocks, storage.Superblock.ClusterSize);

                List<int> dataKeys = new List<int>(dataDict.Keys);

                for (int index = 0; index < iNode.Di_addr.Length && index < dataDict.Count; ++index)
                {
                    iNode.Di_addr[index] = (short)dataKeys[index]; //To change short for int cause data can be lost
                }

                long bytesNum = (nodeNum - 1) * INode.Offset; //How many bytes method need to skip
                ByteWriter.CopyBytes(fs, temp, temp.Position, bytesNum, storage.Superblock.ClusterSize);
                ByteWriter.WriteINode(temp, iNode);
                ByteWriter.CopyData(fs, temp, temp.Position, (Superblock.UsedBlock + Bitmap.UsedBlock +
                    Bitmap.UsedBlock + storage.INodeBlocks) *
                    storage.Superblock.ClusterSize - 1, storage.Superblock.ClusterSize);

                for (int i = 0; i < storage.Bitmap.BitmapValue.Length * 8; ++i)
                {
                    if (dataKeys.Contains(i))
                    {
                        ByteWriter.WriteBlock(temp, storage.Superblock.ClusterSize, dataDict[i]);
                    }
                    else
                    {
                        ByteWriter.CopyBytes(fs, temp, temp.Position, 
                            storage.Superblock.ClusterSize, storage.Superblock.ClusterSize);
                    }
                }
                return true;
            }
        }

        public void AddFile(INode iNode)
        {
            WriteFile(DataExtractor.GetData(Path), iNode);
        }

    }
}
