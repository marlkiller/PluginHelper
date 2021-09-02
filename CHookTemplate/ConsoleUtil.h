#pragma once
#include <string>

#define		REMOTE_PROGRAM_VERSION		"3.1.0.72"



class ConsoleUtil {
public:
	ConsoleUtil();
	~ConsoleUtil();

	DWORD Offset(DWORD offset);

	void OpenConsole();
	void CloseConsole();

private:
	DWORD mBase = NULL;

	FILE* mConsoleOut;
	std::streambuf* mConsoleOutBackup;
	BOOL mConsoleOpen = FALSE;
	char* remoteProgramName = (char*)"notepad.exe";
	BOOL needMenu = true;

public:
	static std::string GetWeChatVersion();
};