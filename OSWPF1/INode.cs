﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSWPF1
{
    class INode
    {
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

        struct flags
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
    }
}
