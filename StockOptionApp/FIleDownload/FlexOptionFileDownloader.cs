using System.Net.Http;

namespace StockOptionApp.FIleDownload
{
    internal class FlexOptionFileDownloader : HttpDownloader<FlexOptionData>
    {
        private readonly FlexOptionDownloadConfig _config;
        public FlexOptionFileDownloader(HttpClient client, FlexOptionDownloadConfig config) : base(client)
        {
            _config = config;
        }

        protected override string GetURL()
        {
            var requestDate = "20200723";
            return _config.RequestURI + requestDate;
            //return $"https://marketdata.theocc.com/flex-reports?reportType=PR&optionType=E&reportDate={requestDate}";
        }
    }

    public class FlexOptionDownloadConfig
    {
        public string RequestURI { get; }
    }
}
