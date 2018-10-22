using System.Runtime.InteropServices;

namespace SchadLucas.ToastNotifications
{
    [ComImport,
    Guid("886D8EEB-8CF2-4446-8D02-CDBA1DBDCF99"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IPropertyStore
    {
        void GetCount([Out] out uint propertyCount);

        void GetAt([In] uint propertyIndex, [Out, MarshalAs(UnmanagedType.Struct)] out PROPERTYKEY key);

        void GetValue([In, MarshalAs(UnmanagedType.Struct)] ref PROPERTYKEY key, [Out, MarshalAs(UnmanagedType.Struct)] out PROPVARIANT pv);

        void SetValue([In, MarshalAs(UnmanagedType.Struct)] ref PROPERTYKEY key, [In, MarshalAs(UnmanagedType.Struct)] ref PROPVARIANT pv);

        void Commit();
    }
}