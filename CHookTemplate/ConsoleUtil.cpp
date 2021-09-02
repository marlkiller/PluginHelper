#include "pch.h"
#include "ConsoleUtil.h"
#include "CommonUtil.h"
#include <iostream>
#include <Windows.h>


using namespace std;

ConsoleUtil::ConsoleUtil() : mConsoleOut(NULL), mConsoleOutBackup(NULL) {
	mBase = (DWORD)GetModuleHandle(L"WeChatWin.dll");
}

ConsoleUtil::~ConsoleUtil() {
}

DWORD ConsoleUtil::Offset(DWORD offset) {
	return mBase + offset;
}

void ConsoleUtil::OpenConsole() {
	if (!mConsoleOpen) {
		if (AllocConsole()) {
			mConsoleOutBackup = cout.rdbuf();
			mConsoleOut = freopen("CONOUT$", "w", stdout);
			char nt[100];
			sprintf_s(nt, "��־[%s]-%lld-%d",remoteProgramName, GetTickCount64(), GetCurrentProcessId());
			SetConsoleTitleA(nt);
			Sleep(100);
			HWND find = FindWindowA(NULL, nt);
			if (find) {
				HMENU menu = GetSystemMenu(find, FALSE);
				if (menu) {
					if (!needMenu) {
						if (RemoveMenu(menu, 0xF060, 0)) {
							sprintf_s(nt, "��־[%s]-%lld-%d-�����ιرհ�ť", remoteProgramName, GetTickCount64(), GetCurrentProcessId());
							SetConsoleTitleA(nt);
						}
					}
				}
			}
		}
		wcout.imbue(locale("", LC_CTYPE));
		mConsoleOpen = TRUE;
	}
}

void ConsoleUtil::CloseConsole() {
	if (mConsoleOpen) {
		if (mConsoleOut != NULL && mConsoleOutBackup != NULL) {
			cout.rdbuf(mConsoleOutBackup);
			fclose(mConsoleOut);
		}
		FreeConsole();
		mConsoleOpen = FALSE;
	}
}

std::string ConsoleUtil::GetWeChatVersion() {
	HMODULE module = (HMODULE)GetModuleHandle(L"WeChatWin.dll");
	char filename[MAX_PATH] = { 0 };
	::GetModuleFileNameA(module, filename, sizeof(filename) - 1);
	return CommonUtil::GetFileVersion(filename);
}