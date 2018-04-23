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

		public MainForm()
		{
			InitializeComponent();

			installedVersion = Properties.Settings.Default.InstalledVersion;
			fileName = $"{Path.GetTempPath()}RoRDevBuild.zip";

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
					LOG($"Version found: {m.Value}");
					dlURL = elemList[i]["link"].InnerText;
					latestVersion = m.Value;
					break;
				}
			}

			if(latestVersion != null)
			{
				UpdateButton.Enabled = true;
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
			ZipFile.ExtractToDirectory(fileName, Application.StartupPath);
			Properties.Settings.Default.InstalledVersion = latestVersion;
			LOG("Done!");
			MessageBox.Show($"Done!\nUpdated from {installedVersion} to {latestVersion}");
		}

		private void LOG(string txt)
		{
			LogWindow.AppendText($"{txt}\n");
		}
	}
}
