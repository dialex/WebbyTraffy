using System;

namespace WebbyTraffy.Classes
{
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

    public class Proxy
    {
        public string Country { get; set; }
        public string Ip { get; set; }
        public string Port { get; set; }
        public string Connection { get { return Ip + ":" + Port; } }

        public Proxy(string country, string connection)
        {
            try
            {
                string[] tokens = connection.Split(':');

                Ip = tokens[0];
                Port = tokens[1];
                Country = country;
            }
            catch (Exception)
            {
                throw new FormatException("Could not create Proxy. Expected connection format as \"IP:PORT\".");
            }
        }

        public static void ResetSettings()
        {
            WinInetInterop.RestoreSystemProxy();
        }

        public static void SetConnection(string ipPort)
        {
            try
            {
                WinInetInterop.SetConnectionProxy(ipPort);
            }
            catch (Exception)
            {
                ResetSettings();
            }
        }
    }
}
