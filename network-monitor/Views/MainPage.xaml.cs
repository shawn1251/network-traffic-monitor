using System.Diagnostics;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using network_monitor.ViewModels;

namespace network_monitor.Views;

public partial class MainPage : Page
{
    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
        viewModel.DataNetworkInterface.RefreshDeviceList();
    }

    private void networkInterfaceSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (DataContext is MainViewModel viewModel)
        {
            SplitButton list = (SplitButton)sender;
            viewModel.DataNetworkInterface.SelectedInterface = list.SelectedValue.ToString();
            Debug.WriteLine(viewModel.DataNetworkInterface.SelectedInterface);
            viewModel.DataNetworkInterface.StartMonitoringTraffic();
        }
    }

    private void refresh_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        if (DataContext is MainViewModel viewModel)
        {
            viewModel.DataNetworkInterface.RefreshDeviceList();
        }
    }
}
