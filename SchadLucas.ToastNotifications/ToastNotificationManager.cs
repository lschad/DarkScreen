using static Windows.UI.Notifications.ToastNotificationManager;
using ToastNotification = Windows.UI.Notifications.ToastNotification;
using ToastTemplateType = Windows.UI.Notifications.ToastTemplateType;

namespace SchadLucas.ToastNotifications
{
    public static class ToastNotificationManager
    {
        private static string _appId;

        public static void Initialize(string appId)
        {
            _appId = appId;
            NotificationActivator.Initialize(appId);
        }

        public static void Uninitialize() => NotificationActivator.Uninitialize();

        public static void NewNotification()
        {
            var toastXml = GetTemplateContent(ToastTemplateType.ToastText04);
            var stringElements = toastXml.GetElementsByTagName("text");
            for (var i = 0; i < stringElements.Length; i++)
            {
                stringElements[i].AppendChild(toastXml.CreateTextNode("Line " + i));
            }
            var toast = new ToastNotification(toastXml);

            CreateToastNotifier(_appId).Show(toast);
        }
    }
}