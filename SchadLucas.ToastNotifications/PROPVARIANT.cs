using System;
using System.Runtime.InteropServices;

namespace SchadLucas.ToastNotifications
{
    [StructLayout(LayoutKind.Explicit)]
    public struct PROPVARIANT
    {
        [FieldOffset(0)]
        public ushort vt;

        [FieldOffset(8)]
        public IntPtr unionmember;

        [FieldOffset(8)]
        public UInt64 forceStructToLargeEnoughSize;
    }
}