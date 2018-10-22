using System;
using System.Runtime.InteropServices;

namespace SchadLucas.ToastNotifications
{
    // These are the new APIs for Windows 10

    #region NewAPIs

    [ComImport,
    Guid("53E31837-6600-4A81-9395-75CFFE746F94"), ComVisible(true),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface INotificationActivationCallback
    {
        void Activate(
            [In, MarshalAs(UnmanagedType.LPWStr)]
            string appUserModelId,
            [In, MarshalAs(UnmanagedType.LPWStr)]
            string invokedArgs,
            [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)]
            NOTIFICATION_USER_INPUT_DATA[] data,
            [In, MarshalAs(UnmanagedType.U4)]
            uint dataCount);
    }

    #endregion NewAPIs
}