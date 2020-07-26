using StockOptionApp.FIleDownload;
using System;
using System.Collections.Generic;

namespace StockOptionApp
{
    public interface IProvide<T>
    {
        event Action<T> OnNewData;
    }

    public class FlexOptionDataProvider : IProvide<FlexOptionData>
    {
        private List<FlexOptionData> _records = new List<FlexOptionData>();
        private readonly IParse<FlexOptionData> _parser;
        private readonly IDownloadFile<FlexOptionData> _fileDownloader;

        private event Action<FlexOptionData> _onNewData;

        public FlexOptionDataProvider(IDownloadFile<FlexOptionData> fileDownloader, IParse<FlexOptionData> parser)
        {
            _parser = parser;
            _fileDownloader = fileDownloader;
            _fileDownloader.OnFileReady += OnFileReady;
        }

        private void OnFileReady(string obj)
        {
            var records = obj.Split(Environment.NewLine);
            foreach(var item in records)
            {
                if(_parser.TryParse(item, out var data))
                {
                    _onNewData?.Invoke(data);
                }
            }
        }

        public event Action<FlexOptionData> OnNewData
        {
            add
            {
                _onNewData += value;
                _records.ForEach(value);
            }
            remove
            {
                _onNewData -= value;
            }
        }
    }
}
