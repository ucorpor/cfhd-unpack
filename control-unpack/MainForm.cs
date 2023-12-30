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
            Process.Start("https://github.com/ucorpor/ibf-unpack");
        }

        private void stringsRepackBtn_Click(object sender, EventArgs e)
        {
            Enabled = false;
            Cursor = Cursors.WaitCursor;

            try
            {
                OpenFileDialog binFileDialog = new OpenFileDialog();
                binFileDialog.FileName = "string_table.bin";
                binFileDialog.Filter = "BIN-files (*.bin)|*.bin"
                    + "|All files (*.*)|*.*";

                DialogResult result = binFileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string binPath = binFileDialog.FileName;
                    string txtPath = $"{binPath}.txt";
                    if (File.Exists(txtPath))
                    {
                        SaveFileDialog saveDialog = new SaveFileDialog();
                        saveDialog.RestoreDirectory = true;
                        saveDialog.FileName = "string_table.bin";
                        saveDialog.Filter = "BIN-files (*.bin)|*.bin"
                            + "|All files (*.*)|*.*";

                        if (saveDialog.ShowDialog() == DialogResult.OK)
                        {
                            string repackedPath = saveDialog.FileName;
                            StringTable.Repack(binPath, repackedPath);
                            string message = $"Repacked string_table.bin saccessufully saved to:{Environment.NewLine}{repackedPath}";
                            MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("string_table.bin.txt file not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    Rmdp.Unpack(rmdpPath, binPath, metaPath);
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
                Enabled = true;
                Cursor = Cursors.Default;
            }
        }

    }
}
