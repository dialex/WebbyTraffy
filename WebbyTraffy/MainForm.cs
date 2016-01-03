﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WebbyTraffy
{
    public partial class MainForm : System.Windows.Forms.Form
    {
        #region Data

        public static Browser[] BrowserChoices = { Browser.CHROME, Browser.SAFARI, Browser.FIREFOX, Browser.IEXPLORER };

        #endregion
        #region Configs

        private readonly string WHITESPACE_S = "  ";
        private readonly string WHITESPACE_M = "    ";
        private readonly string SEPARATOR_DASHES = "------------------------------------------------------";

        private List<Uri> UrlsToCall;
        
        #endregion

        public MainForm()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            Log("Initializing...");
            UrlsToCall = new List<Uri>();

            // Import default configs
            try
            {
                Log(WHITESPACE_S + "Attempting to load \"Urls.txt\" file.");
                if (File.Exists("Urls.txt"))
                    LoadUrls(File.Open("Urls.txt", FileMode.Open));

                //Log("Attempting to load \"Proxies.txt\" file.");
                //if (File.Exists("Urls.txt"))
                //    LoadUrls(File.Open("Urls.txt", FileMode.Open));
            }
            catch (Exception error)
            {
                ShowAndLogErrorMsg("Something went wrong while reading the default configurations.", error.Message);
            }

            // Set default configs
            chkConfigSimulateBrowser.Checked = true;
            chkConfigSimulateCountries.Checked = true;
        }

        private void btnActionBrowser_Click(object sender, EventArgs e)
        {
            picLoading.Visible = true;
            try
            {
                for (int i = 1; i <= 20; i++)
                {
                    SimulateBrowsing("http://adf.ly/10475475/gmod-textures");
                    Thread.Sleep(1000 * 15);
                    SimulateBrowsing("http://www.diogonunes.com/blog/too-weird-to-live-too-unique-to-die/");

                    lblTotalCalls.Text = lblTotalCalls.Tag.ToString() + i.ToString();
                }
            }
            catch (Exception error)
            {
                ShowAndLogErrorMsg("KABOOM!", error.Message);
            }
            finally { picLoading.Visible = false; }
        }

        #region Eventos

        private void comboRepeatConditionType_SelectedValueChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Buttons

        private void btnFileUrls_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = openFileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Stream file = openFileDialog.OpenFile();
                    LoadUrls(file);
                }
            }
            catch (Exception error)
            {
                ShowAndLogErrorMsg("Could not open the file.", error.Message);
            }
        }

        private void lblTotalUrls_Click(object sender, EventArgs e)
        {
            DisplayUrlsToCall();
        }

        private void lblUrlsToCall_Click(object sender, EventArgs e)
        {
            DisplayUrlsToCall();
        }

        #endregion

        #region UI

        private void RefreshTotalUrls()
        {
            lblUrlsToCall.Text = lblUrlsToCall.Tag.ToString() + UrlsToCall.Count;
        }

        private void DisplayUrlsToCall()
        {
            StringBuilder str = new StringBuilder();
            foreach (Uri url in UrlsToCall)
            {
                str.AppendLine(url.ToString());
            }

            if (str.Length > 0)
            {
                ShowInfoMsg("Check the output console below.");
                Log(SEPARATOR_DASHES);
                Log(str.ToString(), false);
                Log(SEPARATOR_DASHES);
            }
            else ShowInfoMsg("You should load a file containing the URLs to be called by this app.");
        }

        #endregion

        #region Mockup methods

        void SimulateBrowsing(string url)
        {
            Browser browserHeader = GetRandomBrowser();
            MockCountry();
            OpenUrl(url, browserHeader);
            MockReadingTime(10);
            WaitBeforeNext();
        }

        private void WaitBeforeNext()
        {
            //TODO
        }

        private void MockReadingTime(int v)
        {
            //TODO
        }

        private void MockCountry()
        {
            //TODO
        }

        private Browser GetRandomBrowser()
        {
            if (chkConfigSimulateBrowser.Checked)
            {
                int randIndex = new Random().Next(0, BrowserChoices.Length - 1);
                return BrowserChoices[randIndex];
            }
            else return null;
        }

        #endregion

        #region Helper methods

        // URLs
        
        private void LoadUrls(Stream file)
        {
            int numValidUrls = 0;
            bool hasInvalidUrls = false;
            string line;

            try
            {
                UrlsToCall.Clear();
                Log("Cleared URL calling list.");

                Log("Loading URLs...");
                using (StreamReader fileReader = new StreamReader(file))
                {
                    while ((line = fileReader.ReadLine()) != null)
                    {
                        Uri validatedUrl;
                        bool isValidUrl =
                            Uri.TryCreate(line, UriKind.Absolute, out validatedUrl) &&
                            (validatedUrl.Scheme == Uri.UriSchemeHttp || validatedUrl.Scheme == Uri.UriSchemeHttps);

                        if (isValidUrl)
                        {
                            UrlsToCall.Add(validatedUrl);
                            numValidUrls++;
                        }
                        else
                        {
                            hasInvalidUrls = true;
                            Log(WHITESPACE_S + "Not a valid URL: " + line);
                        }
                    }
                }

                if (hasInvalidUrls)
                    ShowAndLogErrorMsg("Some URLs were invalid, only " + numValidUrls + " were correctly loaded.");
                else
                    Log("Loaded " + numValidUrls + " valid URLs.");
            }
            catch (Exception error)
            {
                ShowAndLogErrorMsg("Could not read the contents of the file due to an error.", error.Message);
            }
            finally
            {
                file.Close();
                RefreshTotalUrls();
            }
        }

        private void OpenUrl(string url, Browser mockBrowser = null)
        {
            Log("Browsing: " + url);
            if (mockBrowser == null)
            {
                webBrowser.Navigate(url);
            }
            else
            {
                Log(WHITESPACE_S + "UserAgent: " + mockBrowser.Name);
                webBrowser.Navigate(url, "_self", null, mockBrowser.UserAgent + Environment.NewLine);
            }

            Log("Downloading...", false);

            Stopwatch watch = Stopwatch.StartNew();
            while (webBrowser.ReadyState != WebBrowserReadyState.Complete)
            {
                Application.DoEvents();
            }
            watch.Stop();
            Log(string.Format(" done{0}({1}s)", WHITESPACE_M, Math.Truncate(watch.Elapsed.TotalSeconds)), true);
        }

        private string FetchUrl(string url, string hearderUserAgent = "")
        {
            Log("Fetch via code: " + url);
            WebClient web = new WebClient();

            if (hearderUserAgent != string.Empty)
                web.Headers.Add("user-agent", hearderUserAgent);

            return web.DownloadString(url);
        }

        // Logging

        private void Log(string text, bool addNewLine = true)
        {
            txtLogger.AppendText(text + (addNewLine ? Environment.NewLine : string.Empty));
        }

        private void ShowAndLogErrorMsg(string userMessage, string detailMessage = "")
        {
            if (detailMessage == "") detailMessage = userMessage;

            MessageBox.Show(userMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Log("ERROR: " + detailMessage);
        }

        private static void ShowInfoMsg(string message)
        {
            MessageBox.Show(message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion
    }

    #region DomainModel classes

    public class Browser
    {
        public string Name;
        public string UserAgent;

        public Browser(string name, string headerUserAgent)
        {
            Name = name;
            UserAgent = headerUserAgent;
        }

        public static Browser CHROME { get { return new Browser("Chrome", "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36"); } }
        public static Browser SAFARI { get { return new Browser("Safari", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_9_3) AppleWebKit/537.75.14 (KHTML, like Gecko) Version/7.0.3 Safari/7046A194A"); } }
        public static Browser FIREFOX { get { return new Browser("Firefox", "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:40.0) Gecko/20100101 Firefox/40.1"); } }
        public static Browser IEXPLORER { get { return new Browser("IE", "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; AS; rv:11.0) like Gecko"); } }
        public static Browser SEAMONKEY { get { return new Browser("SeaMonkey", "Mozilla/5.0 (Windows NT 5.2; RW; rv:7.0a1) Gecko/20091211 SeaMonkey/9.23a1pre"); } }
        public static Browser KONQUEROR { get { return new Browser("Konqueror", "Mozilla/5.0 (X11; Linux) KHTML/4.9.1 (like Gecko) Konqueror/4.9"); } }
        public static Browser OPERA { get { return new Browser("Opera", "Opera/9.80 (X11; Linux i686; Ubuntu/14.10) Presto/2.12.388 Version/12.16"); } }
    }

    #endregion
}