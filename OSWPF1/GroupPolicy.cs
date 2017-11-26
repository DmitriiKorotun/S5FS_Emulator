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

        public void AddUser()
        {

        }

        private static short FindFreeUID(string data)
        {
            short freeID = 1;
            if (data == "")
            {
                var userGroup = data.Split(';');
                var id = userGroup[userGroup.Length - 1].Split(' ');
                freeID = Convert.ToInt16(id[0]);
            }
            return freeID;
        }

        private static short FindFreeGID(string data)
        {
            short freeID = 1;
            if (data == "")
            {
                var userGroup = data.Split(';');
                var id = userGroup[userGroup.Length - 1].Split(' ');
                freeID = Convert.ToInt16(id[2]);
            }
            return freeID;
        }
    }
}
