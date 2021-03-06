﻿using StockOptionApp.FIleDownload;
using System;
using System.Collections.Generic;

namespace Utilities.FileProvider
{
    public interface IProvide<T>
    {
        event Action<T> OnNewData;
        event Action OnReset;
        void Reset();
    }

    public class BasicProvider<T> : IProvide<T>
    {
        private List<T> _records = new List<T>();
        private readonly IParse<T> _parser;
        private readonly IDownloadFile<T> _fileDownloader;

        private event Action<T> _onNewData;
        private event Action _onReset;

        public BasicProvider(IDownloadFile<T> fileDownloader, IParse<T> parser)
        {
            _parser = parser;
            _fileDownloader = fileDownloader;
            _fileDownloader.OnFileReady += OnFileReady;
        }

        private void OnFileReady(string obj)
        {
            var records = obj.Split(Environment.NewLine);
            foreach (var item in records)
            {
                if (_parser.TryParse(item, out var data))
                {
                    _onNewData?.Invoke(data);
                }
            }
        }

        public void Reset()
        {
            _onReset?.Invoke();
            _fileDownloader.Start();
        }

        public event Action<T> OnNewData
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

        public event Action OnReset
        {
            add { _onReset += value; }
            remove { _onReset -= value; }
        }
    }
}
