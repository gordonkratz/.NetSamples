using System.Net.Http;
using Utilities.Configuration;

namespace StockOptionApp.FIleDownload
{
    internal class FlexOptionFileDownloader : HttpDownloader<FlexOptionData>
    {
        private readonly string _requestURI;
        public FlexOptionFileDownloader(HttpClient client, IProvideConfiguration config) : base(client)
        {
            _requestURI = config.GetValue("flex.option.file.uri");
            Start();
        }

        protected override string GetURL()
        {
            var requestDate = "20200723";
            return _requestURI + requestDate;
            //return $"https://marketdata.theocc.com/flex-reports?reportType=PR&optionType=E&reportDate={requestDate}";
        }
    }
}
