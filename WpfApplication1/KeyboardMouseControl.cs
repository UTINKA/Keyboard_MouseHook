using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MouseKeyboardActivityMonitor;
using MouseKeyboardActivityMonitor.WinApi;

using System.Windows.Forms;



namespace KeyboardMouseActivity
{
    class KeyboardMouseControl
    {
        private KeyboardHookListener m_KeyboardHookManager;
        private MouseHookListener m_MouseHookManager;

        public int keyFlag = 0;
        public int mouseFlag = 0;


        public KeyboardMouseControl()
        {
            initializeHooks();
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


    }
}
