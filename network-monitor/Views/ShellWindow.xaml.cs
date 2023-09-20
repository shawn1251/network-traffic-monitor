using System.Windows.Controls;

using MahApps.Metro.Controls;

using network_monitor.Contracts.Views;
using network_monitor.ViewModels;

namespace network_monitor.Views;

public partial class ShellWindow : MetroWindow, IShellWindow
{
    public ShellWindow(ShellViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }

    public Frame GetNavigationFrame()
        => shellFrame;

    public void ShowWindow()
        => Show();

    public void CloseWindow()
        => Close();
}
