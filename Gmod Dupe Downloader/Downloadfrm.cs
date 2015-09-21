using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gmod_Dupe_Downloader
{
    public partial class Downloadfrm : Form
    {
        public Downloadfrm()
        {
            InitializeComponent();
            new Thread(new ThreadStart(Download7Zip)).Start();
        }

        private void Download7Zip()
        {
            try
            {
                //Make the directory
                if (Directory.Exists(Application.StartupPath + "\\7zip")) { Directory.Delete(Application.StartupPath + "\\7zip", true); }
                Directory.CreateDirectory(Application.StartupPath + "\\7zip");

                //Download 7-Zip
                MainForm.client.DownloadFile("http://www.7-zip.org/a/7z1507.exe" /*Why this version? Because, that's why!*/, Application.StartupPath + "\\7zip\\installer.exe");

                //Change label text
                Invoke(new Action(() => { label.Text = "Extracting..."; }));

                //Extract 7-Zip
                ProcessStartInfo pro = new ProcessStartInfo();
                pro.WindowStyle = ProcessWindowStyle.Hidden;
                pro.FileName = Application.StartupPath + "\\7zip\\installer.exe";
                pro.Arguments = "/S /D=\"" + Application.StartupPath + "\\7zip\"";
                Process x = Process.Start(pro);
                x.WaitForExit();

                //Lastly, CLEAN UP YOUR MESS and close
                if (File.Exists(Application.StartupPath + "\\7zip\\installer.exe")) { File.Delete(Application.StartupPath + "\\7zip\\installer.exe"); }
                Invoke(new Action(() => { Close(); }));
            }
            catch (Exception ex){ Invoke(new Action(() => { MessageBox.Show(this,"7-Zip could not be downloaded. Please download 7-Zip from \"http://www.7-zip.org\" and extract it to \"" + Application.StartupPath + "\\7zip\". " + ex.Message, "GMod Dupe Downloader", MessageBoxButtons.OK, MessageBoxIcon.Error); Close(); })); }
        }
    }
}