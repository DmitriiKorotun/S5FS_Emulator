using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSWPF1
{
    class INode
    {
        public INode()
        {
            flag = new Flags();
        }

        long changeDate;
        public long ChangeDate
        {
            get { return changeDate; }
            set { changeDate = value; }
        }

        long creationDate;
        public long CreationDate
        {
            get { return creationDate; }
            set { creationDate = value; }
        }

        public class Flags
        {
            bool system;
            public bool System
            {
                get { return system; }
                set { system = value; }
            }

            bool hidden;
            public bool Hidden
            {
                get { return hidden; }
                set { hidden = value; }
            }

            bool type;
            public bool Type
            {
                get { return type; }
                set { type = value; }
            }
        }

        Flags flag;
        public Flags Flag
        {
            get { return flag; }
            set { flag = value; }
        }

        static int offset;
        public static int Offset
        {
            get { return 54; } //x17 short + x1 int + x3(4) bool + x2 long; 55?
        }

        int size;
        public int Size
        {
            get { return size; }
            set { size = value; }
        }

        short uid;
        public short UID
        {
            get { return uid; }
            set { uid = value; }
        }

        short gid;
        public short GID
        {
            get { return gid; }
            set { gid = value; }
        }

        short id;
        public short Id
        {
            get { return id; }
            set { id = value; }
        }

        short rights;
        public short Rights
        {
            get { return rights; }
            set { rights = value; }
        }

        short[] di_addr;
        public short[] Di_addr
        {
            get { if (di_addr == null)
                    di_addr = new short[13];
                 return di_addr; }
            set { di_addr = value; }
        }

        string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (value.Length > 16)
                    throw new Exception("String is too big");
                name = value;
            }
        }
    }
}
