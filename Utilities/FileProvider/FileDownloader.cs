using Castle.Core;
using System;
using System.Net.Http;

namespace StockOptionApp.FIleDownload
{
    public interface IDownloadFile<T>
    {
        event Action<string> OnFileReady;
        void Start();
    }

    public abstract class HttpDownloader<T> : IDownloadFile<T>
    {
        private readonly HttpClient _client;
        string _latest;
        event Action<string> _onFileReady;

        protected abstract string GetURL();

        public HttpDownloader(HttpClient client)
        {
            _client = client;
        }

        public event Action<string> OnFileReady
        {
            add
            {
                _onFileReady += value;
                if (_latest != null)
                    value(_latest);
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
                string responseBody = await _client.GetStringAsync(GetURL());

                _latest = responseBody;
                _onFileReady?.Invoke(responseBody);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }
    }
}
