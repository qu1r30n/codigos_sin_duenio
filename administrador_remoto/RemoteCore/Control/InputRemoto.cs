using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace RemoteCore.Control
{
    public static class InputRemoto
    {
        [DllImport("user32.dll")]
        static extern void mouse_event(uint flags, uint dx, uint dy, uint data, UIntPtr extra);

        [DllImport("user32.dll")]
        static extern void keybd_event(byte vk, byte scan, uint flags, UIntPtr extra);

        public static void MoverRaton(int x, int y)
        {
            Cursor.Position = new System.Drawing.Point(x, y);
        }

        public static void ClickRaton(bool down, bool izquierdo)
        {
            uint flag = izquierdo
                ? (down ? 0x0002u : 0x0004u)
                : (down ? 0x0008u : 0x0010u);

            mouse_event(flag, 0, 0, 0, UIntPtr.Zero);
        }

        public static void Tecla(byte codigo, bool down)
        {
            keybd_event(codigo, 0, down ? 0u : 2u, UIntPtr.Zero);
        }
    }
}
