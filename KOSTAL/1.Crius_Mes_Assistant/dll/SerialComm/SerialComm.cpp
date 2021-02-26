// SerialComm.cpp : Defines the entry point for the DLL application.
//

#include "stdafx.h"
#include "SerialComm.h"
#include "RS232c1.h"


CRs232c gComm;

BOOL APIENTRY DllMain( HANDLE hModule, 
                       DWORD  ul_reason_for_call, 
                       LPVOID lpReserved
					 )
{
    switch (ul_reason_for_call)
	{
		case DLL_PROCESS_ATTACH:
		case DLL_THREAD_ATTACH:
		case DLL_THREAD_DETACH:
		case DLL_PROCESS_DETACH:
			break;
    }
    return TRUE;
}

SERIALCOMM_API BOOL Open(int port)
{
	return gComm.Init(9600,port);
}
SERIALCOMM_API void Close()
{
	gComm.Uninit();
}
SERIALCOMM_API BYTE ReadByte()
{
	char data[2] = {0};
	memset(data,0,2);

	int read = 0;

	gComm.ReadBytes(data,1, read);

	return data[0];
}
SERIALCOMM_API BOOL GetInputBufferCount()
{
	return gComm.GetInputBufferCount();
}

