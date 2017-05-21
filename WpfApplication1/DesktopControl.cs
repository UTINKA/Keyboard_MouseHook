using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;

namespace KeyboardMouseActivity
{
    public class DesktopControl
    {
        [DllImport("user32")]
        public static extern void LockWorkStation();

        public void lockDesktop()
        {
            LockWorkStation();
        }

        public void shutDownDesktop()
        {
            System.Diagnostics.Process.Start("shutdown", "/s /t 0");

        }

        public void restartDesktop()
        {

            // TODO restart?
            System.Diagnostics.Process.Start("shutdown", "/r /t 0");
        }

        


    }
}
