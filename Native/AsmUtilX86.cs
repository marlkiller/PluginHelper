using System;
using System.Text;

namespace PluginHelper.Native
{
    public class AsmUtilX86
    {
        public string Asmcode = "";

        public string hexUint(uint address)
        {
            string str = address.ToString("X");
            return str;
        }
        private string hex(long address)
        {
            string str = address.ToString("X");
            return str;
        }

        public string uintTohex(uint value, int num)
        {
            string str1 = null;
            string str2 = "";
            str1 = "0000000" + hexUint(value);
            str1 = str1.Substring(str1.Length - num, num);
            for (int i = 0; i <= str1.Length / 2 - 1; i++) {
                str2 = str2 + str1.Substring(str1.Length - 2 - 2 * i, 2);
            }
            return str2;
        }
        public string intTohex(long value, int num)
        {
            string str1 = null;
            string str2 = "";
            str1 = "0000000" + hex(value);
            str1 = str1.Substring(str1.Length - num, num);
            for (int i = 0; i <= str1.Length / 2 - 1; i++)
            {
                str2 = str2 + str1.Substring(str1.Length - 2 - 2 * i, 2);
            }
            return str2;
        }

        public string IntToHexA(int number)
        {
            return String.Format("{0:x}", number);
        }


        public void SUB_ESP(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "83EC" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "81EC" + intTohex(addre, 8);
            }
        }

        #region "ADD"
        public void Add_EAX_EDX()
        {
            Asmcode = Asmcode + "03C2";
        }

        public void Add_EBX_EAX()
        {
            Asmcode = Asmcode + "03D8";
        }

        public void Add_EAX_DWORD_Ptr(int addre)
        {
            Asmcode = Asmcode + "0305" + intTohex(addre, 8);
        }

        public void Add_EBX_DWORD_Ptr(int addre)
        {
            Asmcode = Asmcode + "031D" + intTohex(addre, 8);
        }

        public void Add_EBP_DWORD_Ptr(int addre)
        {
            Asmcode = Asmcode + "032D" + intTohex(addre, 8);
        }

        public void Add_EAX(int addre)
        {
            Asmcode = Asmcode + "05" + intTohex(addre, 8);
        }

        public void Add_EBX(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "83C3" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "81C3" + intTohex(addre, 8);
            }
        }

        public void Add_ECX(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "83C1" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "81C1" + intTohex(addre, 8);
            }
        }

        public void Add_EDX(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "83C2" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "81C2" + intTohex(addre, 8);
            }
        }

        public void Add_ESI(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "83C6" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "81C6" + intTohex(addre, 8);
            }
        }

        public void Add_ESP(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "83C4" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "81C4" + intTohex(addre, 8);
            }
        }
        #endregion

        public void Nop()
        {
            Asmcode = Asmcode + "90";
        }

        public void RetA(int addre)
        {
            Asmcode = Asmcode + intTohex(addre, 4);
        }

        public void IN_AL_DX()
        {
            Asmcode = Asmcode + "EC";
        }

        public void TEST_EAX_EAX()
        {
            Asmcode = Asmcode + "85C0";
        }

        public void Leave()
        {
            Asmcode = Asmcode + "C9";
        }

        public void Pushad()
        {
            Asmcode = Asmcode + "60";
        }

        public void Int3()
        {
            Asmcode = Asmcode + "CC";
        }

        #region "mov"

        public void Mov_DWORD_Ptr_EAX_ADD(int addre, int addre1)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "C740" + intTohex(addre, 2) + intTohex(addre1, 8);
            }
            else
            {
                Asmcode = Asmcode + "C780" + intTohex(addre, 8) + intTohex(addre1, 8);
            }
        }

        public void Mov_DWORD_Ptr_ESP_ADD(int addre, int addre1)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "C74424" + intTohex(addre, 2) + intTohex(addre1, 8);
            }
            else
            {
                Asmcode = Asmcode + "C78424" + intTohex(addre, 8) + intTohex(addre1, 8);
            }
        }

        public void Mov_DWORD_Ptr_ESP_ADD_EAX(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "894424" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "898424" + intTohex(addre, 8);
            }
        }

        public void Mov_DWORD_Ptr_ESI_ADD_EAX(int addre)
        {
            if ((addre <= 127) && (addre >= -128)) {
                Asmcode = Asmcode + "8946" + intTohex(addre, 2);
            }
            else {
                Asmcode = Asmcode + "8986" + intTohex(addre, 8);
            }
        }

        public void Mov_DWORD_Ptr_ECX_ADD_EAX(int addre)
        {
            if ((addre <= 127) && (addre >= -128)) {
                Asmcode = Asmcode + "8941" + intTohex(addre, 2);
            }
            else {
                Asmcode = Asmcode + "8981" + intTohex(addre, 8);
            }
        }

        public void Mov_DWORD_Ptr_ESP(int addre)
        {
            Asmcode = Asmcode + "C70424" + intTohex(addre, 8);
        }

        public void Mov_DWORD_Ptr_EAX(int addre)
        {
            Asmcode = Asmcode + "A3" + intTohex(addre, 8);
        }

        public void Mov_DWORD_Ptr_EDX_EAX()
        {
            Asmcode = Asmcode + "8902";
        }

        public void Mov_EBX_DWORD_Ptr(int addre)
        {
            Asmcode = Asmcode + "8B1D" + intTohex(addre, 8);
        }

        public void Mov_ECX_DWORD_Ptr(int addre)
        {
            Asmcode = Asmcode + "8B0D" + intTohex(addre, 8);
        }

        public void Mov_EAX_DWORD_Ptr(int addre)
        {
            Asmcode = Asmcode + "A1" + intTohex(addre, 8);
        }

        public void Mov_EDX_DWORD_Ptr(int addre)
        {
            Asmcode = Asmcode + "8B15" + intTohex(addre, 8);
        }

        public void Mov_ESI_DWORD_Ptr(int addre)
        {
            Asmcode = Asmcode + "8B35" + intTohex(addre, 8);
        }

        public void Mov_ESP_DWORD_Ptr(int addre)
        {
            Asmcode = Asmcode + "8B25" + intTohex(addre, 8);
        }

        public void Mov_EBP_DWORD_Ptr(int addre)
        {
            Asmcode = Asmcode + "8B2D" + intTohex(addre, 8);
        }

        public void Mov_EAX_DWORD_Ptr_EAX(int addre)
        {
            Asmcode = Asmcode + "8B00";
        }

        public void Mov_EAX_DWORD_Ptr_EAX()
        {
            Asmcode = Asmcode + "8B00";
        }

        public void Mov_EAX_DWORD_Ptr_EBP()
        {
            Asmcode = Asmcode + "8B4500";
        }

        public void Mov_EAX_DWORD_Ptr_EBX()
        {
            Asmcode = Asmcode + "8B03";
        }

        public void Mov_EAX_DWORD_Ptr_ECX()
        {
            Asmcode = Asmcode + "8B01";
        }

        public void Mov_EAX_DWORD_Ptr_EDX()
        {
            Asmcode = Asmcode + "8B02";
        }

        public void Mov_EAX_DWORD_Ptr_EDI()
        {
            Asmcode = Asmcode + "8B07";
        }

        public void Mov_EAX_DWORD_Ptr_ESP()
        {
            Asmcode = Asmcode + "8B0424";
        }

        public void Mov_EAX_DWORD_Ptr_ESI()
        {
            Asmcode = Asmcode + "8B06";
        }

        public void Mov_EAX_DWORD_Ptr_EAX_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8B40" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8B80" + intTohex(addre, 8);
            }
        }

        public void Mov_EAX_DWORD_Ptr_ESP_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8B4424" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8B8424" + intTohex(addre, 8);
            }
        }

        public void Mov_EAX_DWORD_Ptr_EBX_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8B43" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8B83" + intTohex(addre, 8);
            }
        }

        public void Mov_EAX_DWORD_Ptr_ECX_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8B41" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8B81" + intTohex(addre, 8);
            }
        }

        public void Mov_EAX_DWORD_Ptr_EDX_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8B42" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8B82" + intTohex(addre, 8);
            }
        }

        public void Mov_EAX_DWORD_Ptr_EDI_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8B47" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8B87" + intTohex(addre, 8);
            }
        }

        public void Mov_EAX_DWORD_Ptr_EBP_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8B45" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8B85" + intTohex(addre, 8);
            }
        }

        public void Mov_EAX_DWORD_Ptr_ESI_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8B46" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8B86" + intTohex(addre, 8);
            }
        }

        public void Mov_EBX_DWORD_Ptr_EAX_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8B58" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8B98" + intTohex(addre, 8);
            }
        }

        public void Mov_EBX_DWORD_Ptr_ESP_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8B5C24" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8B9C24" + intTohex(addre, 8);
            }
        }

        public void Mov_EBX_DWORD_Ptr_EBX_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8B5B" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8B9B" + intTohex(addre, 8);
            }
        }

        public void Mov_EBX_DWORD_Ptr_ECX_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8B59" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8B99" + intTohex(addre, 8);
            }
        }

        public void Mov_EBX_DWORD_Ptr_EDX_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8B5A" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8B9A" + intTohex(addre, 8);
            }
        }

        public void Mov_EBX_DWORD_Ptr_EDI_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8B5F" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8B9F" + intTohex(addre, 8);
            }
        }

        public void Mov_EBX_DWORD_Ptr_EBP_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8B5D" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8B9D" + intTohex(addre, 8);
            }
        }

        public void Mov_EBX_DWORD_Ptr_ESI_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8B5E" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8B9E" + intTohex(addre, 8);
            }
        }

        public void Mov_ECX_DWORD_Ptr_EAX_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8B48" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8B88" + intTohex(addre, 8);
            }
        }

        public void Mov_ECX_DWORD_Ptr_ESP_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8B4C24" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8B8C24" + intTohex(addre, 8);
            }
        }

        public void Mov_ECX_DWORD_Ptr_EBX_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8B4B" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8B8B" + intTohex(addre, 8);
            }
        }

        public void Mov_ECX_DWORD_Ptr_ECX_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8B49" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8B89" + intTohex(addre, 8);
            }
        }

        public void Mov_ECX_DWORD_Ptr_EDX_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8B4A" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8B8A" + intTohex(addre, 8);
            }
        }

        public void Mov_ECX_DWORD_Ptr_EDI_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8B4F" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8B8F" + intTohex(addre, 8);
            }
        }

        public void Mov_ECX_DWORD_Ptr_EBP_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8B4D" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8B8D" + intTohex(addre, 8);
            }
        }

        public void Mov_ECX_DWORD_Ptr_ESI_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8B4E" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8B8E" + intTohex(addre, 8);
            }
        }

        public void Mov_EDI_DWORD_Ptr_ECX_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128)) {
                Asmcode = Asmcode + "8B79" + intTohex(addre, 2);
            }
            else {
                Asmcode = Asmcode + "8BB9" + intTohex(addre, 8);
            }
        }

        public void Mov_EDI_DWORD_Ptr_EAX_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128)) {
                Asmcode = Asmcode + "8B78" + intTohex(addre, 2);
            }
            else {
                Asmcode = Asmcode + "8BB8" + intTohex(addre, 8);
            }
        }

        public void Mov_EDX_DWORD_Ptr_EAX_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8B50" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8B90" + intTohex(addre, 8);
            }
        }

        public void Mov_EDX_DWORD_Ptr_ESP_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8B5424" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8B9424" + intTohex(addre, 8);
            }
        }

        public void Mov_EDX_DWORD_Ptr_EBX_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8B53" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8B93" + intTohex(addre, 8);
            }
        }

        public void Mov_EDX_DWORD_Ptr_ECX_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8B51" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8B91" + intTohex(addre, 8);
            }
        }

        public void Mov_EDX_DWORD_Ptr_EDX_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8B52" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8B92" + intTohex(addre, 8);
            }
        }

        public void Mov_EDX_DWORD_Ptr_EDI_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8B57" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8B97" + intTohex(addre, 8);
            }
        }

        public void Mov_EDX_DWORD_Ptr_EBP_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8B55" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8B95" + intTohex(addre, 8);
            }
        }

        public void Mov_EDX_DWORD_Ptr_ESI_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8B56" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8B96" + intTohex(addre, 8);
            }
        }

        public void Mov_ESI_DWORD_Ptr_EAX_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8B70" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8BB0" + intTohex(addre, 8);
            }
        }

        public void Mov_ESI_DWORD_Ptr_EDX_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128)) {
                Asmcode = Asmcode + "8B72" + intTohex(addre, 2);
            }
            else {
                Asmcode = Asmcode + "8BB2" + intTohex(addre, 8);
            }
        }

        public void Mov_EAX(long addre)
        {
            Asmcode = Asmcode + "B8" + intTohex(addre, 8);
        }

        // public void Mov_EAX(float addre)
        // {
        //     int intVal = FloatToIntBits(addre);
        //     Asmcode = Asmcode + "B8" + intTohex(intVal, 8);
        // }

        public void Mov_EBX(int addre)
        {
            Asmcode = Asmcode + "BB" + intTohex(addre, 8);
        }

        public void Mov_ECX(long addre)
        {
            Asmcode = Asmcode + "B9" + intTohex(addre, 8);
        }

        public void Mov_EDX(long addre)
        {
            Asmcode = Asmcode + "BA" + intTohex(addre, 8);
        }

        public void Mov_ESI(int addre)
        {
            Asmcode = Asmcode + "BE" + intTohex(addre, 8);
        }

        public void Mov_ESP(int addre)
        {
            Asmcode = Asmcode + "BC" + intTohex(addre, 8);
        }

        public void Mov_EBP(int addre)
        {
            Asmcode = Asmcode + "BD" + intTohex(addre, 8);
        }

        public void Mov_EDI(int addre)
        {
            Asmcode = Asmcode + "BF" + intTohex(addre, 8);
        }

        public void Mov_ESI_DWORD_Ptr_EAX()
        {
            Asmcode = Asmcode + "8B7020";
        }

        public void Mov_EBX_DWORD_Ptr_EAX()
        {
            Asmcode = Asmcode + "8B18";
        }

        public void Mov_EBX_DWORD_Ptr_EBP()
        {
            Asmcode = Asmcode + "8B5D00";
        }

        public void Mov_EBX_DWORD_Ptr_EBX()
        {
            Asmcode = Asmcode + "8B1B";
        }

        public void Mov_EBX_DWORD_Ptr_ECX()
        {
            Asmcode = Asmcode + "8B19";
        }

        public void Mov_EBX_DWORD_Ptr_EDX()
        {
            Asmcode = Asmcode + "8B1A";
        }

        public void Mov_EBX_DWORD_Ptr_EDI()
        {
            Asmcode = Asmcode + "8B1F";
        }

        public void Mov_EBX_DWORD_Ptr_ESP()
        {
            Asmcode = Asmcode + "8B1C24";
        }

        public void Mov_EBX_DWORD_Ptr_ESI()
        {
            Asmcode = Asmcode + "8B1E";
        }

        public void Mov_ECX_DWORD_Ptr_EAX()
        {
            Asmcode = Asmcode + "8B08";
        }

        public void Mov_ECX_DWORD_Ptr_EBP()
        {
            Asmcode = Asmcode + "8B4D00";
        }

        public void Mov_ECX_DWORD_Ptr_EBX()
        {
            Asmcode = Asmcode + "8B0B";
        }

        public void Mov_ECX_DWORD_Ptr_ECX()
        {
            Asmcode = Asmcode + "8B09";
        }

        public void Mov_ECX_DWORD_Ptr_EDX()
        {
            Asmcode = Asmcode + "8B0A";
        }

        public void Mov_ECX_DWORD_Ptr_EDI()
        {
            Asmcode = Asmcode + "8B0F";
        }

        public void Mov_ECX_DWORD_Ptr_ESP()
        {
            Asmcode = Asmcode + "8B0C24";
        }

        public void Mov_ECX_DWORD_Ptr_ESI()
        {
            Asmcode = Asmcode + "8B0E";
        }

        public void Mov_EDX_DWORD_Ptr_EAX()
        {
            Asmcode = Asmcode + "8B10";
        }

        public void Mov_EDX_DWORD_Ptr_EBP()
        {
            Asmcode = Asmcode + "8B5500";
        }

        public void Mov_EDX_DWORD_Ptr_EBX()
        {
            Asmcode = Asmcode + "8B13";
        }

        public void Mov_EDX_DWORD_Ptr_ECX()
        {
            Asmcode = Asmcode + "8B11";
        }

        public void Mov_EDX_DWORD_Ptr_EDX()
        {
            Asmcode = Asmcode + "8B12";
        }

        public void Mov_EDX_DWORD_Ptr_EDI()
        {
            Asmcode = Asmcode + "8B17";
        }

        public void Mov_EDX_DWORD_Ptr_ESI()
        {
            Asmcode = Asmcode + "8B16";
        }

        public void Mov_EDX_DWORD_Ptr_ESP()
        {
            Asmcode = Asmcode + "8B1424";
        }

        public void Mov_EAX_EBP()
        {
            Asmcode = Asmcode + "8BC5";
        }

        public void Mov_EAX_EBX()
        {
            Asmcode = Asmcode + "8BC3";
        }

        public void Mov_EAX_ECX()
        {
            Asmcode = Asmcode + "8BC1";
        }

        public void Mov_EAX_EDI()
        {
            Asmcode = Asmcode + "8BC7";
        }

        public void Mov_EAX_EDX()
        {
            Asmcode = Asmcode + "8BC2";
        }

        public void Mov_EAX_ESI()
        {
            Asmcode = Asmcode + "8BC6";
        }

        public void Mov_EAX_ESP()
        {
            Asmcode = Asmcode + "8BC4";
        }

        public void Mov_EBX_EBP()
        {
            Asmcode = Asmcode + "8BDD";
        }

        public void Mov_EBX_EAX()
        {
            Asmcode = Asmcode + "8BD8";
        }

        public void Mov_EBX_ECX()
        {
            Asmcode = Asmcode + "8BD9";
        }

        public void Mov_EBX_EDI()
        {
            Asmcode = Asmcode + "8BDF";
        }

        public void Mov_EBX_EDX()
        {
            Asmcode = Asmcode + "8BDA";
        }

        public void Mov_EBX_ESI()
        {
            Asmcode = Asmcode + "8BDE";
        }

        public void Mov_EBX_ESP()
        {
            Asmcode = Asmcode + "8BDC";
        }

        public void Mov_ECX_EBP()
        {
            Asmcode = Asmcode + "8BCD";
        }


        public void Mov_ECX_EAX()
        {
            Asmcode = Asmcode + "8BC8";
        }

        public void Mov_ECX_EBX()
        {
            Asmcode = Asmcode + "8BCB";
        }

        public void Mov_ECX_EDI()
        {
            Asmcode = Asmcode + "8BCF";
        }

        public void Mov_ECX_EDX()
        {
            Asmcode = Asmcode + "8BCA";
        }

        public void Mov_ECX_ESI()
        {
            Asmcode = Asmcode + "8BCE";
        }

        public void Mov_ECX_ESP()
        {
            Asmcode = Asmcode + "8BCC";
        }

        public void Mov_EDX_EBP()
        {
            Asmcode = Asmcode + "8BD5";
        }

        public void Mov_EDX_EBX()
        {
            Asmcode = Asmcode + "8BD3";
        }

        public void Mov_EDX_ECX()
        {
            Asmcode = Asmcode + "8BD1";
        }

        public void Mov_EDX_EDI()
        {
            Asmcode = Asmcode + "8BD7";
        }

        public void Mov_EDX_EAX()
        {
            Asmcode = Asmcode + "8BD0";
        }

        public void Mov_EDX_ESI()
        {
            Asmcode = Asmcode + "8BD6";
        }

        public void Mov_EDX_ESP()
        {
            Asmcode = Asmcode + "8BD4";
        }

        public void Mov_ESI_EBP()
        {
            Asmcode = Asmcode + "8BF5";
        }

        public void Mov_ESI_EBX()
        {
            Asmcode = Asmcode + "8BF3";
        }

        public void Mov_ESI_ECX()
        {
            Asmcode = Asmcode + "8BF1";
        }

        public void Mov_ESI_EDI()
        {
            Asmcode = Asmcode + "8BF7";
        }

        public void Mov_ESI_EAX()
        {
            Asmcode = Asmcode + "8BF0";
        }

        public void Mov_ESI_EDX()
        {
            Asmcode = Asmcode + "8BF2";
        }

        public void Mov_ESI_ESP()
        {
            Asmcode = Asmcode + "8BF4";
        }

        public void Mov_ESP_EBP()
        {
            Asmcode = Asmcode + "8BE5";
        }

        public void Mov_ESP_EBX()
        {
            Asmcode = Asmcode + "8BE3";
        }

        public void Mov_ESP_ECX()
        {
            Asmcode = Asmcode + "8BE1";
        }

        public void Mov_ESP_EDI()
        {
            Asmcode = Asmcode + "8BE7";
        }

        public void Mov_ESP_EAX()
        {
            Asmcode = Asmcode + "8BE0";
        }

        public void Mov_ESP_EDX()
        {
            Asmcode = Asmcode + "8BE2";
        }

        public void Mov_ESP_ESI()
        {
            Asmcode = Asmcode + "8BE6";
        }

        public void Mov_EDI_EBP()
        {
            Asmcode = Asmcode + "8BFD";
        }

        public void Mov_EDI_EAX()
        {
            Asmcode = Asmcode + "8BF8";
        }

        public void Mov_EDI_EBX()
        {
            Asmcode = Asmcode + "8BFB";
        }

        public void Mov_EDI_ECX()
        {
            Asmcode = Asmcode + "8BF9";
        }

        public void Mov_EDI_EDX()
        {
            Asmcode = Asmcode + "8BFA";
        }

        public void Mov_EDI_ESI()
        {
            Asmcode = Asmcode + "8BFE";
        }

        public void Mov_EDI_ESP()
        {
            Asmcode = Asmcode + "8BFC";
        }

        public void Mov_EBP_EDI()
        {
            Asmcode = Asmcode + "8BDF";
        }

        public void Mov_EBP_EAX()
        {
            Asmcode = Asmcode + "8BE8";
        }

        public void Mov_EBP_EBX()
        {
            Asmcode = Asmcode + "8BEB";
        }

        public void Mov_EBP_ECX()
        {
            Asmcode = Asmcode + "8BE9";
        }

        public void Mov_EBP_EDX()
        {
            Asmcode = Asmcode + "8BEA";
        }

        public void Mov_EBP_ESI()
        {
            Asmcode = Asmcode + "8BEE";
        }

        public void Mov_EBP_ESP()
        {
            Asmcode = Asmcode + "8BEC";
        }
        #endregion

        #region "Push"
        public void Push68U(uint addre)
        {
            Asmcode = Asmcode + "68" + uintTohex(addre, 8);

        }
        public void Push68(int addre)
        {
            Asmcode = Asmcode + "68" + intTohex(addre, 8);

        }

        public void Push6A(int addre)
        {
            Asmcode = Asmcode + "6A" + intTohex(addre, 2);
        }

        public void Push_EAX()
        {
            Asmcode = Asmcode + "50";
        }

        public void Push_DWORD_Ptr(int addre)
        {
            Asmcode = Asmcode + "FF35" + intTohex(addre, 8);
        }

        public void Push_ECX()
        {
            Asmcode = Asmcode + "51";
        }

        public void Push_EDX()
        {
            Asmcode = Asmcode + "52";
        }

        public void Push_EBX()
        {
            Asmcode = Asmcode + "53";
        }

        public void Push_ESP()
        {
            Asmcode = Asmcode + "54";
        }

        public void Push_EBP()
        {
            Asmcode = Asmcode + "55";
        }

        public void Push_ESI()
        {
            Asmcode = Asmcode + "56";
        }

        public void Push_EDI()
        {
            Asmcode = Asmcode + "57";
        }
        #endregion

        #region "Call"
        public void Call_EAX()
        {
            Asmcode = Asmcode + "FFD0";
        }

        public void Call_EBX()
        {
            Asmcode = Asmcode + "FFD3";
        }

        public void Call_ECX()
        {
            Asmcode = Asmcode + "FFD1";
        }

        public void Call_EDX()
        {
            Asmcode = Asmcode + "FFD2";
        }

        public void Call_ESP()
        {
            Asmcode = Asmcode + "FFD4";
        }

        public void Call_EBP()
        {
            Asmcode = Asmcode + "FFD5";
        }

        public void Call_ESI()
        {
            Asmcode = Asmcode + "FFD6";
        }

        public void Call_EDI()
        {
            Asmcode = Asmcode + "FFD7";
        }

        public void Call_DWORD_Ptr(int addre)
        {
            Asmcode = Asmcode + "FF15" + intTohex(addre, 8);
        }

        public void Call(int OldAddress, int TargetCall)
        {
            Asmcode = Asmcode + "E8" + intTohex(((TargetCall - OldAddress) - 10), 8);
        }

        public void Call_Adr(int addre)
        {
            Asmcode = Asmcode + "E8" + intTohex(addre, 8);
        }

        public void Call_DWORD_Ptr_EAX()
        {
            Asmcode = Asmcode + "FF10";
        }

        public void Call_DWORD_Ptr_EBX()
        {
            Asmcode = Asmcode + "FF13";
        }
        #endregion

        #region "Lea"
        public void Lea_EAX_DWORD_Ptr_EAX_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8D40" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8D80" + intTohex(addre, 8);
            }
        }

        public void Lea_EAX_DWORD_Ptr_EBX_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8D43" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8D83" + intTohex(addre, 8);
            }
        }

        public void Lea_EAX_DWORD_Ptr_ECX_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8D41" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8D81" + intTohex(addre, 8);
            }
        }

        public void Lea_EAX_DWORD_Ptr_EDX_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8D42" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8D82" + intTohex(addre, 8);
            }
        }

        public void Lea_EAX_DWORD_Ptr_ESI_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8D46" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8D86" + intTohex(addre, 8);
            }
        }

        public void Lea_EAX_DWORD_Ptr_ESP_Add(int addre)
        {
            string test = "8D44" + intTohex(addre, 2);
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8D4424" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8D8424" + intTohex(addre, 8);
            }
        }

        public void Lea_EAX_DWORD_Ptr_EBP_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8D4424" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8D8424" + intTohex(addre, 8);
            }
        }

        public void Lea_EAX_DWORD_Ptr_EDI_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8D47" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8D87" + intTohex(addre, 8);
            }
        }

        public void Lea_EBX_DWORD_Ptr_EAX_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8D58" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8D98" + intTohex(addre, 8);
            }
        }

        public void Lea_EBX_DWORD_Ptr_ESP_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8D5C24" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8D9C24" + intTohex(addre, 8);
            }
        }

        public void Lea_EBX_DWORD_Ptr_EBX_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8D5B" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8D9B" + intTohex(addre, 8);
            }
        }

        public void Lea_EBX_DWORD_Ptr_ECX_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8D59" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8D99" + intTohex(addre, 8);
            }
        }

        public void Lea_EBX_DWORD_Ptr_EDX_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8D5A" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8D9A" + intTohex(addre, 8);
            }
        }

        public void Lea_EBX_DWORD_Ptr_EDI_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8D5F" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8D9F" + intTohex(addre, 8);
            }
        }

        public void Lea_EBX_DWORD_Ptr_EBP_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8D5D" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8D9D" + intTohex(addre, 8);
            }
        }

        public void Lea_EBX_DWORD_Ptr_ESI_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8D5E" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8D9E" + intTohex(addre, 8);
            }
        }

        public void Lea_ECX_DWORD_Ptr_EAX_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8D48" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8D88" + intTohex(addre, 8);
            }
        }

        public void Lea_ECX_DWORD_Ptr_ESP_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8D4C24" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8D8C24" + intTohex(addre, 8);
            }
        }

        public void Lea_ECX_DWORD_Ptr_EBX_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8D4B" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8D8B" + intTohex(addre, 8);
            }
        }

        public void Lea_ECX_DWORD_Ptr_ECX_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8D49" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8D89" + intTohex(addre, 8);
            }
        }

        public void Lea_ECX_DWORD_Ptr_EDX_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8D4A" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8D8A" + intTohex(addre, 8);
            }
        }

        public void Lea_ECX_DWORD_Ptr_EDI_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8D4F" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8D8F" + intTohex(addre, 8);
            }
        }

        public void Lea_ECX_DWORD_Ptr_EBP_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8D4D" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8D8D" + intTohex(addre, 8);
            }
        }

        public void Lea_ECX_DWORD_Ptr_ESI_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8D4E" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8D8E" + intTohex(addre, 8);
            }
        }

        public void Lea_EDX_DWORD_Ptr_EAX_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8D50" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8D90" + intTohex(addre, 8);
            }
        }

        public void Lea_EDX_DWORD_Ptr_ESP_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8D5424" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8D9424" + intTohex(addre, 8);
            }
        }

        public void Lea_EDX_DWORD_Ptr_EBX_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8D53" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8D93" + intTohex(addre, 8);
            }
        }

        public void Lea_EDX_DWORD_Ptr_ECX_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8D51" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8D91" + intTohex(addre, 8);
            }
        }

        public void Lea_EDX_DWORD_Ptr_EDX_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8D52" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8D92" + intTohex(addre, 8);
            }
        }

        public void Lea_EDX_DWORD_Ptr_EDI_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8D57" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8D97" + intTohex(addre, 8);
            }
        }

        public void Lea_EDX_DWORD_Ptr_EBP_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8D55" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8D95" + intTohex(addre, 8);
            }
        }

        public void Lea_EDX_DWORD_Ptr_ESI_Add(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "8D56" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "8D96" + intTohex(addre, 8);
            }
        }
        #endregion

        #region "POP"
        public void Pop_EAX()
        {
            Asmcode = Asmcode + "58";
        }

        public void Pop_EBX()
        {
            Asmcode = Asmcode + "5B";
        }

        public void Pop_ECX()
        {
            Asmcode = Asmcode + "59";
        }

        public void Pop_EDX()
        {
            Asmcode = Asmcode + "5A";
        }

        public void Pop_ESI()
        {
            Asmcode = Asmcode + "5E";
        }

        public void Pop_ESP()
        {
            Asmcode = Asmcode + "5C";
        }

        public void Pop_EDI()
        {
            Asmcode = Asmcode + "5F";
        }

        public void Pop_EBP()
        {
            Asmcode = Asmcode + "5D";
        }
        #endregion
        public void Popad()
        {
            Asmcode = Asmcode + "61";
        }

        public void Ret()
        {
            Asmcode = Asmcode + "C3";
        }

        #region "CMP"
        public void Cmp_EAX(int addre)
        {
            if ((addre <= 127) && (addre >= -128))
            {
                Asmcode = Asmcode + "83F8" + intTohex(addre, 2);
            }
            else
            {
                Asmcode = Asmcode + "3D" + intTohex(addre, 8);
            }
        }

        public void Cmp_EAX_EDX()
        {
            Asmcode = Asmcode + "3BC2";
        }

        public void Cmp_EAX_DWORD_Ptr(int addre)
        {
            Asmcode = Asmcode + "3B05" + intTohex(addre, 8);
        }

        public void Cmp_DWORD_Ptr_EAX(int addre)
        {
            Asmcode = Asmcode + "3905" + intTohex(addre, 8);
        }
        #endregion

        #region "DEC"
        public void Dec_EAX()
        {
            Asmcode = Asmcode + "48";
        }

        public void Dec_EBX()
        {
            Asmcode = Asmcode + "4B";
        }

        public void Dec_ECX()
        {
            Asmcode = Asmcode + "49";
        }

        public void Dec_EDX()
        {
            Asmcode = Asmcode + "4A";
        }
        #endregion

        #region "idiv"
        public void Idiv_EAX()
        {
            Asmcode = Asmcode + "F7F8";
        }

        public void Idiv_EBX()
        {
            Asmcode = Asmcode + "F7FB";
        }

        public void Idiv_ECX()
        {
            Asmcode = Asmcode + "F7F9";
        }

        public void Idiv_EDX()
        {
            Asmcode = Asmcode + "F7FA";
        }
        #endregion

        #region "Imul"
        public void Imul_EAX_EDX()
        {
            Asmcode = Asmcode + "0FAFC2";
        }

        public void Imul_EAX(int addre)
        {
            Asmcode = Asmcode + "6BC0" + intTohex(addre, 2);
        }

        public void ImulB_EAX(int addre)
        {
            Asmcode = Asmcode + "69C0" + intTohex(addre, 8);
        }
        #endregion

        #region "Inc"
        public void Inc_EAX()
        {
            Asmcode = Asmcode + "40";
        }

        public void Inc_EBX()
        {
            Asmcode = Asmcode + "43";
        }

        public void Inc_ECX()
        {
            Asmcode = Asmcode + "41";
        }

        public void Inc_EDX()
        {
            Asmcode = Asmcode + "42";
        }

        public void Inc_EDI()
        {
            Asmcode = Asmcode + "47";
        }

        public void Inc_ESI()
        {
            Asmcode = Asmcode + "46";
        }

        public void Inc_DWORD_Ptr_EAX()
        {
            Asmcode = Asmcode + "FF00";
        }

        public void Inc_DWORD_Ptr_EBX()
        {
            Asmcode = Asmcode + "FF03";
        }

        public void Inc_DWORD_Ptr_ECX()
        {
            Asmcode = Asmcode + "FF01";
        }

        public void Inc_DWORD_Ptr_EDX()
        {
            Asmcode = Asmcode + "FF02";
        }
        #endregion

        #region "jmp"
        public void JMP_EAX()
        {
            Asmcode = Asmcode + "FFE0";
        }
        #endregion

        public int FloatToIntBits(float argument)
        {
            byte[] byteValue = BitConverter.GetBytes(argument);
            int intValue = BitConverter.ToInt32(byteValue, 0);
            return intValue;
        }

        public byte[] inBytes()
        {
            byte[] reAsmCode = new byte[Asmcode.Length / 2];

            for (int i = 0; i <= reAsmCode.Length - 1; i++) {
                reAsmCode[i] = Convert.ToByte(Int32.Parse(Asmcode.Substring(i * 2, 2), System.Globalization.NumberStyles.AllowHexSpecifier));
            }

            Asmcode = "";

            return reAsmCode;
        }

        public string formatAsmCode()
        {
            string result;
            int requiredSpaces = (Asmcode.Length - 1) / 2;
            if (requiredSpaces < 1)
            {
                result = Asmcode;
            }
            else
            {
                int resultLength = Asmcode.Length + requiredSpaces;
                StringBuilder sb = new StringBuilder(resultLength);
                for (int i=0; i<Asmcode.Length; i++)
                {
                    sb.Append(Asmcode[i]);
                    if (i % 2 == 1)
                    {
                        sb.Append(' ');
                    }
                }
                result = sb.ToString();
            }
            return result;
        }
    }
}