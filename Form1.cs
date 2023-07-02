using System.Diagnostics;
using System.Reflection;
using Home.BAR;
using System.Security.Cryptography;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using ICSharpCode.SharpZipLib.Zip.Compression;
using PlayStation_MiniTool;

namespace PlayStation_Home_MiniTool
{
    public partial class Form1 : Form
    {
        private static double taskcount = 0;
        public static double temptask = 0;
        private static bool canrun = true;
        public Form1()
        {
            InitializeComponent();
        }
        private async Task progessupdate()
        {
            while (taskcount != temptask)
            {
                Invoke(new Action(() =>
                {
                    if (base.InvokeRequired)
                    {
                        base.BeginInvoke(new MethodInvoker(delegate ()
                        {
                            progressBarbatch.Maximum = (int)taskcount;
                            progressBarbatch.Value = (int)temptask;
                            progressBarbatch.Refresh();
                        }));
                    }
                    progressBarbatch.Maximum = (int)taskcount;
                    progressBarbatch.Value = (int)temptask;
                    progressBarbatch.Refresh();
                }));
            }

            Invoke(new Action(() =>
            {
                if (base.InvokeRequired)
                {
                    base.BeginInvoke(new MethodInvoker(delegate ()
                    {
                        progressBarbatch.Value = progressBarbatch.Maximum;
                        progressBarbatch.Refresh();
                    }));
                }
                progressBarbatch.Value = progressBarbatch.Maximum;
                progressBarbatch.Refresh();
            }));

            temptask = 0;
            taskcount = 0;

            canrun = true;

            return;
        }
        private void Browseinputbarbutton_Click(object sender, EventArgs e)
        {
            // Show the FolderBrowserDialog.
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                // Set the selected directory path in the text box.
                textBoxbarinput.Text = folderBrowserDialog.SelectedPath;
            }

            return;
        }
        private void convertbutton_Click(object sender, EventArgs e)
        {
            string approot = AppDomain.CurrentDomain.BaseDirectory;

            // Check if the input directory path is valid.
            if (!Directory.Exists(textBoxbarinput.Text))
            {
                MessageBox.Show("Invalid input directory path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Get all .BAR files in the input directory.
            string[] barFiles = Directory.GetFiles(textBoxbarinput.Text, "*.BAR");

            // Process each .BAR file.
            foreach (string barFile in barFiles)
            {
                string command = "make_npdata.exe -e \"" + barFile + "\" \"" + Path.ChangeExtension(barFile, ".sdat") + "\" 0 1 2 0 16 3 00 EP9000-NPEA00013_00-DDDDDDDDDDDDDDDD 0";

                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    WorkingDirectory = approot + @"NPD\",
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = "cmd.exe"
                };

                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.Arguments = "/C " + command;
                startInfo.RedirectStandardOutput = true;
                startInfo.RedirectStandardError = true;
                startInfo.UseShellExecute = false;
                startInfo.CreateNoWindow = true;
                process.StartInfo = startInfo;
                process.Start();
                process.WaitForExit();
                process.Close();
            }

            MessageBox.Show("SDAT files created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            return;
        }
        private void Browsfoldertobarsdatbutton_Click(object sender, EventArgs e)
        {
            // Show the FolderBrowserDialog.
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                // Set the selected directory path in the text box.
                SUBFOLDERtextBox.Text = folderBrowserDialog.SelectedPath;
            }

            return;
        }
        private async Task foldertohome(string inputfolder, string outputlocation, string nameoffile, bool sdat)
        {
            try
            {
                if (sdat)
                {
                    if (File.Exists(outputlocation + @"\" + nameoffile + ".sdat"))
                    {
                        temptask += 1;

                        return;
                    }
                }
                else
                {
                    if (File.Exists(outputlocation + @"\" + nameoffile + ".BAR"))
                    {
                        temptask += 1;

                        return;
                    }
                }

                IEnumerable<string> enumerable = Directory.EnumerateFiles(inputfolder, "*.*", SearchOption.AllDirectories);
                BARArchive bararchive = new BARArchive(string.Format("{0}\\{1}.BAR", outputlocation, nameoffile), inputfolder);
                bararchive.DefaultCompression = CompressionMethod.EdgeZLib;
                bararchive.AllowWhitespaceInFilenames = true;
                bararchive.BeginUpdate();
                foreach (string path in enumerable)
                {
                    bararchive.AddFile(Path.Combine(inputfolder, path));
                }

                // Get the name of the directory
                string directoryName = new DirectoryInfo(inputfolder).Name;

                // Create a text file to write the paths to
                StreamWriter writer = new StreamWriter(inputfolder + @"\files.txt");

                // Get all files in the directory and its immediate subdirectories
                string[] files = Directory.GetFiles(inputfolder, "*.*", SearchOption.AllDirectories);

                // Loop through the files and write their paths to the text file
                foreach (string file in files)
                {
                    string relativePath = "file=\"" + file.Replace(inputfolder + @"\", "") + "\"";
                    writer.WriteLine(relativePath.Replace(@"\", "/"));
                }

                writer.Close();

                bararchive.AddFile(inputfolder + @"\files.txt");

                bararchive.EndUpdate();
                bararchive.Save();

                if (sdat)
                {
                    if (File.Exists(outputlocation + @"\" + nameoffile + ".BAR"))
                    {
                        string approot = AppDomain.CurrentDomain.BaseDirectory;

                        string command = "make_npdata.exe -e \"" + outputlocation + @"\" + nameoffile + ".BAR" + "\" \"" + Path.ChangeExtension(outputlocation + @"\" + nameoffile + ".BAR", ".sdat") + "\" 0 1 2 0 16 3 00 EP9000-NPEA00013_00-DDDDDDDDDDDDDDDD 0";

                        Process process = new Process();
                        ProcessStartInfo startInfo = new ProcessStartInfo
                        {
                            WorkingDirectory = approot + @"NPD\",
                            WindowStyle = ProcessWindowStyle.Hidden,
                            FileName = "cmd.exe"
                        };

                        startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        startInfo.Arguments = "/C " + command;
                        startInfo.RedirectStandardOutput = true;
                        startInfo.RedirectStandardError = true;
                        startInfo.UseShellExecute = false;
                        startInfo.CreateNoWindow = true;
                        process.StartInfo = startInfo;
                        process.Start();
                        process.WaitForExit();
                        process.Close();
                    }
                }

                if (File.Exists(outputlocation + @"\" + nameoffile + ".BAR"))
                {
                    byte[] data = ReadBinaryFile(outputlocation + @"\" + nameoffile + ".BAR", 0x0C, 4); // Read 4 bytes from offset 0x0C to 0x0F
                    Array.Reverse(data); // Reverse the byte array (little-endian)

                    string formattedData = BitConverter.ToString(data).Replace("-", ""); // Convert byte array to hex string

                    File.WriteAllText(outputlocation + @"\" + nameoffile + "_timestamp.txt", formattedData);
                }
            }
            catch (Exception ex)
            {
                temptask += 1;
                throw ex;
            }

            temptask += 1;

            return;
        }
        private void buttonbarsdanonbulk_Click(object sender, EventArgs e)
        {
            ofdGetbartosdat.Filter = "BAR files (*.BAR)|*.BAR";
            ofdGetbartosdat.RestoreDirectory = true;
            ofdGetbartosdat.Multiselect = false;
            ofdGetbartosdat.FileName = "";
            string barfile = "";
            if (ofdGetbartosdat.ShowDialog() == DialogResult.OK)
            {
                barfile = ofdGetbartosdat.FileName;
            }

            string approot = AppDomain.CurrentDomain.BaseDirectory;

            // Check if the input directory path is valid.
            if (barfile == "")
            {
                return;
            }
            else if (!File.Exists(barfile))
            {
                MessageBox.Show("Invalid input.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string command = "make_npdata.exe -e \"" + barfile + "\" \"" + Path.ChangeExtension(barfile, ".sdat") + "\" 0 1 2 0 16 3 00 EP9000-NPEA00013_00-DDDDDDDDDDDDDDDD 0";

            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                WorkingDirectory = approot + @"NPD\",
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = "cmd.exe"
            };

            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.Arguments = "/C " + command;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
            process.Close();

            MessageBox.Show("SDAT file created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            return;
        }
        private void buttonsubfolderconvert_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if the input directory path is valid.
                if (!Directory.Exists(SUBFOLDERtextBox.Text))
                {
                    MessageBox.Show("Invalid input directory path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string path = SUBFOLDERtextBox.Text;

                string[] subDirectories = Directory.GetDirectories(path);

                if (checkBoxsubfolderbarsdat.Checked)
                {
                    foreach (string subDirectory in subDirectories)
                    {
                        if (canrun)
                        {
                            canrun = false;
                            Task.Run(() => progessupdate());
                        }

                        taskcount += 1;

                        Task.Run(() => foldertohome(subDirectory, path, Path.GetFileName(subDirectory), radioButtonSDAT.Checked));
                    }
                }
                else
                {
                    if (canrun)
                    {
                        canrun = false;
                        Task.Run(() => progessupdate());
                    }

                    taskcount += 1;

                    Task.Run(() => foldertohome(path, Path.GetFullPath(Path.Combine(path, @"..\")), Path.GetFileName(path), radioButtonSDAT.Checked));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void buttonbrowsemapperinput_Click(object sender, EventArgs e)
        {
            // Show the FolderBrowserDialog.
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                // Set the selected directory path in the text box.
                textBoxinputsdatdumperoutput.Text = folderBrowserDialog.SelectedPath;
            }

            return;
        }
        private void buttonmap_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBoxinputsdatdumperoutput.Text != "")
                {
                    if (checkBoxsubfolder.Checked && checkBoxbruteforce.Checked)
                    {
                        bool tempbatchoice = checkBoxbatmode.Checked;

                        foreach (var dircursor in Directory.GetDirectories(textBoxinputsdatdumperoutput.Text))
                        {
                            int fileCount = Directory.GetFiles(dircursor).Length;

                            if ((fileCount > 0) && tempbatchoice)
                            {
                                taskcount += 1;

                                Mapper map = new Mapper();

                                Task.Run(() => map.mapperstart(dircursor, true, true, true, ""));
                            }
                            else if ((fileCount > 0) && !tempbatchoice)
                            {
                                taskcount += 1;

                                Mapper map = new Mapper();

                                Task.Run(() => map.mapperstart(dircursor, true, true, false, ""));
                            }
                        }
                    }
                    else if (!checkBoxsubfolder.Checked && checkBoxbruteforce.Checked)
                    {
                        int fileCount = Directory.GetFiles(textBoxinputsdatdumperoutput.Text).Length;

                        if ((fileCount > 0) && checkBoxbatmode.Checked)
                        {
                            taskcount += 1;

                            Mapper map = new Mapper();

                            Task.Run(() => map.mapperstart(textBoxinputsdatdumperoutput.Text, true, false, true, ""));
                        }
                        else if ((fileCount > 0) && !checkBoxbatmode.Checked)
                        {
                            taskcount += 1;

                            Mapper map = new Mapper();

                            Task.Run(() => map.mapperstart(textBoxinputsdatdumperoutput.Text, true, false, false, ""));
                        }
                    }
                    else if (checkBoxsubfolder.Checked && !checkBoxbruteforce.Checked)
                    {
                        bool tempbatchoice = checkBoxbatmode.Checked;

                        foreach (var dircursor in Directory.GetDirectories(textBoxinputsdatdumperoutput.Text))
                        {
                            int fileCount = Directory.GetFiles(dircursor).Length;

                            if ((fileCount > 0) && tempbatchoice)
                            {
                                taskcount += 1;

                                Mapper map = new Mapper();

                                Task.Run(() => map.mapperstart(dircursor, false, true, true, textBoxpathprefix.Text));
                            }
                            else if ((fileCount > 0) && !tempbatchoice)
                            {
                                taskcount += 1;

                                Mapper map = new Mapper();

                                Task.Run(() => map.mapperstart(dircursor, false, true, false, textBoxpathprefix.Text));
                            }
                        }
                    }
                    else
                    {
                        int fileCount = Directory.GetFiles(textBoxinputsdatdumperoutput.Text).Length;

                        if ((fileCount > 0) && checkBoxbatmode.Checked)
                        {
                            taskcount += 1;

                            Mapper map = new Mapper();

                            Task.Run(() => map.mapperstart(textBoxinputsdatdumperoutput.Text, false, false, true, textBoxpathprefix.Text));
                        }
                        else if ((fileCount > 0) && !checkBoxbatmode.Checked)
                        {
                            taskcount += 1;

                            Mapper map = new Mapper();

                            Task.Run(() => map.mapperstart(textBoxinputsdatdumperoutput.Text, false, false, false, textBoxpathprefix.Text));
                        }
                    }

                    if (canrun)
                    {
                        canrun = false;

                        Task.Run(() => progessupdate());
                    }

                    return;
                }
                else
                {
                    MessageBox.Show("No Path to Map for", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception ex)
            {
                temptask += 1;
                throw ex;
            }
        }
        private void buttonbrowseinputunbarfolderbulk_Click(object sender, EventArgs e)
        {
            // Show the FolderBrowserDialog.
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                // Set the selected directory path in the text box.
                textBoxunbarinputunbarbulk.Text = folderBrowserDialog.SelectedPath;
            }

            return;
        }
        private void buttoncalctimestampbatchgetfile_Click(object sender, EventArgs e)
        {
            // Show the FolderBrowserDialog.
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                // Set the selected directory path in the text box.
                textBoxtimestampbatchcalculate.Text = folderBrowserDialog.SelectedPath;
            }

            return;
        }
        private void buttonunbarsinglemode_Click(object sender, EventArgs e)
        {
            try
            {
                ofdGetBAR.Filter = "BAR files (*.BAR)|*.BAR|DAT files (*.dat)|*.dat|SDAT files (*.sdat)|*.sdat";
                ofdGetBAR.RestoreDirectory = true;
                ofdGetBAR.Multiselect = false;
                ofdGetBAR.FileName = "";
                string file = "";
                if (ofdGetBAR.ShowDialog() == DialogResult.OK)
                {
                    file = ofdGetBAR.FileName;
                }

                string approot = AppDomain.CurrentDomain.BaseDirectory;

                // Check if the input directory path is valid.
                if (file == "")
                {
                    return;
                }
                else if (!File.Exists(file))
                {
                    MessageBox.Show("Invalid input.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    if (file.EndsWith(".sdat"))
                    {
                        string command = "UnBAR.exe -d -i " + "\"" + file.Replace(@"\", @"\\") + "\"" + " -o " + "\"" + Path.GetDirectoryName(file).Replace(@"\", @"\\") + "\"";

                        Process process = new Process();
                        ProcessStartInfo startInfo = new ProcessStartInfo
                        {
                            WorkingDirectory = approot + @"unbar\",
                            WindowStyle = ProcessWindowStyle.Hidden,
                            FileName = "cmd.exe"
                        };

                        startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        startInfo.Arguments = "/C " + command;
                        startInfo.RedirectStandardOutput = false;
                        startInfo.RedirectStandardError = false;
                        startInfo.UseShellExecute = false;
                        startInfo.CreateNoWindow = false;
                        process.StartInfo = startInfo;
                        process.Start();
                        process.WaitForExit();
                        process.Close();

                        MessageBox.Show("SDAT file decrypted successfully, as a reminder, " +
                            "please use SDAT dumper for better results with original Home files.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (file.EndsWith(".BAR") || file.EndsWith(".dat"))
                    {
                        string command = "UnBAR.exe -i " + "\"" + file.Replace(@"\", @"\\") + "\"" + " -o " + "\"" + Path.GetDirectoryName(file).Replace(@"\", @"\\") + "\"";

                        Process process = new Process();
                        ProcessStartInfo startInfo = new ProcessStartInfo
                        {
                            WorkingDirectory = approot + @"unbar\",
                            WindowStyle = ProcessWindowStyle.Hidden,
                            FileName = "cmd.exe"
                        };

                        startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        startInfo.Arguments = "/C " + command;
                        startInfo.RedirectStandardOutput = false;
                        startInfo.RedirectStandardError = false;
                        startInfo.UseShellExecute = false;
                        startInfo.CreateNoWindow = false;
                        process.StartInfo = startInfo;
                        process.Start();
                        process.WaitForExit();
                        process.Close();

                        MessageBox.Show("BAR file unpacked successfully, as a reminder, please use SDAT dumper" +
                            " for better results with original Home files.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                return;
            }
            catch (Exception ex)
            {
                temptask += 1;
                throw ex;
            }
        }
        private void buttonunbarbatch_Click(object sender, EventArgs e)
        {
            try
            {
                string approot = AppDomain.CurrentDomain.BaseDirectory;

                // Check if the input directory path is valid.
                if (!Directory.Exists(textBoxunbarinputunbarbulk.Text))
                {
                    MessageBox.Show("Invalid input directory path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (Directory.GetFiles(textBoxunbarinputunbarbulk.Text).Length == 0 && Directory.GetDirectories(textBoxunbarinputunbarbulk.Text).Length == 0)
                {
                    MessageBox.Show("Path is Empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }
                else
                {
                    string[] extensions = { ".BAR", ".sdat", ".dat" };

                    var files = Directory.GetFiles(textBoxunbarinputunbarbulk.Text, "*.*", SearchOption.AllDirectories)
                         .Where(file => extensions.Contains(Path.GetExtension(file)))
                         .ToList();

                    // Process each .BAR file.
                    foreach (string barFile in files)
                    {
                        string command = "";

                        Process process = new Process();
                        ProcessStartInfo startInfo = new ProcessStartInfo
                        {
                            WorkingDirectory = approot + @"unbar\",
                            WindowStyle = ProcessWindowStyle.Hidden,
                            FileName = "cmd.exe"
                        };

                        if (barFile.EndsWith(".BAR"))
                        {
                            command = "UnBAR.exe -i " + "\"" + barFile.Replace(@"\", @"\\") + "\"" + " -o " + "\"" + Path.GetDirectoryName(barFile).Replace(@"\", @"\\") + "\"";

                            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                            startInfo.Arguments = "/C " + command;
                            startInfo.RedirectStandardOutput = false;
                            startInfo.RedirectStandardError = false;
                            startInfo.UseShellExecute = false;
                            startInfo.CreateNoWindow = false;
                            process.StartInfo = startInfo;
                            process.Start();
                            process.WaitForExit();
                            process.Close();
                        }
                        else if (barFile.EndsWith(".sdat"))
                        {
                            command = "UnBAR.exe -d -i " + "\"" + barFile.Replace(@"\", @"\\") + "\"" + " -o " + "\"" + Path.GetDirectoryName(barFile).Replace(@"\", @"\\") + "\"";

                            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                            startInfo.Arguments = "/C " + command;
                            startInfo.RedirectStandardOutput = false;
                            startInfo.RedirectStandardError = false;
                            startInfo.UseShellExecute = false;
                            startInfo.CreateNoWindow = false;
                            process.StartInfo = startInfo;
                            process.Start();
                            process.WaitForExit();
                            process.Close();
                        }
                        else if (barFile.EndsWith(".dat"))
                        {
                            command = "UnBAR.exe -i " + "\"" + barFile.Replace(@"\", @"\\") + "\"" + " -o " + "\"" + Path.GetDirectoryName(barFile).Replace(@"\", @"\\") + "\"";

                            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                            startInfo.Arguments = "/C " + command;
                            startInfo.RedirectStandardOutput = false;
                            startInfo.RedirectStandardError = false;
                            startInfo.UseShellExecute = false;
                            startInfo.CreateNoWindow = false;
                            process.StartInfo = startInfo;
                            process.Start();
                            process.WaitForExit();
                            process.Close();
                        }
                    }

                    MessageBox.Show("PlayStation home files decrypted/unpacked successfully, as a reminder, please use SDAT dumper" +
                        "for better results with original Home files.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return;
                }
            }
            catch (Exception ex)
            {
                temptask += 1;
                throw ex;
            }
        }
        private void buttonedgezlibdecrypt_Click(object sender, EventArgs e)
        {
            try
            {
                ofdGetedgezlib.Filter = "EDGEZLIB files (*.EdgeZlib)|*.EdgeZlib";
                ofdGetedgezlib.RestoreDirectory = true;
                ofdGetedgezlib.Multiselect = false;
                ofdGetedgezlib.FileName = "";
                string file = "";
                if (ofdGetedgezlib.ShowDialog() == DialogResult.OK)
                {
                    file = ofdGetedgezlib.FileName;
                }

                string approot = string.Empty;
                Assembly ass = Assembly.GetAssembly(typeof(Form1));
                if (ass != null)
                {
                    approot = ass.Location;
                    approot = approot.Remove(approot.Length - 39); // We remove the exe name from the output
                }

                // Check if the input directory path is valid.
                if (file == "")
                {
                    return;
                }
                else if (!File.Exists(file))
                {
                    MessageBox.Show("Invalid input.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    File.WriteAllBytes(Path.GetDirectoryName(file) + @"\" + Path.GetFileNameWithoutExtension(file), Decompress(File.ReadAllBytes(file)));
                }

                return;
            }
            catch (Exception ex)
            {
                temptask += 1;
                throw ex;
            }
        }
        private void buttonedgezlibencrypt_Click(object sender, EventArgs e)
        {
            try
            {
                ofdGetnonedgezlib.Filter = "ALL files (*.*)|*.*";
                ofdGetnonedgezlib.RestoreDirectory = true;
                ofdGetnonedgezlib.Multiselect = false;
                ofdGetnonedgezlib.FileName = "";
                string file = "";
                if (ofdGetnonedgezlib.ShowDialog() == DialogResult.OK)
                {
                    file = ofdGetnonedgezlib.FileName;
                }

                string approot = AppDomain.CurrentDomain.BaseDirectory;

                // Check if the input directory path is valid.
                if (file == "")
                {
                    return;
                }
                else if (!File.Exists(file))
                {
                    MessageBox.Show("Invalid input.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    File.WriteAllBytes(Path.GetDirectoryName(file) + @"\" + Path.GetFileName(file) + ".EdgeZlib", Compress(File.ReadAllBytes(file)));
                }

                return;
            }
            catch (Exception ex)
            {
                temptask += 1;
                throw ex;
            }
        }
        private void buttoncalculatetimestamp_Click(object sender, EventArgs e)
        {
            try
            {
                ofdGettimestamp.Filter = "BAR files (*.BAR)|*.BAR|DAT files (*.dat)|*.dat|SDAT files (*.sdat)|*.sdat";
                ofdGettimestamp.RestoreDirectory = true;
                ofdGettimestamp.Multiselect = false;
                ofdGettimestamp.FileName = "";
                string file = "";
                if (ofdGettimestamp.ShowDialog() == DialogResult.OK)
                {
                    file = ofdGettimestamp.FileName;
                }

                string approot = AppDomain.CurrentDomain.BaseDirectory;

                // Check if the input directory path is valid.
                if (file == "")
                {
                    return;
                }
                else if (!File.Exists(file))
                {
                    MessageBox.Show("Invalid input.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    if (file.EndsWith(".sdat"))
                    {
                        string command = "npdtool.exe ds " + "\"" + file.Replace(@"\", @"\\") + "\" \"" + Path.GetDirectoryName(file).Replace(@"\", @"\\") + @"\" + Path.GetFileNameWithoutExtension(file) + ".dat" + "\"";

                        Process process = new Process();
                        ProcessStartInfo startInfo = new ProcessStartInfo
                        {
                            WorkingDirectory = approot + @"NPD\",
                            WindowStyle = ProcessWindowStyle.Hidden,
                            FileName = "cmd.exe"
                        };

                        startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        startInfo.Arguments = "/C " + command;
                        startInfo.RedirectStandardOutput = false;
                        startInfo.RedirectStandardError = false;
                        startInfo.UseShellExecute = false;
                        startInfo.CreateNoWindow = true;
                        process.StartInfo = startInfo;
                        process.Start();
                        process.WaitForExit();
                        process.Close();

                        string datfile = file.Replace(".sdat", ".dat").Replace(".SDAT", ".dat");

                        if (File.Exists(datfile))
                        {
                            byte[] data = ReadBinaryFile(datfile, 0x0C, 4); // Read 4 bytes from offset 0x0C to 0x0F
                            Array.Reverse(data); // Reverse the byte array (little-endian)

                            string formattedData = BitConverter.ToString(data).Replace("-", ""); // Convert byte array to hex string

                            File.WriteAllText(Path.GetDirectoryName(datfile) + @"\" + Path.GetFileNameWithoutExtension(datfile) + "_timestamp.txt", formattedData);

                            MessageBoxWithCopyButton.Show($"timestamp=\"{formattedData}\"", "Put this in your SDC or ODC xml with the archive field as a tag");
                        }
                    }
                    else if (file.EndsWith(".BAR") || file.EndsWith(".dat"))
                    {
                        byte[] data = ReadBinaryFile(file, 0x0C, 4); // Read 4 bytes from offset 0x0C to 0x0F
                        Array.Reverse(data); // Reverse the byte array (little-endian)

                        string formattedData = BitConverter.ToString(data).Replace("-", ""); // Convert byte array to hex string

                        File.WriteAllText(Path.GetDirectoryName(file) + @"\" + Path.GetFileNameWithoutExtension(file) + "_timestamp.txt", formattedData);

                        MessageBoxWithCopyButton.Show($"timestamp=\"{formattedData}\"", "Put this in your SDC or ODC xml with the archive field as a tag");
                    }
                }

                return;
            }
            catch (Exception ex)
            {
                temptask += 1;
                throw ex;
            }
        }
        private void buttoncalculatebatchtimestamp_Click(object sender, EventArgs e)
        {
            try
            {
                string approot = AppDomain.CurrentDomain.BaseDirectory;

                // Check if the input directory path is valid.
                if (!Directory.Exists(textBoxtimestampbatchcalculate.Text))
                {
                    MessageBox.Show("Invalid input directory path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (Directory.GetFiles(textBoxtimestampbatchcalculate.Text).Length == 0 && Directory.GetDirectories(textBoxtimestampbatchcalculate.Text).Length == 0)
                {
                    MessageBox.Show("Path is Empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }
                else
                {
                    string[] extensions = { ".BAR", ".sdat", ".dat" };

                    var files = Directory.GetFiles(textBoxtimestampbatchcalculate.Text, "*.*", SearchOption.AllDirectories)
                         .Where(file => extensions.Contains(Path.GetExtension(file)))
                         .ToList();

                    // Process each .BAR file.
                    foreach (string barFile in files)
                    {
                        if (barFile.EndsWith(".BAR"))
                        {
                            byte[] data = ReadBinaryFile(barFile, 0x0C, 4); // Read 4 bytes from offset 0x0C to 0x0F
                            Array.Reverse(data); // Reverse the byte array (little-endian)

                            string formattedData = BitConverter.ToString(data).Replace("-", ""); // Convert byte array to hex string

                            File.WriteAllText(Path.GetDirectoryName(barFile) + @"\" + Path.GetFileNameWithoutExtension(barFile) + "_timestamp.txt", formattedData);
                        }
                        else if (barFile.EndsWith(".sdat"))
                        {
                            string command = "npdtool.exe ds " + "\"" + barFile.Replace(@"\", @"\\") + "\" \"" + Path.GetDirectoryName(barFile).Replace(@"\", @"\\") + @"\" + Path.GetFileNameWithoutExtension(barFile) + ".dat" + "\"";

                            Process process = new Process();
                            ProcessStartInfo startInfo = new ProcessStartInfo
                            {
                                WorkingDirectory = approot + @"NPD\",
                                WindowStyle = ProcessWindowStyle.Hidden,
                                FileName = "cmd.exe"
                            };

                            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                            startInfo.Arguments = "/C " + command;
                            startInfo.RedirectStandardOutput = false;
                            startInfo.RedirectStandardError = false;
                            startInfo.UseShellExecute = false;
                            startInfo.CreateNoWindow = true;
                            process.StartInfo = startInfo;
                            process.Start();
                            process.WaitForExit();
                            process.Close();

                            string datfile = barFile.Replace(".sdat", ".dat").Replace(".SDAT", ".dat");

                            if (File.Exists(datfile))
                            {
                                byte[] data = ReadBinaryFile(datfile, 0x0C, 4); // Read 4 bytes from offset 0x0C to 0x0F
                                Array.Reverse(data); // Reverse the byte array (little-endian)

                                string formattedData = BitConverter.ToString(data).Replace("-", ""); // Convert byte array to hex string

                                File.WriteAllText(Path.GetDirectoryName(datfile) + @"\" + Path.GetFileNameWithoutExtension(datfile) + "_timestamp.txt", formattedData);
                            }

                        }
                        else if (barFile.EndsWith(".dat"))
                        {
                            byte[] data = ReadBinaryFile(barFile, 0x0C, 4); // Read 4 bytes from offset 0x0C to 0x0F
                            Array.Reverse(data); // Reverse the byte array (little-endian)

                            string formattedData = BitConverter.ToString(data).Replace("-", ""); // Convert byte array to hex string

                            File.WriteAllText(Path.GetDirectoryName(barFile) + @"\" + Path.GetFileNameWithoutExtension(barFile) + "_timestamp.txt", formattedData);
                        }
                    }

                    return;
                }
            }
            catch (Exception ex)
            {
                temptask += 1;
                throw ex;
            }
        }
        private void buttontimestamppatchstart_Click(object sender, EventArgs e)
        {
            try
            {
                string newValueString = textBoxtimestamppatchervalue.Text;

                if (newValueString == "")
                {
                    MessageBox.Show("timestamp value is empty!.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (newValueString.Length != 8)
                {
                    MessageBox.Show("timestamp value is not correct! I expect 8 characters only!.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    ofdGettimestamppatch.Filter = "BAR files (*.BAR)|*.BAR|DAT files (*.dat)|*.dat|SDAT files (*.sdat)|*.sdat";
                    ofdGettimestamppatch.RestoreDirectory = true;
                    ofdGettimestamppatch.Multiselect = false;
                    ofdGettimestamppatch.FileName = "";
                    string file = "";
                    if (ofdGettimestamppatch.ShowDialog() == DialogResult.OK)
                    {
                        file = ofdGettimestamppatch.FileName;
                    }

                    string approot = AppDomain.CurrentDomain.BaseDirectory;

                    // Check if the input directory path is valid.
                    if (file == "")
                    {
                        return;
                    }
                    else if (!File.Exists(file))
                    {
                        MessageBox.Show("Invalid input.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        if (file.EndsWith(".sdat"))
                        {
                            string command = "npdtool.exe ds " + "\"" + file.Replace(@"\", @"\\") + "\" \"" + Path.GetDirectoryName(file).Replace(@"\", @"\\") + @"\" + Path.GetFileNameWithoutExtension(file) + ".dat" + "\"";

                            Process process = new Process();
                            ProcessStartInfo startInfo = new ProcessStartInfo
                            {
                                WorkingDirectory = approot + @"NPD\",
                                WindowStyle = ProcessWindowStyle.Hidden,
                                FileName = "cmd.exe"
                            };

                            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                            startInfo.Arguments = "/C " + command;
                            startInfo.RedirectStandardOutput = false;
                            startInfo.RedirectStandardError = false;
                            startInfo.UseShellExecute = false;
                            startInfo.CreateNoWindow = true;
                            process.StartInfo = startInfo;
                            process.Start();
                            process.WaitForExit();
                            process.Close();

                            string datfile = file.Replace(".sdat", ".dat").Replace(".SDAT", ".dat");

                            if (File.Exists(datfile))
                            {
                                // Read the original data from the file
                                byte[] data = File.ReadAllBytes(datfile);

                                // Patch the value at the specified offset (0x0C to 0x0F)
                                int offset = 0x0C;
                                byte[] newValueBytes = BitConverter.GetBytes(Convert.ToInt32(newValueString, 16));
                                Array.Copy(newValueBytes, 0, data, offset, 4); // Patch the value

                                File.Copy(file, file + ".backup", true);

                                File.Copy(datfile, datfile + ".backup", true);

                                // Write the patched data back to the file
                                File.WriteAllBytes(datfile, data);

                                string commandalt = "make_npdata.exe -e \"" + datfile + "\" \"" + Path.ChangeExtension(datfile, ".sdat") + "\" 0 1 2 0 16 3 00 EP9000-NPEA00013_00-DDDDDDDDDDDDDDDD 0";

                                Process processalt = new Process();
                                ProcessStartInfo startInfoalt = new ProcessStartInfo
                                {
                                    WorkingDirectory = approot + @"NPD\",
                                    WindowStyle = ProcessWindowStyle.Hidden,
                                    FileName = "cmd.exe"
                                };

                                startInfoalt.WindowStyle = ProcessWindowStyle.Hidden;
                                startInfoalt.Arguments = "/C " + commandalt;
                                startInfoalt.RedirectStandardOutput = true;
                                startInfoalt.RedirectStandardError = true;
                                startInfoalt.UseShellExecute = false;
                                startInfoalt.CreateNoWindow = true;
                                processalt.StartInfo = startInfoalt;
                                processalt.Start();
                                processalt.WaitForExit();
                                processalt.Close();
                            }
                        }
                        else if (file.EndsWith(".BAR") || file.EndsWith(".dat"))
                        {
                            // Read the original data from the file
                            byte[] data = File.ReadAllBytes(file);

                            // Patch the value at the specified offset (0x0C to 0x0F)
                            int offset = 0x0C;
                            byte[] newValueBytes = BitConverter.GetBytes(Convert.ToInt32(newValueString, 16));
                            Array.Copy(newValueBytes, 0, data, offset, 4); // Patch the value

                            File.Copy(file, file + ".backup", true);

                            // Write the patched data back to the file
                            File.WriteAllBytes(file, data);
                        }
                    }
                }

                return;
            }
            catch (Exception ex)
            {
                temptask += 1;
                throw ex;
            }
        }
        private void buttonsha1calc_Click(object sender, EventArgs e)
        {
            try
            {
                ofdGetsha1.Filter = "All files (*.*)|*.*";
                ofdGetsha1.RestoreDirectory = true;
                ofdGetsha1.Multiselect = false;
                ofdGetsha1.FileName = "";
                string file = "";
                if (ofdGetsha1.ShowDialog() == DialogResult.OK)
                {
                    file = ofdGetsha1.FileName;
                }

                string approot = AppDomain.CurrentDomain.BaseDirectory;

                // Check if the input directory path is valid.
                if (file == "")
                {
                    return;
                }
                else if (!File.Exists(file))
                {
                    MessageBox.Show("Invalid input.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    using (FileStream fileStream = File.OpenRead(file))
                    {
                        using (SHA1Managed sha1 = new SHA1Managed())
                        {
                            byte[] hash = sha1.ComputeHash(fileStream);
                            textBoxsha1result.Text = BitConverter.ToString(hash).Replace("-", String.Empty);

                            sha1.Dispose();
                        }
                    }
                }

                return;
            }
            catch (Exception ex)
            {
                temptask += 1;
                throw ex;
            }
        }
        private static byte[] ReadBinaryFile(string filePath, int offset, int length)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                fs.Seek(offset, SeekOrigin.Begin);

                byte[] data = new byte[length];
                fs.Read(data, 0, length);

                return data;
            }
        }
        public byte[] Compress(byte[] inData)
        {
            MemoryStream memoryStream = new MemoryStream(inData.Length);
            MemoryStream memoryStream2 = new MemoryStream(inData);
            while (memoryStream2.Position < memoryStream2.Length)
            {
                int num = Math.Min((int)(memoryStream2.Length - memoryStream2.Position), 65535);
                byte[] array = new byte[num];
                memoryStream2.Read(array, 0, num);
                byte[] array2 = this.CompressChunk(array);
                memoryStream.Write(array2, 0, array2.Length);
            }
            memoryStream2.Close();
            memoryStream.Close();
            return memoryStream.ToArray();
        }
        private byte[] CompressChunk(byte[] InData)
        {
            MemoryStream memoryStream = new MemoryStream();
            Deflater deflater = new Deflater(9, true);
            DeflaterOutputStream deflaterOutputStream = new DeflaterOutputStream(memoryStream, deflater);
            deflaterOutputStream.Write(InData, 0, InData.Length);
            deflaterOutputStream.Close();
            memoryStream.Close();
            byte[] array = memoryStream.ToArray();
            byte[] array2;
            if (array.Length >= InData.Length)
            {
                array2 = InData;
            }
            else
            {
                array2 = array;
            }
            byte[] array3 = new byte[array2.Length + 4];
            Array.Copy(array2, 0, array3, 4, array2.Length);
            ChunkHeader chunkHeader = default(ChunkHeader);
            chunkHeader.SourceSize = (ushort)InData.Length;
            chunkHeader.CompressedSize = (ushort)array2.Length;
            byte[] array4 = chunkHeader.GetBytes();
            array4 = Utils.EndianSwap(array4);
            Array.Copy(array4, 0, array3, 0, ChunkHeader.SizeOf);
            return array3;
        }
        public byte[] Decompress(byte[] inData)
        {
            MemoryStream memoryStream = new MemoryStream();
            MemoryStream memoryStream2 = new MemoryStream(inData);
            byte[] array = new byte[ChunkHeader.SizeOf];
            while (memoryStream2.Position < memoryStream2.Length)
            {
                memoryStream2.Read(array, 0, ChunkHeader.SizeOf);
                array = Utils.EndianSwap(array);
                ChunkHeader header = ChunkHeader.FromBytes(array);
                int compressedSize = (int)header.CompressedSize;
                byte[] array2 = new byte[compressedSize];
                memoryStream2.Read(array2, 0, compressedSize);
                byte[] array3 = DecompressChunk(array2, header);
                memoryStream.Write(array3, 0, array3.Length);
            }
            memoryStream2.Close();
            memoryStream.Close();
            return memoryStream.ToArray();
        }
        private byte[] DecompressChunk(byte[] inData, ChunkHeader header)
        {
            if (header.CompressedSize == header.SourceSize)
            {
                return inData;
            }
            MemoryStream baseInputStream = new MemoryStream(inData);
            Inflater inf = new Inflater(true);
            InflaterInputStream inflaterInputStream = new InflaterInputStream(baseInputStream, inf);
            MemoryStream memoryStream = new MemoryStream();
            byte[] array = new byte[4096];
            for (; ; )
            {
                int num = inflaterInputStream.Read(array, 0, array.Length);
                if (num <= 0)
                {
                    break;
                }
                memoryStream.Write(array, 0, num);
            }
            inflaterInputStream.Close();
            return memoryStream.ToArray();
        }

        internal struct ChunkHeader
        {
            internal byte[] GetBytes()
            {
                byte[] array = new byte[4];
                Array.Copy(BitConverter.GetBytes(this.SourceSize), 0, array, 2, 2);
                Array.Copy(BitConverter.GetBytes(this.CompressedSize), 0, array, 0, 2);
                return array;
            }

            internal static int SizeOf
            {
                get
                {
                    return 4;
                }
            }

            internal static ChunkHeader FromBytes(byte[] inData)
            {
                ChunkHeader result = default(ChunkHeader);
                byte[] array = inData;
                if (inData.Length > ChunkHeader.SizeOf)
                {
                    array = new byte[4];
                    Array.Copy(inData, array, 4);
                }
                result.SourceSize = BitConverter.ToUInt16(array, 2);
                result.CompressedSize = BitConverter.ToUInt16(array, 0);
                return result;
            }

            internal ushort SourceSize;

            internal ushort CompressedSize;
        }
    }
    public class MappedList
    {
        public string type;

        public string file;
    }
    public class RegexPatterns
    {
        public string type;

        public string pattern;
    }
}