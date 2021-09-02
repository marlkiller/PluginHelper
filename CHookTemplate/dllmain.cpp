// dllmain.cpp : 定义 DLL 应用程序的入口点。
#include "pch.h"
#include "HiWeChat.h"
#include <iostream>


using namespace std;


HMODULE GModule = NULL;
ConsoleUtil* Util = new ConsoleUtil();


DWORD WINAPI UnloadThreadProc(PVOID context) {
    FreeLibraryAndExitThread(*(HMODULE*)context, 0);
    return 0;
}

void UnLoad(HMODULE module) {
    HANDLE thread = CreateThread(NULL, 0, UnloadThreadProc, (PVOID) & module, NULL, NULL);
    if (thread) {
        CloseHandle(thread);
    }
}


void PrintLogger(DWORD eax);


void PrintLogger(DWORD eax) {
    if (eax == 0) {
        return;
    }
    string log = CommonUtil::ReadAsciiString(eax);
    cout << "WeChatWin.dll >> " << log.c_str() << endl;
    //OutputDebugStringA(log.c_str());
}




BOOL APIENTRY DllMain(HMODULE hModule, DWORD ul_reason_for_call, LPVOID reserved) {

    switch (ul_reason_for_call) {
    case DLL_PROCESS_ATTACH: {
        GModule = hModule;
        Util->OpenConsole();

        string log = "this is log";
        cout << "WeChatWin.dll >> " << log.c_str() << endl;

        // HookLogger();
        break;
    }
    case DLL_PROCESS_DETACH: {
        Util->CloseConsole();

        // UnHookLogger();
        break;
    }
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
        break;
    }
    return TRUE;
}


