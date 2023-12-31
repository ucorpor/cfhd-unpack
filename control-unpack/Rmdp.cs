using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace control_unpack
{
    internal class Rmdp
    {
        public static void Unpack(string rmdpPath, string binPath, string metaPath, MainForm form = null)
        {
            Stream binStream = File.OpenRead(binPath);
            byte endianness = Common.ReadByte(binStream);
            // endianness = 0x0 - pc (little-endian)
            // endianness != 0x0 - console (big-endian)
            bool isBigEndian = endianness != 0x0;

            int ver = Common.ReadInt(binStream, isBigEndian);
            int dirsCount = Common.ReadInt(binStream, isBigEndian);
            int filesCount = Common.ReadInt(binStream, isBigEndian);
            if (form != null)
            {
                form.rmdpProgressLbl.Text = $"0 / {filesCount} files";
                Application.DoEvents();
            }

            KeyValuePair<int, long>[] dirs = new KeyValuePair<int, long>[dirsCount];
            binStream.Seek(157, SeekOrigin.Begin);
            for (int i = 0; i < dirsCount; i++)
            {
                Common.Skip(binStream, 8L); // long
                // parentIndex < dirsCount
                int parentIndex = (int) Common.ReadLong(binStream, isBigEndian);
                Common.Skip(binStream, 4L); // int
                long dirnameOffset = Common.ReadLong(binStream, isBigEndian);
                int dirNumber = Common.ReadInt(binStream); // 1, 2, 3...
                Common.Skip(binStream, 16L); // long, long
                dirs[i] = new KeyValuePair<int, long>(parentIndex, dirnameOffset);
            }

            long[,] files = new long[filesCount, 4];
            for (int i = 0; i < filesCount; i++)
            {
                Common.Skip(binStream, 8L); // long
                long dirIndex = Common.ReadLong(binStream, isBigEndian);
                Common.Skip(binStream, 4L); // int
                long filenameOffset = Common.ReadLong(binStream, isBigEndian);
                long contentOffset = Common.ReadLong(binStream, isBigEndian);
                long contentLength = Common.ReadLong(binStream, isBigEndian);
                Common.Skip(binStream, 16L);

                files[i, 0] = filenameOffset;
                files[i, 1] = contentOffset;
                files[i, 2] = contentLength;
                files[i, 3] = dirIndex;
            }

            Common.Skip(binStream, 44L);
            long start = binStream.Position;

            string[] dirpaths = new string[dirsCount];
            for (int i = 0; i < dirsCount; i++)
            {
                long dirnameOffset = dirs[i].Value;
                if (dirnameOffset == -1L)
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
                File.WriteAllText(path, string.Empty);
                rmdpStream.Seek(contentOffset, SeekOrigin.Begin);
                Common.ReadAndWriteBytes(rmdpStream, contentLength, path);

                if (form != null)
                {
                    form.rmdpProgressLbl.Text = $"{i + 1} / {filesCount} files";
                    Application.DoEvents();
                }
            }

            binStream.Close();
            rmdpStream.Close();
        }

        private static string ReadFilename(Stream stream, long position)
        {
            List<byte> result = new List<byte>();
            stream.Seek(position, SeekOrigin.Begin);

            byte b = Common.ReadByte(stream);
            while (b != 0x0)
            {
                result.Add(b);
                b = Common.ReadByte(stream);
            }

            string filename = Encoding.UTF8.GetString(result.ToArray());
            return filename;
        }

    }
}
