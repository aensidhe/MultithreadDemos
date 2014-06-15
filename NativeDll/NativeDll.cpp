// NativeDll.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "NativeDll.h"

// This is an example of an exported function.
extern "C" NATIVEDLL_API int APIENTRY fnNativeDll()
{
	return 42;
}
