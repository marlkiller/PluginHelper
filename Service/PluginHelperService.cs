using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using PluginHelper.Entity;
using PluginHelper.Handler;
using PluginHelper.Native;
using TextBox = System.Windows.Forms.TextBox;

namespace PluginHelper.Service
{
    public class PluginHelperService
    {
        public event EventHandler<LogEventHandler> logEventHandler;

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

            logEventHandler(this,new LogEventHandler($@"user32.dll >> MessageBoxA lpLLAddress : {lpLLAddress.ToString("x")}"));
            
            var titleBytes = MemUtil.convertStr2ByteArr(title);
            var titleAddress = NativeMethods.VirtualAllocEx(openProcess, (IntPtr)null, (IntPtr)titleBytes.Length, NativeMethods.Commit, NativeMethods.ExecuteReadWrite);
            MemUtil.WriteBytes(openProcess,titleAddress,titleBytes,titleBytes.Length);
            logEventHandler(this,new LogEventHandler("titleAddress >> " + titleAddress.ToString("x")));

            var valueBytes = MemUtil.convertStr2ByteArr(value);
            var valueAddress = NativeMethods.VirtualAllocEx(openProcess, (IntPtr)null, (IntPtr)valueBytes.Length, NativeMethods.Commit, NativeMethods.ExecuteReadWrite);
            MemUtil.WriteBytes(openProcess,valueAddress,valueBytes,valueBytes.Length);
            logEventHandler(this,new LogEventHandler("valueAddress >> " + valueAddress.ToString("x")));

            byte[] buffer;
            if (NativeMethods.Is64BitProcess())
            {
                buffer = buffer64(lpLLAddress,titleAddress,valueAddress);
            }
            else
            {
                buffer = buffer32(lpLLAddress,titleAddress,valueAddress);
            }
            IntPtr lpAddress = NativeMethods.VirtualAllocEx(openProcess, (IntPtr)null, (IntPtr)buffer.Length, NativeMethods.Commit, NativeMethods.ExecuteReadWrite);
            
            if (lpAddress == IntPtr.Zero)
            {
                MessageBox.Show("VirtualAllocEx 异常");
                return false;
            }
        
            Boolean writeBytes = MemUtil.WriteBytes(openProcess,lpAddress,buffer,buffer.Length);
            if (!writeBytes)
            {
                MessageBox.Show("WriteProcessMemory 异常");
                return false;                    
            }
            logEventHandler(this,new LogEventHandler("lpAddress >> " + lpAddress.ToString("x")));
            MessageBox.Show("WriteProcessMemory!!!!");

            // lpLLAddress 要执行的函数地址
            // lpAddress 参数地址
            var remoteThread = NativeMethods.CreateRemoteThread(openProcess, (IntPtr)null, (IntPtr)0, lpAddress, (IntPtr)0, 0, (IntPtr)null); 
            if (remoteThread==(IntPtr)0)
            {
                MessageBox.Show("CreateRemoteThread 异常");
                return false;       
            }

            NativeMethods.WaitForSingleObject(remoteThread, 60 * 1000);    
            
            NativeMethods.VirtualFreeEx( openProcess, lpAddress, (IntPtr)0, NativeMethods.Release );
            NativeMethods.VirtualFreeEx( openProcess, titleAddress, (IntPtr)0, NativeMethods.Release );
            NativeMethods.VirtualFreeEx( openProcess, valueAddress, (IntPtr)0, NativeMethods.Release );
            
            NativeMethods.CloseHandle(remoteThread);
            NativeMethods.CloseHandle(openProcess);
            
            logEventHandler(this,new LogEventHandler("inject code over ~"));

            return true;

        }

        private static byte[] buffer64(IntPtr lpLLAddress,IntPtr titleAddress,IntPtr valueAddress)
        {
            # region 64 位 MessageBoxA
            ArrayList asmList = new ArrayList(); 

            // 00007FF95E46AC20 <user32.Mes | 48:83EC 38               | sub rsp,38                              | MessageBoxA 函数头
            // push all
            // 000001D52AC60019             | 48:83EC 20               | sub rsp,20                              |
            // 000001D52AC6001D             | 49:C7C1 00000000         | mov r9,0                                |
            // 000001D52AC60024             | 41:51                    | push r9                                 |
            // 000001D52AC60026             | 49:B8 0000C42AD5010000   | mov r8,1D52AC40000                      | 1D52AC40000:"this is title"
            // 000001D52AC60030             | 41:50                    | push r8                                 |
            // 000001D52AC60032             | 48:BA 0000C52AD5010000   | mov rdx,1D52AC50000                     | 1D52AC50000:"this is value"
            // 000001D52AC6003C             | 52                       | push rdx                                |
            // 000001D52AC6003D             | 49:C7C1 00000000         | mov r9,0                                |
            // 000001D52AC60044             | 51                       | push rcx                                |
            // 000001D52AC60045             | 48:B8 20AC465EF97F0000   | mov rax,<user32.MessageBoxA>            |
            // 000001D52AC6004F             | FFD0                     | call rax                                |
            // 000001D52AC60051             | 48:83C4 40               | add rsp,40                              |
            // pop all
            // 000001D52AC6006E             | C3                       | ret                                     |
            
            MemUtil.appendAll(asmList, new byte[]
            {
                /*push all*/
                0X9C,0X54,0X50,0X51,0X52,0X53,0X55,0X56,0X57,0X41,0X50,0X41,0X51,0X41,0X52,0X41,0X53,0X41,0X54,0X41,0X55,0X41,0X56,0X41,0X57,
                
                0X48, 0X83, 0XEC, 0X20,
                
                0X49,0XC7,0XC1,0X00,0X00,0X00,0X00,
                
                0x41, 0x51
            });
            
            MemUtil.appendAll(asmList, new byte[]
            {
                0x49,0xB8
            });
            MemUtil.appendAll(asmList, MemUtil.AsmChangebytes(MemUtil.intTohex(titleAddress.ToInt64(), 16)));
            
            MemUtil.appendAll(asmList, new byte[]
            {
                0x41,0x50
            });
            MemUtil.appendAll(asmList, new byte[]
            {
                0x48,0xBA
            });
            MemUtil.appendAll(asmList, MemUtil.AsmChangebytes(MemUtil.intTohex(valueAddress.ToInt64(), 16)));
            MemUtil.appendAll(asmList, new byte[]
            {
                0x52
            });
            MemUtil.appendAll(asmList, new byte[]
            {
                0X49,0XC7,0XC1,0X00,0X00,0X00,0X00,
                
                0x51,
                0x48, 0xB8
            });
            MemUtil.appendAll(asmList, MemUtil.AsmChangebytes(MemUtil.intTohex(lpLLAddress.ToInt64(), 16)));

            MemUtil.appendAll(asmList, new byte[]
            {
                0xFF, 0xD0,
                0X48, 0X83, 0XC4, 0X40,
                /* pop all*/
                0X41,0X5F,0X41,0X5E,0X41,0X5D,0X41,0X5C,0X41,0X5B,0X41,0X5A,0X41,0X59,0X41,0X58,0X5F,0X5E,0X5D,0X5B,0X5A,0X59,0X58,0X5C,0X9D,
                0xC3
            });

            # endregion

            byte[] buffer = asmList.ToArray(typeof(byte)) as byte[];
            return buffer;
        }
        
        private static byte[] buffer32(IntPtr lpLLAddress,IntPtr titleAddress,IntPtr valueAddress)
        {
            # region 32 位 MessageBoxA

            ArrayList asmList = new ArrayList();
            
            // 755DED60 | 8BFF                     | mov edi,edi                             | MessageBoxA 函数头
                
            // pushad
            // 055D0000 | 60                       | pushad                                  |
            // 055D0001 | 6A 00                    | push 0                                  |
            // 055D0003 | B8 00003B05              | mov eax,53B0000                         | 53B0000:"this is title"
            // 055D0008 | 50                       | push eax                                |
            // 055D0009 | B8 00005C05              | mov eax,55C0000                         | 55C0000:"this is value"
            // 055D000E | 50                       | push eax                                |
            // 055D000F | 6A 00                    | push 0                                  |
            // 055D0011 | B8 70ECBD77              | mov eax,<user32.MessageBoxA>            |
            // 055D0016 | FFD0                     | call eax                                |
            // 055D0018 | 61                       | popad                                   |
            // 055D0019 | C3                       | ret                                     |
            // popad
            
            MemUtil.appendAll(asmList,new byte[]{ 
                0x60,0x6A, 0x00
            });
            MemUtil.appendAll(asmList,new byte[]{ 
                0xB8
            });
            MemUtil.appendAll(asmList,MemUtil.AsmChangebytes(MemUtil.intTohex(titleAddress.ToInt32(), 8)));
            MemUtil.appendAll(asmList,new byte[]{ 
                0x50
            });
            MemUtil.appendAll(asmList,new byte[]{ 
                0xB8
            });
            MemUtil.appendAll(asmList,MemUtil.AsmChangebytes(MemUtil.intTohex(valueAddress.ToInt32(), 8)));
            MemUtil.appendAll(asmList,new byte[]{ 
                0x50
            });
            MemUtil.appendAll(asmList,new byte[]{ 
               
                0x6A, 0x00,
                0xB8,
            });
            MemUtil.appendAll(asmList,MemUtil.AsmChangebytes(MemUtil.intTohex(lpLLAddress.ToInt32(), 8)));
            MemUtil.appendAll(asmList,new byte[]{
                0xFF, 0xD0,
                0x61,
                0xC3
            });
            # endregion

            byte[] buffer = asmList.ToArray(typeof(byte)) as byte[];
            return buffer;
        }
    }
}