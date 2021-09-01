using System;
using System.Collections;

namespace PluginHelper.Native
{
    public class ASMUtil64
    {
        private ArrayList asmList;

        private ASMUtil64(ArrayList asmList)
        {
            this.asmList = asmList;
        }
        
        // 0000024231300000             | 48:83EC 20               | sub rsp,20                              |
        // 0000024231300004             | 41:51                    | push r9                                 |
        // 0000024231300006             | 49:B8 00002E3142020000   | mov r8,242312E0000                      | 242312E0000:"this is title"
        // 0000024231300010             | 41:50                    | push r8                                 |
        // 0000024231300012             | 48:BA 00002F3142020000   | mov rdx,242312F0000                     | 242312F0000:"this is value"
        // 000002423130001C             | 52                       | push rdx                                |
        // 000002423130001D             | 51                       | push rcx                                |
        // 000002423130001E             | 48:B8 20AC465EF97F0000   | mov rax,<user32.MessageBoxA>            |
        // 0000024231300028             | FFD0                     | call rax                                |
        // 000002423130002A             | 48:83C4 40               | add rsp,40                              |
        // 000002423130002E             | C3                       | ret                                     |
        public static ASMUtil64 buildASM()
        {
            return new ASMUtil64(new ArrayList());
        }
        
        public ASMUtil64 sub_rsp_()
        {
            asmList.Add(0x48);
            asmList.Add(0x83);
            asmList.Add(0xEC);
            // TODO
            return this;
        }


        public ASMUtil64 push_r9()
        {
            asmList.Add(0x41);
            asmList.Add(0x51);
            return this;
        }
        
        

    }
}