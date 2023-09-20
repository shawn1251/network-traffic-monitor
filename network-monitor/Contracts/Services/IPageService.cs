using System.Windows.Controls;

namespace network_monitor.Contracts.Services;

public interface IPageService
{
    Type GetPageType(string key);

    Page GetPage(string key);
}
