using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace control_unpack
{
    internal class Rmdp
    {
        public static void Unpack(string rmdpPath, string binPath, string metaPath)
        {
            Stream binStream = File.OpenRead(binPath);
            byte endianness = Common.ReadByte(binStream);
            // endianness = 0 - pc (little-endian)
            // endianness != 0 - console (big-endian)
            bool isBigEndian = endianness != 0;

            int ver = Common.ReadInt(binStream, isBigEndian);
            int dirsCount = Common.ReadInt(binStream, isBigEndian);
            int filesCount = Common.ReadInt(binStream, isBigEndian);

            KeyValuePair<int, long>[] dirs = new KeyValuePair<int, long>[dirsCount];
            binStream.Seek(157, SeekOrigin.Begin);
            for (int i = 0; i < dirsCount; i++)
            {
                Common.Skip(binStream, 8); // long
                // parentIndex < dirsCount
                int parentIndex = (int) Common.ReadLong(binStream, isBigEndian);
                Common.Skip(binStream, 4); // int
                long dirnameOffset = Common.ReadLong(binStream, isBigEndian);
                Common.Skip(binStream, 20); // int, long, long
                dirs[i] = new KeyValuePair<int, long>(parentIndex, dirnameOffset);
            }

            long[,] files = new long[filesCount, 4];
            for (int i = 0; i < filesCount; i++)
            {
                Common.Skip(binStream, 8); // long
                long dirIndex = Common.ReadLong(binStream, isBigEndian);
                Common.Skip(binStream, 4); // int
                long filenameOffset = Common.ReadLong(binStream, isBigEndian);
                long contentOffset = Common.ReadLong(binStream, isBigEndian);
                long contentLength = Common.ReadLong(binStream, isBigEndian);
                Common.Skip(binStream, 16);

                files[i, 0] = filenameOffset;
                files[i, 1] = contentOffset;
                files[i, 2] = contentLength;
                files[i, 3] = dirIndex;
            }

            Common.Skip(binStream, 44);
            long start = binStream.Position;

            string[] dirpaths = new string[dirsCount];
            for (int i = 0; i < dirsCount; i++)
            {
                long dirnameOffset = dirs[i].Value;
                if (dirnameOffset == -1)
                {
                    dirpaths[i] = string.Empty; // root
                }
                else
                {
                    int parentIndex = dirs[i].Key;
                    string dirname = ReadFilename(binStream, start + dirnameOffset);
                    dirpaths[i] = Path.Combine(dirpaths[parentIndex], dirname);
                }
            }

            Stream rmdpStream = File.OpenRead(rmdpPath);
            string rmdpDir = Path.GetDirectoryName(rmdpPath);
            for (int i = 0; i < filesCount; i++)
            {
                long filenameOffset = files[i, 0];
                long contentOffset = files[i, 1];
                // stream can't read more than int.MaxValue bytes
                int contentLength = (int) files[i, 2];
                int dirIndex = (int) files[i, 3]; // dirIndex < dirCount

                string filename = ReadFilename(binStream, start + filenameOffset);
                string dirname = Path.Combine(rmdpDir, dirpaths[dirIndex]);
                string path = Path.Combine(dirname, filename);
                Directory.CreateDirectory(dirname);
                rmdpStream.Seek(contentOffset, SeekOrigin.Begin);
                Common.ReadAndWriteBytes(rmdpStream, contentLength, path);
            }

            binStream.Close();
            rmdpStream.Close();
        }

        public static string ReadFilename(Stream stream, long position)
        {
            List<byte> result = new List<byte>();
            stream.Seek(position, SeekOrigin.Begin);
            byte b = Common.ReadByte(stream);
            while (b != 0)
            {
                result.Add(b);
                b = Common.ReadByte(stream);
            }
            string filename = Encoding.UTF8.GetString(result.ToArray());
            return filename;
        }

    }
}
