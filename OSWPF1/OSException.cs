using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSWPF1
{
    class OSException
    {
        public class DirBlocksException : Exception
        {
            public DirBlocksException(string message) : base(message)
            {
            }
        }
        public class BlockSpaceException : Exception
        {
            public BlockSpaceException(string message) : base(message)
            {
            }
        }
        public class FileCorruptedException : Exception { }
        public class BlockAppendException : Exception
        {
            public BlockAppendException(string message) : base(message)
            {
            }
        }
        public class FileNotFoundException : Exception
        {
            public FileNotFoundException(string message) : base(message)
            {
            }
        }
    }
}
