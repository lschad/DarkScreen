using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace SchadLucas.ToastNotifications
{
    [ClassInterface(ClassInterfaceType.None)]
    [ComSourceInterfaces(typeof(INotificationActivationCallback))]
    [Guid("23A5B06E-20BB-4E7E-A0AC-6982ED6A6041"), ComVisible(true)]
    public class NotificationActivator : INotificationActivationCallback
    {
        private static int _cookie = -1;
        private static RegistrationServices _regService;

        private static void InstallShortcut(string shortcutPath, string exePath, string appId)
        {
            var newShortcut = (IShellLinkW)new CShellLink();

            // Create a shortcut to the exe
            newShortcut.SetPath(exePath);

            // Open the shortcut property store, set the AppUserModelId property
            var newShortcutProperties = (IPropertyStore)newShortcut;

            var varAppId = new PropVariantHelper();
            varAppId.SetValue(appId);
            newShortcutProperties.SetValue(PROPERTYKEY.AppUserModel_ID, varAppId.Propvariant);

            var varToastId = new PropVariantHelper { VarType = VarEnum.VT_CLSID };
            varToastId.SetValue(typeof(NotificationActivator).GUID);

            newShortcutProperties.SetValue(PROPERTYKEY.AppUserModel_ToastActivatorCLSID, varToastId.Propvariant);

            // Commit the shortcut to disk
            var newShortcutSave = (IPersistFile)newShortcut;

            newShortcutSave.Save(shortcutPath, true);
        }

        private static void RegisterComServer(string exePath)
        {
            // We register the app process itself to start up when the notification is activated, but
            // other options like launching a background process instead that then decides to launch
            // the UI as needed.
            var regString = $"SOFTWARE\\Classes\\CLSID\\{{{typeof(NotificationActivator).GUID}}}\\LocalServer32";
            var key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(regString);
            key.SetValue(null, exePath);
        }

        // In order to display toasts, a desktop application must have a shortcut on the Start menu.
        // Also, an AppUserModelID must be set on that shortcut.
        //
        // For the app to be activated from Action Center, it needs to register a COM server with the
        // OS and register the CLSID of that COM server on the shortcut.
        //
        // The shortcut should be created as part of the installer. The following code shows how to
        // create a shortcut and assign the AppUserModelID and ToastActivatorCLSID properties using
        // Windows APIs.
        //
        // Included in this project is a wxs file that be used with the WiX toolkit to make an
        // installer that creates the necessary shortcut. One or the other should be used.
        //
        // This sample doesn't clean up the shortcut or COM registration.
        private static void RegisterAppForNotificationSupport(string appId)
        {
            var shortcutPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Microsoft\\Windows\\Start Menu\\Programs\\EatSmart.lnk";
            if (!File.Exists(shortcutPath))
            {
                // Find the path to the current executable
                var exePath = Process.GetCurrentProcess().MainModule.FileName;
                InstallShortcut(shortcutPath, exePath, appId);
                RegisterComServer(exePath);
            }
        }

        internal static void Initialize(string appid)
        {
            RegisterAppForNotificationSupport(appid);

            _regService = new RegistrationServices();

            _cookie = _regService.RegisterTypeForComClients(typeof(NotificationActivator), RegistrationClassContext.LocalServer, RegistrationConnectionType.MultipleUse);
        }

        internal static void Uninitialize()
        {
            if (_cookie != -1)
            {
                _regService?.UnregisterTypeForComClients(_cookie);
            }
        }

        public void Activate(string appUserModelId, string invokedArgs, NOTIFICATION_USER_INPUT_DATA[] data, uint dataCount)
        {
            // todo: :thinkong:
            throw new NotImplementedException();
        }
    }
}