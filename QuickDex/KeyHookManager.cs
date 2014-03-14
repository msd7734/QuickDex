using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;

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
        //ID for: 
        private const int WM_KEYDOWN = 0x0100;
        private LowLevelKeyboardProc _proc; 
        private IntPtr _hookID = IntPtr.Zero;

        private Util.VoidDelegate _externAction;

        public KeyHookManager(Util.VoidDelegate externalCallback)
        {
            //AllocConsole();
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
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                if ((Keys)vkCode == Keys.Q && Keys.Control == Control.ModifierKeys)
                {
                    _externAction();
                    return IntPtr.Zero;
                }
                //Console.WriteLine((Keys)vkCode);
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
