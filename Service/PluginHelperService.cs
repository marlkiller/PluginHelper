using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using PluginHelper.Entity;
using PluginHelper.Handler;
using PluginHelper.Native;

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
            var titleBytes = MemUtil.convertStr2ByteArr(title);
            var titleAddress = NativeMethods.VirtualAllocEx(openProcess, (IntPtr)null, (IntPtr)titleBytes.Length, NativeMethods.Commit, NativeMethods.ExecuteReadWrite);
            MemUtil.WriteBytes(openProcess,titleAddress,titleBytes,titleBytes.Length);
            ConsoleHelper.WriteLine(Level.DEBUG, ("titleAddress >> " + titleAddress.ToString("x")));

            var valueBytes = MemUtil.convertStr2ByteArr(value);
            var valueAddress = NativeMethods.VirtualAllocEx(openProcess, (IntPtr)null, (IntPtr)valueBytes.Length, NativeMethods.Commit, NativeMethods.ExecuteReadWrite);
            MemUtil.WriteBytes(openProcess,valueAddress,valueBytes,valueBytes.Length);
            ConsoleHelper.WriteLine(Level.DEBUG, ("valueAddress >> " + valueAddress.ToString("x")));
            
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
            ConsoleHelper.WriteLine(Level.DEBUG, ("lpAddress >> " + lpAddress.ToString("x")));
            Clipboard.SetText(lpAddress.ToString("x"));
            MessageBox.Show("WriteProcessMemory!!!!");

            // lpLLAddress 要执行的函数地址
            // lpAddress 代码注入地址
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
            
            ConsoleHelper.WriteLine(Level.DEBUG, ("inject code over ~"));

            return true;

        }

        private byte[] buffer64(IntPtr lpLLAddress,IntPtr titleAddress,IntPtr valueAddress)
        {
            var asmUtilX64 = new AsmUtilX64();
            asmUtilX64.Pushall();
            asmUtilX64.SUB_RSP(0X30);
            asmUtilX64.Mov_R9(0x0);
            asmUtilX64.Mov_R8(titleAddress.ToInt64());
            asmUtilX64.Mov_RDX(valueAddress.ToInt64());
            asmUtilX64.Mov_RCX(0x0);
            asmUtilX64.Mov_RAX(lpLLAddress.ToInt64());
            asmUtilX64.Call_RAX();
            asmUtilX64.ADD_RSP(0x30);
            asmUtilX64.Popall();
            asmUtilX64.Ret();

            ConsoleHelper.WriteLine(Level.DEBUG, ($"Shell Code : {asmUtilX64.formatAsmCode()}"));

            return asmUtilX64.inBytes();
            
            
            # region 64 位 MessageBoxA
            ArrayList asmList = new ArrayList(); 

            // 这里的堆栈指针操作, 这么理解
            // 而在x64汇编中，两方面都发生了变化。一是前四个参数分析通过四个寄存器传递：RCX、RDX、R8、R9，如果还有更多的参数，才通过椎栈传递。二是调用者负责椎栈空间的分配与回收。
            // 栈要按照16字节对齐,当我们在 Call函数的时候.返回地址会入栈.
            // push 了 4个参数, = sub rsp,4*(4*2) = 32 = 0x20 ,
            // 加上我们call函数用了一个 eax,  32+8 = 40 =0x28
            // 40 + 8 (函数返回地址 ) = 48 = 0x30
            
            //  00007FFC6036AC30 | 48:83EC 38                           | sub rsp,38                              | MessageBoxA函数头
            // push all
            // 0000019036990019 | 48:83EC 30                           | sub rsp,30                              |
            // 000001903699001D | 41:B9 00000000                       | mov r9d,0                               |
            // 0000019036990023 | 49:B8 0000973690010000               | mov r8,19036970000                      | 19036970000:"this is title"
            // 000001903699002D | 48:BA 0000983690010000               | mov rdx,19036980000                     | 19036980000:"this is value"
            // 0000019036990037 | B9 00000000                          | mov ecx,0                               |
            // 000001903699003C | 48:B8 30AC3660FC7F0000               | mov rax,<user32.MessageBoxA>            |
            // 0000019036990046 | FFD0                                 | call rax                                |
            // 0000019036990048 | 48:83C4 30                           | add rsp,30                              |
            // pop all
            // 0000019036990065 | C3                                   | ret                                     |
            
            MemUtil.appendAll(asmList, new byte[]
            {
                /*push all*/
                0X9C,0X54,0X50,0X51,0X52,0X53,0X55,0X56,0X57,0X41,0X50,0X41,0X51,0X41,0X52,0X41,0X53,0X41,0X54,0X41,0X55,0X41,0X56,0X41,0X57,
                
                0X48,0X83,0XEC,0X30,
                
                0X41,0XB9,0X00,0X00,0X00,0X00
                
            });
            
            MemUtil.appendAll(asmList, new byte[]
            {
                0x49,0xB8
            });
            MemUtil.appendAll(asmList, MemUtil.AsmChangebytes(MemUtil.intTohex(titleAddress.ToInt64(), 16)));
            
           
            MemUtil.appendAll(asmList, new byte[]
            {
                0x48,0xBA
            });
            MemUtil.appendAll(asmList, MemUtil.AsmChangebytes(MemUtil.intTohex(valueAddress.ToInt64(), 16)));
          
            MemUtil.appendAll(asmList, new byte[]
            {
                0XB9,0X00,0X00,0X00,0X00,
                
                0x48, 0xB8
            });
            MemUtil.appendAll(asmList, MemUtil.AsmChangebytes(MemUtil.intTohex(lpLLAddress.ToInt64(), 16)));

            MemUtil.appendAll(asmList, new byte[]
            {
                0xFF, 0xD0,
                0X48, 0X83, 0XC4, 0X30,
                /* pop all*/
                0X41,0X5F,0X41,0X5E,0X41,0X5D,0X41,0X5C,0X41,0X5B,0X41,0X5A,0X41,0X59,0X41,0X58,0X5F,0X5E,0X5D,0X5B,0X5A,0X59,0X58,0X5C,0X9D,
                0xC3
            });
            # endregion

            byte[] buffer = asmList.ToArray(typeof(byte)) as byte[];
            return buffer;
        }
        
        private byte[] buffer32(IntPtr lpLLAddress,IntPtr titleAddress,IntPtr valueAddress)
        {
            
            
            var asmUtilX86 = new AsmUtilX86();
            asmUtilX86.Pushad();
            asmUtilX86.Push6A(0);
            asmUtilX86.Mov_EAX(titleAddress.ToInt32());
            asmUtilX86.Push_EAX();
            asmUtilX86.Mov_EAX(valueAddress.ToInt32());
            asmUtilX86.Push_EAX();
            asmUtilX86.Push6A(0);
            asmUtilX86.Mov_EAX(lpLLAddress.ToInt32());
            asmUtilX86.Call_EAX();
            asmUtilX86.Popad();
            asmUtilX86.Ret();

            ConsoleHelper.WriteLine(Level.DEBUG, ($"Shell Code : {asmUtilX86.formatAsmCode()}"));

            return asmUtilX86.inBytes();
            
            
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

        private IGraph gdiGraphImpl;
        
        public void drawGDI(int type)
        {
            if (gdiGraphImpl != null) return;
            switch (type)
            {
                case 1 :
                    gdiGraphImpl = new GdiGraphImpl();
                    break;
                case 2 :
                    gdiGraphImpl = new GdiGraphWithBufferImpl();
                    break;
                case 3 :
                    gdiGraphImpl = new GdiGraphWithBufferNetImpl();
                    break;
                default:
                    return;
            }
            gdiGraphImpl.init(processId);
            gdiGraphImpl.start();
        }

        public void stopGdi()
        {
            if (gdiGraphImpl == null) return;
            gdiGraphImpl.destory();
            gdiGraphImpl = null;
        }
    }
}