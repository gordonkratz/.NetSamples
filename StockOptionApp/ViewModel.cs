using FrontendFramework;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Ui.Utilities;
using Utilities.FileProvider;
using Utilities.Threading;

namespace StockOptionApp
{
    public class ViewModel : ViewModelBase
    {
        private DateTime _updateTime;
        private RelayCommand _reload;
        private IProvide<FlexOptionData> _provider;

        public ViewModel(IProvide<FlexOptionData> provider, IWpfThread invoker)
        {
            _provider = provider;
            provider.OnNewData += invoker.Wrap<FlexOptionData>(OnNewData);
            Reload = new RelayCommand(LoadData);
        }

        private void OnNewData(FlexOptionData data)
        {
            FlexOptionData.Add(data);
            UpdateTime = DateTime.Now;
        }

        private void LoadData()
        {
            FlexOptionData.Clear();
            _provider.Reset();
        }

        public ICommand Reload { get; }

        public ObservableCollection<FlexOptionData> FlexOptionData { get; } = new ObservableCollection<FlexOptionData>();
    
        public DateTime UpdateTime
        {
            get => _updateTime;
            set => SetProperty(ref _updateTime, value);
        }
    
    }
}
