namespace PlayStation_Home_MiniTool
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            bargroupBox = new GroupBox();
            Browseinputbarbutton = new Button();
            textBoxbarinput = new TextBox();
            label1 = new Label();
            groupBoxbatchbarsdat = new GroupBox();
            convertbutton = new Button();
            groupBoxsinglebarsdat = new GroupBox();
            buttonbarsdanonbulk = new Button();
            groupBoxfoldertobarsdat = new GroupBox();
            groupBoxmode = new GroupBox();
            checkBoxsubfolderbarsdat = new CheckBox();
            radioButtonBAR = new RadioButton();
            labelindicator = new Label();
            radioButtonSDAT = new RadioButton();
            buttonsubfolderconvert = new Button();
            Browsfoldertobarsdatbutton = new Button();
            SUBFOLDERtextBox = new TextBox();
            labelinputfolderbarsdat = new Label();
            progressBarbatch = new ProgressBar();
            groupBoxsdatdumperoutput = new GroupBox();
            groupBoxmappermode = new GroupBox();
            textBoxpathprefix = new TextBox();
            labelprefix = new Label();
            buttonmap = new Button();
            checkBoxbatmode = new CheckBox();
            checkBoxbruteforce = new CheckBox();
            checkBoxsubfolder = new CheckBox();
            labelmapperinputfolder = new Label();
            buttonbrowsemapperinput = new Button();
            textBoxinputsdatdumperoutput = new TextBox();
            ofdGetBAR = new OpenFileDialog();
            ofdGetedgezlib = new OpenFileDialog();
            ofdGetnonedgezlib = new OpenFileDialog();
            ofdGetbartosdat = new OpenFileDialog();
            ofdGettimestamp = new OpenFileDialog();
            ofdGettimestamppatch = new OpenFileDialog();
            ofdGetsha1 = new OpenFileDialog();
            labelunbar = new Label();
            groupBoxunbar = new GroupBox();
            groupBoxunbarbatch = new GroupBox();
            buttonunbarbatch = new Button();
            groupBoxunbarsignlemode = new GroupBox();
            buttonunbarsinglemode = new Button();
            buttonbrowseinputunbarfolderbulk = new Button();
            textBoxunbarinputunbarbulk = new TextBox();
            groupBoxedgezlibencrypt = new GroupBox();
            buttonedgezlibencrypt = new Button();
            groupBoxedgezlibdecrypt = new GroupBox();
            buttonedgezlibdecrypt = new Button();
            tabControlmain = new TabControl();
            tabPagemain = new TabPage();
            tabPagemisc = new TabPage();
            groupBoxSHA1 = new GroupBox();
            labelsha1 = new Label();
            textBoxsha1result = new TextBox();
            buttonsha1calc = new Button();
            groupBoxtimestamp = new GroupBox();
            groupBoxbatchcalculatetimestamp = new GroupBox();
            buttoncalculatebatchtimestamp = new Button();
            buttoncalctimestampbatchgetfile = new Button();
            textBoxtimestampbatchcalculate = new TextBox();
            v = new GroupBox();
            buttontimestamppatchstart = new Button();
            labeltimestamppatcher = new Label();
            textBoxtimestamppatchervalue = new TextBox();
            labeltimestamp = new Label();
            buttoncalculatetimestamp = new Button();
            bargroupBox.SuspendLayout();
            groupBoxbatchbarsdat.SuspendLayout();
            groupBoxsinglebarsdat.SuspendLayout();
            groupBoxfoldertobarsdat.SuspendLayout();
            groupBoxmode.SuspendLayout();
            groupBoxsdatdumperoutput.SuspendLayout();
            groupBoxmappermode.SuspendLayout();
            groupBoxunbar.SuspendLayout();
            groupBoxunbarbatch.SuspendLayout();
            groupBoxunbarsignlemode.SuspendLayout();
            groupBoxedgezlibencrypt.SuspendLayout();
            groupBoxedgezlibdecrypt.SuspendLayout();
            tabControlmain.SuspendLayout();
            tabPagemain.SuspendLayout();
            tabPagemisc.SuspendLayout();
            groupBoxSHA1.SuspendLayout();
            groupBoxtimestamp.SuspendLayout();
            groupBoxbatchcalculatetimestamp.SuspendLayout();
            v.SuspendLayout();
            SuspendLayout();
            // 
            // bargroupBox
            // 
            bargroupBox.Controls.Add(Browseinputbarbutton);
            bargroupBox.Controls.Add(textBoxbarinput);
            bargroupBox.Controls.Add(label1);
            bargroupBox.Controls.Add(groupBoxbatchbarsdat);
            bargroupBox.Controls.Add(groupBoxsinglebarsdat);
            bargroupBox.Location = new Point(3, 471);
            bargroupBox.Name = "bargroupBox";
            bargroupBox.Size = new Size(646, 137);
            bargroupBox.TabIndex = 0;
            bargroupBox.TabStop = false;
            bargroupBox.Text = "BAR -> SDAT";
            // 
            // Browseinputbarbutton
            // 
            Browseinputbarbutton.Location = new Point(541, 44);
            Browseinputbarbutton.Name = "Browseinputbarbutton";
            Browseinputbarbutton.Size = new Size(94, 29);
            Browseinputbarbutton.TabIndex = 8;
            Browseinputbarbutton.Text = "Browse!";
            Browseinputbarbutton.UseVisualStyleBackColor = true;
            Browseinputbarbutton.Click += Browseinputbarbutton_Click;
            // 
            // textBoxbarinput
            // 
            textBoxbarinput.Location = new Point(21, 45);
            textBoxbarinput.Name = "textBoxbarinput";
            textBoxbarinput.Size = new Size(515, 27);
            textBoxbarinput.TabIndex = 7;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(21, 23);
            label1.Name = "label1";
            label1.Size = new Size(208, 20);
            label1.TabIndex = 6;
            label1.Text = "Input Folder (Bulk Mode Only)";
            // 
            // groupBoxbatchbarsdat
            // 
            groupBoxbatchbarsdat.Controls.Add(convertbutton);
            groupBoxbatchbarsdat.Location = new Point(323, 76);
            groupBoxbatchbarsdat.Name = "groupBoxbatchbarsdat";
            groupBoxbatchbarsdat.Size = new Size(282, 61);
            groupBoxbatchbarsdat.TabIndex = 5;
            groupBoxbatchbarsdat.TabStop = false;
            groupBoxbatchbarsdat.Text = "Bulk Mode";
            // 
            // convertbutton
            // 
            convertbutton.Location = new Point(71, 24);
            convertbutton.Name = "convertbutton";
            convertbutton.Size = new Size(139, 32);
            convertbutton.TabIndex = 2;
            convertbutton.Text = "Convert!";
            convertbutton.UseVisualStyleBackColor = true;
            convertbutton.Click += convertbutton_Click;
            // 
            // groupBoxsinglebarsdat
            // 
            groupBoxsinglebarsdat.Controls.Add(buttonbarsdanonbulk);
            groupBoxsinglebarsdat.Location = new Point(35, 76);
            groupBoxsinglebarsdat.Name = "groupBoxsinglebarsdat";
            groupBoxsinglebarsdat.Size = new Size(282, 61);
            groupBoxsinglebarsdat.TabIndex = 4;
            groupBoxsinglebarsdat.TabStop = false;
            groupBoxsinglebarsdat.Text = "Single Mode";
            // 
            // buttonbarsdanonbulk
            // 
            buttonbarsdanonbulk.Location = new Point(70, 24);
            buttonbarsdanonbulk.Name = "buttonbarsdanonbulk";
            buttonbarsdanonbulk.Size = new Size(139, 32);
            buttonbarsdanonbulk.TabIndex = 3;
            buttonbarsdanonbulk.Text = "Convert!";
            buttonbarsdanonbulk.UseVisualStyleBackColor = true;
            buttonbarsdanonbulk.Click += buttonbarsdanonbulk_Click;
            // 
            // groupBoxfoldertobarsdat
            // 
            groupBoxfoldertobarsdat.Controls.Add(groupBoxmode);
            groupBoxfoldertobarsdat.Controls.Add(Browsfoldertobarsdatbutton);
            groupBoxfoldertobarsdat.Controls.Add(SUBFOLDERtextBox);
            groupBoxfoldertobarsdat.Controls.Add(labelinputfolderbarsdat);
            groupBoxfoldertobarsdat.Location = new Point(3, 332);
            groupBoxfoldertobarsdat.Name = "groupBoxfoldertobarsdat";
            groupBoxfoldertobarsdat.Size = new Size(646, 139);
            groupBoxfoldertobarsdat.TabIndex = 3;
            groupBoxfoldertobarsdat.TabStop = false;
            groupBoxfoldertobarsdat.Text = "Folder -> BAR / SDAT";
            // 
            // groupBoxmode
            // 
            groupBoxmode.Controls.Add(checkBoxsubfolderbarsdat);
            groupBoxmode.Controls.Add(radioButtonBAR);
            groupBoxmode.Controls.Add(labelindicator);
            groupBoxmode.Controls.Add(radioButtonSDAT);
            groupBoxmode.Controls.Add(buttonsubfolderconvert);
            groupBoxmode.Location = new Point(66, 88);
            groupBoxmode.Name = "groupBoxmode";
            groupBoxmode.Size = new Size(511, 51);
            groupBoxmode.TabIndex = 11;
            groupBoxmode.TabStop = false;
            groupBoxmode.Text = "Mode";
            // 
            // checkBoxsubfolderbarsdat
            // 
            checkBoxsubfolderbarsdat.AutoSize = true;
            checkBoxsubfolderbarsdat.Location = new Point(18, 23);
            checkBoxsubfolderbarsdat.Name = "checkBoxsubfolderbarsdat";
            checkBoxsubfolderbarsdat.Size = new Size(141, 24);
            checkBoxsubfolderbarsdat.TabIndex = 11;
            checkBoxsubfolderbarsdat.Text = "SubFolder Mode";
            checkBoxsubfolderbarsdat.UseVisualStyleBackColor = true;
            // 
            // radioButtonBAR
            // 
            radioButtonBAR.AutoSize = true;
            radioButtonBAR.Checked = true;
            radioButtonBAR.Location = new Point(178, 23);
            radioButtonBAR.Name = "radioButtonBAR";
            radioButtonBAR.Size = new Size(58, 24);
            radioButtonBAR.TabIndex = 9;
            radioButtonBAR.TabStop = true;
            radioButtonBAR.Text = "BAR";
            radioButtonBAR.UseVisualStyleBackColor = true;
            // 
            // labelindicator
            // 
            labelindicator.AutoSize = true;
            labelindicator.Location = new Point(334, 23);
            labelindicator.Name = "labelindicator";
            labelindicator.Size = new Size(25, 20);
            labelindicator.TabIndex = 8;
            labelindicator.Text = "->";
            // 
            // radioButtonSDAT
            // 
            radioButtonSDAT.AutoSize = true;
            radioButtonSDAT.Location = new Point(261, 23);
            radioButtonSDAT.Name = "radioButtonSDAT";
            radioButtonSDAT.Size = new Size(58, 24);
            radioButtonSDAT.TabIndex = 10;
            radioButtonSDAT.TabStop = true;
            radioButtonSDAT.Text = "sdat";
            radioButtonSDAT.UseVisualStyleBackColor = true;
            // 
            // buttonsubfolderconvert
            // 
            buttonsubfolderconvert.Location = new Point(365, 17);
            buttonsubfolderconvert.Name = "buttonsubfolderconvert";
            buttonsubfolderconvert.Size = new Size(139, 32);
            buttonsubfolderconvert.TabIndex = 7;
            buttonsubfolderconvert.Text = "Convert!";
            buttonsubfolderconvert.UseVisualStyleBackColor = true;
            buttonsubfolderconvert.Click += buttonsubfolderconvert_Click;
            // 
            // Browsfoldertobarsdatbutton
            // 
            Browsfoldertobarsdatbutton.Location = new Point(541, 45);
            Browsfoldertobarsdatbutton.Name = "Browsfoldertobarsdatbutton";
            Browsfoldertobarsdatbutton.Size = new Size(94, 29);
            Browsfoldertobarsdatbutton.TabIndex = 6;
            Browsfoldertobarsdatbutton.Text = "Browse!";
            Browsfoldertobarsdatbutton.UseVisualStyleBackColor = true;
            Browsfoldertobarsdatbutton.Click += Browsfoldertobarsdatbutton_Click;
            // 
            // SUBFOLDERtextBox
            // 
            SUBFOLDERtextBox.Location = new Point(21, 45);
            SUBFOLDERtextBox.Name = "SUBFOLDERtextBox";
            SUBFOLDERtextBox.Size = new Size(515, 27);
            SUBFOLDERtextBox.TabIndex = 5;
            // 
            // labelinputfolderbarsdat
            // 
            labelinputfolderbarsdat.AutoSize = true;
            labelinputfolderbarsdat.Location = new Point(21, 23);
            labelinputfolderbarsdat.Name = "labelinputfolderbarsdat";
            labelinputfolderbarsdat.Size = new Size(89, 20);
            labelinputfolderbarsdat.TabIndex = 4;
            labelinputfolderbarsdat.Text = "Input Folder";
            // 
            // progressBarbatch
            // 
            progressBarbatch.Location = new Point(6, 741);
            progressBarbatch.Name = "progressBarbatch";
            progressBarbatch.Size = new Size(658, 33);
            progressBarbatch.TabIndex = 4;
            // 
            // groupBoxsdatdumperoutput
            // 
            groupBoxsdatdumperoutput.Controls.Add(groupBoxmappermode);
            groupBoxsdatdumperoutput.Controls.Add(labelmapperinputfolder);
            groupBoxsdatdumperoutput.Controls.Add(buttonbrowsemapperinput);
            groupBoxsdatdumperoutput.Controls.Add(textBoxinputsdatdumperoutput);
            groupBoxsdatdumperoutput.Location = new Point(3, 148);
            groupBoxsdatdumperoutput.Name = "groupBoxsdatdumperoutput";
            groupBoxsdatdumperoutput.Size = new Size(646, 183);
            groupBoxsdatdumperoutput.TabIndex = 5;
            groupBoxsdatdumperoutput.TabStop = false;
            groupBoxsdatdumperoutput.Text = "SDAT Dumper output folder -> Mapped PlayStation Home archive";
            // 
            // groupBoxmappermode
            // 
            groupBoxmappermode.Controls.Add(textBoxpathprefix);
            groupBoxmappermode.Controls.Add(labelprefix);
            groupBoxmappermode.Controls.Add(buttonmap);
            groupBoxmappermode.Controls.Add(checkBoxbatmode);
            groupBoxmappermode.Controls.Add(checkBoxbruteforce);
            groupBoxmappermode.Controls.Add(checkBoxsubfolder);
            groupBoxmappermode.Location = new Point(21, 89);
            groupBoxmappermode.Name = "groupBoxmappermode";
            groupBoxmappermode.Size = new Size(615, 93);
            groupBoxmappermode.TabIndex = 12;
            groupBoxmappermode.TabStop = false;
            groupBoxmappermode.Text = "Mode";
            // 
            // textBoxpathprefix
            // 
            textBoxpathprefix.Location = new Point(101, 19);
            textBoxpathprefix.Name = "textBoxpathprefix";
            textBoxpathprefix.Size = new Size(509, 27);
            textBoxpathprefix.TabIndex = 5;
            // 
            // labelprefix
            // 
            labelprefix.AutoSize = true;
            labelprefix.Location = new Point(16, 23);
            labelprefix.Name = "labelprefix";
            labelprefix.Size = new Size(78, 20);
            labelprefix.TabIndex = 4;
            labelprefix.Text = "Path Prefix";
            // 
            // buttonmap
            // 
            buttonmap.Location = new Point(454, 52);
            buttonmap.Name = "buttonmap";
            buttonmap.Size = new Size(135, 29);
            buttonmap.TabIndex = 3;
            buttonmap.Text = "Map!";
            buttonmap.UseVisualStyleBackColor = true;
            buttonmap.Click += buttonmap_Click;
            // 
            // checkBoxbatmode
            // 
            checkBoxbatmode.AutoSize = true;
            checkBoxbatmode.Location = new Point(315, 57);
            checkBoxbatmode.Name = "checkBoxbatmode";
            checkBoxbatmode.Size = new Size(114, 24);
            checkBoxbatmode.TabIndex = 2;
            checkBoxbatmode.Text = "Bat Hooking";
            checkBoxbatmode.UseVisualStyleBackColor = true;
            // 
            // checkBoxbruteforce
            // 
            checkBoxbruteforce.AutoSize = true;
            checkBoxbruteforce.Location = new Point(163, 57);
            checkBoxbruteforce.Name = "checkBoxbruteforce";
            checkBoxbruteforce.Size = new Size(147, 24);
            checkBoxbruteforce.TabIndex = 1;
            checkBoxbruteforce.Text = "BruteForce UUIDs";
            checkBoxbruteforce.UseVisualStyleBackColor = true;
            // 
            // checkBoxsubfolder
            // 
            checkBoxsubfolder.AutoSize = true;
            checkBoxsubfolder.Location = new Point(16, 57);
            checkBoxsubfolder.Name = "checkBoxsubfolder";
            checkBoxsubfolder.Size = new Size(141, 24);
            checkBoxsubfolder.TabIndex = 0;
            checkBoxsubfolder.Text = "SubFolder Mode";
            checkBoxsubfolder.UseVisualStyleBackColor = true;
            // 
            // labelmapperinputfolder
            // 
            labelmapperinputfolder.AutoSize = true;
            labelmapperinputfolder.Location = new Point(21, 23);
            labelmapperinputfolder.Name = "labelmapperinputfolder";
            labelmapperinputfolder.Size = new Size(89, 20);
            labelmapperinputfolder.TabIndex = 3;
            labelmapperinputfolder.Text = "Input Folder";
            // 
            // buttonbrowsemapperinput
            // 
            buttonbrowsemapperinput.Location = new Point(541, 45);
            buttonbrowsemapperinput.Name = "buttonbrowsemapperinput";
            buttonbrowsemapperinput.Size = new Size(94, 29);
            buttonbrowsemapperinput.TabIndex = 1;
            buttonbrowsemapperinput.Text = "Browse!";
            buttonbrowsemapperinput.UseVisualStyleBackColor = true;
            buttonbrowsemapperinput.Click += buttonbrowsemapperinput_Click;
            // 
            // textBoxinputsdatdumperoutput
            // 
            textBoxinputsdatdumperoutput.Location = new Point(21, 45);
            textBoxinputsdatdumperoutput.Name = "textBoxinputsdatdumperoutput";
            textBoxinputsdatdumperoutput.Size = new Size(515, 27);
            textBoxinputsdatdumperoutput.TabIndex = 0;
            // 
            // ofdGetBAR
            // 
            ofdGetBAR.FileName = "openFileDialog1";
            // 
            // ofdGetedgezlib
            // 
            ofdGetedgezlib.FileName = "openFileDialog2";
            // 
            // ofdGetnonedgezlib
            // 
            ofdGetnonedgezlib.FileName = "openFileDialog3";
            // 
            // ofdGetbartosdat
            // 
            ofdGetbartosdat.FileName = "openFileDialog4";
            // 
            // ofdGettimestamp
            // 
            ofdGettimestamp.FileName = "openFileDialog5";
            // 
            // ofdGettimestamppatch
            // 
            ofdGettimestamppatch.FileName = "openFileDialog5";
            // 
            // ofdGetsha1
            // 
            ofdGetsha1.FileName = "openFileDialog6";
            // 
            // labelunbar
            // 
            labelunbar.AutoSize = true;
            labelunbar.Location = new Point(21, 23);
            labelunbar.Name = "labelunbar";
            labelunbar.Size = new Size(208, 20);
            labelunbar.TabIndex = 4;
            labelunbar.Text = "Input Folder (Bulk Mode Only)";
            // 
            // groupBoxunbar
            // 
            groupBoxunbar.Controls.Add(groupBoxunbarbatch);
            groupBoxunbar.Controls.Add(groupBoxunbarsignlemode);
            groupBoxunbar.Controls.Add(buttonbrowseinputunbarfolderbulk);
            groupBoxunbar.Controls.Add(textBoxunbarinputunbarbulk);
            groupBoxunbar.Controls.Add(labelunbar);
            groupBoxunbar.Location = new Point(3, 3);
            groupBoxunbar.Name = "groupBoxunbar";
            groupBoxunbar.Size = new Size(646, 140);
            groupBoxunbar.TabIndex = 6;
            groupBoxunbar.TabStop = false;
            groupBoxunbar.Text = "UnBar (Not Recommended For Original Home Files)";
            // 
            // groupBoxunbarbatch
            // 
            groupBoxunbarbatch.Controls.Add(buttonunbarbatch);
            groupBoxunbarbatch.Location = new Point(323, 81);
            groupBoxunbarbatch.Name = "groupBoxunbarbatch";
            groupBoxunbarbatch.Size = new Size(282, 59);
            groupBoxunbarbatch.TabIndex = 9;
            groupBoxunbarbatch.TabStop = false;
            groupBoxunbarbatch.Text = "Bulk Mode";
            // 
            // buttonunbarbatch
            // 
            buttonunbarbatch.Location = new Point(70, 20);
            buttonunbarbatch.Name = "buttonunbarbatch";
            buttonunbarbatch.Size = new Size(139, 32);
            buttonunbarbatch.TabIndex = 3;
            buttonunbarbatch.Text = "Decrypt!";
            buttonunbarbatch.UseVisualStyleBackColor = true;
            buttonunbarbatch.Click += buttonunbarbatch_Click;
            // 
            // groupBoxunbarsignlemode
            // 
            groupBoxunbarsignlemode.Controls.Add(buttonunbarsinglemode);
            groupBoxunbarsignlemode.Location = new Point(35, 81);
            groupBoxunbarsignlemode.Name = "groupBoxunbarsignlemode";
            groupBoxunbarsignlemode.Size = new Size(282, 59);
            groupBoxunbarsignlemode.TabIndex = 8;
            groupBoxunbarsignlemode.TabStop = false;
            groupBoxunbarsignlemode.Text = "Single Mode";
            // 
            // buttonunbarsinglemode
            // 
            buttonunbarsinglemode.Location = new Point(70, 20);
            buttonunbarsinglemode.Name = "buttonunbarsinglemode";
            buttonunbarsinglemode.Size = new Size(139, 32);
            buttonunbarsinglemode.TabIndex = 3;
            buttonunbarsinglemode.Text = "Decrypt!";
            buttonunbarsinglemode.UseVisualStyleBackColor = true;
            buttonunbarsinglemode.Click += buttonunbarsinglemode_Click;
            // 
            // buttonbrowseinputunbarfolderbulk
            // 
            buttonbrowseinputunbarfolderbulk.Location = new Point(541, 44);
            buttonbrowseinputunbarfolderbulk.Name = "buttonbrowseinputunbarfolderbulk";
            buttonbrowseinputunbarfolderbulk.Size = new Size(94, 29);
            buttonbrowseinputunbarfolderbulk.TabIndex = 7;
            buttonbrowseinputunbarfolderbulk.Text = "Browse!";
            buttonbrowseinputunbarfolderbulk.UseVisualStyleBackColor = true;
            buttonbrowseinputunbarfolderbulk.Click += buttonbrowseinputunbarfolderbulk_Click;
            // 
            // textBoxunbarinputunbarbulk
            // 
            textBoxunbarinputunbarbulk.Location = new Point(21, 45);
            textBoxunbarinputunbarbulk.Name = "textBoxunbarinputunbarbulk";
            textBoxunbarinputunbarbulk.Size = new Size(515, 27);
            textBoxunbarinputunbarbulk.TabIndex = 5;
            // 
            // groupBoxedgezlibencrypt
            // 
            groupBoxedgezlibencrypt.Controls.Add(buttonedgezlibencrypt);
            groupBoxedgezlibencrypt.Location = new Point(327, 613);
            groupBoxedgezlibencrypt.Name = "groupBoxedgezlibencrypt";
            groupBoxedgezlibencrypt.Size = new Size(323, 77);
            groupBoxedgezlibencrypt.TabIndex = 6;
            groupBoxedgezlibencrypt.TabStop = false;
            groupBoxedgezlibencrypt.Text = "EdgeZlib - Encrypt";
            // 
            // buttonedgezlibencrypt
            // 
            buttonedgezlibencrypt.Location = new Point(91, 27);
            buttonedgezlibencrypt.Name = "buttonedgezlibencrypt";
            buttonedgezlibencrypt.Size = new Size(139, 32);
            buttonedgezlibencrypt.TabIndex = 3;
            buttonedgezlibencrypt.Text = "Encrypt!";
            buttonedgezlibencrypt.UseVisualStyleBackColor = true;
            buttonedgezlibencrypt.Click += buttonedgezlibencrypt_Click;
            // 
            // groupBoxedgezlibdecrypt
            // 
            groupBoxedgezlibdecrypt.Controls.Add(buttonedgezlibdecrypt);
            groupBoxedgezlibdecrypt.Location = new Point(3, 613);
            groupBoxedgezlibdecrypt.Name = "groupBoxedgezlibdecrypt";
            groupBoxedgezlibdecrypt.Size = new Size(318, 77);
            groupBoxedgezlibdecrypt.TabIndex = 5;
            groupBoxedgezlibdecrypt.TabStop = false;
            groupBoxedgezlibdecrypt.Text = "EdgeZlib - Decrypt";
            // 
            // buttonedgezlibdecrypt
            // 
            buttonedgezlibdecrypt.Location = new Point(85, 27);
            buttonedgezlibdecrypt.Name = "buttonedgezlibdecrypt";
            buttonedgezlibdecrypt.Size = new Size(139, 32);
            buttonedgezlibdecrypt.TabIndex = 3;
            buttonedgezlibdecrypt.Text = "Decrypt!";
            buttonedgezlibdecrypt.UseVisualStyleBackColor = true;
            buttonedgezlibdecrypt.Click += buttonedgezlibdecrypt_Click;
            // 
            // tabControlmain
            // 
            tabControlmain.Controls.Add(tabPagemain);
            tabControlmain.Controls.Add(tabPagemisc);
            tabControlmain.Location = new Point(6, 3);
            tabControlmain.Margin = new Padding(3, 4, 3, 4);
            tabControlmain.Name = "tabControlmain";
            tabControlmain.SelectedIndex = 0;
            tabControlmain.Size = new Size(663, 732);
            tabControlmain.TabIndex = 7;
            // 
            // tabPagemain
            // 
            tabPagemain.Controls.Add(groupBoxunbar);
            tabPagemain.Controls.Add(groupBoxedgezlibencrypt);
            tabPagemain.Controls.Add(bargroupBox);
            tabPagemain.Controls.Add(groupBoxedgezlibdecrypt);
            tabPagemain.Controls.Add(groupBoxfoldertobarsdat);
            tabPagemain.Controls.Add(groupBoxsdatdumperoutput);
            tabPagemain.Location = new Point(4, 29);
            tabPagemain.Margin = new Padding(3, 4, 3, 4);
            tabPagemain.Name = "tabPagemain";
            tabPagemain.Padding = new Padding(3, 4, 3, 4);
            tabPagemain.Size = new Size(655, 699);
            tabPagemain.TabIndex = 0;
            tabPagemain.Text = "Main";
            tabPagemain.UseVisualStyleBackColor = true;
            // 
            // tabPagemisc
            // 
            tabPagemisc.Controls.Add(groupBoxSHA1);
            tabPagemisc.Controls.Add(groupBoxtimestamp);
            tabPagemisc.Location = new Point(4, 29);
            tabPagemisc.Margin = new Padding(3, 4, 3, 4);
            tabPagemisc.Name = "tabPagemisc";
            tabPagemisc.Padding = new Padding(3, 4, 3, 4);
            tabPagemisc.Size = new Size(655, 699);
            tabPagemisc.TabIndex = 1;
            tabPagemisc.Text = "Misc";
            tabPagemisc.UseVisualStyleBackColor = true;
            // 
            // groupBoxSHA1
            // 
            groupBoxSHA1.Controls.Add(labelsha1);
            groupBoxSHA1.Controls.Add(textBoxsha1result);
            groupBoxSHA1.Controls.Add(buttonsha1calc);
            groupBoxSHA1.Location = new Point(7, 269);
            groupBoxSHA1.Margin = new Padding(3, 4, 3, 4);
            groupBoxSHA1.Name = "groupBoxSHA1";
            groupBoxSHA1.Padding = new Padding(3, 4, 3, 4);
            groupBoxSHA1.Size = new Size(640, 107);
            groupBoxSHA1.TabIndex = 1;
            groupBoxSHA1.TabStop = false;
            groupBoxSHA1.Text = "SHA1 Calculator";
            // 
            // labelsha1
            // 
            labelsha1.AutoSize = true;
            labelsha1.Location = new Point(170, 25);
            labelsha1.Name = "labelsha1";
            labelsha1.Size = new Size(463, 20);
            labelsha1.TabIndex = 3;
            labelsha1.Text = "To use with decrypted XML files and their <SHA1 digest=\"\"> security";
            // 
            // textBoxsha1result
            // 
            textBoxsha1result.Location = new Point(170, 56);
            textBoxsha1result.Margin = new Padding(3, 4, 3, 4);
            textBoxsha1result.Name = "textBoxsha1result";
            textBoxsha1result.ReadOnly = true;
            textBoxsha1result.Size = new Size(451, 27);
            textBoxsha1result.TabIndex = 2;
            // 
            // buttonsha1calc
            // 
            buttonsha1calc.Location = new Point(22, 28);
            buttonsha1calc.Margin = new Padding(3, 4, 3, 4);
            buttonsha1calc.Name = "buttonsha1calc";
            buttonsha1calc.Size = new Size(130, 59);
            buttonsha1calc.TabIndex = 1;
            buttonsha1calc.Text = "Calculate!";
            buttonsha1calc.UseVisualStyleBackColor = true;
            buttonsha1calc.Click += buttonsha1calc_Click;
            // 
            // groupBoxtimestamp
            // 
            groupBoxtimestamp.Controls.Add(groupBoxbatchcalculatetimestamp);
            groupBoxtimestamp.Controls.Add(v);
            groupBoxtimestamp.Controls.Add(labeltimestamp);
            groupBoxtimestamp.Controls.Add(buttoncalculatetimestamp);
            groupBoxtimestamp.Location = new Point(7, 8);
            groupBoxtimestamp.Margin = new Padding(3, 4, 3, 4);
            groupBoxtimestamp.Name = "groupBoxtimestamp";
            groupBoxtimestamp.Padding = new Padding(3, 4, 3, 4);
            groupBoxtimestamp.Size = new Size(640, 253);
            groupBoxtimestamp.TabIndex = 0;
            groupBoxtimestamp.TabStop = false;
            groupBoxtimestamp.Text = "Timestamp Factory (Do not use this with SHARC files or you will break stuff)";
            // 
            // groupBoxbatchcalculatetimestamp
            // 
            groupBoxbatchcalculatetimestamp.Controls.Add(buttoncalculatebatchtimestamp);
            groupBoxbatchcalculatetimestamp.Controls.Add(buttoncalctimestampbatchgetfile);
            groupBoxbatchcalculatetimestamp.Controls.Add(textBoxtimestampbatchcalculate);
            groupBoxbatchcalculatetimestamp.Location = new Point(170, 28);
            groupBoxbatchcalculatetimestamp.Margin = new Padding(3, 4, 3, 4);
            groupBoxbatchcalculatetimestamp.Name = "groupBoxbatchcalculatetimestamp";
            groupBoxbatchcalculatetimestamp.Padding = new Padding(3, 4, 3, 4);
            groupBoxbatchcalculatetimestamp.Size = new Size(470, 108);
            groupBoxbatchcalculatetimestamp.TabIndex = 5;
            groupBoxbatchcalculatetimestamp.TabStop = false;
            groupBoxbatchcalculatetimestamp.Text = "Batch Mode";
            // 
            // buttoncalculatebatchtimestamp
            // 
            buttoncalculatebatchtimestamp.Location = new Point(161, 68);
            buttoncalculatebatchtimestamp.Margin = new Padding(3, 4, 3, 4);
            buttoncalculatebatchtimestamp.Name = "buttoncalculatebatchtimestamp";
            buttoncalculatebatchtimestamp.Size = new Size(130, 32);
            buttoncalculatebatchtimestamp.TabIndex = 6;
            buttoncalculatebatchtimestamp.Text = "Calculate!";
            buttoncalculatebatchtimestamp.UseVisualStyleBackColor = true;
            buttoncalculatebatchtimestamp.Click += buttoncalculatebatchtimestamp_Click;
            // 
            // buttoncalctimestampbatchgetfile
            // 
            buttoncalctimestampbatchgetfile.Location = new Point(342, 26);
            buttoncalctimestampbatchgetfile.Margin = new Padding(3, 4, 3, 4);
            buttoncalctimestampbatchgetfile.Name = "buttoncalctimestampbatchgetfile";
            buttoncalctimestampbatchgetfile.Size = new Size(110, 31);
            buttoncalctimestampbatchgetfile.TabIndex = 5;
            buttoncalctimestampbatchgetfile.Text = "Browse!";
            buttoncalctimestampbatchgetfile.UseVisualStyleBackColor = true;
            buttoncalctimestampbatchgetfile.Click += buttoncalctimestampbatchgetfile_Click;
            // 
            // textBoxtimestampbatchcalculate
            // 
            textBoxtimestampbatchcalculate.Location = new Point(21, 28);
            textBoxtimestampbatchcalculate.Margin = new Padding(3, 4, 3, 4);
            textBoxtimestampbatchcalculate.Name = "textBoxtimestampbatchcalculate";
            textBoxtimestampbatchcalculate.Size = new Size(315, 27);
            textBoxtimestampbatchcalculate.TabIndex = 4;
            // 
            // v
            // 
            v.Controls.Add(buttontimestamppatchstart);
            v.Controls.Add(labeltimestamppatcher);
            v.Controls.Add(textBoxtimestamppatchervalue);
            v.Location = new Point(170, 141);
            v.Margin = new Padding(3, 4, 3, 4);
            v.Name = "v";
            v.Padding = new Padding(3, 4, 3, 4);
            v.Size = new Size(470, 112);
            v.TabIndex = 3;
            v.TabStop = false;
            v.Text = "Patcher";
            // 
            // buttontimestamppatchstart
            // 
            buttontimestamppatchstart.Location = new Point(313, 25);
            buttontimestamppatchstart.Margin = new Padding(3, 4, 3, 4);
            buttontimestamppatchstart.Name = "buttontimestamppatchstart";
            buttontimestamppatchstart.Size = new Size(111, 72);
            buttontimestamppatchstart.TabIndex = 7;
            buttontimestamppatchstart.Text = "Patch!";
            buttontimestamppatchstart.UseVisualStyleBackColor = true;
            buttontimestamppatchstart.Click += buttontimestamppatchstart_Click;
            // 
            // labeltimestamppatcher
            // 
            labeltimestamppatcher.AutoSize = true;
            labeltimestamppatcher.Location = new Point(46, 25);
            labeltimestamppatcher.Name = "labeltimestamppatcher";
            labeltimestamppatcher.Size = new Size(183, 20);
            labeltimestamppatcher.TabIndex = 6;
            labeltimestamppatcher.Text = "Timestamp Value To Insert";
            // 
            // textBoxtimestamppatchervalue
            // 
            textBoxtimestamppatchervalue.Location = new Point(46, 49);
            textBoxtimestamppatchervalue.Margin = new Padding(3, 4, 3, 4);
            textBoxtimestamppatchervalue.Name = "textBoxtimestamppatchervalue";
            textBoxtimestamppatchervalue.Size = new Size(220, 27);
            textBoxtimestamppatchervalue.TabIndex = 5;
            textBoxtimestamppatchervalue.Text = "FFFFFFFF";
            // 
            // labeltimestamp
            // 
            labeltimestamp.AutoSize = true;
            labeltimestamp.Location = new Point(17, 48);
            labeltimestamp.Name = "labeltimestamp";
            labeltimestamp.Size = new Size(148, 20);
            labeltimestamp.TabIndex = 2;
            labeltimestamp.Text = "Calculate Timestamp";
            // 
            // buttoncalculatetimestamp
            // 
            buttoncalculatetimestamp.Location = new Point(22, 96);
            buttoncalculatetimestamp.Margin = new Padding(3, 4, 3, 4);
            buttoncalculatetimestamp.Name = "buttoncalculatetimestamp";
            buttoncalculatetimestamp.Size = new Size(130, 111);
            buttoncalculatetimestamp.TabIndex = 0;
            buttoncalculatetimestamp.Text = "Calculate!";
            buttoncalculatetimestamp.UseVisualStyleBackColor = true;
            buttoncalculatetimestamp.Click += buttoncalculatetimestamp_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(671, 777);
            Controls.Add(tabControlmain);
            Controls.Add(progressBarbatch);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Form1";
            Text = "MiniTools For Home";
            bargroupBox.ResumeLayout(false);
            bargroupBox.PerformLayout();
            groupBoxbatchbarsdat.ResumeLayout(false);
            groupBoxsinglebarsdat.ResumeLayout(false);
            groupBoxfoldertobarsdat.ResumeLayout(false);
            groupBoxfoldertobarsdat.PerformLayout();
            groupBoxmode.ResumeLayout(false);
            groupBoxmode.PerformLayout();
            groupBoxsdatdumperoutput.ResumeLayout(false);
            groupBoxsdatdumperoutput.PerformLayout();
            groupBoxmappermode.ResumeLayout(false);
            groupBoxmappermode.PerformLayout();
            groupBoxunbar.ResumeLayout(false);
            groupBoxunbar.PerformLayout();
            groupBoxunbarbatch.ResumeLayout(false);
            groupBoxunbarsignlemode.ResumeLayout(false);
            groupBoxedgezlibencrypt.ResumeLayout(false);
            groupBoxedgezlibdecrypt.ResumeLayout(false);
            tabControlmain.ResumeLayout(false);
            tabPagemain.ResumeLayout(false);
            tabPagemisc.ResumeLayout(false);
            groupBoxSHA1.ResumeLayout(false);
            groupBoxSHA1.PerformLayout();
            groupBoxtimestamp.ResumeLayout(false);
            groupBoxtimestamp.PerformLayout();
            groupBoxbatchcalculatetimestamp.ResumeLayout(false);
            groupBoxbatchcalculatetimestamp.PerformLayout();
            v.ResumeLayout(false);
            v.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox bargroupBox;
        private Button convertbutton;
        private GroupBox groupBoxfoldertobarsdat;
        private RadioButton radioButtonSDAT;
        private RadioButton radioButtonBAR;
        private Label labelindicator;
        private Button buttonsubfolderconvert;
        private Button Browsfoldertobarsdatbutton;
        private TextBox SUBFOLDERtextBox;
        private Label labelinputfolderbarsdat;
        private GroupBox groupBoxmode;
        private ProgressBar progressBarbatch;
        private GroupBox groupBoxsdatdumperoutput;
        private GroupBox groupBoxmappermode;
        private Label labelmapperinputfolder;
        private Button buttonbrowsemapperinput;
        private TextBox textBoxinputsdatdumperoutput;
        private Button buttonmap;
        private CheckBox checkBoxbatmode;
        private CheckBox checkBoxbruteforce;
        private CheckBox checkBoxsubfolder;
        private TextBox textBoxpathprefix;
        private Label labelprefix;
        private CheckBox checkBoxsubfolderbarsdat;
        private GroupBox groupBoxsinglebarsdat;
        private Button buttonbarsdanonbulk;
        private GroupBox groupBoxbatchbarsdat;
        private OpenFileDialog ofdGetBAR;
        private Label labelunbar;
        private GroupBox groupBoxunbar;
        private Button buttonbrowseinputunbarfolderbulk;
        private TextBox textBoxunbarinputunbarbulk;
        private GroupBox groupBoxunbarbatch;
        private Button buttonunbarbatch;
        private GroupBox groupBoxunbarsignlemode;
        private Button buttonunbarsinglemode;
        private Button Browseinputbarbutton;
        private TextBox textBoxbarinput;
        private Label label1;
        private GroupBox groupBoxedgezlibencrypt;
        private Button buttonedgezlibencrypt;
        private GroupBox groupBoxedgezlibdecrypt;
        private Button buttonedgezlibdecrypt;
        private OpenFileDialog ofdGetnonedgezlib;
        private OpenFileDialog ofdGetedgezlib;
        private OpenFileDialog ofdGetbartosdat;
        private OpenFileDialog ofdGettimestamp;
        private OpenFileDialog ofdGettimestamppatch;
        private OpenFileDialog ofdGetsha1;
        private TabControl tabControlmain;
        private TabPage tabPagemain;
        private TabPage tabPagemisc;
        private GroupBox groupBoxtimestamp;
        private Button buttoncalculatetimestamp;
        private GroupBox groupBoxbatchcalculatetimestamp;
        private Button buttoncalctimestampbatchgetfile;
        private TextBox textBoxtimestampbatchcalculate;
        private GroupBox v;
        private Label labeltimestamp;
        private Button buttoncalculatebatchtimestamp;
        private TextBox textBoxtimestamppatchervalue;
        private Button buttontimestamppatchstart;
        private Label labeltimestamppatcher;
        private GroupBox groupBoxSHA1;
        private Button buttonsha1calc;
        private TextBox textBoxsha1result;
        private Label labelsha1;
    }
}