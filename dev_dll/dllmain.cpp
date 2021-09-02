// dllmain.cpp : 定义 DLL 应用程序的入口点。
#include "pch.h"

BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
                     )
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
        MessageBox(NULL, TEXT("C++ DLL DLL_PROCESS_ATTACH Injection Successful"), TEXT("Title"), MB_OK);
        break;
    case DLL_THREAD_ATTACH:
        MessageBox(NULL, TEXT("C++ DLL DLL_THREAD_ATTACH Injection Successful"), TEXT("Title"), MB_OK);
        break;
    case DLL_THREAD_DETACH:
        MessageBox(NULL, TEXT("C++ DLL DLL_THREAD_DETACH Injection Successful"), TEXT("Title"), MB_OK);
        break;
    case DLL_PROCESS_DETACH:
        MessageBox(NULL, TEXT("C++ DLL Un_injection Successful"), TEXT("Title"), MB_OK);
        break;
    }
    return TRUE;
}

void dev() {
    MessageBox(NULL, TEXT("C++ DLL Injection Successful"), TEXT("Title"), MB_OK);

}