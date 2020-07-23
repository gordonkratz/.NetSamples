using System.Collections.ObjectModel;
using Ui.Utilities;

namespace StockOptionApp
{
    public class ViewModel : ViewModelBase
    {

        public ViewModel(IProvideFlexOptionData provider)
        {
            provider.OnNewData += _provider_OnNewData;
        }

        private void _provider_OnNewData(FlexOptionData data)
        {
            FlexOptionData.Add(data);
        }

        public ObservableCollection<FlexOptionData> FlexOptionData { get; } = new ObservableCollection<FlexOptionData>();
    }
}
