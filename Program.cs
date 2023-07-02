using System.Reflection;

namespace PlayStation_Home_MiniTool
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                bool pass = true;

                string approot = AppDomain.CurrentDomain.BaseDirectory;

                if (!Directory.Exists(approot + @"BAT_SCRIPTS"))
                {
                    MessageBox.Show("Startup Error", "Directory BAT_SCRIPTS not present in application's root folder");
                    pass = false;
                }

                if (!Directory.Exists(approot + @"HELPER_FILES"))
                {
                    MessageBox.Show("Startup Error", "Directory HELPER_FILES not present in application's root folder");
                    pass = false;
                }

                if (!File.Exists(approot + @"BRUTEFORCE-xml\BRUTEFORCE_LOCALISATION.XML"))
                {
                    MessageBox.Show("Startup Error", @"File BRUTEFORCE-xml\BRUTEFORCE_LOCALISATION.XML not present in application's root folder");
                    pass = false;
                }

                if (!File.Exists(approot + @"BRUTEFORCE-xml\BRUTEFORCE_OBJECT.XML"))
                {
                    MessageBox.Show("Startup Error", @"File BRUTEFORCE-xml\BRUTEFORCE_OBJECT.XML not present in application's root folder");
                    pass = false;
                }

                if (!File.Exists(approot + @"BRUTEFORCE-xml\BRUTEFORCE_RESOURCES.XML"))
                {
                    MessageBox.Show("Startup Error", @"File BRUTEFORCE-xml\BRUTEFORCE_RESOURCES.XML not present in application's root folder");
                    pass = false;
                }

                if (!File.Exists(approot + @"BRUTEFORCE-xml\GATE.GATE"))
                {
                    MessageBox.Show("Startup Error", @"File BRUTEFORCE-xml\GATE.GATE not present in application's root folder");
                    pass = false;
                }

                if (!File.Exists(approot + @"BRUTEFORCE-xml\UUID-HELPER-NONBULK.XML"))
                {
                    MessageBox.Show("Startup Error", @"File BRUTEFORCE-xml\UUID-HELPER-NONBULK.XML not present in application's root folder");
                    pass = false;
                }

                if (!File.Exists(approot + @"ODC\CATALOGUEENTRYTEMPLATE.XML"))
                {
                    MessageBox.Show("Startup Error", @"File ODC\CATALOGUEENTRYTEMPLATE.XML not present in application's root folder");
                    pass = false;
                }

                if (!File.Exists(approot + @"ODC\CATALOGUEENTRYTEMPLATEFORCLOTH.XML"))
                {
                    MessageBox.Show("Startup Error", @"File ODC\CATALOGUEENTRYTEMPLATEFORCLOTH.XML not present in application's root folder");
                    pass = false;
                }

                if (!File.Exists(approot + @"ODC\CATALOGUEENTRYTEMPLATEFORFURNITURE.XML"))
                {
                    MessageBox.Show("Startup Error", @"File ODC\CATALOGUEENTRYTEMPLATEFORFURNITURE.XML not present in application's root folder");
                    pass = false;
                }

                if (!File.Exists(approot + @"ODC\EDITOR.OXML"))
                {
                    MessageBox.Show("Startup Error", @"File ODC\EDITOR.OXML not present in application's root folder");
                    pass = false;
                }

                if (!File.Exists(approot + @"ODC\PLACE_HOLDER.PNG"))
                {
                    MessageBox.Show("Startup Error", @"File ODC\PLACE_HOLDER.PNG not present in application's root folder");
                    pass = false;
                }

                if (!File.Exists(approot + @"ODC\TEMPLATE.ODC"))
                {
                    MessageBox.Show("Startup Error", @"File ODC\TEMPLATE.ODC not present in application's root folder");
                    pass = false;
                }

                if (!File.Exists(approot + @"SDC\DEFAULT_LARGE.PNG"))
                {
                    MessageBox.Show("Startup Error", @"File SDC\DEFAULT_LARGE.PNG not present in application's root folder");
                    pass = false;
                }

                if (!File.Exists(approot + @"SDC\DEFAULT_MAKER.PNG"))
                {
                    MessageBox.Show("Startup Error", @"File SDC\DEFAULT_MAKER.PNG not present in application's root folder");
                    pass = false;
                }

                if (!File.Exists(approot + @"SDC\DEFAULT_SMALL.PNG"))
                {
                    MessageBox.Show("Startup Error", @"File SDC\DEFAULT_SMALL.PNG not present in application's root folder");
                    pass = false;
                }

                if (!File.Exists(approot + @"SDC\TEMPLATE.SDC"))
                {
                    MessageBox.Show("Startup Error", @"File SDC\TEMPLATE.SDC not present in application's root folder");
                    pass = false;
                }

                if (!File.Exists(approot + @"unbar\UnBAR.exe"))
                {
                    MessageBox.Show("Startup Error", @"File unbar\UnBAR.exe not present in application's root folder");
                    pass = false;
                }

                if (!File.Exists(approot + @"unbar\UnBAR.dll"))
                {
                    MessageBox.Show("Startup Error", @"File unbar\UnBAR.dll not present in application's root folder");
                    pass = false;
                }

                if (!File.Exists(approot + @"unbar\zlib.net.dll"))
                {
                    MessageBox.Show("Startup Error", @"File unbar\zlib.net.dll not present in application's root folder");
                    pass = false;
                }

                if (!File.Exists(approot + @"unbar\CommandLine.dll"))
                {
                    MessageBox.Show("Startup Error", @"File unbar\CommandLine.dll not present in application's root folder");
                    pass = false;
                }

                if (!File.Exists(approot + @"unbar\Home.BAR.dll"))
                {
                    MessageBox.Show("Startup Error", @"File unbar\Home.BAR.dll not present in application's root folder");
                    pass = false;
                }

                if (!File.Exists(approot + @"unbar\ICSharpCode.SharpZipLib.dll"))
                {
                    MessageBox.Show("Startup Error", @"File unbar\ICSharpCode.SharpZipLib.dll not present in application's root folder");
                    pass = false;
                }

                if (!File.Exists(approot + @"NPD\make_npdata.exe"))
                {
                    MessageBox.Show("Startup Error", @"File NPD\make_npdata.debug.exe not present in application's root folder");
                    pass = false;
                }

                if (!File.Exists(approot + @"NPD\liblzr.dll"))
                {
                    MessageBox.Show("Startup Error", @"File NPD\liblzr.dll not present in application's root folder");
                    pass = false;
                }

                if (!File.Exists(approot + @"NPD\npdtool.exe"))
                {
                    MessageBox.Show("Startup Error", @"File NPD\npdtool.exe not present in application's root folder");
                    pass = false;
                }

                if (pass)
                {
                    ApplicationConfiguration.Initialize();
                    Application.Run(new Form1());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Startup Error", @"Application crashed when checking for npdata exe");
                throw ex;
            }
        }
    }
}