using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMqttSubApp.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly IDialogCoordinator dialogCoordinator;
        private string _brokerHost;
        private string _dataBaseHost;

        // 속성 BrokerHost, DatabasHost
        // 메서드 ConnectBroker, ConnectDatabase

        public MainViewModel(IDialogCoordinator Coordinator)
        {
            this.dialogCoordinator = Coordinator;

            BrokerHost = "210.119.12.68";
            DatabaseHost = "210.119.12.68";
        }

        public string BrokerHost
        {
            get => _brokerHost;
            set => SetProperty(ref _brokerHost, value); 
        }

        public string DatabaseHost
        {
            get => _dataBaseHost;
            set => SetProperty(ref _dataBaseHost, value);
        }

        [RelayCommand]
        public async Task ConnectBroker()
        {
            await this.dialogCoordinator.ShowMessageAsync(this, "브로커연결", "연결합니다!");
        }

        [RelayCommand]
        public async Task ConnectDatabase()
        {
            await this.dialogCoordinator.ShowMessageAsync(this, "DB연결", "DB연결합니다!");
        }
    }
}
