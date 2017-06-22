using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MouseKeyboardActivityMonitor;
using MouseKeyboardActivityMonitor.WinApi;

using System.Windows.Forms;

using System.IO;

using System.Collections;


namespace KeyboardMouseActivity
{
    class KeyboardMouseControl
    {
        private KeyboardHookListener m_KeyboardHookManager;
        private MouseHookListener m_MouseHookManager;

        bool escDown = false;

        int keyFlag = 0;


        private void InitiateKeyboardHook()
        {
            if (m_KeyboardHookManager != null)
                return;

            m_KeyboardHookManager = new KeyboardHookListener(new GlobalHooker());

            m_KeyboardHookManager.Enabled = true;

            m_KeyboardHookManager.KeyDown += HookManager_KeyDown;
            m_KeyboardHookManager.KeyUp += HookManager_KeyUp;
        }

        private void RemoveKeyboardHook()
        {
            if (m_KeyboardHookManager == null)
                return;

            m_KeyboardHookManager.Enabled = false;

            m_KeyboardHookManager.KeyDown -= HookManager_KeyDown;
            m_KeyboardHookManager.KeyUp -= HookManager_KeyUp;

            m_KeyboardHookManager = null;
        }

        
        private void InitiateMouseHook()
        {
            if (m_MouseHookManager != null)
                return;

            m_MouseHookManager = new MouseHookListener(new GlobalHooker());

            m_MouseHookManager.Enabled = true;

            m_MouseHookManager.MouseDownExt += HookManager_MouseDown;
            m_MouseHookManager.MouseUp += HookManager_MouseUp;
        }
        
        private void RemoveMouseHook()
        {
            if (m_MouseHookManager == null)
                return;

            m_MouseHookManager.Enabled = false;

            m_MouseHookManager.MouseDownExt -= HookManager_MouseDown;
            m_MouseHookManager.MouseUp -= HookManager_MouseUp;

            m_MouseHookManager = null;
        }


        private void HookManager_KeyDown(object sender, KeyEventArgs e)
        {

            Console.WriteLine(e.KeyData.ToString() + " Pressed");
                        
            
            if (e.KeyCode==Keys.Escape && escDown==false)
            {

                escDown = true;

                Console.WriteLine("PUSHING in ESC:Value:"+escDown);
                
            }

            if (e.KeyCode == Keys.G && escDown==true)
            {
                Console.WriteLine("PRESSED G while ESC is down");

                keyEnable();
                mouseEnable();
                        
            }

            
            if (keyFlag == 1)
            {
                e.SuppressKeyPress = true;
            }

        }


        private void HookManager_KeyUp(object sender, KeyEventArgs e)
        {
            Console.WriteLine(e.KeyData.ToString() + " Released");

            if (e.KeyCode == Keys.Escape && escDown==true)
            {                
                escDown = false;
                Console.WriteLine("Poping ESC:Value:"+escDown);
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
            
            e.Handled = true;
           

        }

        
        public void keyDisable()
        {

            InitiateKeyboardHook();

            keyFlag = 1;

            Console.WriteLine("disabling keyboard");
            
        }
        
        public void keyEnable()
        {
            RemoveKeyboardHook();

            keyFlag = 0;

            Console.WriteLine("enabling key");
           
        }

        public void mouseDisable()
        {

            InitiateMouseHook();
            InitiateKeyboardHook();
            
            Console.WriteLine("disabling mouse");

        }

        public void mouseEnable()
        {

            RemoveMouseHook();
            
            if (keyFlag == 0)
            {
                RemoveKeyboardHook();
            }

            Console.WriteLine("enabling mouse");
            
        }

       
    }
}
