using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSWPF1
{
    class GroupPolicy
    {
        public class Entity
        {
            string name;
            public string Name
            {
                get { return name; }
                set { name = value; }
            }

            protected short id;
            public short ID
            {
                get { return id; }
            }

            public Entity(string entName, string ugdata)
            {
                Name = entName;
            }
        }

        public class User : Entity
        {
            public User(string entName, string ugdata) : base(entName, ugdata)
            {
                id = FindFreeUID(ugdata);
            }
        }

        public class Group : Entity
        {
            public Group(string entName, string ugdata) : base(entName, ugdata)
            {
                id = FindFreeGID(ugdata);
            }
        }

        public User CreateUser(string name, string ugdata)
        {
            return new User(name, ugdata);
        }

        public Group CreateGroup(string name, string ugdata)
        {
            return new Group(name, ugdata);
        }

        public void AddUser(string name, int gid)
        {
            var example = "1 Petr;2 Admin";
        }

        private static short FindFreeUID(string data)
        {
            short freeID = 1;
            if (data != "")
            {
                var userGroup = data.Split(';');
                var id = userGroup[0].Split(' ');
                freeID = Convert.ToInt16(id[0]);
            }
            return freeID;
        }

        private static short FindFreeGID(string data)
        {
            short freeID = 1;
            if (data != "")
            {
                var userGroup = data.Split(';');
                var id = userGroup[1].Split(' ');
                freeID = Convert.ToInt16(id[0]);
            }
            return freeID;
        }

        private static string GetGroup(string data, short gid)
        {
            var group = "";
            if (data != "")
            {
                var userGroup = data.Split(';');
                var id = userGroup[1].Split(' ');
                if (Convert.ToInt16(id[0]) == gid)
                    group = id[1];
            }
            return group;
        }

        private static string GetUser(string data, short uid)
        {
            var user = "";
            if (data != "")
            {
                var userGroup = data.Split(';');
                var id = userGroup[0].Split(' ');
                if (Convert.ToInt16(id[0]) == uid)
                    user = id[1];
            }
            return user;
        }

        private static void WriteGroup(string name, short uid)
        {
            using (System.IO.FileStream fs = System.IO.File.OpenWrite("FS"))
            {
                var node = DataExtractor.GetINode(fs, 3);
                var freeBlock = DirHandler.GetFreeBlock(fs, node.Di_addr, 4096,                 //Make 4096 dynamic
                    OffsetHandbook.GetOffs(OffsetHandbook.sizeGuide.GROUP));
                var offset = DirHandler.GetOffset(BlocksHandler.GetBlock(fs, 4096, (short)freeBlock),
                    OffsetHandbook.GetOffs(OffsetHandbook.sizeGuide.GROUP));
                var block = ByteReader.ReadBlock(fs, 4096,
                    OffsetHandbook.GetPos(OffsetHandbook.posGuide.MAINDIR) + (freeBlock - 1) * 4096);
                var data = ComposeGroupInfo(name, uid);
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
    }
}
