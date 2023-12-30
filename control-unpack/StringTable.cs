using System;
using System.IO;
using System.Text;

namespace control_unpack
{
    static class StringTable
    {

        public static void Unpack(string binPath)
        {
            string txtPath = $"{binPath}.txt";
            Stream binStream = File.OpenRead(binPath);
            byte[] magic = Common.ReadBytes(binStream, 2);
            Common.Skip(binStream, 2); // skip

            string output = "";
            while (binStream.Position < binStream.Length)
            {
                int keyLength = Common.ReadInt(binStream);
                string key = Common.ReadStringASCII(binStream, keyLength);

                int valueLength = Common.ReadInt(binStream);
                string value = Common.ReadStringUTF16(binStream, valueLength);

                output += $"{key}=`{value}`{Environment.NewLine}";
            }
            binStream.Close();
            output = output.Remove(output.Length - Environment.NewLine.Length);

            FileStream txtStream = File.Create(txtPath);
            StreamWriter sw = new StreamWriter(txtStream, new UnicodeEncoding(false, true));
            sw.Write(output);
            sw.Close();
        }

        public static void Repack(string binPath, string repackedPath)
        {
            string txtFile = $"{binPath}.txt";
            File.WriteAllText(repackedPath, string.Empty);

            Stream binStream = File.OpenRead(binPath);
            byte[] magic = Common.ReadBytes(binStream, 4);
            Common.WriteBytes(magic, repackedPath);
            binStream.Close();

            StreamReader txtReader = new StreamReader(txtFile, Encoding.Unicode, true);
            while (!txtReader.EndOfStream)
            {
                string[] keyValue = txtReader.ReadLine().Split('=');
                string key = keyValue[0];
                string value = keyValue[1];
                if (value.Length > 0)
                {
                    while (value[value.Length - 1] != '`')
                    {
                        value += $"{Environment.NewLine}{txtReader.ReadLine()}";
                    }
                    value = value.Replace("`", "");
                }

                byte[] keyBytes = Encoding.ASCII.GetBytes(key);
                byte[] valueBytes = Encoding.Unicode.GetBytes(value);
                byte[] keyLength = BitConverter.GetBytes(keyBytes.Length);
                byte[] valueLength = BitConverter.GetBytes(valueBytes.Length / 2);
                Common.WriteBytes(keyLength, repackedPath);
                Common.WriteBytes(keyBytes, repackedPath);
                Common.WriteBytes(valueLength, repackedPath);
                Common.WriteBytes(valueBytes, repackedPath);
            }
            txtReader.Close();
        }
    }
}
