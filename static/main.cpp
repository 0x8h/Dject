#include "main.h"

typedef bool(*Dllinject)(const WCHAR* process, const WCHAR* path, BOOL successmessage);

BOOL WINAPI Dllinjectbridge(const WCHAR* process, const WCHAR* path, BOOL showsuccessmessage) {
	auto lib = ::LoadLibraryW(L"Dject.dll");
	if (lib == NULL) {
		cout << "LoadLibraryW";
		return FALSE;
	}
	auto func = reinterpret_cast<Dllinject>(::GetProcAddress(lib, "Dllinject"));
	if (func == NULL) {
		cout << "GetProcAddress";
		return FALSE;
	}
	return func(process, path, showsuccessmessage);
}