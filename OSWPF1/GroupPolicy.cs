using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSWPF1
{
    class GroupPolicy
    {
        private static int GetUserId(string login, bool isIdGid)
        {
            var id = -1;
            using (System.IO.FileStream fs = System.IO.File.OpenWrite("FS"))
            {
                var node = DataExtractor.GetINode(fs, 3);
                for (int i = 0; i < node.Di_addr.Length; ++i)
                {
                    if (node.Di_addr[i] == 0)
                        continue;
                    var block = BlocksHandler.GetBlock(fs, 4096, node.Di_addr[i]);
                    for (var j = 0; j < block.Length / (int)OffsetHandbook.sizeGuide.USER;
                        j += (int)OffsetHandbook.sizeGuide.USER)
                    {
                        if (login == BitConverter.ToString(block, j, 16))
                        {
                            if (!isIdGid)
                                id = BitConverter.ToInt16(block, j + 32);
                            else
                                id = BitConverter.ToInt16(block, j + 34);
                            break;
                        }    
                    }
                    if (id > -1)
                        break;
                }
            }
            return id;
        }

        public static short GetUserGID(short uid)
        {
            var gid = -1;
            var list = GetUserList();
            foreach (KeyValuePair<short, string[]> pair in list)
            {
                if (pair.Key != uid)
                    continue;
                else
                {
                    gid = Int16.Parse(pair.Value[1]);
                } 
            }
            return (short)gid;
        }

        public static void WriteGroup(string name)
        {
            WriteUserGroup(name, null, -1, GetFreeGID(), (int)OffsetHandbook.sizeGuide.GROUP, 2);
        }

        public static void WriteUser(string name, string pass, short gid)
        {
            WriteUserGroup(name, pass, GetFreeUID(), gid, (int)OffsetHandbook.sizeGuide.USER, 3);
        }

        public static void WriteUserGroup(string name, string pass, short uid, short gid, int incr, int nodeNum)
        {
            using (System.IO.FileStream fs = System.IO.File.Open("FS", System.IO.FileMode.Open))
            {
                var node = DataExtractor.GetINode(fs, nodeNum);
                int freeBlock = -1;
                try
                {
                    
                    freeBlock = DirHandler.GetFreeBlock(fs, node.Di_addr, 4096, incr);            //Make 4096 dynamic
                }
                catch (OSException.DirBlocksException)
                {
                    DirHandler.SetNewDirBlock(fs, ref node, 4096, (short)nodeNum);
                    //DirHandler.AppendBlockToFile(ref node, DataExtractor.GetBitmap(fs, 1920));
                    freeBlock = DirHandler.GetFreeBlock(fs, node.Di_addr, 4096, incr);            //Make 4096 dynamic
                }
                var offset = DirHandler.GetOffset(BlocksHandler.GetBlock(fs, 4096, (short)freeBlock), incr);

                var block = ByteReader.ReadBlock(fs, 4096,                                  //Make 4096 dynamic
                    OffsetHandbook.GetPos(OffsetHandbook.posGuide.MAINDIR) + (freeBlock - 1) * 4096);
                byte[] data = new byte[0];
                if (incr == OffsetHandbook.GetOffs(OffsetHandbook.sizeGuide.GROUP))
                    data = ComposeGroupInfo(name, gid);
                else
                    data = ComposeUserInfo(name, pass, uid, gid);
                data.CopyTo(block, offset);
                ByteWriter.WriteBlock(fs,
                    OffsetHandbook.GetPos(OffsetHandbook.posGuide.MAINDIR) + (freeBlock - 1) * 4096,
                    4096, block);
            }
        }

        private static byte[] ComposeGroupInfo(string name, short uid)
        {
            var data = new byte[OffsetHandbook.GetOffs(OffsetHandbook.sizeGuide.GROUP)];
            byte[] gName = Encoding.ASCII.GetBytes(name);
            if (gName.Length > 16)
                throw new OverflowException("FileName is too long");
            gName.CopyTo(data, 0);
            BitConverter.GetBytes(uid).CopyTo(data, 16);
            return data;
        }

        private static byte[] ComposeUserInfo(string name, string pass, short uid, short gid)
        {
            var data = new byte[OffsetHandbook.GetOffs(OffsetHandbook.sizeGuide.USER)];
            var uName = Encoding.ASCII.GetBytes(name);
            if (uName.Length > 16)
                throw new OverflowException("FileName is too long");
            var passbytes = Encoding.ASCII.GetBytes(pass);
            uName.CopyTo(data, 0);
            passbytes.CopyTo(data, 16);
            BitConverter.GetBytes(uid).CopyTo(data, 32);
            BitConverter.GetBytes(gid).CopyTo(data, 34);
            return data;
        }

        public static bool CheckLoginPass(string login, string pass)
        {
            var isOk = false;
            using (System.IO.FileStream fs = System.IO.File.OpenRead("FS"))
            {
                var node = DataExtractor.GetINode(fs, 3);
                for (int i = 0; i < node.Di_addr.Length; ++i)
                {
                    if (node.Di_addr[i] == 0)
                        continue;
                    var block = BlocksHandler.GetBlock(fs, 4096, node.Di_addr[i]);
                    for (var j = 0; j < block.Length / (int)OffsetHandbook.sizeGuide.USER;
                        j += (int)OffsetHandbook.sizeGuide.USER)
                    {
                        if (login == DirSeeker.GetTruncatedName(Encoding.ASCII.GetString(block, j, 16)) &&
                             DirSeeker.GetTruncatedName(Encoding.ASCII.GetString(block, j + 16, 16)) == pass)
                        {
                            isOk = true;
                            break;
                        }
                    }
                    if (isOk)
                        break;
                }
            }
            return isOk;
        }

        public static short GetFreeUID()
        {
            var id = 1;
            using (System.IO.FileStream fs = System.IO.File.OpenRead("FS"))
            {
                var node = DataExtractor.GetINode(fs, 3);
                for (int i = 0; i < node.Di_addr.Length; ++i)
                {
                    if (node.Di_addr[i] == 0)
                        continue;
                    var block = BlocksHandler.GetBlock(fs, 4096, node.Di_addr[i]);
                    var prevUid = 0;
                    for (var j = 0; j < block.Length / (int)OffsetHandbook.sizeGuide.USER;
                        j += (int)OffsetHandbook.sizeGuide.USER)
                    {
                        var uid = BitConverter.ToInt16(block, j + 32);
                        if (uid == 0)
                        {
                            id = prevUid + 1;
                            break;
                        }
                        prevUid = uid;
                    }
                }
            }
            return (short)id;
        }

        public static short GetFreeGID()
        {
            var id = 1;
            using (System.IO.FileStream fs = System.IO.File.OpenRead("FS"))
            {
                var node = DataExtractor.GetINode(fs, 2);
                for (int i = 0; i < node.Di_addr.Length; ++i)
                {
                    if (node.Di_addr[i] == 0)
                        continue;
                    var block = BlocksHandler.GetBlock(fs, 4096, node.Di_addr[i]);
                    var prevGid = 0;
                    for (var j = 0; j < block.Length / (int)OffsetHandbook.sizeGuide.GROUP;
                        j += (int)OffsetHandbook.sizeGuide.GROUP)
                    {
                        var gid = BitConverter.ToInt16(block, j + 16);
                        if (gid == 0)
                        {
                            id = prevGid + 1;
                            break;
                        }
                        prevGid = gid;
                    }
                }
            }
            return (short)id;
        }

        public static Dictionary<short, string> GetGroupList()
        {
            var list = new Dictionary<short, string>();
            using (System.IO.FileStream fs = System.IO.File.OpenRead("FS"))
            {
                var node = DataExtractor.GetINode(fs, 2);
                for (int i = 0; i < node.Di_addr.Length; ++i)
                {
                    if (node.Di_addr[i] == 0)
                        continue;
                    var block = BlocksHandler.GetBlock(fs, 4096, node.Di_addr[i]);
                    for (var j = 0; j < block.Length / (int)OffsetHandbook.sizeGuide.GROUP;
                        j += (int)OffsetHandbook.sizeGuide.GROUP)
                    {
                        var gid = BitConverter.ToInt16(block, j + 16);
                        if (gid == 0)
                            break;
                        list.Add(gid, DirSeeker.GetTruncatedName(Encoding.ASCII.GetString(block, j, 16)));
                    }
                }
            }
            return list;
        }

        public static Dictionary<short, string[]> GetUserList()
        {
            var list = new Dictionary<short, string[]>();
            using (System.IO.FileStream fs = System.IO.File.OpenRead("FS"))
            {
                var node = DataExtractor.GetINode(fs, 3);
                for (int i = 0; i < node.Di_addr.Length; ++i)
                {
                    if (node.Di_addr[i] == 0)
                        continue;
                    var block = BlocksHandler.GetBlock(fs, 4096, node.Di_addr[i]);
                    for (var j = 0; j < block.Length / (int)OffsetHandbook.sizeGuide.USER;
                        j += (int)OffsetHandbook.sizeGuide.USER)
                    {
                        var uid = BitConverter.ToInt16(block, j + 32);
                        if (uid == 0)
                            break;
                        list.Add(uid, new string[]
                        { DirSeeker.GetTruncatedName(Encoding.ASCII.GetString(block, j, 16)),
                            BitConverter.ToInt16(block, j + 34).ToString() });
                    }
                }
            }
            return list;
        }

        public static string GetUser(short uid)
        {
            var name = "";
            var uList = GetUserList();
            foreach (KeyValuePair<short, string[]> entry in uList)
            {
                if (uid == entry.Key)
                    name = entry.Value[0];
            }
            return name;
        }

        public static string GetGroup(short gid)
        {
            var name = "";
            var gList = GetGroupList();
            foreach (KeyValuePair<short, string> entry in gList)
            {
                if (gid == entry.Key)
                    name = entry.Value;
            }
            return name;
        }

        public static short GetID(string name, bool isGroup)
        {
            var id = -1;
            if (isGroup)
            {
                var gList = GetGroupList();
                foreach (KeyValuePair<short, string> entry in gList)
                {
                    if (name == entry.Value)
                        id = entry.Key;
                }
            }
            else
            {
                var uList = GetUserList();
                foreach (KeyValuePair<short, string[]> entry in uList)
                {
                    if (name == entry.Value[0])
                        id = entry.Key;
                }
            }
            return (short)id;
        }

        public static void BanUser(short uid)
        {
            WriteBanned(uid, (int)OffsetHandbook.sizeGuide.BANNED, 4);
        }

        private static void WriteBanned(short uid, int incr, int nodeNum)
        {
            using (System.IO.FileStream fs = System.IO.File.Open("FS", System.IO.FileMode.Open))
            {
                var node = DataExtractor.GetINode(fs, nodeNum);
                int freeBlock = -1;
                try
                {
                    freeBlock = DirHandler.GetFreeBlock(fs, node.Di_addr, 4096, incr);            //Make 4096 dynamic
                }
                catch (OSException.DirBlocksException)
                {
                    DirHandler.SetNewDirBlock(fs, ref node, 4096, (short)nodeNum);
                    freeBlock = DirHandler.GetFreeBlock(fs, node.Di_addr, 4096, incr);            //Make 4096 dynamic
                }
                var offset = DirHandler.GetOffset(BlocksHandler.GetBlock(fs, 4096, (short)freeBlock), incr);

                var block = ByteReader.ReadBlock(fs, 4096,                                  //Make 4096 dynamic
                    OffsetHandbook.GetPos(OffsetHandbook.posGuide.MAINDIR) + (freeBlock - 1) * 4096);
                var data = BitConverter.GetBytes(uid);
                data.CopyTo(block, offset);
                ByteWriter.WriteBlock(fs,
                    OffsetHandbook.GetPos(OffsetHandbook.posGuide.MAINDIR) + (freeBlock - 1) * 4096,
                    4096, block);
            }
        }
    }
}
