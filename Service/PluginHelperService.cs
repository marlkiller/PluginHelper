using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Web.UI.MobileControls;
using System.Windows.Forms;
using Binarysharp.Assemblers.Fasm;
using PluginHelper.Entity;
using PluginHelper.Native;
using TextBox = System.Windows.Forms.TextBox;

namespace PluginHelper.Service
{
    public class PluginHelperService
    {
        public IntPtr processId { get; set; }

        public List<ProcessEntity> refreshProcessList(string filterName)
        {
            List <ProcessEntity >  processEntities = new List<ProcessEntity>();
            Process[] processes = Process.GetProcesses();
         
            foreach (var process in processes)
            {
                try
                {

                    if (process.ProcessName=="System" || process.ProcessName=="Idle")
                    {
                        continue;
                    }
                    if (!string.IsNullOrEmpty(filterName))
                    {
                        if (!process.ProcessName.Contains(filterName))
                        {
                            continue;
                        }
                    }
                    var processEntity = new ProcessEntity();
                    processEntity.ProcessId = process.Id;
                    processEntity.ProcessFullPath = process.ProcessName;
                    processEntities.Add(processEntity);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }

            return processEntities;

        }

        public ProcessModuleCollection getModulesWithProcessId(int processId)
        {
            var processById = Process.GetProcessById(processId);

            return processById.Modules;
        }


        private IntPtr dllPathAddress;
        public bool dllInject(string dllPath)
        {
            // 用来获取目标进程句柄  (0x2 | 0x8 | 0x10 | 0x20 | 0x400)
            IntPtr openProcess = NativeMethods.OpenProcess(NativeMethods.PROCESS_ALL_ACCESS, false, processId);
            if (openProcess == IntPtr.Zero)
            {
                MessageBox.Show("OpenProcess 异常");
                return false;
            }
            
            // 在远程进程中为 sDllPath 分配内存
            dllPathAddress = NativeMethods.VirtualAllocEx(openProcess, (IntPtr)null, (IntPtr)dllPath.Length, NativeMethods.Commit, NativeMethods.ExecuteReadWrite);

            if (dllPathAddress == IntPtr.Zero)
            {
                MessageBox.Show("VirtualAllocEx 异常");
                return false;
            }
            
            byte[] buffer = new byte[dllPath.Length];
            int index = 0;
            foreach (char ch in dllPath)
            {
                buffer[index] = (byte)ch;
                index++;
            }
            Boolean writeBytes = MemUtil.WriteBytes(openProcess,dllPathAddress,buffer,dllPath.Length);
            if (!writeBytes)
            {
                MessageBox.Show("WriteProcessMemory 异常");
                return false;                    
            }
            IntPtr lpLLAddress = NativeMethods.GetProcAddress(NativeMethods.GetModuleHandle("kernel32.dll"), "LoadLibraryA");
            if (lpLLAddress == IntPtr.Zero)
            {
                MessageBox.Show("GetProcAddress 异常");
                return false;
            }
            // lpLLAddress 要执行的函数地址
            // lpAddress 参数地址
            var remoteThread = NativeMethods.CreateRemoteThread(openProcess, (IntPtr)null, (IntPtr)0, lpLLAddress, dllPathAddress, 0, (IntPtr)null); 
            if (remoteThread==(IntPtr)0)
            {
                MessageBox.Show("CreateRemoteThread 异常");
                return false;       
            }

            NativeMethods.WaitForSingleObject(remoteThread, 60 * 1000);    
            
            NativeMethods.CloseHandle(remoteThread);
            NativeMethods.CloseHandle(openProcess);
            
            return true;
        }

        public bool dllUnInject(string dllPath)
        {
            bool flag = false;
            var modEntry = new NativeMethods.MODULEENTRY32 {dwSize = (uint) Marshal.SizeOf(typeof(NativeMethods.MODULEENTRY32))};
            var snapshot = NativeMethods.CreateToolhelp32Snapshot(NativeMethods.SnapshotFlags.Module | NativeMethods.SnapshotFlags.Module32, processId);
            var module32First = NativeMethods.Module32First(snapshot, ref modEntry);
            
            if (module32First)
            {
                do {
                    if (modEntry.szModule.Equals(dllPath) || modEntry.szExePath.Equals(dllPath))
                    {
                        flag = true;
                        break;
                    }
                } while (NativeMethods.Module32Next(snapshot, ref modEntry));
            }
            if (!flag)
            {
                NativeMethods.CloseHandle(snapshot);
                MessageBox.Show("没有找到该模块");
                return false;
            }

            var openProcess = NativeMethods.OpenProcess(NativeMethods.PROCESS_ALL_ACCESS, false, processId);
            if (openProcess.Equals(IntPtr.Zero))
            {
                MessageBox.Show("openProcess.Equals(IntPtr.Zero)");
            }

 
            IntPtr lpLLAddress = NativeMethods.GetProcAddress(NativeMethods.GetModuleHandle("kernel32.dll"), "FreeLibrary");
            if (lpLLAddress == IntPtr.Zero)
            {
                MessageBox.Show("GetProcAddress 异常");
                return false;
            }
            
            var remoteThread = NativeMethods.CreateRemoteThread(openProcess, (IntPtr)null, (IntPtr)0, lpLLAddress, modEntry.modBaseAddr, 0, (IntPtr)null); 
            if (remoteThread==(IntPtr)0)
            {
                MessageBox.Show("CreateRemoteThread 异常");
                return false;
            }
            
            NativeMethods.WaitForSingleObject(remoteThread, 60 * 1000);        

            // 释放申请的内存
            NativeMethods.VirtualFreeEx( openProcess, dllPathAddress, (IntPtr)dllPath.Length, NativeMethods.Release );
            
            NativeMethods.CloseHandle(remoteThread);
            NativeMethods.CloseHandle(openProcess);
            if (flag)
            {
                NativeMethods.CloseHandle(snapshot);
            }

            return true;
        }

        public bool codeInject(string asmCodeStr)
        {
            
          
            return true;
        }

        public bool msgInject(string title, string value)
        {
              
            IntPtr openProcess = NativeMethods.OpenProcess(NativeMethods.PROCESS_ALL_ACCESS, false, processId);
            if (openProcess == IntPtr.Zero)
            {
                MessageBox.Show("OpenProcess 异常");
                return false;
            }
            
            IntPtr lpLLAddress = NativeMethods.GetProcAddress(NativeMethods.GetModuleHandle("user32.dll"), "MessageBoxA");
            MessageBox.Show(
                $@"lpAddress toString x : {lpLLAddress.ToString("x")}");
            
            
            var titleBytes = MemUtil.convertStr2ByteArr(title);
            var titleAddress = NativeMethods.VirtualAllocEx(openProcess, (IntPtr)null, (IntPtr)titleBytes.Length, NativeMethods.Commit, NativeMethods.ExecuteReadWrite);
            MemUtil.WriteBytes(openProcess,titleAddress,titleBytes,titleBytes.Length);

            var valueBytes = MemUtil.convertStr2ByteArr(value);
            var valueAddress = NativeMethods.VirtualAllocEx(openProcess, (IntPtr)null, (IntPtr)valueBytes.Length, NativeMethods.Commit, NativeMethods.ExecuteReadWrite);
            MemUtil.WriteBytes(openProcess,valueAddress,valueBytes,valueBytes.Length);

            // 32 位 MessageBoxA
            // 0C740000 | 6A 00                    | push 0                                  |
            // 0C740002 | 6A 00                    | push 0                                  |
            // 0C740004 | 6A 00                    | push 0                                  |
            // 0C740006 | 6A 00                    | push 0                                  |
            // 0C740008 | B8 60ED5D75              | mov eax,<user32.MessageBoxA>            |
            // 0C74000D | FFD0                     | call eax                                |
            // 0C74000F | C3                       | ret                                     |
            
            var fasmNet = new FasmNet();
            fasmNet.AddLine("use32");
            fasmNet.AddLine("push 0");
            fasmNet.AddLine("push {0}",titleAddress.ToInt32());
            fasmNet.AddLine("push {0}",valueAddress.ToInt32());
            fasmNet.AddLine("push 0");
            fasmNet.AddLine("mov eax,{0}" , lpLLAddress.ToInt32());
            fasmNet.AddLine("call eax");
            fasmNet.AddLine("ret");
            byte[] buffer = fasmNet.Assemble();
            
                
            // 64 位 MessageBoxA
            // 00007FFC6036AC30 | 48:83EC 38                           | sub rsp,38                              |
            
            
            // 000001A7F4690000 | 6A 00                                | push 0                                  |
            // 000001A7F4690002 | 6A 00                                | push 0                                  |
            // 000001A7F4690004 | 6A 00                                | push 0                                  |
            // 000001A7F4690006 | 6A 00                                | push 0                                  |
            // 000001A7F4690008 | 48:B8 30AC3660FC7F0000               | mov rax,<user32.MessageBoxA>            |
            // 000001A7F4690012 | FFD0                                 | call rax                                |
            // 000001A7F4690014 | 48:83C4 20                           | add rsp,20                              |
            // 000001A7F4690018 | C3                                   | ret                                     |
            // byte[] buffer = { 0x6A, 0x00, 
            //     0x6A, 0x00, 
            //     0x6A, 0x00, 
            //     0x6A, 0x00,
            // 0x48, 0xB8,0x30,0xAC,0x36,0x60,0xFC,0x7F,0x00,0x00,
            // 0xFF,0xD0,
            // 0x48,0x83,0xc4,0x20,0xC3};
            
            IntPtr lpAddress = NativeMethods.VirtualAllocEx(openProcess, (IntPtr)null, (IntPtr)buffer.Length, NativeMethods.Commit, NativeMethods.ExecuteReadWrite);
            if (lpAddress == IntPtr.Zero)
            {
                MessageBox.Show("VirtualAllocEx 异常");
                return false;
            }
            MessageBox.Show(
                $@"lpAddress toString x : {lpAddress.ToString("x")}");

            
            Boolean writeBytes = MemUtil.WriteBytes(openProcess,lpAddress,buffer,buffer.Length);
            if (!writeBytes)
            {
                MessageBox.Show("WriteProcessMemory 异常");
                return false;                    
            }
            MessageBox.Show("WriteProcessMemory");

            // lpLLAddress 要执行的函数地址
            // lpAddress 参数地址
            var remoteThread = NativeMethods.CreateRemoteThread(openProcess, (IntPtr)null, (IntPtr)0, lpAddress, (IntPtr)0, 0, (IntPtr)null); 
            if (remoteThread==(IntPtr)0)
            {
                MessageBox.Show("CreateRemoteThread 异常");
                return false;       
            }

            NativeMethods.WaitForSingleObject(remoteThread, 60 * 1000);    
            
            NativeMethods.VirtualFreeEx( openProcess, lpAddress, (IntPtr)buffer.Length, NativeMethods.Release );
            NativeMethods.VirtualFreeEx( openProcess, titleAddress, (IntPtr)titleBytes.Length, NativeMethods.Release );
            NativeMethods.VirtualFreeEx( openProcess, valueAddress, (IntPtr)valueBytes.Length, NativeMethods.Release );
            
            NativeMethods.CloseHandle(remoteThread);
            NativeMethods.CloseHandle(openProcess);
            return true;

        }
    }
}