using System.Configuration;
using System.IO;
using System.Net;

namespace LTB_Verwaltung
{
    class FTP
    {
        private static FTP instance = null;
        private static readonly object padlock = new object();

        private static readonly string FTP_HOST = ConfigurationManager.AppSettings["ftpserver"];
        private static readonly string FTP_USER = ConfigurationManager.AppSettings["username"];
        private static readonly string FTP_PASS = ConfigurationManager.AppSettings["password"];
        public readonly string WIN_TMP = Path.GetTempPath();
        public readonly string CSV_FILE = "LTB.csv";

        private FTP() { }

        public static FTP Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new FTP();
                    }
                    return instance;
                }
            }
        }

        public void OpenConfig()
        {
            System.Diagnostics.Process.Start(System.AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
        }

#pragma warning disable IDE0017
        public void Download()
        {
            WebClient client = new WebClient();
            client.Credentials = new NetworkCredential(FTP_USER, FTP_PASS);
            client.DownloadFile(FTP_HOST + CSV_FILE, WIN_TMP + CSV_FILE);
            client.Dispose();
        }

        public void Upload()
        {
            WebClient client = new WebClient();
            client.Credentials = new NetworkCredential(FTP_USER, FTP_PASS);
            client.UploadFile(FTP_HOST + CSV_FILE, WIN_TMP + CSV_FILE);
            client.Dispose();
        }
    }
}
