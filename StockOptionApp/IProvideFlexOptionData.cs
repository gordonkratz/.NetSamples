using System;
using System.Collections.Generic;

namespace StockOptionApp
{
    public interface IProvideFlexOptionData
    {
        event Action<FlexOptionData> OnNewData;
    }

    public class FlexOptionDownloader : IProvideFlexOptionData
    {
        private List<FlexOptionData> _records = new List<FlexOptionData>();
        private event Action<FlexOptionData> _onNewData;

        public FlexOptionDownloader()
        {
            _records.Add(new FlexOptionData("AAPL1", OptionType.Call, new DateTime(2021, 7, 31), 123.4m, 54.98m, 10.34m));
            _records.Add(new FlexOptionData("AMZN", OptionType.Put, new DateTime(2021, 3, 15), 13.4m, 5.98m, 25.34m));
            _records.Add(new FlexOptionData("TRGT", OptionType.Call, new DateTime(2021, 9, 1), 23.4m, 594.98m, 16m));
            _records.Add(new FlexOptionData("HOG", OptionType.Put, new DateTime(2021, 12, 25), 1230.4m, 5134.98m, 139m));
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
