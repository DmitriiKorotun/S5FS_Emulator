using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSWPF1
{
    class FileDataStorage
    {
        Superblock superblock;
        public Superblock Superblock
        {
            get { return superblock; }
            set { superblock = value; }
        }

        Bitmap bitmap;
        public Bitmap Bitmap
        {
            get { return bitmap; }
            set { bitmap = value; }
        }

        Bitmap iNodeMap;
        public Bitmap INodeMap
        {
            get { return iNodeMap; }
            set { iNodeMap = value; }
        }

        public int NodeBlocksOffset
        {
            get { return INode.Offset * 15360; } //Change to be dynamic
        }

        public FileDataStorage()
        {
            superblock = new Superblock();
            bitmap = new Bitmap();
            iNodeMap = new Bitmap();
        }
    }
}
