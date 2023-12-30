using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace control_unpack
{
    internal class Common
    {

        public static byte[] ReadBytes(Stream stream, int length)
        {
            byte[] bytes = new byte[length];
            stream.Read(bytes, 0, length);
            return bytes;
        }

        public static string ReadStringASCII(Stream stream, int length)
        {
            byte[] bytes = ReadBytes(stream, length);
            return Encoding.ASCII.GetString(bytes);
        }

        public static string ReadStringUTF16(Stream stream, int length)
        {
            byte[] bytes = ReadBytes(stream, length);
            return Encoding.Unicode.GetString(bytes);
        }

        public static void WriteBytes(byte[] bytes, string path)
        {
            FileStream stream = new FileStream(path, FileMode.Append);
            stream.Write(bytes, 0, bytes.Length);
            stream.Close();
        }

        private static void ReadAndWriteBytes(Stream stream, int length, string path)
        {
            byte[] bytes = ReadBytes(stream, length);
            WriteBytes(bytes, path);
        }

    }
}
