using FrontendFramework;
using System.Collections.ObjectModel;
using Ui.Utilities;
using Utilities.Threading;

namespace StockOptionApp
{
    public class ViewModel : ViewModelBase
    {

        public ViewModel(IProvideFlexOptionData provider, IWpfThread invoker)
        {
            provider.OnNewData += invoker.Wrap<FlexOptionData>(OnNewData);
        }

        private void OnNewData(FlexOptionData data)
        {
            FlexOptionData.Add(data);
        }

        public ObservableCollection<FlexOptionData> FlexOptionData { get; } = new ObservableCollection<FlexOptionData>();
    }
}
