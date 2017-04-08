using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hardware;

namespace FileSystem
{
    class Inode
    {
        //ID > 128 error 
        public Inode(byte ID = 255)
        {
            if (ID == 255)
            {
                fileID = InodeTable.GetID();
            }
            else fileID = ID;
        }

        private byte[] arrayBlocks = new byte[1024];
        public byte GetArrayBlock(int index)
        {

            byte[] buffer = new byte[1];
            HDD.Read(ref buffer, SuperBlock.InodeStart + fileID * SuperBlock.InodeSize + index);
            arrayBlocks[index] = buffer[0];

            return arrayBlocks[index];
        }

        private byte fileID;

        private int blockCount = -1;
        public int BlockCount
        {
            get
            {
                if (blockCount == -1)
                {
                    byte[] buffer = new byte[4];
                    HDD.Read(ref buffer, SuperBlock.InodeStart + fileID * SuperBlock.InodeSize + 1024);
                    while (!HDD.isNullReadHandler()) ;

                    blockCount = BitConverter.ToInt32(buffer, 0);
                }
                return blockCount;
            }
            set
            {
                byte[] buffer = new byte[4];
                HDD.Write(buffer, SuperBlock.InodeStart + fileID * SuperBlock.InodeSize + 1024);
                while (!HDD.isNullWriteHandler()) ;

                blockCount = BitConverter.ToInt32(buffer, 0);
            }
        }

        public int modifyTime;
        public int accsesTime;

    }
}
