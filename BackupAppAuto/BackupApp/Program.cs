using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;
using System.Xml;
using System.Threading;
using Microsoft.Office.Interop.Word;

namespace BackupApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        #region Variables
        //single folder backup variables
        static string[] changedFiles = new string[0];//Files that were changed between source/ backup folders
        static string[] sameFolders = new string[0];//Folders existing in both folders
        static string[] newFiles = new string[0];//Files in source folder but not in backup folder
        static string[] newFolders = new string[0];//Folders in source folder but not in backup folder
        static string[] fileS = new string[0];//List of files in the source folder
        static string[] folderS = new string[0];//List of folders in the source folder
        static string[] fileB = new string[0];//List of files in the backup folder
        static string[] folderB = new string[0];//List of folders in the backup folder
        static string initialFolder;// The path of the folder copied from in the single backup event
        static string finalFolder;//The backup folder
        static bool newOne = true;//Variable checking for the file/folder is new to the backup folder 
        //multiple folder backup variables
        static string[] folderPaths = new string[0];//File paths from the text file
        static string[] changedFiles2 = new string[0];//Files that were changed between the source/ backup folders
        static string[] sameFolders2 = new string[0];//Folders existing in both folders
        static string[] newFiles2 = new string[0];//Files in source folder but not in backup folder
        static string[] newFolders2 = new string[0];//Folders in source folder but not in backup folder
        static string[] fileS2 = new string[0];//List of files in the source folder
        static string[] folderS2 = new string[0];//List of folders in the source folder
        static string[] fileB2 = new string[0];//List of files in the backup folder
        static string[] folderB2 = new string[0];//List of folders in the backup folder
        static string[] folderList = new string[0];
        static string initialFolder2; //The path of one of the folders in the text file
        static string finalFolder2; //The backup folder
        static string extention = "";
        static string extLong = "";
        static bool txtFile = true;
        static bool indivSel = false;
        static bool newOne2 = true;//Variable checking for the file/folder is new to the backup folder
        static int numFolders = 0;
        //both
        static int fileN = 0;
        static bool multiple = false; //Checks if the multiple or single button was pushed
        static bool singleChecked = false;
        //bool multiChecked = false;
        static bool currentLog = false;
        static string textFilePath;//File path of text file??? 
        static string[] tempFiles = new string[0];
        static string endStatus = "";
        //static string logFilePath = "";
        #endregion

        #region Auto Variables
        static string XMLfilePath = "";
        static string str3 = "";
        //static bool daily = false;

        static int folderNum = 0;
        static string fListType = "text";
        static string originPath = "";
        static string destinationPath = "";
        //static string frequency = "";
        //static string date = "";
        //static string time = "";
        //static int sec = 0;

        #endregion

        [STAThread]
        
        static void Main(string[] args)
        {
            string temp = "";
            int c = 0;
            foreach (string arg in args)
            {
                temp += arg + "\n";
                c++;
            }
            //MessageBox.Show(temp + "\n" + c);
            if (args.Length == 2)
            {
                originPath = args[0];
                destinationPath = args[1];
                backupFiles(args[0], args[1]);
            }
            else
            {
                MessageBox.Show("Invalid number of arguments");
            }
            #region other
            //MessageBox.Show("hi");
            //string startPath = System.Windows.Forms.Application.StartupPath;
            /*string path = startPath.Substring(0, startPath.LastIndexOf(@"\")) + "README";
            if (!File.Exists(path))
            {
                string text = "ezBackup";
                File.WriteAllText(path, text);
            }*/

            //XMLfilePath = startPath + @"\ezBackupAuto.xml";
            ////MessageBox.Show(XMLfilePath);
            //if (File.Exists(XMLfilePath))
            //{
            //    //XmlDataDocument xmldoc = new XmlDataDocument();
            //    XmlDocument xmldoc = new XmlDocument();
            //    XmlNodeList xmlnode;
            //    string[] str = new string[6];
            //    //MessageBox.Show(XMLfilePath);
            //    //FileStream fs = new FileStream("ezBackupAuto.xml", FileMode.Open, FileAccess.Read);
            //    xmldoc.Load(XMLfilePath);
            //    //xmldoc.Load(fs);
            //    xmlnode = xmldoc.GetElementsByTagName("Information");
            //    xmlnode[0].ChildNodes.Item(0).InnerText.Trim();
            //    str[0] = xmlnode[0].ChildNodes.Item(0).InnerText.Trim();
            //    str[1] = xmlnode[1].ChildNodes.Item(0).InnerText.Trim();
            //    str[2] = xmlnode[2].ChildNodes.Item(0).InnerText.Trim();
            //    str[3] = xmlnode[3].ChildNodes.Item(0).InnerText.Trim();
            //    str[4] = xmlnode[4].ChildNodes.Item(0).InnerText.Trim();
            //    str[5] = xmlnode[5].ChildNodes.Item(0).InnerText.Trim();
            //    int fNum = 0;
            //    Int32.TryParse(str[0], out fNum);
            //    bool itemsNotEmpty = (str[0] != "" && str[1] != ""
            //        && str[2] != "" && str[3] != "" && str[4] != "" && str[5] != "");
            //    if (itemsNotEmpty)
            //    {
            //        folderNum = fNum;
            //        originPath = str[1];
            //        destinationPath = str[2];
            //        str3 = str[3].ToLower();
            //        if (str3 == "weekly" || str3 == "daily" || str3 == "hourly"
            //            || str3 == "monthly" || str3 == "biweekly")
            //        {
            //            frequency = str[3];
            //        }
            //        else
            //        {
            //            MessageBox.Show("Invalid frequency");
            //        }

            //        MessageBox.Show("STOP");
            //        time = str[5];
            //        if (str3 == "weekly" || str3 == "monthly" || str3 == "biweekly")
            //        {
            //            date = str[4];
            //        }
            //        if (str3 == "hourly")
            //        {
            //            int minute = 0;
            //            Int32.TryParse(str[5], out minute);

            //            sec = 60;
            //        }
            //        else if (str3 == "daily")
            //        {
            //            int hour = 0;
            //            int minute = 0;
            //            Int32.TryParse(str[5].Substring(0, str[5].IndexOf(":")), out hour);
            //            Int32.TryParse(str[5].Substring(str[5].LastIndexOf(":") + 1), out minute);

            //            sec = 24 * 60 * 60;
            //        }
            //        else if (str3 == "weekly")
            //        {
            //            string date = str[4];
            //            int hour = 0;
            //            int minute = 0;
            //            Int32.TryParse(str[5].Substring(0, str[5].IndexOf(":")), out hour);
            //            Int32.TryParse(str[5].Substring(str[5].LastIndexOf(":") + 1), out minute);

            //            sec = 7 * 24 * 3600;
            //        }
            //        else if (str3 == "biweekly")
            //        {
            //            int hour = 0;
            //            int minute = 0;
            //            Int32.TryParse(str[5].Substring(0, str[5].IndexOf(":")), out hour);
            //            Int32.TryParse(str[5].Substring(str[5].LastIndexOf(":") + 1), out minute);

            //            sec = 2 * 7 * 24 * 60 * 60;
            //        }
            //        else if (str3 == "monthly")
            //        {
            //            int hour = 0;
            //            int minute = 0;
            //            Int32.TryParse(str[5].Substring(0, str[5].IndexOf(":")), out hour);
            //            Int32.TryParse(str[5].Substring(str[5].LastIndexOf(":") + 1), out minute);

            //            sec = 24 * 60 * 60;
            //        }
            //    }

            //}
            //else
            //{
            //    XmlTextWriter writer = new XmlTextWriter(XMLfilePath,
            //        System.Text.Encoding.UTF8);
            //    writer.WriteStartDocument(true);
            //    writer.Formatting = Formatting.Indented;
            //    writer.Indentation = 2;
            //    //writer.WriteStartElement("Table");
            //    createNode(writer);
            //    //writer.WriteEndElement();
            //    writer.WriteEndDocument();
            //    writer.Close();
            //    File.Open(XMLfilePath, FileMode.OpenOrCreate);
            //}
#endregion
        }

        /*private static void run()
        {
            backupFiles();
            if (str3 == "monthly")
            {
                int[] oddMon = {1, 3, 5, 7, 8, 10, 12};
                int[] evenMon = {4, 6, 9, 11};
                if(oddMon.All(x => x == DateTime.Now.Month) )
                {
                    sec *= 31;
                }
                else if(evenMon.All(x => x == DateTime.Now.Month) )
                {
                    sec *= 30;
                }
                else if (DateTime.Now.Month == 2)
                {
                    sec *= 28;
                }
            }
            Thread.Sleep(sec * 1000);
            run();
        }*/

        public static void backupFiles(string origin, string destination)
        {
            //MessageBox.Show("hi0");
            finalFolder = destination;
            finalFolder2 = destination;
            textFilePath = origin;
            if (1==1)
            {//If both text boxes are filled
                if (Directory.Exists(destinationPath)  && File.Exists(originPath))
                {
                    if (1==1)
                    {//If this is the multiple folder backup option
                        if (1==1)
                        {
                            //MessageBox.Show("hi");
                            string allText = "";
                            if (extention == ".txt")
                            {//Text file
                                allText = File.ReadAllText(textFilePath);
                            }
                            else
                            {//Word File
                                allText = wordDocText(textFilePath);
                                ////MessageBox.Show(allText);
                            }
                            int numFolders = 0;
                            string text1 = allText.Trim();
                            string text2 = allText.Trim();
                            for (int n = 0; n < allText.Length; n++)
                            {//Find out the number of file paths they want
                                //by searching for the number of new lines in the text file
                                if (allText[n].ToString() == "\n" && text1 != "")
                                {
                                    numFolders++;
                                }
                                text1 = text1.Substring(text1.IndexOf("\n"));
                                //text2 = text2.Substring(text2.IndexOf("\v"));
                            }
                            numFolders++;//There's always going to be one less than the acual number
                            string partOfText = allText;
                            folderPaths = new string[numFolders];
                            string text = partOfText.Trim();
                            char[] arr = { '\n', '\r' };
                            folderPaths = text.Split(arr);
                            for (int a = 0; a < folderPaths.Length; a++)
                            {
                                ////MessageBox.Show(folderPaths[a]);
                            }
                            for (int n = 0; n < folderPaths.Length; n++)
                            {//Remove white space in what exists and delete indexes without text
                                if (folderPaths[n] == null || folderPaths[n].Length <= 0
                                    || folderPaths[n].Trim() == null || folderPaths[n].Trim() == "")
                                {
                                    folderPaths = Delete(folderPaths, n);
                                    n--;
                                }
                            }
                            for (int m = 0; m < folderPaths.Length; m++)
                            {
                                ////MessageBox.Show("b|" + folderPaths[m] + "|");
                            }
                        }
                        /*else
                        {
                            if (Directory.Exists(originPath))
                            {
                                folderPaths = Add(folderPaths, originPath);
                                folderList = folderPaths;
                            }
                        }*/
                        for (int i = 0; i < folderPaths.Length; i++)
                        {//loop through the outer folders
                            if (Directory.Exists(folderPaths[i]))
                            {//If the folder path actually exists
                                folderPaths[i].Trim();
                                initialFolder2 = folderPaths[i].Replace("\r", string.Empty)
                                    .Replace("\n", string.Empty);
                                string finalPath2 =
                                    finalFolder2 + initialFolder2.Substring(initialFolder2.LastIndexOf(@"\"));
                                changedFiles2 = new string[0];
                                sameFolders2 = new string[0];
                                newFiles2 = new string[0];
                                newFolders2 = new string[0];//^Set stuff and initalize stuff
                                Directory.CreateDirectory(finalPath2);
                                fileB2 = Directory.GetFiles(destinationPath//Set the files/folders to things
                                    + initialFolder2.Substring(initialFolder2.LastIndexOf(@"\")));
                                folderB2 = Directory.GetDirectories(destinationPath
                                    + initialFolder2.Substring(initialFolder2.LastIndexOf(@"\")));
                                fileS2 = Directory.GetFiles(folderPaths[i].Trim());
                                folderS2 = Directory.GetDirectories(folderPaths[i].Trim());
                                for (int k = 0; k < fileS2.Length; k++)
                                {
                                    newOne2 = true;
                                    for (int n = 0; n < fileB2.Length; n++)
                                    {
                                        string partOfTextS2 = fileS2[k].Substring(0, fileS2[k].LastIndexOf(@"\"));
                                        string partOfTextB2 = fileB2[n].Substring(0, fileB2[n].LastIndexOf(@"\"));
                                        bool foldersSame = partOfTextS2.Substring(partOfTextS2.LastIndexOf(@"\"))
                                            == partOfTextB2.Substring(partOfTextB2.LastIndexOf(@"\"));
                                        //If the folder names are the same
                                        if (foldersSame && fileS2[k].Substring(fileS2[k].LastIndexOf(@"\"))
                                            == fileB2[n].Substring(fileB2[n].LastIndexOf(@"\")))
                                        {//If that and the file names are the same, too
                                            newOne2 = false;
                                            if (File.GetLastWriteTime(fileS2[k]) > File.GetLastWriteTime(fileB2[n]))
                                            {//If the source folder's file if newer, 
                                                //Put changed files into the backup folder
                                                //START
                                                createLogFile(fileS2[k], "Replaced");
                                                File.Delete(fileB2[n]);
                                                File.Copy(fileS2[k], fileB2[n]);
                                            }
                                        }
                                    }
                                    if (newOne2)
                                    {//Insert new files into the backup folder
                                        createLogFile(fileS2[k], "Copied");
                                        File.Copy(fileS2[k],
                                            finalPath2 + fileS2[k].Substring(fileS2[k].LastIndexOf(@"\")));
                                    }
                                }
                                newOne2 = true;//reset
                                int sameFromBack = 0;
                                int same = 0;
                                string[] subDirPathN = new string[0];
                                string[] subDirPathO = new string[0];
                                for (int m = 0; m < folderS2.Length; m++)
                                {
                                    newOne2 = true;
                                    same = 0;
                                    for (int n = 0; n < folderB2.Length; n++)
                                    {//Go through the subfolders to check files/folders
                                        string firstPartS = folderS2[m];
                                        string firstPartB = folderB2[n];
                                        string compPartS = "";
                                        string compPartB = "";
                                        sameFromBack = 0;
                                        int temp = 0;
                                        while (sameFromBack == 0)
                                        {//Find out when the two paths diverge (backwards)
                                            compPartS = firstPartS.Substring(firstPartS.LastIndexOf(@"\"));
                                            compPartB = firstPartB.Substring(firstPartB.LastIndexOf(@"\"));
                                            if (compPartS != compPartB)
                                            {//If the last parts aren't the same, break the loop
                                                if (temp != 0)
                                                {
                                                    sameFromBack = temp;
                                                }
                                                else
                                                {
                                                    sameFromBack = 1;
                                                }
                                            }
                                            else
                                            {//If they are, add to the temp variable
                                                temp += firstPartB.Length - firstPartB.LastIndexOf(@"\");
                                            }
                                            firstPartS = firstPartS.Substring(0, firstPartS.LastIndexOf(@"\"));
                                            firstPartB = firstPartB.Substring(0, firstPartB.LastIndexOf(@"\"));
                                        }
                                        if ((folderS2[m].Substring(folderS2[m].Length - sameFromBack)
                                            == folderB2[n].Substring(folderB2[n].Length - sameFromBack))
                                            && sameFromBack != 1)
                                        {//Determines if a folder is new to the backup folder or not
                                            same = sameFromBack;
                                            newOne2 = false;
                                        }
                                    }
                                    //Add folders to their respective arrays
                                    if (newOne2)
                                    { //If the folder's new
                                        int index =
                                            folderS2[m].IndexOf(initialFolder2.Substring(initialFolder2.LastIndexOf(@"\")))
                                            + initialFolder2.Substring(initialFolder2.LastIndexOf(@"\")).Length;
                                        subDirPathN = Add(subDirPathN, folderS2[m].Substring(index));//Find the folder path
                                        newFolders2 = Add(newFolders2, folderS2[m]); //and add it to the array
                                    }
                                    else
                                    { //If it isn't
                                        subDirPathO = Add(subDirPathO,
                                                folderS2[m].Substring(folderS2[m].Length - same));
                                        sameFolders2 = Add(sameFolders2, folderS2[m]);//Find and add
                                    }
                                }
                                for (int n = 0; n < newFolders2.Length; n++)
                                {//Inserts new folders (with their files into the backup folder)
                                    DirectoryCopy(newFolders2[n], finalPath2 + subDirPathN[n]);
                                }
                                for (int a = 0; a < sameFolders2.Length; a++)
                                {//Replaces files in not-new folders that are newer in the source folder
                                    DirectoryReplace(sameFolders2[a], finalFolder2 + subDirPathO[a]);
                                }
                            }
                            else if (!Directory.Exists(folderPaths[i]))
                            {//If the folder path doesn't exist, it errors
                                //MessageBox.Show('"' + folderPaths[i] + '"' + " does not exist.");
                                //AutoClosing//MessageBox.Show('"' + folderPaths[i] + '"' 
                                //    + " does not exist..", "", 2000);
                            }
                        }
                        //panel3.Visible = false;//Finishing touches to make it seem blank
                        folderList = new string[0];
                        folderPaths = new string[0];
                        //numFolders = 0;
                    }
                    //single backup
                    #region single
                    /*else
                    {
                        fileS = Directory.GetFiles(originPath);//Assigning stuff
                        folderS = Directory.GetDirectories(originPath);
                        initialFolder = originPath;
                        string finalPath = destinationPath
                            + initialFolder.Substring(initialFolder.LastIndexOf(@"\"));
                        Directory.CreateDirectory(finalPath);
                        fileB = Directory.GetFiles(finalPath); // f/f of folder you're backing up to
                        folderB = Directory.GetDirectories(finalPath);
                        fileS = Directory.GetFiles(initialFolder);// f/f of folder you're backing up from
                        folderS = Directory.GetDirectories(initialFolder);
                        for (int i = 0; i < fileS.Length; i++)
                        {
                            newOne = true;
                            //pathLabel.Text = fileS[i];
                            System.Threading.Thread.Sleep(100);
                            for (int n = 0; n < fileB.Length; n++)
                            {//Check if the file's already there and replaces it if it isn't
                                string partOfTextS = fileS[i].Substring(0, fileS[i].LastIndexOf(@"\"));
                                string partOfTextB = fileB[n].Substring(0, fileB[n].LastIndexOf(@"\"));
                                bool foldersSame = partOfTextS.Substring(partOfTextS.LastIndexOf(@"\"))
                                    == partOfTextB.Substring(partOfTextB.LastIndexOf(@"\"));
                                if (foldersSame && fileS[i].Substring(fileS[i].LastIndexOf(@"\"))
                                    == fileB[n].Substring(fileB[n].LastIndexOf(@"\")))
                                {//If the name's the same
                                    newOne = false;
                                    if (File.GetLastWriteTime(fileS[i]) > File.GetLastWriteTime(fileB[n]))
                                    {//If the file in the source folder's newer
                                        //Put changed files into the backup folder
                                        createLogFile(fileS[i], "Replaced");
                                        File.Delete(fileB[n]);
                                        File.Copy(fileS[i], fileB[n]);
                                    }
                                }
                            }
                            if (newOne) //Insert new files into the backup folder
                            {
                                createLogFile(fileS[i], "Copied");
                                File.Copy(fileS[i], finalPath + fileS[i].Substring(fileS[i].LastIndexOf(@"\")));
                            }
                        }
                        int sameFromBack = 0;
                        int same = 0;
                        string[] subDirPathN = new string[0];
                        string[] subDirPathO = new string[0];
                        for (int i = 0; i < folderS.Length; i++)
                        {
                            newOne = true;
                            same = 0;
                            //pathLabel.Text = folderS[i];
                            System.Threading.Thread.Sleep(100);
                            for (int n = 0; n < folderB.Length; n++)
                            {//Check sub folders
                                string firstPartS = folderS[i];
                                string firstPartB = folderB[n];
                                string compPartS = "";
                                string compPartB = "";
                                sameFromBack = 0;
                                int temp = 0;
                                while (sameFromBack == 0)
                                {//While it's the same, loop
                                    compPartS = firstPartS.Substring(firstPartS.LastIndexOf(@"\"));
                                    compPartB = firstPartB.Substring(firstPartB.LastIndexOf(@"\"));
                                    //If they aren't the same, add to the variable, 
                                    //if they are then break the loop
                                    if (compPartS != compPartB)
                                    {
                                        if (temp != 0)
                                        {
                                            sameFromBack = temp;
                                        }
                                        else
                                        {
                                            sameFromBack = 1;
                                        }
                                    }
                                    else
                                    {
                                        temp += firstPartB.Length - firstPartB.LastIndexOf(@"\");
                                    }
                                    firstPartS = firstPartS.Substring(0, firstPartS.LastIndexOf(@"\"));
                                    firstPartB = firstPartB.Substring(0, firstPartB.LastIndexOf(@"\"));
                                }
                                if ((folderS[i].Substring(folderS[i].Length - sameFromBack)
                                    == folderB[n].Substring(folderB[n].Length - sameFromBack))
                                    && sameFromBack != 1)
                                {//Determines if a folder is new to the backup folder or not
                                    newOne = false;
                                    same = sameFromBack;
                                }
                            }
                            //Add folders to their respective arrays
                            if (newOne)
                            { //If it's new then find the path and add to the array
                                int index =
                                        folderS[i].IndexOf(initialFolder.Substring(initialFolder.LastIndexOf(@"\")))
                                        + initialFolder.Substring(initialFolder.LastIndexOf(@"\")).Length;
                                subDirPathN = Add(subDirPathN, folderS[i].Substring(index));
                                newFolders = Add(newFolders, folderS[i]);
                            }
                            else
                            { //If it's not then find the path and add to the other array
                                subDirPathO = Add(subDirPathO,
                                            folderS[i].Substring(folderS[i].Length - same));
                                sameFolders = Add(sameFolders, folderS[i]);
                            }
                        }
                        for (int n = 0; n < newFolders.Length; n++)
                        {//Inserts new folders (with their files) into the backup folder
                            DirectoryCopy(newFolders[n], finalPath + subDirPathN[n]);
                        }
                        for (int i = 0; i < sameFolders.Length; i++)
                        {//Replaces files in already existing folders that have the source's version as newer
                            DirectoryReplace(sameFolders[i], finalFolder + subDirPathO[i]);
                        }
                        //panel3.Visible = false; //Make it blank afterwards
                    }*/
                    
                    #endregion
                    if (endStatus.Length != 0)
                    {
                        //MessageBox.Show(endStatus);
                    }
                    else
                    {
                        createLogFile("", "None");
                        //MessageBox.Show("No backup-worthy files were found");
                    }
                    //MessageBox.Show("Finished!");
                    originPath = "";
                    multiple = false;
                    /*#region Buttons
                    backupButton.Enabled = false;
                    oriButton.Enabled = false;
                    backupButton.Enabled = false;
                    singleRadio.Checked = false;
                    multiRadio.Checked = false;
                    indivRadio.Checked = false;
                    textRadio.Checked = false;
                    #endregion
                    textBox2.Enabled = false;
                    multiOpPanel.Visible = false;*/
                    resetVars();
                }
                else
                {//Error messages for if at least one of the text boxes isn't valid
                    if (!Directory.Exists(destinationPath) && !Directory.Exists(originPath)
                        && !File.Exists(originPath))
                    {//If neither are valid
                        //MessageBox.Show("Please insert valid paths");
                        createLogFile("", "|Invalid folders");
                    }
                    else if (!Directory.Exists(destinationPath))
                    {//If the "backup to" path isn't valid
                        //MessageBox.Show("Please fill in a valid Destination file path.");
                        createLogFile("", "|Invalid destination folder");
                    }
                    else if (!Directory.Exists(originPath) || !File.Exists(originPath))
                    {//If the path of the folder to backup from isn't valid
                        //MessageBox.Show("Please fill in a valid Origin file path.");
                        createLogFile("", "|Invalid origin folder");
                    }
                    else
                    {//Generic
                        //MessageBox.Show("Text boxes are invalid. Please try again.");
                        createLogFile("", "|Error");
                    }
                }
            }
            /*else
            {//If one of the text boxes aren't filled at all
                //MessageBox.Show("Please fill in both text boxes.");
            }*/
            //pathLabel.Text = "";
            Cursor.Current = Cursors.Default;
            //panel5.Visible = false;
        }

        #region misc functions
        public static string[] FolderToFile(string[] directories, string[] fileS)
        {//Puts all files in a folder array into an array of file (subfolders included)
            tempFiles = fileS;
            for (int i = 0; i < directories.Length; i++)
            {
                string[] folderFiles = Directory.GetFiles(directories[i]);
                for (int n = 0; n < folderFiles.Length; n++)
                { //Add all the files already in the folder
                    tempFiles = Add(tempFiles, folderFiles[n]);
                } //Loop through the subdirectory to add its files to the array
                FolderToFile(Directory.GetDirectories(directories[i]), tempFiles);
            }
            tempFiles = new string[0];
            return tempFiles;
        }

        public static void DirectoryCopy(string sourceDirName/*Initial*/, string destDirName/*End*/)
        { //Copies files and folders directly from their initial folder to the ending folder
            DirectoryInfo dirI = new DirectoryInfo(sourceDirName);//Info on source folder
            DirectoryInfo[] dirsI = dirI.GetDirectories();//Directories in the source folder
            DirectoryInfo dirE = new DirectoryInfo(sourceDirName);//Info on destination folder
            DirectoryInfo[] dirsE = dirE.GetDirectories();//Directories in the destination folder
            FileInfo[] files = dirI.GetFiles();//Files in the source folder
            Directory.CreateDirectory(destDirName);
            foreach (FileInfo file in files)
            {//Files get copied from the initial to the end folder while the text box is updated
                //pathLabel.Text = Path.Combine(sourceDirName, file.Name);
                System.Threading.Thread.Sleep(100);
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
                ////MessageBox.Show("hi3");
                createLogFile(file.FullName, "Copied");
            }
            foreach (DirectoryInfo subdir in dirsE)
            {//Copies folders to end folder
                string temppath = Path.Combine(destDirName, subdir.Name);
                DirectoryCopy(subdir.FullName, temppath); //Recursively checks sub folders
            }
        }

        public static void DirectoryReplace(string sourceDirName/*Initial*/, string destDirName/*End*/)
        { //In a folder present in both directories, 
            //this replaces older files and inserts new files and folders into the end folder
            DirectoryInfo dirI = new DirectoryInfo(sourceDirName);//Info on the source dir
            DirectoryInfo[] dirsI = dirI.GetDirectories();//Info on the dirs in the source dir
            FileInfo[] filesI = dirI.GetFiles();//Info on files in source dir
            DirectoryInfo dirE = new DirectoryInfo(destDirName);//Info on the dest. dir
            DirectoryInfo[] dirsE = dirE.GetDirectories();//Info on the dirs in the dest. dir
            FileInfo[] filesE = dirE.GetFiles();//Info on files in dest. dir
            bool newOne = true; //If a file is new or not
            string pathOfFile; //File path
            string fileName; //File's title
            foreach (FileInfo fIle in filesI) //Replaces old files in the ending folder 
            {//Files in the source folder
                //pathLabel.Text = Path.Combine(sourceDirName, fIle.Name);
                System.Threading.Thread.Sleep(100);
                string temppath;
                foreach (FileInfo filE1 in filesE)
                {//Files in the dest. folder
                    if (fIle.Name == filE1.Name)
                    {//If they have the same file name
                        newOne = false;
                        if (fIle.LastWriteTime > filE1.LastWriteTime)
                        {//If the source's file was more recent, replace the dest. version
                            temppath = Path.Combine(destDirName, fIle.Name);
                            ////MessageBox.Show("hi4");
                            createLogFile(fIle.FullName, "Replaced");
                            File.Delete(temppath);
                            fIle.CopyTo(temppath, true);//...
                        }
                    }
                }
                if (newOne)
                {//If the file's newer, copy the file directly
                    fileName = fIle.FullName.Substring(sourceDirName.LastIndexOf(@"\"));
                    pathOfFile = fIle.FullName.Substring(fIle.FullName.IndexOf(fileName));
                    File.Copy(fIle.FullName,
                        destDirName.Substring(0, destDirName.LastIndexOf(@"\")) + pathOfFile);
                }
            }
            foreach (DirectoryInfo subdir in dirsI) //Checks subdirectories
            {
                foreach (DirectoryInfo subdir1 in dirsE)
                {
                    if (subdir.Name == subdir1.Name)
                    {//If they have the same name
                        string temppath = Path.Combine(destDirName, subdir.Name);
                        DirectoryReplace(subdir.FullName, temppath); //Recurse with the subfolder
                    }
                }
            }
        }

        public static void resetVars()
        {
            //Single
            changedFiles = new string[0];
            sameFolders = new string[0];
            newFiles = new string[0];
            newFolders = new string[0];
            fileS = new string[0];
            folderS = new string[0];
            fileB = new string[0];
            folderB = new string[0];
            newOne = true;
            //multiple
            folderPaths = new string[0];
            changedFiles2 = new string[0];
            sameFolders2 = new string[0];
            newFiles2 = new string[0];
            newFolders2 = new string[0];
            fileS2 = new string[0];
            folderS2 = new string[0];
            fileB2 = new string[0];
            folderB2 = new string[0];
            folderList = new string[0];
            extention = "";
            extLong = "";
            txtFile = true;
            indivSel = false;
            newOne2 = true;
            numFolders = 0;
            //both
            fileN = 0;
            multiple = false;
            singleChecked = false;
            //multiChecked = false;
            currentLog = false;
            tempFiles = new string[0];
            endStatus = "";
            //logFilePath = "";
        }

        public static string wordDocText(string filePath) //Gets text of a Microsoft Word doc
        {
            string temp = "";
            string extension = filePath.Substring(filePath.LastIndexOf("."));
            object path = @filePath;
            object readOnly = true;
            object miss = System.Reflection.Missing.Value;
            Microsoft.Office.Interop.Word.Application word; //Open the Word Document for reading
            word = new Microsoft.Office.Interop.Word.Application();
            Microsoft.Office.Interop.Word.Document docs =
                word.Documents.Open(ref path, ref miss, ref readOnly, ref miss,
                ref miss, ref miss, ref miss, ref miss, ref miss, ref miss,
                ref miss, ref miss, ref miss, ref miss, ref miss, ref miss);
            for (int i = 0; i < docs.Paragraphs.Count; i++)
            {//Add all the paragraphs together into one string
                temp += "\r\n" + docs.Paragraphs[i + 1].Range.Text.ToString();
            }
            word.Quit();
            return temp;
        }

        public static void createLogFile(string filePath, string status)
        {//Creates a log file for every run.
            string folderPath = System.Windows.Forms.Application.StartupPath;
            string dPath = folderPath + @"\Logs";
            DateTime dateNow = DateTime.Now;
            string date = "";
            if (dateNow.Month.ToString().Length == 1)
            {
                if (dateNow.Day.ToString().Length == 1)
                {
                    date = dateNow.Year.ToString() + "0" + dateNow.Month.ToString()
                        + "0" + dateNow.Day.ToString();
                }
                else
                {
                    date = dateNow.Year.ToString() + "0" + dateNow.Month.ToString()
                        + dateNow.Day.ToString();
                }
            }
            else if (dateNow.Day.ToString().Length == 1)
            {
                date = dateNow.Year.ToString() + dateNow.Month.ToString()
                        + "0" + dateNow.Day.ToString();
            }
            else
            {
                date = dateNow.Year.ToString() + dateNow.Month.ToString()
                 + dateNow.Day.ToString();
            }
            ////MessageBox.Show("1" + dateNow.ToString() + ", " + date);
            string text = "";
            Directory.CreateDirectory(dPath);
            string fPath = dPath + @"\" + date/* + "_Log.txt"*/;
            int i = 0;
            //if(File.Exists(fPath + "_Log.txt")
            //while (File.Exists(fPath) && !currentLog)
            while ((File.Exists(fPath + "_Log_" + i.ToString() + ".txt")
                || (i == 0 && File.Exists(fPath + "_Log.txt"))))
            {
                i++;
            }
            if (!currentLog)
            {
                fileN = i;
                currentLog = true;
            }
            if (fileN != 0)
                fPath += "_Log_" + fileN.ToString() + ".txt";
            else
                fPath += "_Log.txt";
            if (!File.Exists(fPath))
            {
                File.Create(fPath).Close();
            }
            if (status == "Copied")
            { //If it was a new file
                text = status + "     " + filePath + "\n";
                endStatus += status + "     " + filePath + "\n";
            }
            else if (status == "Replaced")
            {//If it was a file in both folders
                text = status + "   " + filePath + "\n";
                endStatus += status + "   " + filePath + "\n";
            }
            if (status != "None")
            {
                string temp = text;
                string textInFile = File.ReadAllText(fPath);
                if (textInFile.Trim().Length == 0)
                {
                    text = temp + System.Environment.NewLine;
                }
                else
                {
                    text = File.ReadAllText(fPath) + temp + System.Environment.NewLine;
                }
                File.WriteAllText(fPath, text);
            }
            else
            {
                File.WriteAllText(fPath, "No backup-worthy files were found");
            }
            if (status.Substring(0, 1) == "|")
            {
                File.WriteAllText(fPath, status.Substring(1));
            }
        }

        public static string[] Add(string[] array, string newValue)
        { //Adds an extra value to an array
            int newLength = array.Length + 1;
            string[] result = new string[newLength];
            result[newLength - 1] = newValue; //The last value is the variable
            for (int i = 0; i < array.Length; i++)
            { //Match the other indexs of the array
                result[i] = array[i];
            }
            return result;
        }

        public static string[] Delete(string[] array, int index)
        { //Deletes the selected index of the array
            ////MessageBox.Show(array[index]);
            int newLength = array.Length - 1;
            string[] result = new string[newLength];

            for (int i = 0; i < array.Length; i++)
            {
                if (index < 0 || index >= array.Length)
                { //If it's out of bounds, show an error message
                    //MessageBox.Show("ERROR");
                }
                else if (index == 0)
                { //If the first index is selected.
                    if (i < array.Length - 1)
                        result[i] = array[i + 1];//Set index to next in "array"
                }
                else if (index == array.Length - 1)
                {//If the last one's ommitted, continue as usual
                    if (i < array.Length - 1)
                        result[i] = array[i];
                }
                else if (0 < index || index < array.Length - 1)
                {//If an in-between index is selected
                    ////MessageBox.Show(array[i].ToString());
                    if (i < index)
                    {//If the loop isn't to the index, continue as usual
                        result[i] = array[i];
                    }
                    else if (i > index)
                    {//If it's past it, then skip the selected index
                        result[i - 1] = array[i];
                    }
                }
            }
            return result;
        }

        public static void createNode(XmlTextWriter writer)
        {
            writer.WriteStartElement("Information");
            writer.WriteStartElement("Folder_number");
            writer.WriteString("\n\n");
            writer.WriteEndElement();
            /*writer.WriteStartElement("File_type");
            writer.WriteEndElement();*/
            writer.WriteStartElement("Origin_folder(s)");
            writer.WriteString("\n\n");
            writer.WriteEndElement();
            writer.WriteStartElement("Destination_folder");
            writer.WriteString("\n\n");
            writer.WriteEndElement();
            writer.WriteStartElement("Backup_frequency");
            writer.WriteString("\n\n");
            writer.WriteEndElement();
            writer.WriteStartElement("Backup_date");
            writer.WriteString("\n\n");
            writer.WriteEndElement();
            writer.WriteStartElement("Backup_time");
            writer.WriteString("\n\n");
            writer.WriteEndElement();
            writer.WriteEndElement();
        }
        #endregion
    }
}
