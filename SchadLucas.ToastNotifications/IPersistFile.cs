using System.Runtime.InteropServices;
using System.Text;

namespace SchadLucas.ToastNotifications
{
    [ComImport,
    Guid("0000010b-0000-0000-C000-000000000046"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IPersistFile
    {
        void GetCurFile(
            [Out(), MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszFile
        );

        void IsDirty();

        void Load(
            [MarshalAs(UnmanagedType.LPWStr)] string pszFileName,
            [MarshalAs(UnmanagedType.U4)] long dwMode);

        void Save(
            [MarshalAs(UnmanagedType.LPWStr)] string pszFileName,
            bool fRemember);

        void SaveCompleted(
            [MarshalAs(UnmanagedType.LPWStr)] string pszFileName);
    }
}