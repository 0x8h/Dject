#pragma once
#include <iostream>
#include <Windows.h>
#include <string>
using namespace std;
BOOL WINAPI Dllinjectbridge(const WCHAR* process, const WCHAR* path, BOOL showsuccessmessage);