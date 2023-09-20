using System.Diagnostics;
using System.Windows.Controls;

using network_monitor.ViewModels;

namespace network_monitor.Views;

public partial class MainPage : Page
{
    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }

    private void networkInterfaceSelector_DropDownOpened(object sender, EventArgs e)
    {
        Debug.WriteLine("lllll");
        if (DataContext is MainViewModel viewModel)
        {
            Debug.WriteLine("oooo");
            viewModel.DataNetworkInterface.RefreshDeviceList();
        }
    }
}
