using System;
using System.Runtime.InteropServices;

namespace SchadLucas.ToastNotifications
{
    [StructLayout(LayoutKind.Sequential), Serializable]
    public struct NOTIFICATION_USER_INPUT_DATA
    {
        [MarshalAs(UnmanagedType.LPWStr)]
        public string Key;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string Value;
    }
}