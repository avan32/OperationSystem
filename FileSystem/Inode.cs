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
                //Console.WriteLine(fileID);
            }
            else fileID = ID;
        }

        private int[] arrayBlocks = new int[1024];//block numbers
        public int GetArrayBlock(int index)
        {

            byte[] buffer = new byte[4];
            HDD.Read(ref buffer, SuperBlock.InodeStart + fileID * SuperBlock.InodeSize + 4*index);
            arrayBlocks[index] = BitConverter.ToInt32(buffer,0);

            return arrayBlocks[index];
        }

        public void SetArrayBlock(int index, int number) // index , number block
        {
            byte[] buffer = new byte[4];
            buffer = BitConverter.GetBytes(number);
            HDD.Write(buffer, SuperBlock.InodeStart + fileID * SuperBlock.InodeSize + index);
            arrayBlocks[index] = number;
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
                buffer = BitConverter.GetBytes(value);
                HDD.Write(buffer, SuperBlock.InodeStart + fileID * SuperBlock.InodeSize + 1024);
                while (!HDD.isNullWriteHandler()) ;

                blockCount = value;
            }
        }

        public int modifyTime;
        public int accsesTime;

    }
}
