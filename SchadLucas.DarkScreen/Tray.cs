using System;
using System.Windows.Forms;

namespace SchadLucas.DarkScreen
{
    public class Tray
    {
        private NotifyIcon NotifyIcon { get; }

        public Tray(System.Drawing.Icon icon)
        {
            NotifyIcon = new NotifyIcon
            {
                Visible = true,
                ContextMenu = new ContextMenu(),
                Icon = icon
            };
        }

        public void Close()
        {
            NotifyIcon.Icon = null;
            NotifyIcon.Visible = false;
            NotifyIcon.Dispose();
        }

        public void CreateMenuItem(string name, Action clickedCallback) => CreateMenuItem(name, clickedCallback, () => true);

        public void CreateMenuItem(string name, Action clickedCallback, Func<bool> isEnabled)
        {
            var menuItem = new MenuItem
            {
                Index = 1,
                Name = name,
                Text = $"&{name}"
            };

            NotifyIcon.ContextMenu.Popup += (s, e) => menuItem.Enabled = isEnabled();
            menuItem.Click += (s, e) => clickedCallback();

            NotifyIcon.ContextMenu.MenuItems.Add(menuItem);
        }
    }
}