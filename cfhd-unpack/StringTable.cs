using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;

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

        private static Dictionary<string, string> ReadStringTable(string txtPath)
        {
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
            return table;
        }

        public static void Pack(string txtPath, string binPath)
        {
            File.WriteAllText(binPath, string.Empty);
            Dictionary<string, string> table = ReadStringTable(txtPath);
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

        public static void TxtToExcel(string txtPath)
        {
            Dictionary<string, string> table = ReadStringTable(txtPath);

            Excel.Application excel = new Excel.Application();
            Excel.Workbook book = excel.Workbooks.Add();
            Excel.Worksheet sheet = book.Worksheets[1];
            sheet.Activate();

            int i = 0;
            foreach (KeyValuePair<string, string> pair in table)
            {
                i++;
                sheet.Cells[i, 1] = i;
                sheet.Cells[i, 2].NumberFormat = "@";
                sheet.Cells[i, 2] = pair.Key;
                sheet.Cells[i, 3].NumberFormat = "@";
                sheet.Cells[i, 3] = pair.Value;
            }

            sheet.Columns.AutoFit();
            sheet.Rows.AutoFit();
            excel.Visible = true;
        }

        public static void ExcelToTxt(string excelPath, string txtPath)
        {
            Excel.Application excel = new Excel.Application();
            Excel.Workbook book = excel.Workbooks.Add(excelPath);
            Excel.Worksheet sheet = book.Worksheets[1];
            sheet.Activate();

            string output = "";
            uint i = 1;
            string stringNumber = Convert.ToString(sheet.Cells[i, 1].Value2);
            while (!string.IsNullOrWhiteSpace(stringNumber))
            {
                string key = Convert.ToString(sheet.Cells[i, 2].Value2);
                string value = Convert.ToString(sheet.Cells[i, 4].Value2);
                output += $"{key}=`{value}`{Environment.NewLine}";

                stringNumber = Convert.ToString(sheet.Cells[++i, 1].Value2);
            }

            book.Close(0);
            excel.Quit();

            output = output.Remove(output.Length - Environment.NewLine.Length);
            File.WriteAllText(txtPath, output, Encoding.Unicode);
        }

    }
}
