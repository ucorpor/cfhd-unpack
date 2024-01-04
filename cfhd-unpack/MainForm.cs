using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace control_unpack
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string version = FileVersionInfo.GetVersionInfo(assembly.Location).FileVersion;
            Text += $" v{version}";
            rmdpProgressLbl.Text = string.Empty;
        }

        private void stringPathBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.FileName = "string_table.bin";
            dialog.Filter = "BIN-files (*.bin)|*.bin"
                + "|All files (*.*)|*.*";
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                stringPathTxt.Text = dialog.FileName;
            }
        }

        private void stringUnpackBtn_Click(object sender, EventArgs e)
        {
            Enabled = false;
            Cursor = Cursors.WaitCursor;

            try
            {
                string path = stringPathTxt.Text.Trim();
                StringTable.Unpack(path);

                string txtPath = $"{path}.txt";
                string message = $"Successfully unpacked to:{Environment.NewLine}{txtPath}";
                MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/ucorpor/control-unpack");
        }

        private void stringsPackBtn_Click(object sender, EventArgs e)
        {
            Enabled = false;
            Cursor = Cursors.WaitCursor;

            try
            {
                OpenFileDialog txtFileDialog = new OpenFileDialog();
                txtFileDialog.FileName = "string_table.bin.txt";
                txtFileDialog.Filter = "TXT-files (*.txt)|*.txt"
                    + "|All files (*.*)|*.*";

                DialogResult result = txtFileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string txtPath = txtFileDialog.FileName;
                    if (File.Exists(txtPath))
                    {
                        SaveFileDialog saveDialog = new SaveFileDialog();
                        saveDialog.RestoreDirectory = true;
                        saveDialog.FileName = "string_table.bin";
                        saveDialog.Filter = "BIN-files (*.bin)|*.bin"
                            + "|All files (*.*)|*.*";

                        if (saveDialog.ShowDialog() == DialogResult.OK)
                        {
                            string savePath = saveDialog.FileName;
                            StringTable.Pack(txtPath, savePath);
                            string message = $"Created string_table.bin saccessufully saved to:{Environment.NewLine}{savePath}";
                            MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show($"File not found:{Environment.NewLine}{txtPath}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private void rmdpPathBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "RMDP-files (*.rmdp)|*.rmdp"
                + "|All files (*.*)|*.*";
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                rmdpPathTxt.Text = dialog.FileName;
            }
        }

        private void rmdpUnpackBtn_Click(object sender, EventArgs e)
        {
            Enabled = false;
            Cursor = Cursors.WaitCursor;

            try
            {
                string rmdpPath = rmdpPathTxt.Text.Trim();
                string rmdpFilename = Path.GetFileName(rmdpPath);
                string binPath = Path.ChangeExtension(rmdpPath, ".bin");
                string metaPath = Path.ChangeExtension(rmdpPath, ".packmeta");
                bool isBinExists = File.Exists(binPath);
                bool isMetaExists = File.Exists(metaPath);
                if (isBinExists && isMetaExists)
                {
                    Rmdp.Unpack(rmdpPath, binPath, metaPath, this);
                    MessageBox.Show("Successfully unpacked", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string filename = isBinExists ? Path.GetFileName(metaPath) : Path.GetFileName(binPath);
                    MessageBox.Show($"{filename} file not found near {rmdpFilename}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                rmdpProgressLbl.Text = string.Empty;
                Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private void txt2xlsxBtn_Click(object sender, EventArgs e)
        {
            Enabled = false;
            Cursor = Cursors.WaitCursor;

            // try
            // {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "TXT-files (*.txt)|*.txt"
                    + "|All files (*.*)|*.*";
                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    StringTable.ToXlsx(dialog.FileName);
                }
            // }
            // catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //finally
            //{
                rmdpProgressLbl.Text = string.Empty;
                Enabled = true;
                Cursor = Cursors.Default;
            //}
        }

        private void xlsx2txtBtn_Click(object sender, EventArgs e)
        {

        }
    }
}
