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
            byte control = Common.ReadByte(binStream);
            // control = 0 - pc (little-endian), control != 0 - console (big-endian)
            bool isBigEndian = control != 0;

            int ver = Common.ReadInt(binStream, isBigEndian);
            int dirsCount = Common.ReadInt(binStream, isBigEndian);
            int filesCount = Common.ReadInt(binStream, isBigEndian);

            List<KeyValuePair<long, long>> dirs = new List<KeyValuePair<long, long>>();
            binStream.Seek(157, SeekOrigin.Begin);
            for (int i = 0; i < dirsCount; i++)
            {
                Common.Skip(binStream, 8); // long
                long parent = Common.ReadLong(binStream, isBigEndian);
                Common.Skip(binStream, 4); // int
                long dirnameOffset = Common.ReadLong(binStream, isBigEndian);
                Common.Skip(binStream, 20); // int, long, long
                KeyValuePair<long, long> pair = new KeyValuePair<long, long>(parent, dirnameOffset);
                dirs.Add(pair);
            }

            List<long[]> r = new List<long[]>();
            for (int i = 0; i < filesCount; i++)
            {
                Common.Skip(binStream, 8); // long
                long dirIndex = Common.ReadLong(binStream, isBigEndian);
                Common.Skip(binStream, 4); // int
                long filenameOffset = Common.ReadLong(binStream, isBigEndian);
                long contentOffset = Common.ReadLong(binStream, isBigEndian);
                long contentLength = Common.ReadLong(binStream, isBigEndian);
                Common.Skip(binStream, 16);
                long[] array = { filenameOffset, contentOffset, contentLength, dirIndex };
                r.Add(array);
            }

            Common.Skip(binStream, 44);
            long start = binStream.Position;
            List<string> fullNames = new List<string>();
            foreach (KeyValuePair<long, long> dir in dirs)
            {
                if (dir.Value == -1)
                {
                    fullNames.Add(string.Empty);
                }
                else
                {
                    string dirname = ReadFilename(binStream, start + dir.Value);
                    string path = Path.Combine(fullNames[Convert.ToInt32(dir.Key)], dirname);
                    fullNames.Add(path);
                }
            }

            Stream rmdpStream = File.OpenRead(rmdpPath);
            string rmdpDir = Path.GetDirectoryName(rmdpPath);
            foreach (long[] i in r)
            {
                long filenameOffset = i[0];
                long contentOffset = i[1];
                int contentLength = Convert.ToInt32(i[2]);
                long dirIndex = i[3];

                string filename = ReadFilename(binStream, start + filenameOffset);
                string dirname = Path.Combine(rmdpDir, fullNames[Convert.ToInt32(dirIndex)]);
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
