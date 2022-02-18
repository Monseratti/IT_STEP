using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;
using System.Reflection;
using System.Management;
using System.Windows.Forms;


namespace SP_LAB2
{
    public partial class Form1 : Form
    {
        List<Process> processes = new List<Process>();
        int count = 0;
        public const int WM_SETTEXT = 0xC;
        public delegate void ProcessDelegate(Process process);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        private void LoadAvailableAssemblies()
        {
            availableProcesses.Items.Clear();
            string except = Path.GetFileNameWithoutExtension(Application.ExecutablePath);
            foreach (string fileName in Directory.GetFiles(Application.StartupPath, "*.exe"))
            {
                string shortName = new FileInfo(fileName).Name;
                if (!shortName.Contains(except))
                {
                    availableProcesses.Items.Add(Path.GetFileNameWithoutExtension(shortName));
                }
            }
        }
        public Form1()
        {
            InitializeComponent();
            LoadAvailableAssemblies();
        }
        private void RunProcess(string AssemblyName)
        {
            Process process = Process.Start(AssemblyName);
            processes.Add(process);
            process.EnableRaisingEvents = true;
            process.Exited += Process_Exited;
            if (Process.GetCurrentProcess().Id == GetParentProcessId(process.Id))
                MessageBox.Show($"Process {process.Id} is a child for {Process.GetCurrentProcess().Id}");
            if (process != null)
            {
                SendMessage(process.MainWindowHandle, WM_SETTEXT, 0, $"Дочернее приложение №{++count}");
                if (!executingProcesses.Items.Contains(AssemblyName))
                {
                    executingProcesses.Items.Add(AssemblyName);
                    availableProcesses.Items.Remove(AssemblyName);
                }
            }
        }
        private int GetParentProcessId(int id)
        {
            int parentId = 0;
            using (ManagementObject obj = new ManagementObject($"Win32_Process.Handle={id}"))
            {
                obj.Get();
                parentId = int.Parse(obj["ParentProcessId"].ToString());
            };
            return parentId;
        }
        private void Process_Exited(object sender, EventArgs e)
        {
            Process process = sender as Process;
            processes.Remove(process);
            for (int i = 1; i < processes.Count; i++)
            {
                SendMessage(processes[i].MainWindowHandle, WM_SETTEXT, 0, $"Дочернее приложение №{i}");
            }
        }
        private void Kill(Process process)
        {
            process.Kill();
        }
        private void CloseWindow(Process process)
        {
            process.CloseMainWindow();
        }
        private void Refresh(Process process)
        {
            process.Refresh();
        }

        private void ExecuteOnProcessByName(string ProcessName, ProcessDelegate func)
        {
            foreach (Process process in Process.GetProcessesByName(ProcessName))
            {
                if (Process.GetCurrentProcess().Id == GetParentProcessId(process.Id))
                {
                    func(process);
                }
            }
        }

        private void Start_Click(object sender, EventArgs e)
        {
            if (availableProcesses.SelectedIndex != -1)
            {
                RunProcess(availableProcesses.SelectedItem.ToString());
            }
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            if (executingProcesses.SelectedIndex != -1)
            {
                ExecuteOnProcessByName(executingProcesses.SelectedItem.ToString(), Kill);
                availableProcesses.Items.Add(executingProcesses.SelectedItem);
                executingProcesses.Items.Remove(executingProcesses.SelectedItem);
            }
        }

        private void CloseWindow_Click(object sender, EventArgs e)
        {
            if (executingProcesses.SelectedIndex != -1)
            {
                ExecuteOnProcessByName(executingProcesses.SelectedItem.ToString(), CloseWindow);
                availableProcesses.Items.Add(executingProcesses.SelectedItem);
                executingProcesses.Items.Remove(executingProcesses.SelectedItem);
            }
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            if (executingProcesses.SelectedIndex != -1)
            {
                ExecuteOnProcessByName(executingProcesses.SelectedItem.ToString(), Refresh);
            }
        }

        private void RunNotepad_Click(object sender, EventArgs e)
        {
            RunProcess("notepad.exe");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (Process p in processes)
            {
                p.CloseMainWindow();
                p.Close();
                //p.Kill();
            }
        }
    }
}
