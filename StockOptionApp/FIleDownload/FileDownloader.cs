using Castle.Core;
using System;
using System.Net.Http;

namespace StockOptionApp.FIleDownload
{
    public interface IDownloadFile<T> : IStartable
    {
        event Action<string> OnFileReady;
    }

    public class FlecxOptionFileDownloader : IDownloadFile<FlexOptionData>
    {
        private readonly HttpClient _client;
        string latest;
        event Action<string> _onFileReady;

        public FlecxOptionFileDownloader(HttpClient client)
        {
            _client = client;
        }

        public event Action<string> OnFileReady
        {
            add
            {
                _onFileReady += value;
                if (latest != null)
                    value(latest);
            }
            remove
            {
                _onFileReady -= value;
            }
        }

        public async void Start()
        {
            try
            {
                var requestDate = "20200723";
                var uri = $"https://marketdata.theocc.com/flex-reports?reportType=PR&optionType=E&reportDate={requestDate}";
                string responseBody = await _client.GetStringAsync(uri);

                _onFileReady?.Invoke(responseBody);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }

        public void Stop()
        {
        }
    }
}
