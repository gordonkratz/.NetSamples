using System;
using System.Net.Http;

namespace StockOptionApp.FIleDownload
{
    internal class FlexOptionFileDownloader : HttpDownloader<FlexOptionData>
    {
        public FlexOptionFileDownloader(HttpClient client) : base(client)
        {
        }

        protected override string GetURL()
        {
            var requestDate = "20200723";
            return $"https://marketdata.theocc.com/flex-reports?reportType=PR&optionType=E&reportDate={requestDate}";
        }
    }
}
