using System;
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
        #region Constants

        private static readonly char LINE_THIN_CHAR = '-';
        private static readonly char LINE_STRONG_CHAR = '#';
        private static readonly string LINE_THIN = new string(LINE_THIN_CHAR, 60);
        private static readonly string LINE_STRONG = new string(LINE_STRONG_CHAR, 60);
        private static readonly string WHITESPACE_S = new string(' ', 2);
        private static readonly string WHITESPACE_M = new string(' ', 4);

        #endregion
        #region Configs

        private static readonly int VISIT_TIME_MINVALID = 10;  //seconds

        private int TotalVisits;
        private int TotalLoops;
        private List<Uri> UrlsToVisit;

        #endregion

        public MainForm()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            Log("Initializing...");
            UrlsToVisit = new List<Uri>();
            ResetCounters();

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
            spinAvgReadTime.Minimum = VISIT_TIME_MINVALID;
        }

        private void btnActionBrowser_Click(object sender, EventArgs e)
        {
            if (ValidateConfigs() == false) return;

            try
            {
                picLoading.Visible = true;
                Log(LINE_STRONG);

                while (TotalLoops < spinNumberLoops.Value)
                {
                    int urlIndex = 0;
                    foreach (Uri url in UrlsToVisit)
                    {
                        Log(string.Format("{0} Action: {1} - Url {2} is being visited by the {3} time.",
                            new string(LINE_STRONG_CHAR, 5), TotalVisits + 1, ++urlIndex, DisplayOrdinal(TotalLoops + 1)));
                        SimulateBrowsing(url.ToString());
                        RefreshTotalVisits(1);
                        WaitBeforeNext();
                    }
                    RefreshTotalLoops(1);
                }
            }
            catch (Exception error)
            {
                ShowAndLogErrorMsg("An error occurred while visiting the URL." + Environment.NewLine + "The app will stop the execution.", error.Message);
            }
            finally
            {
                picLoading.Visible = false;
                Log(LINE_STRONG);
            }
        }

        #region Validations

        private bool ValidateConfigs()
        {
            //if (int.TryParse(spinAvgReadTime.Value)
            //{
            //    ShowInfoMsg("Please make sure your configurations are correct.");
            //    spinAvgReadTime.Focus();
            //}

            return true;
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
            lblTotalUrlsToVisit.Text = lblTotalUrlsToVisit.Tag.ToString() + UrlsToVisit.Count;
        }

        private void RefreshTotalLoops(int increment)
        {
            TotalLoops += increment;
            lblTotalLoops.Text = lblTotalLoops.Tag.ToString() + TotalLoops.ToString();
        }

        private void RefreshTotalVisits(int increment)
        {
            TotalVisits += increment;
            lblTotalVisits.Text = lblTotalVisits.Tag.ToString() + TotalVisits.ToString();
        }

        private void DisplayUrlsToCall()
        {
            StringBuilder str = new StringBuilder();
            foreach (Uri url in UrlsToVisit)
            {
                str.AppendLine(url.ToString());
            }

            if (str.Length > 0)
            {
                Log(LINE_THIN);
                Log("URLs that will be visited:");
                Log(str.ToString(), false);
                Log(LINE_THIN);
            }
            else ShowInfoMsg("You should load a file containing the URLs to be called by this app.");
        }

        public string DisplayOrdinal(int number)
        {
            if (number <= 0) return number.ToString();

            switch (number % 100)
            {
                case 11:
                case 12:
                case 13:
                    return number + "th";
            }

            switch (number % 10)
            {
                case 1:
                    return number + "st";
                case 2:
                    return number + "nd";
                case 3:
                    return number + "rd";
                default:
                    return number + "th";
            }

        }

        #endregion

        #region Mockup methods

        void SimulateBrowsing(string url)
        {
            Browser browserHeader = GetRandomBrowser();
            //Proxy countryProxy = GetRandomProxy();
            OpenUrl(url, browserHeader);
            SimulateReading();
        }

        private void WaitBeforeNext()
        {
            //TODO
        }

        private void SimulateReading()
        {
            int minReadTime = VISIT_TIME_MINVALID;
            int maxReadTime = (int)Math.Floor(spinAvgReadTime.Value * 1.6M);
            int readTime = new Random(DateTime.Now.Millisecond).Next(minReadTime, maxReadTime);

            Log("Reading", false);
            Stopwatch watch = Stopwatch.StartNew();

            //watch.ElapsedTicks

            while (true)
            {
                TimeSpan passedTime = watch.Elapsed;

                if (passedTime.TotalSeconds > readTime)
                    break;
                else if ((passedTime.Milliseconds % 1000) == 0)
                {
                    bool simulateScrolling = (new Random().Next(0, 2) == 1) ? true : false;
                    if (simulateScrolling)
                    {
                        //TODO: do scrolling
                        Log(".", false);
                    }
                }
                Application.DoEvents();
            }

            watch.Stop();
            Log(string.Format(" done{0}({1}s)", WHITESPACE_M, Math.Truncate(watch.Elapsed.TotalSeconds)), true);
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

        private void ResetCounters()
        {
            TotalLoops = TotalVisits = 0;
        }

        // URLs
        
        private void LoadUrls(Stream file)
        {
            int numValidUrls = 0;
            bool hasInvalidUrls = false;
            string line;

            try
            {
                UrlsToVisit.Clear();
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
                            UrlsToVisit.Add(validatedUrl);
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
