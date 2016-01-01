using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdClicky
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAction_Click(object sender, EventArgs e)
        {
            string htmlPage;

            for (int i = 1; i <= 10; i++)
            {
                using (WebClient client = new WebClient())
                {
                    htmlPage = FetchUrl("http://adf.ly/10475475/gmod-maps");
                    Thread.Sleep(1000 * 10);
                    htmlPage = FetchUrl("http://www.diogonunes.com/blog/install-config-gmod-guide/");
                }
                lblTotalCalls.Text = lblTotalCalls.Tag.ToString() + i.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 1; i <= 10; i++)
                {
                    OpenUrl("http://adf.ly/10475475/gmod-textures");
                    OpenUrl("http://www.diogonunes.com/blog/minecraft-a-game-as-a-metaphor-of-life/");

                    lblTotalCalls.Text = lblTotalCalls.Tag.ToString() + i.ToString();
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("KABOOM: " + error.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #region Mockup methods

        void SimulateBrowsing(string url)
        {
            MockBrowser();
            MockCountry();
            OpenUrl(url);
            MockReadingTime(10);
            WaitBeforeNext();
        }

        private void WaitBeforeNext()
        {
            throw new NotImplementedException();
        }

        private void MockReadingTime(int v)
        {
            throw new NotImplementedException();
        }

        private void MockCountry()
        {
            throw new NotImplementedException();
        }

        private void MockBrowser()
        {
            throw new NotImplementedException();
        }

        #endregion
        #region URL

        private void OpenUrl(string url)
        {
            Log("Open on fake browser: " + url);
            webBrowser1.Navigate(url);

            Log("Downloading...", false);
            while (webBrowser1.ReadyState != WebBrowserReadyState.Complete)
            {
                Application.DoEvents();
            }
            Log(" done", true);
        }

        private string FetchUrl(string url)
        {
            Log("Fetch via code: " + url);
            string htmlPage = new WebClient().DownloadString(url);

            return htmlPage;
        }

        #endregion
        #region Helper methods

        void Log(string text, bool addNewLine = true)
        {
            txtLogger.AppendText(text + (addNewLine ? Environment.NewLine : string.Empty));
        }

        #endregion
    }

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
