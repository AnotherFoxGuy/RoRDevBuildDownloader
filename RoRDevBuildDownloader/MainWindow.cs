using System;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace RoRDevBuildDownloader
{
	public partial class MainForm : Form
	{
		WebClient client;

		string latestVersion = null;
		string installedVersion;
		string dlURL;
		string fileName;
		string tempFolder;

		public MainForm()
		{
			InitializeComponent();

			installedVersion = Properties.Settings.Default.InstalledVersion;
			fileName	= $"{Path.GetTempPath()}\\RoRDevBuild.zip";
			tempFolder	= $"{Path.GetTempPath()}\\RoR-dev-build";

			LOG("Getting newest version...");
			XmlDocument doc = new XmlDocument();
			doc.Load("https://sourceforge.net/p/rigs-of-rods/activity/feed.rss");

			//Display all the book titles.
			var elemList = doc.GetElementsByTagName("item");
			var pat = @"RoR.*CIBuild-\d*\.zip";
			var reg = new Regex(pat, RegexOptions.IgnoreCase);
			for (int i = 0; i < elemList.Count; i++)
			{
				var x = elemList[i]["title"];
				//LOG(x.InnerText); 
				Match m = reg.Match(x.InnerText);
				if(m.Success)
				{
					dlURL = elemList[i]["link"].InnerText;
					latestVersion = m.Value;
					LOG($"Version found: {latestVersion}");
					LOG($"Will update from {installedVersion} to {latestVersion}");
					break;
				}
			}

			if(latestVersion != null)
			{
				UpdateButton.Enabled = true;
				UpdateButton.Text = "Repair";
				if (latestVersion == installedVersion)
					MessageBox.Show($"Already up to date");
			}
		}

		private void UpdateButton_Click(object sender, EventArgs e)
		{
			LOG($"Downloading: {dlURL}");

			client = new WebClient();
			client.DownloadFileAsync(new Uri(dlURL), fileName);
			client.DownloadProgressChanged += UpdateDlProgress;
			client.DownloadFileCompleted += ExtractZip;
		}

		private void UpdateDlProgress(object sender, DownloadProgressChangedEventArgs e)
		{
			progressBar.Value = e.ProgressPercentage;
		}

		private void ExtractZip(object sender, AsyncCompletedEventArgs e)
		{
			LOG($"Extracting zip: {fileName} to {Application.StartupPath}");
			ZipFile.ExtractToDirectory(fileName, tempFolder);
			CopyDirectory(tempFolder, Application.StartupPath, true);
			Properties.Settings.Default.InstalledVersion = latestVersion;
			Properties.Settings.Default.Save();
			Directory.Delete(tempFolder, true);
			LOG("Done!");
			MessageBox.Show($"Done!\nUpdated from {installedVersion} to {latestVersion}");
		}

		private void LOG(string txt)
		{
			LogWindow.AppendText($"{txt}\n");
		}

		private static void CopyDirectory(string sourceDirName, string destDirName, bool copySubDirs)
		{
			// Get the subdirectories for the specified directory.
			DirectoryInfo dir = new DirectoryInfo(sourceDirName);

			if (!dir.Exists)
			{
				throw new DirectoryNotFoundException(
					"Source directory does not exist or could not be found: "
					+ sourceDirName);
			}

			DirectoryInfo[] dirs = dir.GetDirectories();
			// If the destination directory doesn't exist, create it.
			if (!Directory.Exists(destDirName))
			{
				Directory.CreateDirectory(destDirName);
			}

			// Get the files in the directory and copy them to the new location.
			FileInfo[] files = dir.GetFiles();
			foreach (FileInfo file in files)
			{
				string temppath = Path.Combine(destDirName, file.Name);
				file.CopyTo(temppath, true);
			}

			// If copying subdirectories, copy them and their contents to new location.
			if (copySubDirs)
			{
				foreach (DirectoryInfo subdir in dirs)
				{
					string temppath = Path.Combine(destDirName, subdir.Name);
					CopyDirectory(subdir.FullName, temppath, copySubDirs);
				}
			}
		}
	}
}
