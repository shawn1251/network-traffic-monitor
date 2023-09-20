using CommunityToolkit.WinUI.Notifications;

using network_monitor.Contracts.Services;

using Windows.UI.Notifications;

namespace network_monitor.Services;

public partial class ToastNotificationsService : IToastNotificationsService
{
    public ToastNotificationsService()
    {
    }

    public void ShowToastNotification(ToastNotification toastNotification)
    {
        ToastNotificationManagerCompat.CreateToastNotifier().Show(toastNotification);
    }
}
