using System;
using System.Collections;
using System.Text;

namespace PluginHelper.Native
{
    public class AsmUtilX64 : AsmUtilX86
    {
        
        public void Pushall()
        {
            Asmcode += "9C545051525355565741504151415241534154415541564157";
        }
        public void Popall()
        {
            Asmcode += "415F415E415D415C415B415A415941585F5E5D5B5A59585C9D";
        }

        public void SUB_RSP(long address)
        {
            Asmcode = Asmcode + "48";
            SUB_ESP(address);
        }

        public void Mov_R9(long address)
        {
            if (is64(address))
            {
                Asmcode = Asmcode + "49B9" + intTohex(address, 16);
            }
            else
            {
                Asmcode = Asmcode + "41B9" + intTohex(address, 8);
            }
        }

        public void Mov_R8(long address)
        {
            if (is64(address))
            {
                Asmcode = Asmcode + "49B8" + intTohex(address, 16);
            }
            else
            {
                Asmcode = Asmcode + "41B8" + intTohex(address, 8);
            }        
        }

        public void Mov_RDX(long address)
        {
            if (is64(address))
            {
                Asmcode = Asmcode + "48BA" + intTohex(address, 16);
            }
            else
            {
                Mov_EDX(address);
            }        
        }

        public void Mov_RCX(long address)
        {
            if (is64(address))
            {
                Asmcode = Asmcode + "48B9" + intTohex(address, 16);
            }
            else
            {
                Mov_ECX(address);
            }  
        }

        public void Mov_RAX(long address)
        {
            
            if (is64(address))
            {
                Asmcode = Asmcode + "48B8" + intTohex(address, 16);
            }
            else
            {
                Mov_EAX(address);
            }  
        }

        public void Call_RAX()
        {
            Call_EAX();
        }

        public void ADD_RSP(long address)
        {
            Asmcode = Asmcode + "48";
            Add_ESP(address);
        }

        public new byte[] inBytes()
        {
            byte[] reAsmCode = new byte[Asmcode.Length / 2];

            for (int i = 0; i <= reAsmCode.Length - 1; i++) {
                reAsmCode[i] = Convert.ToByte(Int64.Parse(Asmcode.Substring(i * 2, 2), System.Globalization.NumberStyles.AllowHexSpecifier));
            }
            Asmcode = "";
            return reAsmCode;
        }
        
        private static bool is64(long address)
        {
            return !(address <= Int32.MaxValue) && (address >= Int32.MinValue);
        }
    }
}