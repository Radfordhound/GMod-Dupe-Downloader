using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace Gmod_Dupe_Downloader
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Used for accessing "teh interwebs."
        /// </summary>
        public static WebClient client;
        public static string startuppath;

        #region GUI events n' stuff
        public MainForm()
        {
            InitializeComponent();
            client = new WebClient();
            startuppath = Application.StartupPath;
        }

        /// <summary>
        /// The event that's executed when the form is loaded.
        /// </summary>
        private void MainForm_Load(object sender, EventArgs e)
        {
            if (File.Exists(startuppath+"\\config.txt")) { textBox1.Text = File.ReadAllLines(startuppath+ "\\config.txt")[0]; }
            if (!Directory.Exists(Application.StartupPath+"\\7zip") || !File.Exists(Application.StartupPath+"\\7zip\\7z.exe"))
            {
                if (MessageBox.Show("To extract dupes, GMod Dupe Downloader needs a file archiver called \"7-Zip.\" Would you like to download it?", "GMod Dupe Downloader", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    new Downloadfrm().ShowDialog();
                }
                else { MessageBox.Show("Alright. Just keep in mind that dupes will not successfully install themselves and will have to be manually extracted.","GMod Dupe Downloader",MessageBoxButtons.OK,MessageBoxIcon.Warning); }
            }
        }

        /// <summary>
        /// The event that's executed when the textbox is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtbx_Click(object sender, EventArgs e)
        {
            if (txtbx.ForeColor == Color.DimGray)
            {
                txtbx.Font = new Font("Segoe UI", 8, FontStyle.Regular);
                txtbx.ForeColor = Color.Red;
                txtbx.Text = "";
            }
        }

        /// <summary>
        /// The event that's executed when the text in the textbox is changed.
        /// </summary>
        private void txtbx_TextChanged(object sender, EventArgs e)
        {
            if ((txtbx.Text.Length == 9 && Stringisonlynumbers(txtbx.Text)) || (txtbx.Text.Contains("http") && txtbx.Text.Contains("id=") && txtbx.Text.Length >= 62 && Stringisonlynumbers(txtbx.Text.Substring((txtbx.Text.Contains("https")?55:54), 9)))) { txtbx.ForeColor = Color.Green; }
            else { txtbx.ForeColor = Color.Red; }
        }

        /// <summary>
        /// The event that's executed when the download button is clicked.
        /// </summary>
        private void downloadbtn_Click(object sender, EventArgs e)
        {
            if (txtbx.ForeColor == Color.Green || MessageBox.Show("The given URL or id seems to be invalid. Would you like to try and download it anyway?", "GMod Dupe Downloader", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Thread downloaddupe = new Thread(new ThreadStart(DownloadDupe));
                downloadbtn.Text = "Downloading...";
                downloaddupe.Start();
            }
        }
        #endregion

        /// <summary>
        /// The main event! What it does is pretty self-explanatory. :P
        /// </summary>
        private void DownloadDupe()
        {
            string dupetitle = "dupe", id = (txtbx.Text.Length == 9 && Stringisonlynumbers(txtbx.Text) ? txtbx.Text : txtbx.Text.Substring((txtbx.Text.Contains("https") ? 55 : 54), 9));

            //Download the JSON file
            if (File.Exists(Application.StartupPath + "\\temp.json")) { File.Delete(Application.StartupPath + "\\temp.json"); }
            try { client.DownloadFile("http://steamworkshopdownloader.com/api/workshop/" + id, Application.StartupPath + "\\temp.json"); }
            catch (Exception ex) { Invoke(new Action(() => { MessageBox.Show("The given dupe could not be downloaded. " + ex.Message, "GMod Dupe Downloader", MessageBoxButtons.OK, MessageBoxIcon.Error); })); }

            //If it downloaded successfully, read it's contents
            if (File.Exists(Application.StartupPath + "\\temp.json"))
            {
                string[] jsonfile = File.ReadAllLines(Application.StartupPath + "\\temp.json");
                if (jsonfile.Length >= 3 && jsonfile[2].IndexOf("file_url") != -1)
                {
                    if (jsonfile.Length >= 4 && jsonfile[3].IndexOf("preview_url") != -1)
                    {
                        //Use the URL in the JSON file's "preview_url" tag to download the dupe's thumbnail
                        if (File.Exists(Application.StartupPath + "\\temp.jpg")) { File.Delete(Application.StartupPath + "\\temp.jpg"); }
                        client.DownloadFile(jsonfile[3].Substring(jsonfile[3].IndexOf("preview_url") + 15, jsonfile[3].Length - (jsonfile[3].IndexOf("preview_url") + 17)), Application.StartupPath + "\\temp.jpg");
                    }
                    else { Invoke(new Action(() => { MessageBox.Show(this, "The downloaded JSON file doesn't contain a \"preview_url\" tag, and may be invalid or corrupt.", "GMod Dupe Downloader", MessageBoxButtons.OK, MessageBoxIcon.Error); })); }

                    //Use the URL in the JSON file's "file_url" tag to download the dupe
                    if (File.Exists(Application.StartupPath + "\\temp.dupe")) { File.Delete(Application.StartupPath + "\\temp.dupe"); }
                    client.DownloadFile(jsonfile[2].Substring(jsonfile[2].IndexOf("file_url") + 12, jsonfile[2].Length - (jsonfile[2].IndexOf("file_url") + 14)), Application.StartupPath + "\\temp.dupe");

                    //If it downloaded successfully, extract it
                    if (Directory.Exists(Application.StartupPath + "\\tempout")) { Directory.Delete(Application.StartupPath + "\\tempout", true); }
                    Directory.CreateDirectory(Application.StartupPath + "\\tempout");
                    if (File.Exists(Application.StartupPath + "\\temp.dupe")) { ExtractFile(Application.StartupPath + "\\temp.dupe", Application.StartupPath + "\\tempout"); }

                    //If it extracted successfully, rename it as well as the downloaded thumbnail and install the dupe to GMod
                    string gmoddupesfolder = (Directory.Exists(textBox1.Text)) ? textBox1.Text : (Directory.Exists(@"C:\Program Files (x86)\Steam\SteamApps\common\GarrysMod\garrysmod\dupes"))? @"C:\Program Files (x86)\Steam\SteamApps\common\GarrysMod\garrysmod\dupes" : (Directory.Exists(@"C:\Program Files\Steam\SteamApps\common\GarrysMod\garrysmod\dupes")) ? @"C:\Program Files\Steam\SteamApps\common\GarrysMod\garrysmod\dupes" : null;
                    if (jsonfile.Length >= 5 && jsonfile[4].IndexOf("title") != -1)
                    {
                        dupetitle = FileNameString(jsonfile[4].Substring(jsonfile[4].IndexOf("title") + 9, jsonfile[4].Length - (jsonfile[4].IndexOf("title") + 10)));

                        //This prevents errors if a dupe or thumbnail in the Garry's Mod dupe folder already has the same name
                        string originaldupetitle = dupetitle; int i = 2;
                        while (File.Exists(gmoddupesfolder + "\\" + dupetitle + ".dupe"))
                        {
                            dupetitle = originaldupetitle + i.ToString(); i++;
                        }
                    }

                    if (gmoddupesfolder != null)
                    {
                        if (Directory.Exists(Application.StartupPath + "\\tempout") && Directory.GetFiles(Application.StartupPath + "\\tempout").Length > 0) { File.Move(Directory.GetFiles(Application.StartupPath + "\\tempout")[0], gmoddupesfolder + "\\" + dupetitle + ".dupe"); }
                        if (File.Exists(Application.StartupPath + "\\temp.jpg")) { File.Move(Application.StartupPath + "\\temp.jpg", gmoddupesfolder + "\\" + dupetitle + ".jpg"); }

                        //Done! :D
                        Invoke(new Action(() => { MessageBox.Show(this, "Dupe \"" + dupetitle + "\" was successfully downloaded and installed!", "GMod Dupe Downloader", MessageBoxButtons.OK, MessageBoxIcon.Information); }));
                    }
                    else { Invoke(new Action(() => { MessageBox.Show(this, "Dupe was successfully downloaded, but GMod dupe folder could not be found.", "GMod Dupe Downloader", MessageBoxButtons.OK, MessageBoxIcon.Error); })); }

                }
                else { Invoke(new Action(() => { MessageBox.Show(this, "The downloaded JSON file doesn't contain a \"file_url\" tag, and may be invalid or corrupt.", "GMod Dupe Downloader", MessageBoxButtons.OK, MessageBoxIcon.Error); })); }
            }

            //Change the button's text back to "Download Dupe"
            downloadbtn.Invoke(new MethodInvoker(() => downloadbtn.Text = "&Download Dupe"));

            //Clean up after yourself, you horrid little pig!
            if (keepdeletedchkbx.Checked) //...Unless the user is a pig as well. ._.
            {
                if (File.Exists(Application.StartupPath + "\\temp.dupe")) { File.Delete(Application.StartupPath + "\\temp.dupe"); }
                if (File.Exists(Application.StartupPath + "\\temp.jpg")) { File.Delete(Application.StartupPath + "\\temp.jpg"); }
                if (File.Exists(Application.StartupPath + "\\temp.json")) { File.Delete(Application.StartupPath + "\\temp.json"); }
                if (Directory.Exists(Application.StartupPath + "\\tempout")) { Directory.Delete(Application.StartupPath + "\\tempout", true); }
            }
        }

        /// <summary>
        /// Returns a boolean indicating wether or not the given string contains only numbers.
        /// </summary>
        /// <param name="str">The string :P</param>
        /// <returns>Wether or not the given string contains only numbers.</returns>
        private static bool Stringisonlynumbers(string str)
        {
            bool containsonlynumbers = false;
            foreach (char ch in str)
            {
                if (ch != '0' && ch != '1' && ch != '2' && ch != '3' && ch != '4' && ch != '5' && ch != '6' && ch != '7' && ch != '8' && ch != '9') { return false; }
                else { containsonlynumbers = true; }
            }
            return containsonlynumbers;
        }

        /// <summary>
        /// Calls 7-Zip's executable externally to extract a given file's contents to the given destination.
        /// </summary>
        /// <param name="source">The path of the file to be extracted</param>
        /// <param name="destination">The destination to extract the file's contents</param>
        private void ExtractFile(string source, string destination)
        {
            try
            {
                ProcessStartInfo pro = new ProcessStartInfo();
                pro.WindowStyle = ProcessWindowStyle.Hidden;
                pro.FileName = Application.StartupPath+"\\7zip\\7z.exe";
                pro.Arguments = "e \"" + source + "\" -o\"" + destination+"\"";
                Process x = Process.Start(pro);
                x.WaitForExit();
            }
            catch (Exception ex) { Invoke(new Action(() => { MessageBox.Show("The downloaded dupe could not be extracted. " + ex.Message, "GMod Dupe Downloader", MessageBoxButtons.OK, MessageBoxIcon.Error); })); }
        }

        /// <summary>
        /// This function takes a string and removes weird characters present in it that can't be used in Windows file names.
        /// </summary>
        /// <param name="originalstring">The string :P</param>
        /// <returns>A Windows file-name-friendly version of the given string.</returns>
        private static string FileNameString(string originalstring)
        {
            string newstring = "";

            foreach (char ch in originalstring)
            {
                if (!Path.GetInvalidFileNameChars().Contains(ch)) { newstring += ch; }
                else { newstring += '-'; }
            }

            return newstring;
        }

        private void MainForm_Closing(object sender, FormClosingEventArgs e)
        {
            File.WriteAllText(startuppath+ "\\config.txt", (string.IsNullOrEmpty(textBox1.Text) || !Directory.Exists(textBox1.Text))? @"C:\Program Files (x86)\Steam\SteamApps\common\GarrysMod\garrysmod\dupes" : textBox1.Text);
        }
    }
}
