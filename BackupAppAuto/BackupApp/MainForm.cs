//The purpose of this program is to backup files to a certain folder
//Made by Daniel Zheng
//Created on 7/12/15

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
using Microsoft.Office.Interop.Word;

namespace Backup_App
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {//Things that need to happen before the window appears.
            this.Left = -10;
            this.Top = 0;
            this.Text = "Backup Application";
            resetSize();
            toolTip1.SetToolTip(backupButton, "Backs up files from one folder to another");
            toolTip2.SetToolTip(multiBackButton, "Backs up files from multiple folders");
            toolTip3.SetToolTip(cancelButton, "Starts over");
            toolTip4.SetToolTip(goButton, "Launches the backup sequence");
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        { //Change the textbox's size relative to the window
            resetSize();
        }

        //Variables
        //single folder backup variables
        string[] changedFiles = new string[0];//Files that were changed between source/ backup folders
        string[] sameFolders = new string[0];//Folders existing in both folders
        string[] newFiles = new string[0];//Files in source folder but not in backup folder
        string[] newFolders = new string[0];//Folders in source folder but not in backup folder
        string[] fileS = new string[0];//List of files in the source folder
        string[] folderS = new string[0];//List of folders in the source folder
        string[] fileB = new string[0];//List of files in the backup folder
        string[] folderB = new string[0];//List of folders in the backup folder
        string initialFolder;// The path of the folder copied from in the single backup event
        string finalFolder;//The backup folder
        bool newOne = true;//Variable checking for the file/folder is new to the backup folder 
        //multiple folder backup variables
        string[] folderPaths;//File paths from the text file
        string[] changedFiles2 = new string[0];//Files that were changed between the source/ backup folders
        string[] sameFolders2 = new string[0];//Folders existing in both folders
        string[] newFiles2 = new string[0];//Files in source folder but not in backup folder
        string[] newFolders2 = new string[0];//Folders in source folder but not in backup folder
        string[] fileS2 = new string[0];//List of files in the source folder
        string[] folderS2 = new string[0];//List of folders in the source folder
        string[] fileB2 = new string[0];//List of files in the backup folder
        string[] folderB2 = new string[0];//List of folders in the backup folder
        string initialFolder2; //The path of one of the folders in the text file
        string finalFolder2; //The backup folder
        bool newOne2 = true;//Variable checking for the file/folder is new to the backup folder
        //both
        bool multiple = false; //Checks if the multiple or single button was pushed
        string textFilePath;//File path of text file???

        //Button code
        private void backupButton_Click_1(object sender, EventArgs e)
        { //The option for backing up from a folder, diagnostic stuff.
            multiple = false;
            flowLayoutPanel1.Visible = true;
            flowLayoutPanel3.Visible = true;
            goButton.Enabled = true; 
            cancelButton.Enabled = true;
            textBox2.Text = "";
            label2.Text = "Backup From";
            flowLayoutPanel1.BackgroundImageLayout = ImageLayout.Zoom;//Shows one icon
        }

        private void multiBackButton_Click(object sender, EventArgs e)
        { //The option for backing up from multiple folders, diagnostic stuff.
            flowLayoutPanel3.Visible = true;
            multiple = true;
            flowLayoutPanel1.Visible = true;
            flowLayoutPanel3.Visible = true;
            goButton.Enabled = true;
            cancelButton.Enabled = true;
            flowLayoutPanel1.BackgroundImageLayout = ImageLayout.Tile;//Image shows multiple icons
            textBox2.Text = "";
            label2.Text = "Folder Path List";
            resetSize();
        }

        private void button1_Click(object sender, EventArgs e)
        {//Choose the folder to backup to
            if (folderBrowserDialog2.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog2.SelectedPath;
            }
        }

        private void button2_Click(object sender, EventArgs e) //(Backup from)/(File list)
        {//Set the second text box (top one) to the file/folder dialog's path
            if (multiple)
            {//If this is the multiple folder option
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    textFilePath = openFileDialog1.FileName;
                    textBox2.Text = textFilePath;
                    MessageBox.Show("Seperate file paths with line breaks.");
                    Process.Start(textFilePath);
                }
            }
            else
            {//If this is the single folder option
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    textBox2.Text = folderBrowserDialog1.SelectedPath;
                }
            }
            
        }

        private void goButton_Click_1(object sender, EventArgs e)
        {//A bunch of code for the actual backing up
            finalFolder = textBox1.Text;
            finalFolder2 = textBox1.Text;
            textFilePath = textBox2.Text;
            if (textBox1.Text != "" && textBox2.Text != "")
            {//If both text boxes are filled
                if (Directory.Exists(textBox1.Text) //The backup to folder indicated exists
                    && (File.Exists(textBox2.Text) || Directory.Exists(textBox2.Text)))
                {//Either the File name or Folder name exists
                    if (multiple)
                    {//If this is the multiple folder backup option
                        string allText = File.ReadAllText(textFilePath);
                        //NOTE: The following four lines are relatively unpolished
                        //because I don't seriously expect anyone to do this with Word
                        string textPath = textFilePath.Substring(textFilePath.LastIndexOf("."));
                        if (textPath == ".doc" || textPath == ".docx")
                        {//In case it's a word document
                            allText = wordDocText(textFilePath);
                        }
                        int numFolders = 0;
                        string text1 = allText.Trim();
                        for (int n = 0; n < allText.Length; n++)
                        {//Find out the number of file paths they want
                            //by searching for the number of new lines in the text file
                            if (allText[n].ToString() == "\n" && text1 != "")
                            {
                                numFolders++;
                            }
                            text1 = text1.Substring(text1.IndexOf("\n"));
                        }
                        numFolders++;//There's always going to be one less than the acual number
                        string partOfText = allText;
                        folderPaths = new string[numFolders];
                        for (int i = 0; i < numFolders; i++)
                        {//Split the text in the text file to parts of the array
                            string text = partOfText.Trim();
                            text = text.Replace("\n", "");
                            if (text != "")
                            {
                                if (i != numFolders - 1)
                                {
                                    //Set the next empty space equal to the next file path
                                    folderPaths[i] = partOfText.Substring(0, partOfText.IndexOf("\n") - 1)
                                        .Trim();
                                    //Shorten the string by one file path 
                                    partOfText = partOfText.Substring(allText.IndexOf("\n") + 1);
                                }
                                else
                                {//If it's the last one, just set the index equal to what's left.
                                    folderPaths[i] = partOfText;
                                }
                            }
                        }
                        for (int n = 0; n < folderPaths.Length; n++)
                        {//Remove white space in what exists and delete indexes without text
                            folderPaths[n] = folderPaths[n].Trim();
                            if (folderPaths[n].Trim() == null || folderPaths[n].Trim() == "")
                            {
                                folderPaths = Delete(folderPaths, n);
                            }
                        }
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
                                fileB2 = Directory.GetFiles(textBox1.Text//Set the files/folders to things
                                    + initialFolder2.Substring(initialFolder2.LastIndexOf(@"\")));
                                folderB2 = Directory.GetDirectories(textBox1.Text
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
                                                MessageBox.Show("hi1");
                                                File.Delete(fileB2[n]);
                                                File.Copy(fileS2[k], fileB2[n]);
                                            }
                                        }
                                    }
                                    if (newOne2)
                                    {//Insert new files into the backup folder
                                        createLogFile(fileS2[k], "Copied");
                                        MessageBox.Show("hi2");
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
                            else if(!Directory.Exists(folderPaths[i]))
                            {//If the folder path doesn't exist, it errors
                                MessageBox.Show('"' + folderPaths[i] + '"' + " does not exist..");
                                AutoClosingMessageBox.Show('"' + folderPaths[i] + '"' 
                                    + " does not exist..", "", 2000);
                            }
                        }
                        MessageBox.Show("Finished!");//Let the user know the program's done.
                        flowLayoutPanel1.Visible = false;//Finishing touches to make it seem blank
                        flowLayoutPanel3.Visible = false;
                        goButton.Enabled = false;
                        cancelButton.Enabled = false;
                    }
                    //single backup
                    else
                    {
                        fileS = Directory.GetFiles(textBox2.Text);//Assigning stuff
                        folderS = Directory.GetDirectories(textBox2.Text);
                        initialFolder = textBox2.Text;
                        string finalPath = textBox1.Text
                            + initialFolder.Substring(initialFolder.LastIndexOf(@"\"));
                        Directory.CreateDirectory(finalPath);
                        fileB = Directory.GetFiles(finalPath); // f/f of folder you're backing up to
                        folderB = Directory.GetDirectories(finalPath);
                        fileS = Directory.GetFiles(initialFolder);// f/f of folder you're backing up from
                        folderS = Directory.GetDirectories(initialFolder);
                        for (int i = 0; i < fileS.Length; i++)
                        {
                            newOne = true;
                            pathLabel.Text = fileS[i];
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
                                        File.Delete(fileB[n]);
                                        File.Copy(fileS[i], fileB[n]);
                                    }
                                }
                            }
                            if (newOne) //Insert new files into the backup folder
                            {
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
                            pathLabel.Text = folderS[i];
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
                        MessageBox.Show("Finished!");
                        flowLayoutPanel1.Visible = false; //Make it blank afterwards
                        flowLayoutPanel3.Visible = false;
                        goButton.Enabled = false;
                        cancelButton.Enabled = false;
                    }
                }
                else
                {//Error messages for if at least one of the text boxes isn't valid
                    if (!Directory.Exists(textBox1.Text) && !Directory.Exists(textBox2.Text) 
                        &&!File.Exists(textBox2.Text))
                    {//If neither are valid
                        MessageBox.Show("Please insert valid paths");
                    }
                    else if (!Directory.Exists(textBox1.Text))
                    {//If the "backup to" path isn't valid
                        MessageBox.Show("Please fill in a valid folder to back up to.");
                    }
                    else if (!Directory.Exists(textBox2.Text) && !multiple)
                    {//If the path of the folder to backup from isn't valid (single)
                        MessageBox.Show("Please fill in a valid folder to back up from.");
                    }
                    else if (!File.Exists(textBox2.Text) && multiple)
                    {//If the path of the file with the list isn't valid (multiple)
                        MessageBox.Show("Please fill in a valid file of backup folders.");
                    }
                    else
                    {//Generic
                        MessageBox.Show("Text boxes are invalid. Please try again.");
                    }
                }
            }
            else
            {//If one of the text boxes aren't filled at all
                MessageBox.Show("Please fill in both text boxes.");
            }
            pathLabel.Text = "";
            textBox2.Text = "";
            multiple = false;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        { //Cancel the backing up and clear (al)most everything
            flowLayoutPanel3.Visible = false;
            flowLayoutPanel1.Visible = false;
            goButton.Enabled = false;
            cancelButton.Enabled = false;
            pathLabel.Text = "";
            textBox2.Text = ""; //Clear the 2nd textbox (backup from/file list) only
            //Only the 2nd because people are more likely to want to keep the backup to loc.
        }

        //Methods for the button code
        string[] tempFiles = new string[0];
        public string[] FolderToFile(string[] directories, string[] fileS)
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

        private void DirectoryCopy(string sourceDirName/*Initial*/, string destDirName/*End*/)
        { //Copies files and folders directly from their initial folder to the ending folder
            DirectoryInfo dirI = new DirectoryInfo(sourceDirName);//Info on source folder
            DirectoryInfo[] dirsI = dirI.GetDirectories();//Directories in the source folder
            DirectoryInfo dirE = new DirectoryInfo(sourceDirName);//Info on destination folder
            DirectoryInfo[] dirsE = dirE.GetDirectories();//Directories in the destination folder
            FileInfo[] files = dirI.GetFiles();//Files in the source folder
            Directory.CreateDirectory(destDirName);
            foreach (FileInfo file in files)
            {//Files get copied from the initial to the end folder while the text box is updated
                pathLabel.Text = Path.Combine(sourceDirName, file.Name);
                System.Threading.Thread.Sleep(100);
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
                MessageBox.Show("hi3");
                createLogFile(file.FullName, "Copied");
            }
            foreach (DirectoryInfo subdir in dirsE)
            {//Copies folders to end folder
                string temppath = Path.Combine(destDirName, subdir.Name);
                DirectoryCopy(subdir.FullName, temppath); //Recursively checks sub folders
            }
        }

        private void DirectoryReplace(string sourceDirName/*Initial*/, string destDirName/*End*/)
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
                pathLabel.Text = Path.Combine(sourceDirName, fIle.Name);
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
                            MessageBox.Show("hi4");
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

        public string wordDocText(string filePath) //Gets text of a Microsoft Word doc
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

        public void createLogFile(string filePath, string status)
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
            MessageBox.Show("1" + dateNow.ToString() + ", " + date);
            string text = "";
            Directory.CreateDirectory(dPath);
            string fPath = dPath + @"\" + date + "_Log.txt";
            if (!File.Exists(fPath))
            {
                File.Create(fPath).Close();   
            }
            if (status == "Copied")
            { //If it was a new file
                text = status + "     " + filePath;
            }
            else if (status == "Replaced")
            {//If it was a file in both folders
                text = status + "   " + filePath;
            }
            string temp = text;
            string textInFile = File.ReadAllText(fPath);
            if (textInFile.Trim().Length == 0)
            {
                text = temp + System.Environment.NewLine;
            }
            else
            {
                text = File.ReadAllText(fPath) + temp;
            }
            File.WriteAllText(fPath, text);
        }

        public void resetSize()
        {//Resets the size of the text boxes/panels whenever the window changes
            textBox1.Width = this.Width - button1.Width - label1.Width - 50;
            textBox1.Height = 26;
            textBox2.Width = this.Width - button2.Width - label2.Width - 50;
            textBox2.Height = 26;
            flowLayoutPanel4.Width = flowLayoutPanel3.Width;
            flowLayoutPanel5.Width = flowLayoutPanel3.Width;
            flowLayoutPanel4.Height = flowLayoutPanel3.Height / 2;
            flowLayoutPanel5.Height = flowLayoutPanel3.Height / 2;

        }

        public string[] Add(string[] array, string newValue)
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

        public string[] Delete(string[] array, int index)
        { //Deletes the selected index of the array
            int newLength = array.Length - 1;
            string[] result = new string[newLength];
            for (int i = 0; i < array.Length - 1; i++)
            {
                if (index < 0 || index >= array.Length)
                { //If it's out of bounds, show an error message
                    MessageBox.Show("ERROR");
                }
                else if (index == 0)
                { //If the first index is selected.
                    result[i] = array[i + 1];//Set index to next in "array"
                }
                else if (index == array.Length - 1)
                {//If the last one's ommitted, continue as usual
                    result[i] = array[i];
                }
                else if (0 < index || index < array.Length - 1)
                {//If an in-between index is selected
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

        //Methods needed to run the program. Nothing's inside
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void logFiles(string[] items, string[] status)
        {
            //opens a text file and inserts the file paths to show what happened at what time.

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
public class AutoClosingMessageBox
{
    System.Threading.Timer _timeoutTimer;
    string _caption;
    AutoClosingMessageBox(string text, string caption, int timeout)
    {
        _caption = caption;
        _timeoutTimer = new System.Threading.Timer(OnTimerElapsed,
            null, timeout, System.Threading.Timeout.Infinite);
        MessageBox.Show(text, caption);
    }
    public static void Show(string text, string caption, int timeout)
    {
        new AutoClosingMessageBox(text, caption, timeout);
    }
    void OnTimerElapsed(object state)
    {
        IntPtr mbWnd = FindWindow("#32770", _caption); // lpClassName is #32770 for MessageBox
        if (mbWnd != IntPtr.Zero)
            SendMessage(mbWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
        _timeoutTimer.Dispose();
    }
    const int WM_CLOSE = 0x0010;
    [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
    static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
    [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
    static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
}
