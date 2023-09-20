using System.Windows.Controls;

using network_monitor.ViewModels;

namespace network_monitor.Views;

public partial class SettingsPage : Page
{
    public SettingsPage(SettingsViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
