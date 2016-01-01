using System;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace AdClicky
{
    public partial class Form1 : Form
    {
        #region Data

        public static Browser[] BrowserChoices = { Browser.CHROME, Browser.SAFARI, Browser.FIREFOX, Browser.IEXPLORER };

        #endregion
        #region Configs

        private readonly string WHITESPACE_S = "  ";
        private readonly string WHITESPACE_M = "    ";

        #endregion

        public Form1()
        {
            InitializeComponent();
        }
        
        private void btnActionBrowser_Click(object sender, EventArgs e)
        {
            try
            {
                LoadUrls();

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
                MessageBox.Show("KABOOM: " + error.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Buttons

        private void button2_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Mockup methods

        void SimulateBrowsing(string url)
        {
            //Browser browserHeader = GetRandomBrowser();
            Browser browserHeader = Browser.SEAMONKEY;
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

        private void LoadUrls()
        {
            //TODO
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

    public class CookieAwareWebClient : WebClient
    {
        public CookieAwareWebClient()
        {
            CookieContainer = new CookieContainer();
        }

        public CookieContainer CookieContainer { get; private set; }

        protected override WebRequest GetWebRequest(Uri address)
        {
            var request = base.GetWebRequest(address);
            var httpRequest = request as HttpWebRequest;
            if (httpRequest != null)
            {
                httpRequest.CookieContainer = CookieContainer;
            }
            return request;
        }
    }
}
