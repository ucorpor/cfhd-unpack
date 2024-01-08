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

            uint ver = Common.ReadUInt(binStream, isBigEndian);
            uint dirsCount = Common.ReadUInt(binStream, isBigEndian);
            uint filesCount = Common.ReadUInt(binStream, isBigEndian);
            if (form != null)
            {
                form.rmdpProgressLbl.Text = $"0 / {filesCount} files";
                Application.DoEvents();
            }

            Common.Skip(binStream, 8L);
            uint nameSize = Common.ReadUInt(binStream, isBigEndian);
            Common.Skip(binStream, 128L);

            KeyValuePair<long, long>[] dirs = new KeyValuePair<long, long>[dirsCount];
            for (uint i = 0; i < dirsCount; i++)
            {
                uint dirnameHash = Common.ReadUInt(binStream, isBigEndian);
                long nextNeighbourFolder = Common.ReadLong(binStream, isBigEndian);
                long parentIndex = Common.ReadLong(binStream, isBigEndian);
                uint unknown = Common.ReadUInt(binStream, isBigEndian);
                long dirnameOffset = Common.ReadLong(binStream, isBigEndian);
                long nextLowerFolder = Common.ReadLong(binStream);
                long nextFile = Common.ReadLong(binStream);
                dirs[i] = new KeyValuePair<long, long>(parentIndex, dirnameOffset);
            }

            long[,] files = new long[filesCount, 4];
            for (uint i = 0; i < filesCount; i++)
            {
                uint filenameHash = Common.ReadUInt(binStream, isBigEndian);
                long nextFile = Common.ReadLong(binStream, isBigEndian);
                long dirIndex = Common.ReadLong(binStream, isBigEndian);
                uint flags = Common.ReadUInt(binStream, isBigEndian);
                long filenameOffset = Common.ReadLong(binStream, isBigEndian);
                long contentOffset = Common.ReadLong(binStream, isBigEndian);
                long contentLength = Common.ReadLong(binStream, isBigEndian);
                long contentHash = Common.ReadUInt(binStream, isBigEndian);
                long writeTime = Common.ReadLong(binStream, isBigEndian);

                files[i, 0] = filenameOffset;
                files[i, 1] = contentOffset;
                files[i, 2] = contentLength;
                files[i, 3] = dirIndex;
            }

            Common.Skip(binStream, 48L);
            long start = binStream.Position;

            string[] dirpaths = new string[dirsCount];
            for (uint i = 0; i < dirsCount; i++)
            {
                long dirnameOffset = dirs[i].Value;
                if (dirnameOffset == -1L)
                {
                    dirpaths[i] = string.Empty; // root
                }
                else
                {
                    long parentIndex = dirs[i].Key;
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
                long contentLength = files[i, 2];
                long dirIndex = files[i, 3];

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
