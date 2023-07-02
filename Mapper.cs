using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace PlayStation_Home_MiniTool
{
    public class Mapper
    {
        private async Task mapperprepare(string foldertomap)
        {
            bool hasextension = false;

            try
            {
                foreach (string filetoscanforcorruption in Directory.GetFiles(foldertomap))
                {
                    FileInfo fi1 = new FileInfo(filetoscanforcorruption);

                    uint dummy, sectorsPerCluster, bytesPerSector;

                    int result = GetDiskFreeSpaceW(fi1.Directory.Root.FullName, out sectorsPerCluster, out bytesPerSector, out dummy, out dummy);
                    if (result == 0) throw new Win32Exception();
                    uint clusterSize = sectorsPerCluster * bytesPerSector;

                    uint hosize;
                    uint losize = GetCompressedFileSizeW(filetoscanforcorruption, out hosize);
                    long size;
                    size = (long)hosize << 32 | losize;

                    long FileLengthOnHarddisc = ((size + clusterSize - 1) / clusterSize) * clusterSize;

                    //MessageBox.Show(FileLengthOnHarddisc.ToString(), "File Size on Disk");

                    if ((FileLengthOnHarddisc == 0))
                    {
                        string directoryforfailedfiles = Path.GetDirectoryName(filetoscanforcorruption);
                        string filenameforfailedfiles = Path.GetFileName(filetoscanforcorruption).Replace("0X", "");

                        Microsoft.VisualBasic.FileIO.FileSystem.MoveFile(filetoscanforcorruption, directoryforfailedfiles + @"\" + /* @"\!CORRUPTED\" + /*@"\FAILED\" +*/ filenameforfailedfiles + ".FAILED", true);
                    }
                }

                string[] initialcursorcontainer = Directory.GetFiles(foldertomap + @"\");

                foreach (string myfile in initialcursorcontainer)
                {
                    hasextension = Path.HasExtension(myfile);

                    if (myfile.EndsWith(".FAILED"))
                    {
                        string tempnewcorruptedname = myfile.Remove(myfile.Length - 7);

                        if (!File.Exists(tempnewcorruptedname))
                        {
                            File.Move(myfile, tempnewcorruptedname);
                        }

                        goto finished;
                    }

                    if (!hasextension)
                    {
                        string newFileName = myfile.Replace("0X", "");

                        if (File.ReadLines(myfile).First().Contains("DDS |"))
                        {
                            if (!File.Exists(newFileName + ".dds"))
                            {
                                File.Move(myfile, newFileName + ".dds");
                            }
                        }
                        else if (File.ReadLines(myfile).First().Contains("‰PNG") || File.ReadAllText(myfile).Contains("Photoshop ICC profile") || File.ReadAllText(myfile).Contains("IHDR"))
                        {
                            if (!File.Exists(newFileName + ".png"))
                            {
                                File.Move(myfile, newFileName + ".png");
                            }
                        }
                        else if (File.ReadLines(myfile).First().Contains("LuaQ"))
                        {
                            if (!File.Exists(newFileName + ".luac"))
                            {
                                File.Move(myfile, newFileName + ".luac");
                            }
                        }
                        else if (File.ReadLines(myfile).First().Contains("CHNK"))
                        {
                            if (!File.Exists(newFileName + ".effect"))
                            {
                                File.Move(myfile, newFileName + ".effect");
                            }
                        }
                        else if (File.ReadLines(myfile).First().Contains("HM") || File.ReadLines(myfile).First().Contains("MR04"))
                        {
                            if (!File.Exists(newFileName + ".mdl"))
                            {
                                File.Move(myfile, newFileName + ".mdl");
                            }
                        }
                        else if (File.ReadLines(myfile).First().Contains("WW") || File.ReadAllText(myfile).Contains("Havok-5.0.0-r1"))
                        {
                            if (!File.Exists(newFileName + ".hkx"))
                            {
                                File.Move(myfile, newFileName + ".hkx");
                            }
                        }
                        else if (File.ReadAllText(myfile).Contains("AC11"))
                        {
                            if (!File.Exists(newFileName + ".ani"))
                            {
                                File.Move(myfile, newFileName + ".ani");
                            }
                        }
                        else if (File.ReadAllText(myfile).Contains("LoadLibrary") || File.ReadAllText(myfile).Contains("function") || File.ReadAllText(myfile).Contains("<copyright>"))
                        {
                            if (!File.Exists(newFileName + ".lua"))
                            {
                                File.Move(myfile, newFileName + ".lua");
                            }
                        }
                        else if (File.ReadAllText(myfile).Contains("SK08"))
                        {
                            if (!File.Exists(newFileName + ".skn"))
                            {
                                File.Move(myfile, newFileName + ".skn");
                            }
                        }
                        else if (File.ReadAllText(myfile).Contains("klBS"))
                        {
                            if (!File.Exists(newFileName + ".bnk"))
                            {
                                File.Move(myfile, newFileName + ".bnk");
                            }
                        }
                        else if (File.ReadAllText(myfile).Contains("gap:game") || File.ReadAllText(myfile).Contains("</gameObject>"))
                        {
                            if (!File.Exists(newFileName + ".SCENE"))
                            {
                                File.Move(myfile, newFileName + ".SCENE");
                            }
                        }
                        else if (File.ReadAllText(myfile).Contains("LAME3.97") || File.ReadAllText(myfile).Contains("LAME3.98") || File.ReadAllText(myfile).Contains("SfMarkers"))
                        {
                            if (!File.Exists(newFileName + ".mp3"))
                            {
                                File.Move(myfile, newFileName + ".mp3");
                            }
                        }
                        else if (File.ReadAllText(myfile).Contains("DSIG"))
                        {
                            if (!File.Exists(newFileName + ".ttf"))
                            {
                                File.Move(myfile, newFileName + ".ttf");
                            }
                        }
                        else
                        {
                            if (!File.Exists(newFileName + ".xml"))
                            {
                                File.Move(myfile, newFileName + ".xml");
                            }
                        }
                    }
                finished:;
                }
                return;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private async Task batfactoryperpare(string foldertomap)
        {
            string approot = AppDomain.CurrentDomain.BaseDirectory;

            try
            {
                string[] batfiles = Directory.GetFiles(approot + @"BAT_SCRIPTS", "*.bat", SearchOption.AllDirectories);
                foreach (string file in batfiles)
                {
                    if (!File.Exists(foldertomap + @"\" + Path.GetFileName(file)))
                    {
                        File.Copy(file, foldertomap + @"\" + Path.GetFileName(file));
                    }
                }

                return;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private async Task filehelperfactoryperpare(string foldertomap)
        {
            string approot = AppDomain.CurrentDomain.BaseDirectory;

            try
            {
                string[] filehelperfiles = Directory.GetFiles(approot + @"HELPER_FILES", "*.*", SearchOption.AllDirectories);
                foreach (string file in filehelperfiles)
                {
                    if (!File.Exists(foldertomap + @"\" + Path.GetFileName(file) + ".HELPML"))
                    {
                        File.Copy(file, foldertomap + @"\" + Path.GetFileName(file) + ".HELPML");
                    }
                }

                return;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task mapperstart(string foldertomap, bool bruteforcemode, bool subfoldermode, bool batmode, string prefixfromtext)
        {
            string resultforfoldername = "";

            try
            {
                resultforfoldername = new DirectoryInfo(foldertomap).Name;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (resultforfoldername == "")
            {
                Form1.temptask += 1;

                return;
            }

            await Task.Run(() => mapperprepare(foldertomap));

            if (batmode && !subfoldermode)
            {
                await Task.Run(() => batfactoryperpare(foldertomap));
            }

            await Task.Run(() => filehelperfactoryperpare(foldertomap));

            bool objectcomplementscan = false;

            bool passvalue = true;

            string objectprefix = prefixfromtext;

            string objectprefixtemp = "";

            string originalprefixuuid = "";

            string approot = AppDomain.CurrentDomain.BaseDirectory;

            if (File.Exists(foldertomap + @"\" + "Object_UUID_Brute.xml"))
            {
                File.Delete(foldertomap + @"\" + "Object_UUID_Brute.xml");
            }

            if (File.Exists(foldertomap + @"\" + "Object_UUID_Brute_Light.xml"))
            {
                File.Delete(foldertomap + @"\" + "Object_UUID_Brute_Light.xml");
            }

            if (File.Exists(foldertomap + @"\" + "Object_UUID_Brute_alt.xml"))
            {
                File.Delete(foldertomap + @"\" + "Object_UUID_Brute_alt.xml");
            }

            if (File.Exists(foldertomap + @"\" + "Object_UUID_Brute_alt_alt.xml"))
            {
                File.Delete(foldertomap + @"\" + "Object_UUID_Brute_alt_alt.xml");
            }

            List<RegexPatterns> listforscan = new List<RegexPatterns>
                {
                    new RegexPatterns
                    {
                        type = ".mdl",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.mdl"
                    },
                    new RegexPatterns
                    {
                        type = ".dds",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.dds"
                    },
                    new RegexPatterns
                    {
                        type = ".dds",
                        pattern = "(?<=\\b(?<=href=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.dds"
                    },
                    new RegexPatterns
                    {
                        type = ".dds",
                        pattern = "(?<=\\b(?<=src=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.dds"
                    },
                    new RegexPatterns
                    {
                        type = ".dds",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.DDS"
                    },
                    new RegexPatterns
                    {
                        type = ".dds",
                        pattern = "(?<=\\b(?<=href=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.DDS"
                    },
                    new RegexPatterns
                    {
                        type = ".dds",
                        pattern = "(?<=\\b(?<=src=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.DDS"
                    },
                    new RegexPatterns
                    {
                        type = ".dds",
                        pattern = "([\\w-\\s]+\\\\)+[\\w-\\s]+\\.dds"
                    },
                    new RegexPatterns
                    {
                        type = ".dds",
                        pattern = "([\\w-\\s]+\\\\)+[\\w-\\s]+\\.DDS"
                    },
                    new RegexPatterns
                    {
                        type = "..dds",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*..dds"
                    },
                    new RegexPatterns
                    {
                        type = "..dds",
                        pattern = "(?<=\\b(?<=href=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*..dds"
                    },
                    new RegexPatterns
                    {
                        type = "..dds",
                        pattern = "(?<=\\b(?<=src=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*..dds"
                    },
                    new RegexPatterns
                    {
                        type = "..dds",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*..DDS"
                    },
                    new RegexPatterns
                    {
                        type = "..dds",
                        pattern = "(?<=\\b(?<=href=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*..DDS"
                    },
                    new RegexPatterns
                    {
                        type = "..dds",
                        pattern = "(?<=\\b(?<=src=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*..DDS"
                    },
                    new RegexPatterns
                    {
                        type = "..dds",
                        pattern = "([\\w-\\s]+\\\\)+[\\w-\\s]+\\..dds"
                    },
                    new RegexPatterns
                    {
                        type = "..dds",
                        pattern = "([\\w-\\s]+\\\\)+[\\w-\\s]+\\..DDS"
                    },
                    new RegexPatterns
                    {
                        type = "passphrase",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*passphrase"
                    },
                    new RegexPatterns
                    {
                        type = "passphrase_EU",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*passphrase_EU"
                    },
                    new RegexPatterns
                    {
                        type = ".repertoire_circuit",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.repertoire_circuit"
                    },
                    new RegexPatterns
                    {
                        type = ".lua",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.lua"
                    },
                    new RegexPatterns
                    {
                        type = ".lua",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.LUA"
                    },
                    new RegexPatterns
                    {
                        type = ".lua",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.Lua"
                    },
                    new RegexPatterns
                    {
                        type = ".luac",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.luac"
                    },
                    new RegexPatterns
                    {
                        type = ".json",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.json"
                    },
                    new RegexPatterns
                    {
                        type = ".luac",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.LUAC"
                    },
                    new RegexPatterns
                    {
                        type = ".xml",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.xml"
                    },
                    new RegexPatterns
                    {
                        type = ".xml",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.XML"
                    },
                    new RegexPatterns
                    {
                        type = ".efx",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|efx_filename=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.efx"
                    },
                    new RegexPatterns
                    {
                        type = ".efx",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.effect"
                    },
                    new RegexPatterns
                    {
                        type = ".bnk",
                        pattern = "(?<=\\b(?<=source=\"|filename=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.bnk"
                    },
                    new RegexPatterns
                    {
                        type = ".ttf",
                        pattern = "(?<=\\b(?<=source=\"|filename=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.ttf"
                    },
                    new RegexPatterns
                    {
                        type = ".bank",
                        pattern = "(?<=\\b(?<=source=\"|filename=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.bank"
                    },
                    new RegexPatterns
                    {
                        type = ".hkx",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.hkx"
                    },
                    new RegexPatterns
                    {
                        type = ".probe",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.probe"
                    },
                    new RegexPatterns
                    {
                        type = ".ocean",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.ocean"
                    },
                    new RegexPatterns
                    {
                        type = ".skn",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.skn"
                    },
                    new RegexPatterns
                    {
                        type = ".ani",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.ani"
                    },
                    new RegexPatterns
                    {
                        type = ".mp3",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.mp3"
                    },
                    new RegexPatterns
                    {
                        type = ".atmos",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.atmos"
                    },
                    new RegexPatterns
                    {
                        type = ".png",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.png"
                    },
                    new RegexPatterns
                    {
                        type = ".cer",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.cer"
                    },
                    new RegexPatterns
                    {
                        type = ".der",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.der"
                    },
                    new RegexPatterns
                    {
                        type = ".bin",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.bin"
                    },
                    new RegexPatterns
                    {
                        type = ".raw",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.raw"
                    },
                    new RegexPatterns
                    {
                        type = ".ini",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.ini"
                    },
                    new RegexPatterns
                    {
                        type = ".enemy",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.enemy"
                    },
                    new RegexPatterns
                    {
                        type = ".ui-setup",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.ui-setup"
                    },
                    new RegexPatterns
                    {
                        type = ".cam-def",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.cam-def"
                    },
                    new RegexPatterns
                    {
                        type = ".level-setup",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.level-setup"
                    },
                    new RegexPatterns
                    {
                        type = ".node-def",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.node-def"
                    },
                    new RegexPatterns
                    {
                        type = ".spline-def",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.spline-def"
                    },
                    new RegexPatterns
                    {
                        type = ".psd",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.psd"
                    },
                    new RegexPatterns
                    {
                        type = ".tmx",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.tmx"
                    },
                    new RegexPatterns
                    {
                        type = ".atgi",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.atgi"
                    },
                    new RegexPatterns
                    {
                        type = ".fpo",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.fpo"
                    },
                    new RegexPatterns
                    {
                        type = ".bank",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.bank"
                    },
                    new RegexPatterns
                    {
                        type = ".bnk",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.bnk"
                    },
                    new RegexPatterns
                    {
                        type = ".agf",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.agf"
                    },
                    new RegexPatterns
                    {
                        type = ".avtr",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.avtr"
                    },
                    new RegexPatterns
                    {
                        type = ".vpo",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.vpo"
                    },
                    new RegexPatterns
                    {
                        type = ".vxd",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.vxd"
                    },
                    new RegexPatterns
                    {
                        type = ".jpg",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.jpg"
                    },
                    new RegexPatterns
                    {
                        type = ".mp4",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.mp4"
                    },
                    new RegexPatterns
                    {
                        type = ".sdat",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.sdat"
                    },
                    new RegexPatterns
                    {
                        type = ".dat",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.dat"
                    },
                    new RegexPatterns
                    {
                        type = ".svml",
                        pattern = "(?<=\\b(?<=href=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.svml"
                    },
                    new RegexPatterns
                    {
                        type = ".svml",
                        pattern = "(?<=\\b(?<=src=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.svml"
                    },
                    new RegexPatterns
                    {
                        type = ".sql",
                        pattern = "(?<=\\b(?<=src=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.sql"
                    },
                    new RegexPatterns
                    {
                        type = ".svml",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.svml"
                    },
                    new RegexPatterns
                    {
                        type = ".fp",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.fp"
                    },
                    new RegexPatterns
                    {
                        type = ".sql",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.sql"
                    },
                    new RegexPatterns
                    {
                        type = ".BAR",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.BAR"
                    },
                    new RegexPatterns
                    {
                        type = ".vp",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.vp"
                    },
                    new RegexPatterns
                    {
                        type = ".dat",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.dat"
                    },
                    new RegexPatterns
                    {
                        type = ".bar",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.bar"
                    },
                    new RegexPatterns
                    {
                        type = ".odc",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.odc"
                    },
                    new RegexPatterns
                    {
                        type = ".scene",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.scene"
                    },
                    new RegexPatterns
                    {
                        type = ".gui-setup",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.gui-setup"
                    },
                    new RegexPatterns
                    {
                        type = ".oel",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.oel"
                    },
                    new RegexPatterns
                    {
                        type = ".wav",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.wav"
                    },
                    new RegexPatterns
                    {
                        type = ".gui-setup",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.gui-setup"
                    },
                    new RegexPatterns
                    {
                        type = ".sdc",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.sdc"
                    },
                    new RegexPatterns
                    {
                        type = ".oxml",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.oxml"
                    },
                    new RegexPatterns
                    {
                        type = ".ttf",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.ttf"
                    },
                    new RegexPatterns
                    {
                        type = ".ttf",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.TTF"
                    },
                    new RegexPatterns
                    {
                        type = ".tga",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.tga"
                    },
                    new RegexPatterns
                    {
                        type = ".rig",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.rig"
                    },
                    new RegexPatterns
                    {
                        type = ".jsp",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.jsp"
                    },
                    new RegexPatterns
                    {
                        type = ".fnt",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.fnt"
                    },
                    new RegexPatterns
                    {
                        type = ".shpack",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.shpack"
                    },
                    new RegexPatterns
                    {
                        type = ".php",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.php"
                    },
                    new RegexPatterns
                    {
                        type = ".html",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.html"
                    },
                    new RegexPatterns
                    {
                        type = ".txt",
                        pattern = "(?<=\\b(?<=source=\"|file=\"|texture\\s=\\s\"|spriteTexture\\s=\\s\"))[^\"]*.txt"
                    }
                };

        secondscan:;

            if (bruteforcemode)
            {
                try
                {
                    if (passvalue)
                    {
                        File.Copy(approot + @"BRUTEFORCE-xml\BRUTEFORCE_RESOURCES.XML", foldertomap + @"\Object_UUID_Brute.xml");
                        File.Copy(approot + @"BRUTEFORCE-xml\BRUTEFORCE_OBJECT.XML", foldertomap + @"\Object_UUID_Brute_alt.xml");
                        File.Copy(approot + @"BRUTEFORCE-xml\BRUTEFORCE_LOCALISATION.XML", foldertomap + @"\Object_UUID_Brute_alt_alt.xml");
                        passvalue = false;
                    }
                    else
                    {
                        if (File.Exists(foldertomap + @"\" + "Object_UUID_Brute.xml"))
                        {
                            File.Delete(foldertomap + @"\" + "Object_UUID_Brute.xml");
                        }
                        if (File.Exists(foldertomap + @"\" + "Object_UUID_Brute_alt.xml"))
                        {
                            File.Delete(foldertomap + @"\" + "Object_UUID_Brute_alt.xml");
                        }
                        if (File.Exists(foldertomap + @"\" + "Object_UUID_Brute_alt_alt.xml"))
                        {
                            File.Delete(foldertomap + @"\" + "Object_UUID_Brute_alt_alt.xml");
                        }
                    }

                    if (objectcomplementscan)
                    {
                        objectprefixtemp = @"objects/" + objectprefix + @"/";
                        originalprefixuuid = objectprefix;
                        objectprefix = "";
                    }
                }
                catch (Exception ex)
                {
                    Form1.temptask += 1;
                    throw ex;
                }
            }
            else
            {
                if (passvalue)
                {
                    try
                    {
                        if (objectprefix == "")
                        {
                            string currentresourcesdirectoryrewformat = foldertomap.Substring(foldertomap.Length - 40); // Rew bat format

                            currentresourcesdirectoryrewformat = currentresourcesdirectoryrewformat.Remove(35); // Rew bat format

                            string textlightcursorbrute = File.ReadAllText(approot + @"BRUTEFORCE-xml\UUID-HELPER-NONBULK.XML");

                            textlightcursorbrute = textlightcursorbrute.Replace("UUID_HERE", currentresourcesdirectoryrewformat);

                            if (!File.Exists(foldertomap + @"\" + "Object_UUID_Brute_Light.xml"))
                            {
                                File.WriteAllText(foldertomap + @"\" + "Object_UUID_Brute_Light.xml", textlightcursorbrute);
                            }
                        }
                        else
                        {
                            if (!File.Exists(foldertomap + @"\" + "Object_UUID_Brute_Light.xml"))
                            {
                                File.WriteAllText(foldertomap + @"\" + "Object_UUID_Brute_Light.xml", File.ReadAllText(approot + @"BRUTEFORCE-xml\UUID-HELPER-PREFIX.XML"));
                            }
                        }
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        // Invalid for uuid testing.
                    }
                    catch (Exception ex)
                    {
                        Form1.temptask += 1;
                        throw ex;
                    }

                    passvalue = false;
                }
                else
                {
                    try
                    {
                        if (File.Exists(foldertomap + @"\" + "Object_UUID_Brute_Light.xml"))
                        {
                            File.Delete(foldertomap + @"\" + "Object_UUID_Brute_Light.xml");
                        }
                    }
                    catch (Exception ex)
                    {
                        Form1.temptask += 1;
                        throw ex;
                    }
                }
            }

            IEnumerable<string> enumerable = from s in Directory.EnumerateFiles(foldertomap, "*.*", SearchOption.AllDirectories)
                                             where s.EndsWith(".mdl") || s.EndsWith(".xml") || s.EndsWith(".svml") || s.EndsWith(".MDL") || s.EndsWith(".XML") || s.EndsWith(".efx") || s.EndsWith(".EFX") || s.EndsWith(".SVML") || s.EndsWith(".SCENE") || s.EndsWith(".scene") || s.EndsWith(".atmos") || s.EndsWith(".ATMOS") || s.EndsWith(".HELPML")
                                             select s;

            List<MappedList> list = new List<MappedList>();
            foreach (string sourceFile in enumerable)
            {
                string input = string.Empty;
                List<MappedList> list2 = new List<MappedList>();
                using (StreamReader streamReader = File.OpenText(sourceFile))
                {
                    input = streamReader.ReadToEnd();
                    streamReader.Close();
                }
                foreach (RegexPatterns regexPatterns in listforscan)
                {
                    foreach (object obj in Regex.Matches(input, regexPatterns.pattern))
                    {
                        Match match = (Match)obj;
                        if (!list2.Contains(new MappedList
                        {
                            type = regexPatterns.type,
                            file = match.Value
                        }))
                        {
                            list2.Add(new MappedList
                            {
                                type = regexPatterns.type,
                                file = match.Value
                            });
                        }
                    }
                }

                List<MappedList> collection = list2;
                list.AddRange(collection);
            }

            foreach (MappedList mappedList in list)
            {
                string refhkxname = "";
                string refcdataname = "";
                string text = Regex.Replace(mappedList.file, "file:(\\/+)resource_root\\/build\\/", "", RegexOptions.IgnoreCase);
                text = Regex.Replace(text, "file:", "", RegexOptions.IgnoreCase);
                text = Regex.Replace(text, "///", "", RegexOptions.IgnoreCase);
                text = Regex.Replace(text, "/", "\\", RegexOptions.IgnoreCase);
                text = Regex.Replace(text, "reward//ConfederateGeneral_M_Reward.dds", "reward/ConfederateGeneral_M_Reward.dds", RegexOptions.IgnoreCase);
                text = Regex.Replace(text, "NATGParticles", "ATGParticles", RegexOptions.IgnoreCase);
                text = Regex.Replace(text, "xenvironments", "environments", RegexOptions.IgnoreCase);
                text = Regex.Replace(text, "Tenvironments", "environments", RegexOptions.IgnoreCase);
                text = Regex.Replace(text, "netinit.svml", "host_svml\\netinit.svml", RegexOptions.IgnoreCase);
                text = Regex.Replace(text, "home2.svml", "host_svml\\home2.svml", RegexOptions.IgnoreCase);
                text = Regex.Replace(text, "home.svml", "host_svml\\home.svml", RegexOptions.IgnoreCase);
                text = Regex.Replace(text, "agecheck.svml", "host_svml\\agecheck.svml", RegexOptions.IgnoreCase);
                text = Regex.Replace(text, "ageratingfailed.svml", "host_svml\\ageratingfailed.svml", RegexOptions.IgnoreCase);
                text = Regex.Replace(text, "GAMERETURNHOME2.svml", "host_svml\\GAMERETURNHOME2.svml", RegexOptions.IgnoreCase);
                text = Regex.Replace(text, "HUBLOGIN.svml", "host_svml\\HUBLOGIN.svml", RegexOptions.IgnoreCase);
                text = Regex.Replace(text, "LOGOUT.svml", "host_svml\\LOGOUT.svml", RegexOptions.IgnoreCase);
                text = Regex.Replace(text, "nekoonline.svml", "host_svml\\nekoonline.svml", RegexOptions.IgnoreCase);
                text = Regex.Replace(text, "onliness.svml", "host_svml\\onliness.svml", RegexOptions.IgnoreCase);
                text = Regex.Replace(text, "onlinetext.svml", "host_svml\\onlinetext.svml", RegexOptions.IgnoreCase);
                text = Regex.Replace(text, "placeholder.svml", "host_svml\\placeholder.svml", RegexOptions.IgnoreCase);
                text = Regex.Replace(text, "popup.svml", "host_svml\\popup.svml", RegexOptions.IgnoreCase);
                text = Regex.Replace(text, "unityinit.svml", "host_svml\\unityinit.svml", RegexOptions.IgnoreCase);
                text = Regex.Replace(text, "unitymuis.svml", "host_svml\\unitymuis.svml", RegexOptions.IgnoreCase);
                text = Regex.Replace(text, "unitytersm.svml", "host_svml\\unitytersm.svml", RegexOptions.IgnoreCase);
                text = Regex.Replace(text, "update.svml", "host_svml\\update.svml", RegexOptions.IgnoreCase);
                text = Regex.Replace(text, "nekomuis.svml", "host_svml\\nekomuis.svml", RegexOptions.IgnoreCase);

                if (text.EndsWith(".hkx") || text.EndsWith(".HKX"))
                {
                    refhkxname = text; // Not good yet, must convert \ wih /.
                }

                if (text.EndsWith(".atmos") || text.EndsWith(".ATMOS"))
                {
                    refcdataname = text; // Not good yet, must convert \ wih /.
                }

                string text2 = "";
                string fileinputforhash = "";

                if (objectprefix != "")
                {

                    fileinputforhash = objectprefix + text;

                    int num = 0;
                    foreach (char value in fileinputforhash.ToLower().Replace(Path.DirectorySeparatorChar, '/'))
                    {
                        num *= 37;
                        num += Convert.ToInt32(value);
                    }

                    text2 = num.ToString("X8");
                }
                else
                {
                    fileinputforhash = text;

                    int num = 0;
                    foreach (char value in fileinputforhash.ToLower().Replace(Path.DirectorySeparatorChar, '/'))
                    {
                        num *= 37;
                        num += Convert.ToInt32(value);
                    }

                    text2 = num.ToString("X8");
                }

                DirectoryInfo dirInfo = new DirectoryInfo(foldertomap);
                FileInfo[] filesToMap = dirInfo.GetFiles(text2 + ".*");

                foreach (FileInfo foundFile in filesToMap)
                {
                    bool flag = File.Exists(Path.Combine(foldertomap, foundFile.Name));
                    if (flag)
                    {
                        try
                        {
                            new FileInfo(Path.Combine(foldertomap, text).ToUpper()).Directory.Create();
                            File.Move(Path.Combine(foldertomap, foundFile.Name), Path.Combine(foldertomap, text.ToUpper()));
                        }
                        catch (Exception ex)
                        {
                            Form1.temptask += 1;
                            throw ex;
                        }
                    }
                }

                if (refcdataname != "")
                {
                    string cdatafromatmostemp = refcdataname.Remove(refcdataname.Length - 6);
                    string cdatafromatmos = cdatafromatmostemp + ".cdata";

                    int num = 0;
                    foreach (char value in cdatafromatmos.ToLower().Replace(Path.DirectorySeparatorChar, '/'))
                    {
                        num *= 37;
                        num += Convert.ToInt32(value);
                    }

                    string hashforcdata = num.ToString("X8");

                    DirectoryInfo dirInfocdata = new DirectoryInfo(foldertomap);
                    FileInfo[] filesToMapcdata = dirInfocdata.GetFiles(hashforcdata + ".*");

                    foreach (FileInfo foundFilecdata in filesToMapcdata)
                    {
                        bool flag = File.Exists(Path.Combine(foldertomap, foundFilecdata.Name));
                        if (flag)
                        {
                            try
                            {
                                new FileInfo(Path.Combine(foldertomap, cdatafromatmos).ToUpper()).Directory.Create();
                                File.Move(Path.Combine(foldertomap, foundFilecdata.Name), Path.Combine(foldertomap, cdatafromatmos.ToUpper()));
                            }
                            catch (Exception ex)
                            {
                                Form1.temptask += 1;
                                throw ex;
                            }
                        }
                    }
                }

                if (refhkxname != "")
                {
                    try
                    {
                        string hkxfile = Path.Combine(foldertomap, text.ToUpper());

                        string hkxfolder = Path.GetDirectoryName(hkxfile);

                        string hkxuppercasename = Path.GetFileName(hkxfile);

                        string hkxuppercasenamewithoutextension = hkxuppercasename.Remove(hkxuppercasename.Length - 4);

                        if (File.Exists(hkxfolder + @"\" + hkxuppercasenamewithoutextension + @".SCENE"))
                        {
                            goto skiped;
                        }

                        foreach (string scenefile in Directory.GetFiles(@foldertomap, "*.*").Where(ys => ys.EndsWith(".SCENE") || ys.EndsWith(".scene") || ys.EndsWith(".xml") || ys.EndsWith(".XML")))
                        {
                            if (File.ReadAllText(scenefile).Contains(refhkxname.Replace(@"\", @"/")) && (File.ReadAllText(scenefile).Contains("gap:game") || File.ReadAllText(scenefile).Contains("</gameObject>")))
                            {
                                if (!File.Exists(hkxfolder + @"\" + hkxuppercasenamewithoutextension + @".SCENE"))
                                {
                                    if (!Directory.Exists(hkxfolder))
                                    {
                                        Directory.CreateDirectory(hkxfolder);
                                    }

                                    string newscenefile = hkxfolder + @"\" + hkxuppercasenamewithoutextension + @".SCENE";

                                    File.Move(scenefile, newscenefile);

                                    if (!File.Exists(hkxfolder + @"\" + hkxuppercasenamewithoutextension + ".SDC"))
                                    {
                                        string destpngfolder = foldertomap + @"\THUMBNAILS\" + hkxuppercasenamewithoutextension + @"\";

                                        string hdkversion = "No HDK version found";

                                        if (File.Exists(newscenefile))
                                        {
                                            XmlDocument scenehdk = new XmlDocument();
                                            scenehdk.Load(newscenefile);

                                            XmlElement cursorscenehdk = scenehdk.DocumentElement;

                                            // Check to see if the element has a genre attribute.
                                            if (cursorscenehdk.HasAttribute("hdk_version"))
                                            {
                                                hdkversion = cursorscenehdk.GetAttribute("hdk_version");
                                            }

                                            string textforinput = File.ReadAllText(approot + @"SDC\TEMPLATE.SDC");

                                            string properscenename = Regex.Replace(hkxuppercasenamewithoutextension, @"(\B[A-Z])", m => "" + m.ToString().ToLower());

                                            textforinput = textforinput.Replace("SCENE_NAME", properscenename.Replace('_', ' '));
                                            textforinput = textforinput.Replace("SCENE_HDK_VERSION", hdkversion);
                                            textforinput = textforinput.Replace("THUMBNAIL_NAME", hkxuppercasenamewithoutextension + @"/" + hkxuppercasenamewithoutextension);

                                            File.WriteAllText(hkxfolder + @"\" + hkxuppercasenamewithoutextension + ".SDC", textforinput);

                                            if (!Directory.Exists(destpngfolder))
                                            {
                                                Directory.CreateDirectory(destpngfolder);
                                            }

                                            if (!File.Exists(destpngfolder + hkxuppercasenamewithoutextension + @"_MAKER.PNG"))
                                            {
                                                File.Copy(approot + @"SDC\DEFAULT_MAKER.PNG", destpngfolder + hkxuppercasenamewithoutextension + @"_MAKER.PNG");
                                            }

                                            if (!File.Exists(destpngfolder + hkxuppercasenamewithoutextension + @"_SMALL.PNG"))
                                            {
                                                File.Copy(approot + @"SDC\DEFAULT_SMALL.PNG", destpngfolder + hkxuppercasenamewithoutextension + @"_SMALL.PNG");
                                            }

                                            if (!File.Exists(destpngfolder + hkxuppercasenamewithoutextension + @"_LARGE.PNG"))
                                            {
                                                File.Copy(approot + @"SDC\DEFAULT_LARGE.PNG", destpngfolder + hkxuppercasenamewithoutextension + @"_LARGE.PNG");
                                            }
                                        }
                                        else
                                        {
                                            goto skiped;
                                        }
                                    }
                                }
                            }
                        }
                    skiped:;
                    }
                    catch (XmlException exception)
                    {
                        // No need to break, can happen if scene file isn't standard.
                    }
                    catch (Exception ex)
                    {
                        Form1.temptask += 1;
                        throw ex;
                    }
                }
            }

            if (objectcomplementscan)
            {
                objectprefix = objectprefixtemp;

                objectprefixtemp = "";

                objectcomplementscan = false;
                goto secondscan;
            }

            if (originalprefixuuid != "")
            {
                objectprefix = originalprefixuuid;
                originalprefixuuid = "";
            }

            if (!bruteforcemode && objectprefix == "")
            {
                foreach (string currentresourcesxml in Directory.GetFiles(foldertomap + @"\", "RESOURCES.XML", SearchOption.AllDirectories))
                {
                    string currentresourcesdirectory = Path.GetDirectoryName(currentresourcesxml);

                    originalprefixuuid = currentresourcesdirectory.Substring(currentresourcesdirectory.Length - 35);

                    objectprefix = @"objects/" + originalprefixuuid + @"/";

                    goto secondscan;
                }
            }

            if (objectprefix != "")
            {
                try
                {
                    string objectxmlfile = foldertomap + @"\OBJECTS\" + objectprefix + @"\" + "OBJECT.XML";
                    string ressourcesxmlfile = foldertomap + @"\OBJECTS\" + objectprefix + @"\" + "RESOURCES.XML";
                    string localisationxmlfile = foldertomap + @"\" + @"OBJECTS\" + objectprefix + @"\" + "LOCALISATION.XML";

                    if (File.Exists(objectxmlfile))
                    {
                        try
                        {
                            string hdkversion = "No HDK version found";
                            string namefromobjectxml = "No name found";

                            XmlDocument objecthdk = new XmlDocument();
                            objecthdk.Load(objectxmlfile);

                            XmlElement cursorobjecthdk = objecthdk.DocumentElement;

                            // Check to see if the element has a genre attribute.
                            if (cursorobjecthdk.HasAttribute("hdk_version"))
                            {
                                hdkversion = cursorobjecthdk.GetAttribute("hdk_version");
                            }

                            XmlNodeList nodeListname = objecthdk.GetElementsByTagName("name");
                            foreach (XmlNode node in nodeListname)
                            {
                                namefromobjectxml = node.InnerText;
                                goto finished;
                            }

                        finished:;

                            if (namefromobjectxml == "kName" || namefromobjectxml == "NAME" || namefromobjectxml == "Name")
                            {
                                if (File.Exists(localisationxmlfile))
                                {
                                    XmlDocument xmlforkName = new XmlDocument();
                                    xmlforkName.Load(localisationxmlfile);
                                    XmlNodeList xnList = xmlforkName.GetElementsByTagName("LANG");
                                    bool step = false;
                                    for (int i = 0; i < xnList.Count; i++)
                                    {
                                        if (step == true)
                                        {
                                            namefromobjectxml = xnList[i].InnerXml;
                                            goto done;
                                        }
                                        if (xnList[i].InnerXml == "kName" || xnList[i].InnerXml == "NAME" || xnList[i].InnerXml == "Name")
                                        {
                                            step = true;
                                        }
                                    }
                                }
                            }

                        done:;

                            if (namefromobjectxml == "")
                            {
                                namefromobjectxml = "No name found";
                            }

                            string descfromobjectxml = "No description found";

                            XmlNodeList nodeListdesc = objecthdk.GetElementsByTagName("description");
                            foreach (XmlNode node in nodeListdesc)
                            {
                                descfromobjectxml = node.InnerText;
                                goto finished1;
                            }

                        finished1:;

                            if (descfromobjectxml == "kDesc" || descfromobjectxml == "DESC" || descfromobjectxml == "Description")
                            {
                                if (File.Exists(localisationxmlfile))
                                {
                                    XmlDocument xmlforkDesc = new XmlDocument();
                                    xmlforkDesc.Load(localisationxmlfile);
                                    XmlNodeList xnList = xmlforkDesc.GetElementsByTagName("LANG");
                                    bool step = false;
                                    for (int i = 0; i < xnList.Count; i++)
                                    {
                                        if (step == true)
                                        {
                                            descfromobjectxml = xnList[i].InnerXml;
                                            goto donealt;
                                        }
                                        if (xnList[i].InnerXml == "kDesc" || xnList[i].InnerXml == "DESC" || xnList[i].InnerXml == "Description")
                                        {
                                            step = true;
                                        }
                                    }
                                }
                            }

                        donealt:;

                            if (descfromobjectxml == "")
                            {
                                descfromobjectxml = "No description found";
                            }

                            string companyfromobjectxml = "No company found";

                            XmlNodeList nodeListcompany = objecthdk.GetElementsByTagName("company");
                            foreach (XmlNode node in nodeListcompany)
                            {
                                companyfromobjectxml = node.InnerText;
                            }

                            if (companyfromobjectxml == "")
                            {
                                companyfromobjectxml = "No company found";
                            }

                            string objecttype = "Unknown"; // Default Fallback

                            if (File.ReadAllText(objectxmlfile).Contains("</objectdef>"))
                            {
                                if (File.ReadAllText(objectxmlfile).Contains("</clothing>"))
                                {
                                    objecttype = "clothing";
                                }

                                if (File.ReadAllText(objectxmlfile).Contains("</furniture>"))
                                {
                                    objecttype = "furniture";
                                }

                                if ((File.ReadAllText(objectxmlfile).Contains("</mini_game>") && File.ReadAllText(objectxmlfile).Contains("</lua_environment>")) || (File.ReadAllText(objectxmlfile).Contains("</game_spawner>")))
                                {
                                    objecttype = "mini_game";
                                }

                                if (File.ReadAllText(objectxmlfile).Contains("</lua_environment>") && File.ReadAllText(objectxmlfile).Contains("</furniture>"))
                                {
                                    objecttype = "active";
                                }

                                if (File.ReadAllText(objectxmlfile).Contains("</game_spawner>") && File.ReadAllText(objectxmlfile).Contains("</furniture>"))
                                {
                                    objecttype = "arcade_machine";
                                }

                                if (File.ReadAllText(objectxmlfile).Contains("</lua_environment>") && File.ReadAllText(objectxmlfile).Contains("</arcade_game>"))
                                {
                                    objecttype = "arcade_game";
                                }

                                if (File.ReadAllText(objectxmlfile).Contains("</lua_environment>") && File.ReadAllText(objectxmlfile).Contains("</portable>"))
                                {
                                    objecttype = "portable";
                                }

                                if (File.ReadAllText(objectxmlfile).Contains("avatar_anim_pack</template>") && File.ReadAllText(objectxmlfile).Contains("lua.boot</script>"))
                                {
                                    objecttype = "animations";
                                }

                                if ((File.ReadAllText(objectxmlfile).Contains("locomotion_item</template>") || File.ReadAllText(objectxmlfile).Contains("locomotion_item_basic</template>")) && File.ReadAllText(objectxmlfile).Contains("lua.boot</script>"))
                                {
                                    objecttype = "LMOs";
                                }

                                if ((File.ReadAllText(objectxmlfile).Contains("companion_follower</template>") || File.ReadAllText(objectxmlfile).Contains("companion_follower_2</template>") || File.ReadAllText(objectxmlfile).Contains("portable_item</template>")) && (File.ReadAllText(objectxmlfile).Contains("lua.boot</script>") || File.ReadAllText(objectxmlfile).Contains("script.lua</script>")))
                                {
                                    objecttype = "companions";
                                }
                            }

                            string destodcname = Path.GetFileName(objectxmlfile);

                            destodcname = destodcname.Remove(destodcname.Length - 4);

                            string destpngfolder = foldertomap + @"\OBJECTS\" + objectprefix + @"\";

                            string cursortext = File.ReadAllText(approot + @"ODC\TEMPLATE.ODC");

                            string fileoutcataloguexml = foldertomap + @"\OBJECTS\" + objectprefix + @"\" + "CATALOGUEENTRY.XML";

                            cursortext = cursortext.Replace("OBJECT_HDK_VERSION", hdkversion);
                            cursortext = cursortext.Replace("PUT_UUID_HERE", objectprefix);
                            cursortext = cursortext.Replace("PUT_NAME_HERE", namefromobjectxml);
                            cursortext = cursortext.Replace("PUT_DESCRIPTION_HERE", descfromobjectxml);
                            cursortext = cursortext.Replace("PUT_COMPANY_HERE", companyfromobjectxml);

                            if (!File.Exists(foldertomap + @"\OBJECTS\" + objectprefix + @"\" + destodcname + ".ODC"))
                            {
                                File.WriteAllText(foldertomap + @"\OBJECTS\" + objectprefix + @"\" + destodcname + ".ODC", cursortext);
                            }

                            if (!File.Exists(foldertomap + @"\OBJECTS\" + objectprefix + @"\" + "EDITOR.OXML"))
                            {
                                File.WriteAllText(foldertomap + @"\OBJECTS\" + objectprefix + @"\" + "EDITOR.OXML", File.ReadAllText(approot + @"ODC\EDITOR.OXML"));
                            }

                            if (objecttype == "clothing")
                            {
                                string objectdescriptor = "Outfits";

                                string rigdescriptor = "00000000-00000000-00000010-00000000";

                                if (File.ReadAllText(objectxmlfile).Contains("Hair"))
                                {
                                    objectdescriptor = "Hair";
                                }

                                if (File.ReadAllText(objectxmlfile).Contains("Hat"))
                                {
                                    objectdescriptor = "Hat";
                                }

                                if (File.ReadAllText(objectxmlfile).Contains("JewelLeftEar"))
                                {
                                    objectdescriptor = "JewelLeftEar";
                                }

                                if (File.ReadAllText(objectxmlfile).Contains("JewelRightEar"))
                                {
                                    objectdescriptor = "JewelRightEar";
                                }

                                if (File.ReadAllText(objectxmlfile).Contains("HeadPhones"))
                                {
                                    objectdescriptor = "HeadPhones";
                                }

                                if (File.ReadAllText(objectxmlfile).Contains("Glasses"))
                                {
                                    objectdescriptor = "Glasses";
                                }

                                if (File.ReadAllText(objectxmlfile).Contains("FacialHair"))
                                {
                                    objectdescriptor = "FacialHair";
                                }

                                if (File.ReadAllText(objectxmlfile).Contains("Tors"))
                                {
                                    objectdescriptor = "Tors";
                                }

                                if (File.ReadAllText(objectxmlfile).Contains("Hands"))
                                {
                                    objectdescriptor = "Hands";
                                }

                                if (File.ReadAllText(objectxmlfile).Contains("Legs"))
                                {
                                    objectdescriptor = "Legs";
                                }

                                if (File.ReadAllText(objectxmlfile).Contains("Feet"))
                                {
                                    objectdescriptor = "Feet";
                                }

                                if (File.ReadAllText(objectxmlfile).Contains("Outfits"))
                                {
                                    objectdescriptor = "Outfits";
                                }

                                if (File.ReadAllText(ressourcesxmlfile).Contains("characters_v2/male"))
                                {
                                    rigdescriptor = "00000000-00000000-00000010-00000000";
                                }

                                if (File.ReadAllText(ressourcesxmlfile).Contains("characters_v2/female"))
                                {
                                    rigdescriptor = "00000000-00000000-00000010-00000001";
                                }

                                string cursortext2 = File.ReadAllText(approot + @"ODC\CATALOGUEENTRYTEMPLATEFORCLOTH.XML");
                                cursortext2 = cursortext2.Replace("PUT_UUID_HERE", objectprefix);
                                cursortext2 = cursortext2.Replace("PUT_RIGUUID_HERE", rigdescriptor);
                                cursortext2 = cursortext2.Replace("PUT_OBJECTTYPE_HERE", objecttype);
                                cursortext2 = cursortext2.Replace("PUT_OBJECTDESCRIPTOR_HERE", objectdescriptor);
                                cursortext2 = cursortext2.Replace("PUT_OBJECTNAME_HERE", namefromobjectxml);

                                if (!File.Exists(foldertomap + @"\OBJECTS\" + objectprefix + @"\" + "CATALOGUEENTRY.XML"))
                                {
                                    File.WriteAllText(fileoutcataloguexml, cursortext2);
                                }
                            }
                            else if (objecttype == "furniture")
                            {
                                string objectdescriptor = "Ornament";

                                if (File.ReadAllText(objectxmlfile).Contains("Table"))
                                {
                                    objectdescriptor = "Table";
                                }
                                if (File.ReadAllText(objectxmlfile).Contains("Chair"))
                                {
                                    objectdescriptor = "Chair";
                                }
                                if (File.ReadAllText(objectxmlfile).Contains("Sofa"))
                                {
                                    objectdescriptor = "Sofa";
                                }
                                if (File.ReadAllText(objectxmlfile).Contains("Ornament"))
                                {
                                    objectdescriptor = "Ornament";
                                }
                                if (File.ReadAllText(objectxmlfile).Contains("Light"))
                                {
                                    objectdescriptor = "Light";
                                }
                                if (File.ReadAllText(objectxmlfile).Contains("Frame"))
                                {
                                    objectdescriptor = "Frame";
                                }
                                if (File.ReadAllText(objectxmlfile).Contains("Flooring"))
                                {
                                    objectdescriptor = "Flooring";
                                }
                                if (File.ReadAllText(objectxmlfile).Contains("Cube"))
                                {
                                    objectdescriptor = "Cube";
                                }
                                if (File.ReadAllText(objectxmlfile).Contains("Appliance"))
                                {
                                    objectdescriptor = "Appliance";
                                }
                                if (File.ReadAllText(objectxmlfile).Contains("Storage"))
                                {
                                    objectdescriptor = "Storage";
                                }

                                string cursortext2 = File.ReadAllText(approot + @"ODC\CATALOGUEENTRYTEMPLATEFORFURNITURE.XML");
                                cursortext2 = cursortext2.Replace("PUT_UUID_HERE", objectprefix);
                                cursortext2 = cursortext2.Replace("PUT_OBJECTTYPE_HERE", objecttype);
                                cursortext2 = cursortext2.Replace("PUT_OBJECTDESCRIPTOR_HERE", objectdescriptor);
                                cursortext2 = cursortext2.Replace("PUT_OBJECTNAME_HERE", namefromobjectxml);

                                if (!File.Exists(foldertomap + @"\OBJECTS\" + objectprefix + @"\" + "CATALOGUEENTRY.XML"))
                                {
                                    File.WriteAllText(fileoutcataloguexml, cursortext2);
                                }
                            }
                            else
                            {
                                string cursortext2 = File.ReadAllText(approot + @"ODC\CATALOGUEENTRYTEMPLATE.XML");
                                cursortext2 = cursortext2.Replace("PUT_UUID_HERE", objectprefix);
                                cursortext2 = cursortext2.Replace("PUT_OBJECTTYPE_HERE", objecttype);
                                cursortext2 = cursortext2.Replace("PUT_OBJECTNAME_HERE", namefromobjectxml);

                                if (!File.Exists(foldertomap + @"\OBJECTS\" + objectprefix + @"\" + "CATALOGUEENTRY.XML"))
                                {
                                    File.WriteAllText(fileoutcataloguexml, cursortext2);
                                }
                            }

                            if (!Directory.Exists(destpngfolder))
                            {
                                Directory.CreateDirectory(destpngfolder);
                            }

                            if (!File.Exists(destpngfolder + @"MAKER.PNG"))
                            {
                                File.Copy(approot + @"SDC\DEFAULT_MAKER.PNG", destpngfolder + @"MAKER.PNG");
                            }

                            if (!File.Exists(destpngfolder + @"SMALL.PNG"))
                            {
                                File.Copy(approot + @"SDC\DEFAULT_SMALL.PNG", destpngfolder + @"SMALL.PNG");
                            }

                            if (!File.Exists(destpngfolder + @"LARGE.PNG"))
                            {
                                File.Copy(approot + @"ODC\PLACE_HOLDER.PNG", destpngfolder + @"LARGE.PNG");
                            }
                        }
                        catch (XmlException exception)
                        {
                            // No need to break, can happen if scene file isn't standard.
                        }
                        catch (Exception ex)
                        {
                            Form1.temptask += 1;
                            throw ex;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Form1.temptask += 1;
                    throw ex;
                }
            }

            if (objectprefix != "" && bruteforcemode)
            {
                try
                {
                    if (!File.Exists(foldertomap + @"\" + @"OBJECTS\" + objectprefix + @"\" + "GATE.GATE"))
                    {
                        File.Copy(approot + @"BRUTEFORCE-xml\GATE.GATE", foldertomap + @"\" + @"OBJECTS\" + objectprefix + @"\" + "GATE.GATE");
                    }

                    objectprefix = "";
                }
                catch (Exception ex)
                {
                    Form1.temptask += 1;
                    throw ex;
                }
            }

            if (objectprefix == "" && bruteforcemode)
            {
                try
                {
                    foreach (string currentresourcesxml in Directory.GetFiles(foldertomap + @"\", "RESOURCES.XML", SearchOption.AllDirectories))
                    {
                        string currentresourcesdirectory = Path.GetDirectoryName(currentresourcesxml);

                        if (!File.Exists(currentresourcesdirectory + @"\" + "GATE.GATE"))
                        {
                            string UUIDgotfromfolderresearch = currentresourcesdirectory.Substring(currentresourcesdirectory.Length - 35);

                            objectprefix = UUIDgotfromfolderresearch;

                            objectcomplementscan = true;

                            goto secondscan;

                        }
                    }
                }
                catch (Exception ex)
                {
                    Form1.temptask += 1;
                    throw ex;
                }
            }

            try
            {
                if (File.Exists(foldertomap + @"\" + "4E545585.dds"))
                {
                    File.Move(foldertomap + @"\" + "4E545585.dds", foldertomap + @"\" + "PLACEHOLDER_N.DDS");
                }

                if (File.Exists(foldertomap + @"\" + "4EE3523A.dds"))
                {
                    File.Move(foldertomap + @"\" + "4EE3523A.dds", foldertomap + @"\" + "PLACEHOLDER_S.DDS");
                }

                if (File.Exists(foldertomap + @"\" + "696E72D6.dds"))
                {
                    File.Move(foldertomap + @"\" + "696E72D6.dds", foldertomap + @"\" + "HATBUBBLE.DDS");
                }

                if (File.Exists(foldertomap + @"\" + "Object_UUID_Brute.xml"))
                {
                    File.Delete(foldertomap + @"\" + "Object_UUID_Brute.xml");
                }

                if (File.Exists(foldertomap + @"\" + "Object_UUID_Brute_alt.xml"))
                {
                    File.Delete(foldertomap + @"\" + "Object_UUID_Brute_alt.xml");
                }

                if (File.Exists(foldertomap + @"\" + "Object_UUID_Brute_alt_alt.xml"))
                {
                    File.Delete(foldertomap + @"\" + "Object_UUID_Brute_alt_alt.xml");
                }

                if (File.Exists(foldertomap + @"\" + "Object_UUID_Brute_Light.xml"))
                {
                    File.Delete(foldertomap + @"\" + "Object_UUID_Brute_Light.xml");
                }

                string[] filehelperfiles = Directory.GetFiles(foldertomap, "*.HELPML");
                foreach (string file in filehelperfiles)
                {
                    if (File.Exists(file))
                    {
                        File.Delete(file);
                    }
                }
            }
            catch (Exception ex)
            {
                Form1.temptask += 1;
                throw ex;
            }

            if (bruteforcemode)
            {
                foreach (string gatefile in Directory.GetFiles(@foldertomap, "*.GATE", SearchOption.AllDirectories))
                {
                    try
                    {
                        File.Delete(gatefile);
                    }
                    catch (Exception ex)
                    {
                        Form1.temptask += 1;
                        throw ex;
                    }
                }
            }

            if (batmode && !subfoldermode)
            {
                try
                {
                    string[] batfiles = Directory.GetFiles(foldertomap, "*.bat");
                    foreach (string file in batfiles)
                    {
                        var processhometoolsbat = new Process();
                        var startinfo = new ProcessStartInfo(file);
                        startinfo.UseShellExecute = false;
                        startinfo.CreateNoWindow = true;
                        startinfo.WorkingDirectory = foldertomap;
                        processhometoolsbat.StartInfo = startinfo;
                        processhometoolsbat.Start();
                        processhometoolsbat.WaitForExit();

                        if (File.Exists(file))
                        {
                            File.Delete(file);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Form1.temptask += 1;
                    throw ex;
                }
            }

            if (subfoldermode)
            {
                int fileCount = Directory.GetFiles(foldertomap).Length;

                if (fileCount > 0 && (File.Exists(foldertomap + @"\" + "PLACEHOLDER_N.DDS") || File.Exists(foldertomap + @"\\" + @"\placeholder_n.dds") || File.Exists(foldertomap + @"\" + "PLACEHOLDER_S.DDS") || File.Exists(foldertomap + @"\" + "ENV.DDS") || File.Exists(foldertomap + @"\" + "ENV_ROOM.DDS") || File.Exists(foldertomap + @"\" + "PARS02WINTER_ENVIRONMENTMAP.DDS") || File.Exists(foldertomap + @"\" + "HATBUBBLE.DDS") || File.Exists(foldertomap + @"\" + "ENVIRONMENTMAP.DDS")))
                {
                    string destprefix = @"\_MAPPED\";

                    foreach (string filetoscanforcorruption in Directory.GetFiles(foldertomap, "*.*", SearchOption.AllDirectories))
                    {
                        FileInfo fi1 = new FileInfo(filetoscanforcorruption);

                        uint dummy, sectorsPerCluster, bytesPerSector;

                        int result = GetDiskFreeSpaceW(fi1.Directory.Root.FullName, out sectorsPerCluster, out bytesPerSector, out dummy, out dummy);
                        if (result == 0) throw new Win32Exception();
                        uint clusterSize = sectorsPerCluster * bytesPerSector;

                        uint hosize;
                        uint losize = GetCompressedFileSizeW(filetoscanforcorruption, out hosize);
                        long size;
                        size = (long)hosize << 32 | losize;

                        long FileLengthOnHarddisc = ((size + clusterSize - 1) / clusterSize) * clusterSize;

                        if ((FileLengthOnHarddisc == 0))
                        {
                            destprefix = @"\_CORRUPT\";
                            goto skipcorruptcheck;
                        }
                    }

                skipcorruptcheck:;

                    string newPathinroot = Path.GetFullPath(Path.Combine(foldertomap, @"..\"));
                    string temppath = newPathinroot + destprefix + resultforfoldername + @"\";

                    try
                    {
                        Microsoft.VisualBasic.FileIO.FileSystem.MoveDirectory(foldertomap, temppath, true);

                        if (Directory.Exists(foldertomap))
                        {
                            try
                            {
                                Microsoft.VisualBasic.FileIO.FileSystem.DeleteDirectory(foldertomap, Microsoft.VisualBasic.FileIO.DeleteDirectoryOption.DeleteAllContents);
                            }
                            catch (Exception ex)
                            {
                                Form1.temptask += 1;
                                throw ex;
                            }
                        }

                        if (batmode)
                        {
                            await Task.Run(() => batfactoryperpare(temppath));

                            try
                            {
                                string[] batfiles = Directory.GetFiles(temppath, "*.bat");
                                foreach (string file in batfiles)
                                {
                                    var processhometoolsbat = new Process();
                                    var startinfo = new ProcessStartInfo(file);
                                    startinfo.UseShellExecute = false;
                                    startinfo.CreateNoWindow = true;
                                    startinfo.WorkingDirectory = temppath;
                                    processhometoolsbat.StartInfo = startinfo;
                                    processhometoolsbat.Start();
                                    processhometoolsbat.WaitForExit();

                                    if (File.Exists(file))
                                    {
                                        File.Delete(file);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Form1.temptask += 1;
                                throw ex;
                            }
                        }

                        Form1.temptask += 1;

                        return;
                    }
                    catch (Exception ex)
                    {
                        Form1.temptask += 1;
                        throw ex;
                    }
                }
                else if (fileCount > 0)
                {
                    string destprefix = @"\_UNMAPPED\";

                    foreach (string filetoscanforcorruption in Directory.GetFiles(foldertomap, "*.*", SearchOption.AllDirectories))
                    {
                        FileInfo fi1 = new FileInfo(filetoscanforcorruption);

                        uint dummy, sectorsPerCluster, bytesPerSector;

                        int result = GetDiskFreeSpaceW(fi1.Directory.Root.FullName, out sectorsPerCluster, out bytesPerSector, out dummy, out dummy);
                        if (result == 0) throw new Win32Exception();
                        uint clusterSize = sectorsPerCluster * bytesPerSector;

                        uint hosize;
                        uint losize = GetCompressedFileSizeW(filetoscanforcorruption, out hosize);
                        long size;
                        size = (long)hosize << 32 | losize;

                        long FileLengthOnHarddisc = ((size + clusterSize - 1) / clusterSize) * clusterSize;

                        if ((FileLengthOnHarddisc == 0))
                        {
                            destprefix = @"\_CORRUPT\";
                            goto skipcorruptcheck;
                        }
                    }
                skipcorruptcheck:;

                    string newPathinroot = Path.GetFullPath(Path.Combine(foldertomap, @"..\"));
                    string temppath = newPathinroot + destprefix + resultforfoldername + @"\";

                    try
                    {
                        Microsoft.VisualBasic.FileIO.FileSystem.MoveDirectory(foldertomap, temppath, true);

                        if (Directory.Exists(foldertomap))
                        {
                            try
                            {
                                Microsoft.VisualBasic.FileIO.FileSystem.DeleteDirectory(foldertomap, Microsoft.VisualBasic.FileIO.DeleteDirectoryOption.DeleteAllContents);
                            }
                            catch (Exception ex)
                            {
                                Form1.temptask += 1;
                                throw ex;
                            }
                        }

                        if (batmode)
                        {
                            await Task.Run(() => batfactoryperpare(temppath));

                            try
                            {
                                string[] batfiles = Directory.GetFiles(temppath, "*.bat");
                                foreach (string file in batfiles)
                                {
                                    var processhometoolsbat = new Process();
                                    var startinfo = new ProcessStartInfo(file);
                                    startinfo.UseShellExecute = false;
                                    startinfo.CreateNoWindow = true;
                                    startinfo.WorkingDirectory = temppath;
                                    processhometoolsbat.StartInfo = startinfo;
                                    processhometoolsbat.Start();
                                    processhometoolsbat.WaitForExit();

                                    if (File.Exists(file))
                                    {
                                        File.Delete(file);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Form1.temptask += 1;
                                throw ex;
                            }
                        }

                        Form1.temptask += 1;

                        return;
                    }
                    catch (Exception ex)
                    {
                        Form1.temptask += 1;
                        throw ex;
                    }
                }
                else
                {
                    string destprefix = @"\_MAPPED\";

                    foreach (string filetoscanforcorruption in Directory.GetFiles(foldertomap, "*.*", SearchOption.AllDirectories))
                    {
                        FileInfo fi1 = new FileInfo(filetoscanforcorruption);

                        uint dummy, sectorsPerCluster, bytesPerSector;

                        int result = GetDiskFreeSpaceW(fi1.Directory.Root.FullName, out sectorsPerCluster, out bytesPerSector, out dummy, out dummy);
                        if (result == 0) throw new Win32Exception();
                        uint clusterSize = sectorsPerCluster * bytesPerSector;

                        uint hosize;
                        uint losize = GetCompressedFileSizeW(filetoscanforcorruption, out hosize);
                        long size;
                        size = (long)hosize << 32 | losize;

                        long FileLengthOnHarddisc = ((size + clusterSize - 1) / clusterSize) * clusterSize;

                        if ((FileLengthOnHarddisc == 0))
                        {
                            destprefix = @"\_CORRUPT\";
                            goto skipcorruptcheck;
                        }
                    }
                skipcorruptcheck:;

                    string newPathinroot = Path.GetFullPath(Path.Combine(foldertomap, @"..\"));
                    string temppath = newPathinroot + destprefix + resultforfoldername + @"\";

                    try
                    {
                        Microsoft.VisualBasic.FileIO.FileSystem.MoveDirectory(foldertomap, temppath, true);

                        if (Directory.Exists(foldertomap))
                        {
                            try
                            {
                                Microsoft.VisualBasic.FileIO.FileSystem.DeleteDirectory(foldertomap, Microsoft.VisualBasic.FileIO.DeleteDirectoryOption.DeleteAllContents);
                            }
                            catch (Exception ex)
                            {
                                Form1.temptask += 1;
                                throw ex;
                            }
                        }

                        if (batmode)
                        {
                            await Task.Run(() => batfactoryperpare(temppath));

                            try
                            {
                                string[] batfiles = Directory.GetFiles(temppath, "*.bat");
                                foreach (string file in batfiles)
                                {
                                    var processhometoolsbat = new Process();
                                    var startinfo = new ProcessStartInfo(file);
                                    startinfo.UseShellExecute = false;
                                    startinfo.CreateNoWindow = true;
                                    startinfo.WorkingDirectory = temppath;
                                    processhometoolsbat.StartInfo = startinfo;
                                    processhometoolsbat.Start();
                                    processhometoolsbat.WaitForExit();

                                    if (File.Exists(file))
                                    {
                                        File.Delete(file);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Form1.temptask += 1;
                                throw ex;
                            }
                        }

                        Form1.temptask += 1;

                        return;
                    }
                    catch (Exception ex)
                    {
                        Form1.temptask += 1;
                        throw ex;
                    }
                }
            }

            Form1.temptask += 1;

            return;
        }

        [DllImport("kernel32.dll")]
        static extern uint GetCompressedFileSizeW([In, MarshalAs(UnmanagedType.LPWStr)] string lpFileName,
           [Out, MarshalAs(UnmanagedType.U4)] out uint lpFileSizeHigh);

        [DllImport("kernel32.dll", SetLastError = true, PreserveSig = true)]
        static extern int GetDiskFreeSpaceW([In, MarshalAs(UnmanagedType.LPWStr)] string lpRootPathName,
           out uint lpSectorsPerCluster, out uint lpBytesPerSector, out uint lpNumberOfFreeClusters,
           out uint lpTotalNumberOfClusters);
    }
}
