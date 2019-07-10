using System;
using System.Windows.Forms;

namespace GeoMapConverter
{
    public class MyTreeView : TreeView
    {
        protected override void WndProc(ref Message m)
        {
            // Suppress double click action
            if (m.Msg == 0x203) { m.Result = IntPtr.Zero; }
            else base.WndProc(ref m);
        }
    }
}
