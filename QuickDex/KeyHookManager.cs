using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using WindowsInput;
using WindowsInput.Native;
using QuickDex.Properties;

namespace QuickDex
{
    class KeyHookManager : IDisposable
    {
        #region Extern Methods
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
            LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);


        [DllImport("kernel32")]
        static extern bool AllocConsole();

        [DllImport("kernel32")]
        static extern bool FreeConsole();
        #endregion

        //ID for: Hook procedure that monitors low-level keyboard input events. 
        private const int WH_KEYBOARD_LL = 13;
        //ID for: Keydown event
        private const int WM_KEYDOWN = 0x0100;
        //ID for: Keyup event
        private const int WM_KEYUP = 0x0101;
        private LowLevelKeyboardProc _proc; 
        private IntPtr _hookID = IntPtr.Zero;
        private bool mainKeyIsDown;
        private bool modKeyIsDown;

        private Util.VoidDelegate _externAction;

        public KeyHookManager(Util.VoidDelegate externalCallback)
        {
            //AllocConsole();
            mainKeyIsDown = false;
            modKeyIsDown = false;
            _proc = HookCallback;
            _hookID = SetHook(_proc);
            _externAction = externalCallback;
            //Application.Run();
        }

        private IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private delegate IntPtr LowLevelKeyboardProc(
            int nCode, IntPtr wParam, IntPtr lParam);

        private IntPtr HookCallback(
            int nCode, IntPtr wParam, IntPtr lParam)
        {
            //Less than 0 is invalid and must be passed through.
            if (nCode >= 0)
            {
                //WARNING: This will probably be a mess.
                //TODO: Implement a better way to swap out shortcuts
                // - Will probably simply capture a keyevent and pass it into here/save in settings
                // - However, will still need special handling for Win Key
                if (wParam == (IntPtr)WM_KEYDOWN)
                {
                    int vkCode = Marshal.ReadInt32(lParam);

                    if (Settings.Default["Shortcut"].ToString() == ShortcutEnum.WinQ.ToString())
                    {
                        if ((Keys)vkCode == Keys.LWin)
                            modKeyIsDown = true;

                        if ((Keys)vkCode == Keys.Q)
                        {
                            mainKeyIsDown = true;

                            if (modKeyIsDown)
                            {
                                _externAction();
                                //stop propagation of key event
                                return (IntPtr)1;
                            }
                        }
                    }

                    else if (Settings.Default["Shortcut"].ToString() == ShortcutEnum.CtrlQ.ToString())
                    {
                        if ((Keys)vkCode == Keys.LControlKey)
                            modKeyIsDown = true;

                        if ((Keys)vkCode == Keys.Q)
                        {
                            mainKeyIsDown = true;

                            if (modKeyIsDown)
                            {
                                _externAction();
                                //stop propagation of key event
                                return (IntPtr)1;
                            }
                        }
                    }
                }
                    
                else if (wParam == (IntPtr)WM_KEYUP)
                {
                    int vkCode = Marshal.ReadInt32(lParam);

                    if (Settings.Default["Shortcut"].ToString() == ShortcutEnum.WinQ.ToString())
                    {
                        if ((Keys)vkCode == Keys.LWin)
                        {
                            modKeyIsDown = false;

                            if (mainKeyIsDown)
                            {
                                //need to do this to release windows key without opening start menu
                                InputSimulator sim = new InputSimulator();
                                sim.Keyboard.KeyDown(VirtualKeyCode.CONTROL);
                                sim.Keyboard.KeyUp(VirtualKeyCode.LWIN);
                                sim.Keyboard.KeyUp(VirtualKeyCode.CONTROL);
                                return (IntPtr)1;
                            }
                        }
                        else if ((Keys)vkCode == Keys.Q)
                            mainKeyIsDown = false;
                    }

                    else if (Settings.Default["Shortcut"].ToString() == ShortcutEnum.CtrlQ.ToString())
                    {
                        if ((Keys)vkCode == Keys.LControlKey)
                        {
                            modKeyIsDown = false;
                        }
                        else if ((Keys)vkCode == Keys.Q)
                            mainKeyIsDown = false;
                    }
                }
            }

            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        public void Dispose()
        {
            UnhookWindowsHookEx(_hookID);
            //FreeConsole();
        }
    }
}
