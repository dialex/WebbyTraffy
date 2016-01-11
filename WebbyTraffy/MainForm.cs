using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using WebbyTraffy.Classes;

namespace WebbyTraffy
{
    public partial class MainForm : System.Windows.Forms.Form
    {
        #region Data

        public enum State { Running, Stopped };

        public static UserAgent[] BrowserChoices = { UserAgent.CHROME, UserAgent.SAFARI, UserAgent.FIREFOX, UserAgent.IEXPLORER };
        public static List<Proxy> ProxyChoices;

        #endregion
        #region Constants

        private static readonly int DOWNLOAD_TIMEOUT = 30;

        private static readonly char LINE_THIN_CHAR = '-';
        private static readonly char LINE_STRONG_CHAR = '#';
        private static readonly string LINE_THIN = new string(LINE_THIN_CHAR, 60);
        private static readonly string LINE_STRONG = new string(LINE_STRONG_CHAR, 60);
        private static readonly string WHITESPACE_S = new string(' ', 2);
        private static readonly string WHITESPACE_M = new string(' ', 4);

        #endregion
        #region Configs

        private static readonly int VISIT_TIME_MINVALID = 10;  //seconds

        private State _currentState;
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
            SoundHandler.SetVolume(0);
            ResetCounters();
            _currentState = State.Stopped;
            _urlsToVisit = new List<Uri>();
            ProxyChoices = new List<Proxy>();

            LoadLastConfigs();
            Log(new string(LINE_THIN_CHAR, 5) + "READY" + new string(LINE_THIN_CHAR, 5));
        }

        private void StartRunning()
        {
            _currentState = State.Running;
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
                        if (_currentState == State.Stopped) { RefreshActionButton(); return; }

                        Log(string.Format("{0} Action: {1} - Url {2} is being visited by the {3} time.",
                            new string(LINE_STRONG_CHAR, 5), _totalVisits + 1, ++urlIndex, DisplayOrdinal(_totalLoops + 1)));
                        SimulateBrowsing(url.ToString());
                        RefreshTotalVisits(1);
                        WaitBeforeNext();
                    }
                    RefreshTotalLoops(1);
                }
                ShowInfoMsg("Execution finished.");
                Log("Execution finished.");
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
            _currentState = State.Stopped;
            btnAction.Text = "Stopping...";
        }

        #region Simulation methods

        private UserAgent GetRandomBrowser()
        {
            if (chkConfigSimulateBrowser.Checked)
            {
                int randIndex = new Random().Next(0, BrowserChoices.Length - 1);
                return BrowserChoices[randIndex];
            }
            else return null;
        }

        void SimulateBrowsing(string url)
        {
            UserAgent browserHeader = GetRandomBrowser();
            Proxy currentProxy = SimulateRandomCountry();
            OpenUrl(url, browserHeader, currentProxy);
            SimulateReading();
        }

        private Proxy SimulateRandomCountry()
        {
            if (chkConfigSimulateCountries.Checked)
            {
                int randIndex = new Random().Next(0, ProxyChoices.Count);
                if (randIndex != ProxyChoices.Count)
                {
                    Proxy countryProxy = ProxyChoices[randIndex];
                    Proxy.SetConnection(countryProxy.Connection);
                    return countryProxy;
                }
                else return null;
            }
            else return null;
        }

        private void SimulateReading()
        {
            bool alertsDismissed = false;
            int minReadTime = VISIT_TIME_MINVALID;
            int maxReadTime = (int)Math.Floor(spinAvgReadTime.Value * 1.6M);
            int readTime = new Random(DateTime.Now.Millisecond).Next(minReadTime, maxReadTime);

            Log("Reading", false);
            Stopwatch watch = Stopwatch.StartNew();

            while (_currentState == State.Running)
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
            while (_currentState == State.Running)
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
            while (_currentState == State.Running)
            {
                SignalToStopRunning();
                Application.DoEvents();
            }

            Proxy.ResetSettings();
            SaveConfigs();
            Log("Shutting down...");
        }

        #endregion
        #region Buttons

        private void btnActionBrowser_Click(object sender, EventArgs e)
        {
            switch (_currentState)
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

        private void btnFileProxies_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = openFileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Stream file = openFileDialog.OpenFile();
                    LoadProxies(file);
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
            switch (_currentState)
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
            RefreshTotalLoops(0);
            RefreshTotalVisits(0);
        }

        private void ShuffleList(List<Uri> list)
        {
            Random randGen = new Random(DateTime.Now.Millisecond);
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = randGen.Next(n + 1);
                Uri value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        // URLs

        private void LoadUrls(Stream file, bool shuffle = true)
        {
            int numValidUrls = 0;
            bool hasInvalidUrls = false;
            string line;

            try
            {
                _urlsToVisit.Clear();

                Log("Loading URLs...");
                Cursor.Current = Cursors.WaitCursor;
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

                if (shuffle) ShuffleList(_urlsToVisit);

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
                Cursor.Current = Cursors.Default;
                file.Close();
                RefreshTotalUrls();
            }
        }

        private void OpenUrl(string url, UserAgent mockBrowser = null, Proxy mockCountry = null)
        {
            Log("Browsing: " + url);

            if (mockCountry != null) Log(WHITESPACE_S + "Country: " + mockCountry.Country + " @" + mockCountry.Ip);
            else Log(WHITESPACE_S + "Country: local machine");

            if (mockBrowser == null)
            {
                Log(WHITESPACE_S + "UserAgent: local machine");
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
                if (_currentState == State.Stopped) { RefreshActionButton(); return; }

                if (watch.Elapsed.Seconds > DOWNLOAD_TIMEOUT) { Log("timeout"); return; }

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

        private void LoadProxies(Stream file)
        {
            int numValidProxies = 0;
            bool hasInvalidProxies = false;
            string line;

            try
            {
                ProxyChoices.Clear();

                Log("Loading proxies...");
                Cursor.Current = Cursors.WaitCursor;
                using (StreamReader fileReader = new StreamReader(file))
                {
                    while ((line = fileReader.ReadLine()) != null)
                    {
                        line = line.Trim();
                        if (IsEmptyOrComment(line)) continue;

                        string[] parsedLine = line.Split('|');
                        try
                        {
                            Proxy inputProxy = new Proxy(parsedLine[0].Trim(), parsedLine[1].Trim());
                            ProxyChoices.Add(inputProxy);
                            numValidProxies++;
                        }
                        catch (Exception)
                        {
                            hasInvalidProxies = true;
                            Log(WHITESPACE_S + "Not a valid proxy: " + line);
                        }
                    }
                }

                if (hasInvalidProxies)
                    ShowAndLogErrorMsg("Some proxies were invalid, only " + numValidProxies + " were correctly loaded.");
                else
                    Log("Loaded " + numValidProxies + " valid proxies.");
            }
            catch (Exception error)
            {
                ShowAndLogErrorMsg("Could not read the contents of the file due to an error.", error.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                file.Close();
            }
        }

        // Logging

        private void Log(string text, bool addNewLine = true)
        {
            txtLogger.AppendText(text + (addNewLine ? Environment.NewLine : string.Empty));

            txtLogger.Select(txtLogger.Text.Length - 1, 0);
            txtLogger.ScrollToCaret();
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
}
