using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace WebbyTraffy
{
    public partial class MainForm : System.Windows.Forms.Form
    {
        #region Data

        public enum State { Running, Stopped };
        public static UserAgent[] BrowserChoices = { UserAgent.CHROME, UserAgent.SAFARI, UserAgent.FIREFOX, UserAgent.IEXPLORER };

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

        private State _internalState;
        private int _totalVisits;
        private int _totalLoops;
        private List<Uri> _urlsToVisit;

        #endregion

        public MainForm()
        {
            InitializeComponent();
            Init();
            this.Icon = Properties.Resources.AppIco;
        }

        private void Init()
        {
            Log("Initializing...");
            ResetCounters();
            _internalState = State.Stopped;
            _urlsToVisit = new List<Uri>();

            LoadLastConfigs();
        }

        private void StartRunning()
        {
            _internalState = State.Running;
            RefreshActionButton();
            ResetCounters();

            try
            {
                picLoading.Visible = true;
                Log(LINE_STRONG);

                while (_totalLoops < spinNumberLoops.Value)
                {
                    int urlIndex = 0;
                    foreach (Uri url in _urlsToVisit)
                    {
                        // If a stop signal was sent meanwhile, it's time to stop
                        if (_internalState == State.Stopped) { RefreshActionButton(); return; }

                        Log(string.Format("{0} Action: {1} - Url {2} is being visited by the {3} time.",
                            new string(LINE_STRONG_CHAR, 5), _totalVisits + 1, ++urlIndex, DisplayOrdinal(_totalLoops + 1)));
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

        private void SignalToStopRunning()
        {
            _internalState = State.Stopped;
            btnAction.Text = "Stopping...";
        }

        #region Simulation methods

        void SimulateBrowsing(string url)
        {
            UserAgent browserHeader = GetRandomBrowser();
            //Proxy countryProxy = GetRandomProxy();
            OpenUrl(url, browserHeader);
            SimulateReading();
        }

        private void SimulateReading()
        {
            bool alertsDismissed = false;
            int minReadTime = VISIT_TIME_MINVALID;
            int maxReadTime = (int)Math.Floor(spinAvgReadTime.Value * 1.6M);
            int readTime = new Random(DateTime.Now.Millisecond).Next(minReadTime, maxReadTime);

            Log("Reading", false);
            Stopwatch watch = Stopwatch.StartNew();

            while (_internalState == State.Running)
            {
                TimeSpan passedTime = watch.Elapsed;

                if (passedTime.TotalSeconds > readTime)
                    break;
                else if ((passedTime.Milliseconds % 1000) == 0)
                {
                    bool simulateScrolling = (new Random().Next(0, 2) == 1) ? true : false;
                    if (simulateScrolling)
                    {
                        SimulateScrolling();
                        Log(".", false);
                    }
                    DismissAlertBoxes(out alertsDismissed);
                }
                Application.DoEvents();
            }

            watch.Stop();
            Log(string.Format(" done{0}({1}s)", WHITESPACE_M, Math.Truncate(watch.Elapsed.TotalSeconds)), true);
        }

        private void SimulateScrolling()
        {
            //webBrowser.Focus();
            //SendKeys.SendWait("{PGDN}");
            //Log("D", false);

            //bool scrollDown = (new Random().Next(0, 2) == 1) ? true : false;
            //if (scrollDown) { 
            //    KeyHandle.SendKey(webBrowser.Handle, Keys.PageDown);
            //    Log("D", false);
            //}
            //else { 
            //    KeyHandle.SendKey(webBrowser.Handle, Keys.PageUp);
            //    Log("U", false);
            //}
        }

        private void WaitBeforeNext()
        {
            int estimatedExecTime = (int)spinLoopDuration.Value * 60; // in seconds
            int numLoops = (int)spinNumberLoops.Value;
            int numCallsPerLoop = _urlsToVisit.Count;

            int timePerLoop = estimatedExecTime / numLoops;
            int avgTimeBetweenCalls = timePerLoop / numCallsPerLoop;

            Random randGen = new Random(DateTime.Now.Millisecond);
            int timeIrregularity = randGen.Next(1, (int)Math.Truncate(estimatedExecTime * 0.01));
            int waitTime = new Random(DateTime.Now.Millisecond).Next(avgTimeBetweenCalls - timeIrregularity, avgTimeBetweenCalls + timeIrregularity);

            Log("Waiting next visitor...", false);
            Stopwatch watch = Stopwatch.StartNew();
            while (_internalState == State.Running)
            {
                TimeSpan passedTime = watch.Elapsed;

                if (passedTime.TotalSeconds > waitTime)
                    break;
                else if ((passedTime.Milliseconds % 1000) == 0)
                    Log(".", false);

                Application.DoEvents();
            }
            watch.Stop();
            Log(string.Format(" done{0}({1}s)", WHITESPACE_M, Math.Truncate(watch.Elapsed.TotalSeconds)), true);
        }

        private UserAgent GetRandomBrowser()
        {
            if (chkConfigSimulateBrowser.Checked)
            {
                int randIndex = new Random().Next(0, BrowserChoices.Length - 1);
                return BrowserChoices[randIndex];
            }
            else return null;
        }

        /// <summary>
        /// Find alert boxes and simulate a press on "OK" or "Continue".
        /// </summary>
        private void DismissAlertBoxes(out bool alertsDismissed)
        {
            // Adfly pages
            HtmlElement pageButton = webBrowser.Document.GetElementById("abC");
            if (pageButton != null) pageButton.InvokeMember("click");

            alertsDismissed = true;
        }

        #endregion

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

        private bool IsEmptyOrComment(string line)
        {
            if (string.IsNullOrEmpty(line))
                return true;
            else if (line.StartsWith("//") || line.StartsWith("#"))
                return true;
            else
                return false;
        }

        #endregion

        #region Events

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            while (_internalState == State.Running)
            {
                SignalToStopRunning();
                Application.DoEvents();
            }

            SaveConfigs();
            Log("Shutting down...");
        }

        #endregion
        #region Buttons

        private void btnActionBrowser_Click(object sender, EventArgs e)
        {
            switch (_internalState)
            {
                case State.Stopped:
                    if (ValidateConfigs() == false) return;
                    StartRunning();
                    break;
                case State.Running:
                    SignalToStopRunning();
                    break;
            }
        }

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

        private void RefreshActionButton()
        {
            switch (_internalState)
            {
                case State.Stopped:
                    btnAction.Text = "START!";
                    btnAction.Image = Properties.Resources.Play;
                    break;
                case State.Running:
                    btnAction.Text = "Stop";
                    btnAction.Image = Properties.Resources.Stop;
                    break;
            }
        }

        private void RefreshTotalUrls()
        {
            lblTotalUrlsToVisit.Text = lblTotalUrlsToVisit.Tag.ToString() + _urlsToVisit.Count;
        }

        private void RefreshTotalLoops(int increment)
        {
            _totalLoops += increment;
            lblTotalLoops.Text = lblTotalLoops.Tag.ToString() + _totalLoops.ToString();
        }

        private void RefreshTotalVisits(int increment)
        {
            _totalVisits += increment;
            lblTotalVisits.Text = lblTotalVisits.Tag.ToString() + _totalVisits.ToString();
        }

        private void DisplayUrlsToCall()
        {
            StringBuilder str = new StringBuilder();
            foreach (Uri url in _urlsToVisit)
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

        #region Config methods

        private void LoadLastConfigs()
        {
            string lastUsedConfig;
            try
            {
                Log(WHITESPACE_S + "Attempting to load \"Urls.txt\" file.");
                if (File.Exists("Urls.txt"))
                    LoadUrls(File.Open("Urls.txt", FileMode.Open));

                Log(WHITESPACE_S + "Attempting to load \"Proxies.txt\" file.");
                if (File.Exists("Proxies.txt"))
                    LoadProxies(File.Open("Proxies.txt", FileMode.Open));

                // ========================
                // Import last used configs
                // ========================

                lastUsedConfig = ReadConfigValue("UseProxies");
                chkConfigSimulateCountries.Checked = (lastUsedConfig != null) ? Convert.ToBoolean(lastUsedConfig) : true;

                lastUsedConfig = ReadConfigValue("MockBrowsers");
                chkConfigSimulateBrowser.Checked = (lastUsedConfig != null) ? Convert.ToBoolean(lastUsedConfig) : true;

                lastUsedConfig = ReadConfigValue("NumVisits");
                spinNumberLoops.Value = (lastUsedConfig != null) ? Convert.ToInt32(lastUsedConfig) : 100;

                lastUsedConfig = ReadConfigValue("RunTime");
                spinLoopDuration.Value = (lastUsedConfig != null) ? Convert.ToInt32(lastUsedConfig) : 480;

                lastUsedConfig = ReadConfigValue("VisitTime");
                spinAvgReadTime.Value = (lastUsedConfig != null) ? Convert.ToInt32(lastUsedConfig) : VISIT_TIME_MINVALID;
            }
            catch (Exception error)
            {
                ShowAndLogErrorMsg("Something went wrong while reading the default configurations.", error.Message);
            }
        }

        /// <summary>
        /// Saves current configs persistenly, for next run.
        /// </summary>
        private void SaveConfigs()
        {
            try
            {
                WriteConfigValue("UseProxies", chkConfigSimulateCountries.Checked.ToString());
                WriteConfigValue("MockBrowsers", chkConfigSimulateBrowser.Checked.ToString());
                WriteConfigValue("NumVisits", spinNumberLoops.Value.ToString());
                WriteConfigValue("RunTime", spinLoopDuration.Value.ToString());
                WriteConfigValue("VisitTime", spinAvgReadTime.Value.ToString());
            }
            catch (Exception error)
            {
                ShowAndLogErrorMsg("Something went wrong while saving your current configurations.", error.Message);
            }
        }

        /// <summary>
        /// Reads value from App.config file.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private string ReadConfigValue(string key)
        {
            try
            {
                return ConfigurationManager.AppSettings[key];
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Writes value to App.config file.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        private void WriteConfigValue(string key, string value)
        {
            Configuration configFile = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);

            if (ReadConfigValue(key) == null)
            {
                configFile.AppSettings.Settings.Add(key, value);
                configFile.Save(ConfigurationSaveMode.Modified);
            }
            else
            {
                configFile.AppSettings.Settings[key].Value = value;
                configFile.Save();
            }
        }

        #endregion
        #region Helper methods

        private void ResetCounters()
        {
            _totalLoops = _totalVisits = 0;
        }

        // URLs

        private void LoadUrls(Stream file)
        {
            int numValidUrls = 0;
            bool hasInvalidUrls = false;
            string line;

            try
            {
                _urlsToVisit.Clear();
                Log("Cleared URL calling list.");

                Log("Loading URLs...");
                using (StreamReader fileReader = new StreamReader(file))
                {
                    while ((line = fileReader.ReadLine()) != null)
                    {
                        line = line.Trim();
                        if (IsEmptyOrComment(line)) continue;

                        Uri validatedUrl;
                        bool isValidUrl =
                            Uri.TryCreate(line, UriKind.Absolute, out validatedUrl) &&
                            (validatedUrl.Scheme == Uri.UriSchemeHttp || validatedUrl.Scheme == Uri.UriSchemeHttps);

                        if (isValidUrl)
                        {
                            _urlsToVisit.Add(validatedUrl);
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

        private void OpenUrl(string url, UserAgent mockBrowser = null)
        {
            Log("Browsing: " + url);
            if (mockBrowser == null)
            {
                webBrowser.Navigate(url);
            }
            else
            {
                Log(WHITESPACE_S + "UserAgent: " + mockBrowser.Name);
                webBrowser.Navigate(url, "_self", null, mockBrowser.UserAgentStr + Environment.NewLine);
            }

            Log("Downloading...", false);
            Stopwatch watch = Stopwatch.StartNew();
            while (webBrowser.ReadyState != WebBrowserReadyState.Complete)
            {
                // If a stop signal was sent meanwhile, it's time to stop
                if (_internalState == State.Stopped) { RefreshActionButton(); return; }

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

        // Proxies

        private void LoadProxies(Stream fileStream)
        {
            //TODO
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

    /// <summary>
    /// Contains UserAgent strings. Check http://www.useragentstring.com/pages/All/.
    /// </summary>
    public class UserAgent
    {
        public string Name;
        public string UserAgentStr;

        public UserAgent(string name, string headerUserAgent)
        {
            Name = name;
            UserAgentStr = headerUserAgent;
        }

        public static UserAgent CHROME { get { return new UserAgent("Chrome", "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36"); } }
        public static UserAgent SAFARI { get { return new UserAgent("Safari", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_9_3) AppleWebKit/537.75.14 (KHTML, like Gecko) Version/7.0.3 Safari/7046A194A"); } }
        public static UserAgent FIREFOX { get { return new UserAgent("Firefox", "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:40.0) Gecko/20100101 Firefox/40.1"); } }
        public static UserAgent IEXPLORER { get { return new UserAgent("IE", "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; AS; rv:11.0) like Gecko"); } }
        public static UserAgent SEAMONKEY { get { return new UserAgent("SeaMonkey", "Mozilla/5.0 (Windows NT 5.2; RW; rv:7.0a1) Gecko/20091211 SeaMonkey/9.23a1pre"); } }
        public static UserAgent KONQUEROR { get { return new UserAgent("Konqueror", "Mozilla/5.0 (X11; Linux) KHTML/4.9.1 (like Gecko) Konqueror/4.9"); } }
        public static UserAgent OPERA { get { return new UserAgent("Opera", "Opera/9.80 (X11; Linux i686; Ubuntu/14.10) Presto/2.12.388 Version/12.16"); } }
    }
    
    #endregion
}
