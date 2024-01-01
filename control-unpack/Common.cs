using System;
using System.IO;
using System.Linq;
using System.Text;

namespace control_unpack
{
    internal class Common
    {

        public static byte[] ReadBytes(Stream stream, long count)
        {
            int intCount = (int) count;
            byte[] bytes = new byte[intCount];
            stream.Read(bytes, 0, intCount);
            return bytes;
        }

        public static byte ReadByte(Stream stream)
        {
            byte[] bytes = ReadBytes(stream, 1);
            return bytes[0];
        }

        public static uint ReadUInt(Stream stream, bool isBigEndian = false)
        {
            byte[] bytes = ReadBytes(stream, 4);
            if (isBigEndian) bytes = bytes.Reverse().ToArray();
            return BitConverter.ToUInt32(bytes, 0);
        }

        public static long ReadLong(Stream stream, bool isBigEndian = false)
        {
            byte[] bytes = ReadBytes(stream, 8);
            if (isBigEndian) bytes = bytes.Reverse().ToArray();
            return BitConverter.ToInt64(bytes, 0);
        }

        public static string ReadASCII(Stream stream, long length)
        {
            byte[] bytes = ReadBytes(stream, length);
            return Encoding.ASCII.GetString(bytes);
        }

        public static string ReadUTF16(Stream stream, long length)
        {
            byte[] bytes = ReadBytes(stream, length * 2);
            return Encoding.Unicode.GetString(bytes);
        }

        public static void WriteBytes(byte[] bytes, string path)
        {
            FileStream stream = new FileStream(path, FileMode.Append);
            stream.Write(bytes, 0, bytes.Length);
            stream.Close();
        }

        public static void ReadAndWriteBytes(Stream stream, long count, string path)
        {
            byte[] bytes = ReadBytes(stream, count);
            WriteBytes(bytes, path);
        }

        public static void Skip(Stream stream, long count)
        {
            stream.Seek(count, SeekOrigin.Current);
        }

    }
}
