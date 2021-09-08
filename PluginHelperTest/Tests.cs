using System;
using NUnit.Framework;
using PluginHelper.Native;

namespace PluginHelperTest
{
    [TestFixture]
    public class Tests
    {
        
        [Test]
        public void AsmTest64()
        {
            
            
            var asmUtilX64 = new AsmUtilX64();
            // asmUtilX64.Pushall();
            asmUtilX64.Mov_R9(0x2);
            asmUtilX64.Mov_R8(0x242);
            asmUtilX64.Mov_RDX(0x242db320000);
            asmUtilX64.Mov_RCX(0x242db330000);
            asmUtilX64.Mov_EAX(0x242db340000);
            asmUtilX64.Call_EAX();
            // asmUtilX64.Popad();
            Console.WriteLine(asmUtilX64.formatAsmCode());
            Assert.True(true);
        }
        
        [Test]
        public void AsmTest86()
        {
            var asmUtilX86 = new AsmUtilX86();
            // asmUtilX64.Pushall();
            asmUtilX86.Push6A(0x0);
            asmUtilX86.Push6A(0x0);
            asmUtilX86.Mov_EAX(0x001234);
            asmUtilX86.Push_EAX();
            asmUtilX86.Mov_EAX(0x002234);
            asmUtilX86.Push_EAX();
            asmUtilX86.Mov_EAX(0x003234);
            asmUtilX86.Call_EAX();
            // asmUtilX64.Popad();
            Console.WriteLine(asmUtilX86.formatAsmCode());
            Assert.True(true);
        }
    }
}