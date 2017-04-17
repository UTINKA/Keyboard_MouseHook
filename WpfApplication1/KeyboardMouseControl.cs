using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MouseKeyboardActivityMonitor;
using MouseKeyboardActivityMonitor.WinApi;

using System.Windows.Forms;

using System.Runtime.InteropServices;
using System.IO;

using System.Threading;


namespace KeyboardMouseActivity
{
    class KeyboardMouseControl
    {
        private KeyboardHookListener m_KeyboardHookManager;
        private MouseHookListener m_MouseHookManager;

        public int keyFlag = 0;
        public int mouseFlag = 0;

        [DllImport("user32")]

        public static extern void LockWorkStation();


        String[] buffer = new String[3] ;
        int count = 0;

        private static Mutex mutex = new Mutex();

        


        public KeyboardMouseControl()
        {
            initializeHooks();

            buffer[0] = "";
            buffer[1] = "";
            buffer[2] = "";

            File.WriteAllText("d:/file.txt", String.Empty);
        }
        private void initializeHooks()
        {

            Console.WriteLine("init hook");


            m_KeyboardHookManager = new KeyboardHookListener(new GlobalHooker());
            m_KeyboardHookManager.Enabled = true;
            m_KeyboardHookManager.KeyDown += HookManager_KeyDown;
            m_KeyboardHookManager.KeyUp += HookManager_KeyUp;



            m_MouseHookManager = new MouseHookListener(new GlobalHooker());
            m_MouseHookManager.Enabled = true;
            m_MouseHookManager.MouseDownExt += HookManager_MouseDown;
            m_MouseHookManager.MouseUp += HookManager_MouseUp;


        }

        private void HookManager_KeyDown(object sender, KeyEventArgs e)
        {

            Console.WriteLine(e.KeyData.ToString() + " Pressed");

            int index = count % 3;

            mutex.WaitOne();

            buffer[index] = e.KeyData.ToString();

            /*
            if ((e.KeyCode.ToString().Equals("LControlKey")) && (e.KeyCode.ToString().Equals("LMenu")))
            {
                Console.WriteLine("CHECK!!!!");
            }
            */
            
            int check = check_ctrlAltDel();

            if (check == 1)
            {
                Console.WriteLine("hello there!!!!!!!!!!!!!!");
                // Compose a string that consists of three lines.
                string lines = "ctr+alt+del pressed"+count+" "+DateTime.Now.ToString();

                // Write the string to a file.
                //System.IO.StreamWriter file =  new System.IO.StreamWriter("d:\\testOut.txt");
                
                //file.WriteLine(lines);

                //file.Close();


                File.AppendAllText(@"d:\file.txt", lines + Environment.NewLine);
                

            }
                       

            mutex.ReleaseMutex();
            
            
            if (e.KeyCode == Keys.Escape)
            {
                keyFlag = 0;
                mouseFlag = 0;
            }

            if (keyFlag == 1)
            {
                e.SuppressKeyPress = true;
            }

            count++;

        }

        private int check_ctrlAltDel()
        {
            int i = 0;

            String str1 = buffer[0];
            String str2 = buffer[1];
            String str3 = buffer[2];

            if (str1.Equals("LControlKey") || str2.Equals("LControlKey") || str3.Equals("LControlKey"))
            {
                if (str1.Equals("LMenu") || str2.Equals("LMenu") || str3.Equals("LMenu"))
                {
                    if (str1.Equals("Delete") || str2.Equals("Delete") || str3.Equals("Delete"))
                    {
                        i = 1;
                    }
                }
            }

            return i;
        }

        private void HookManager_KeyUp(object sender, KeyEventArgs e)
        {
            Console.WriteLine(e.KeyData.ToString() + " Released");

            if (e.KeyCode == Keys.Escape)
            {
                keyFlag = 0;
                mouseFlag = 0;
            }
            
            if (keyFlag == 1)
            {
                e.SuppressKeyPress = true;
            }
        }


        //as mousekeydown event is suppressed,there is no chance of mouseup event occuring.
        private void HookManager_MouseUp(object sender, MouseEventArgs e)
        {
            Console.WriteLine(e.Button.ToString() + " Released");
            //   e.Handled = true;
            


        }

        private void HookManager_MouseDown(object sender, MouseEventExtArgs e)
        {
            Console.WriteLine(e.Button.ToString() + " Pressed");
            //e.Handled = true;

            if (mouseFlag == 1)
            {
                e.Handled = true;
            }

        }

        public void keyDisable()
        {

            keyFlag = 1;

            Console.WriteLine("disabling keyboard");



        }

        public void keyEnable()
        {
            keyFlag = 0;

            Console.WriteLine("enabling key");
           
        }

        public void mouseDisable()
        {

            mouseFlag = 1;

            Console.WriteLine("disabling mouse");



        }

        public void mouseEnable()
        {
            mouseFlag = 0;

            Console.WriteLine("enabling mouse");
            
        }

        public void lockDesktop()
        {
            LockWorkStation();
        }


    }
}
