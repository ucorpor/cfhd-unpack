using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace control_unpack
{
    static class StringTable
    {

        public static void Unpack(string binPath)
        {
            string txtPath = $"{binPath}.txt";
            string output = "";
            Stream binStream = File.OpenRead(binPath);
            uint stringsCount = Common.ReadUInt(binStream);
            for (uint i = 0; i < stringsCount; i++)
            {
                uint keyLength = Common.ReadUInt(binStream);
                string key = Common.ReadASCII(binStream, keyLength);

                uint valueLength = Common.ReadUInt(binStream);
                string value = Common.ReadUTF16(binStream, valueLength);

                output += $"{key}=`{value}`{Environment.NewLine}";
            }
            binStream.Close();
            output = output.Remove(output.Length - Environment.NewLine.Length);
            File.WriteAllText(txtPath, output, Encoding.Unicode);
        }

        public static void Pack(string txtPath, string binPath)
        {
            File.WriteAllText(binPath, string.Empty);
            StreamReader txtReader = new StreamReader(txtPath, Encoding.Unicode, true);
            Dictionary<string, string> table = new Dictionary<string, string>(); 
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

                table.Add(key, value);
            }
            txtReader.Close();

            uint stringsCount = Convert.ToUInt32(table.Count);
            byte[] stringsCountBytes = BitConverter.GetBytes(stringsCount);
            Common.WriteBytes(stringsCountBytes, binPath);

            foreach (KeyValuePair<string, string> pair in table)
            {
                byte[] keyLength = BitConverter.GetBytes(pair.Key.Length);
                byte[] keyBytes = Encoding.ASCII.GetBytes(pair.Key);
                byte[] valueLength = BitConverter.GetBytes(pair.Value.Length);
                byte[] valueBytes = Encoding.Unicode.GetBytes(pair.Value);
                
                Common.WriteBytes(keyLength, binPath);
                Common.WriteBytes(keyBytes, binPath);
                Common.WriteBytes(valueLength, binPath);
                Common.WriteBytes(valueBytes, binPath);
            }
        }

    }
}
