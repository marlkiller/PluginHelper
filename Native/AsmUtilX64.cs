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

        public void SUB_RSP(long addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "4883EC" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "4881EC" + intTohex(addre, 8);
            }
        }

        public void Mov_R9(long addre)
        {
            if (addre>Int32.MaxValue)
            {
                Asmcode = Asmcode + "49B9" + intTohex(addre, 16);
            }
            else
            {
                Asmcode = Asmcode + "41B9" + intTohex(addre, 8);
            }
        }

        public void Mov_R8(long addre)
        {
            if (addre>Int32.MaxValue)
            {
                Asmcode = Asmcode + "49B8" + intTohex(addre, 16);
            }
            else
            {
                Asmcode = Asmcode + "41B8" + intTohex(addre, 8);
            }        
        }

        public void Mov_RDX(long addre)
        {
            if (addre>Int32.MaxValue)
            {
                Asmcode = Asmcode + "48BA" + intTohex(addre, 16);
            }
            else
            {
                Mov_EDX(addre);
            }        
        }

        public void Mov_RCX(long addre)
        {
            if (addre>Int32.MaxValue)
            {
                Asmcode = Asmcode + "48B9" + intTohex(addre, 16);
            }
            else
            {
                Mov_ECX(addre);
            }  
        }

        public void Mov_RAX(long addre)
        {
            if (addre>Int32.MaxValue)
            {
                Asmcode = Asmcode + "48B8" + intTohex(addre, 16);
            }
            else
            {
                Mov_EAX(addre);
            }  
        }

        public void Call_RAX()
        {
            Asmcode = Asmcode + "FFD0";
        }

        public void ADD_RSP(long addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "4883C4" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "4881EC" + intTohex(addre, 8);
            }
            
        }

        public byte[] inBytes()
        {
            byte[] reAsmCode = new byte[Asmcode.Length / 2];

            for (int i = 0; i <= reAsmCode.Length - 1; i++) {
                reAsmCode[i] = Convert.ToByte(Int64.Parse(Asmcode.Substring(i * 2, 2), System.Globalization.NumberStyles.AllowHexSpecifier));
            }
            Asmcode = "";
            return reAsmCode;
        }
    }
}