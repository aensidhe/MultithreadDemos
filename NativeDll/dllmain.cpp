// dllmain.cpp : Defines the entry point for the DLL application.
#include "stdafx.h"
#include <iostream>
#include <string>
#include <time.h>
#include <chrono>
#include <thread>

// Get current date/time, format is YYYY-MM-DD.HH:mm:ss
const std::string currentDateTime() {
	time_t     now = time(0);
	struct tm  tstruct;
	char       buf[80];
	tstruct = *localtime(&now);
	// Visit http://en.cppreference.com/w/cpp/chrono/c/strftime
	// for more information about date/time format
	strftime(buf, sizeof(buf), "%X", &tstruct);

	return buf;
}

void wait(char* reason)
{
	std::chrono::milliseconds sleepTime(2000);
	auto id = std::this_thread::get_id();
	std::cout << currentDateTime() << ". Let's celebrate " << reason << " of thread " << id << std::endl;
	std::this_thread::sleep_for(sleepTime);
}

BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
					 )
{
	switch (ul_reason_for_call)
	{
		case DLL_PROCESS_ATTACH:
			break;
		case DLL_THREAD_ATTACH:
			wait("birth");
			break;
		case DLL_THREAD_DETACH:
			wait("death");
			break;
		case DLL_PROCESS_DETACH:
			break;
	}
	return TRUE;
}

