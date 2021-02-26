
// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the SERIALCOMM_EXPORTS
// symbol defined on the command line. this symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// SERIALCOMM_API functions as being imported from a DLL, wheras this DLL sees symbols
// defined with this macro as being exported.
#ifdef SERIALCOMM_EXPORTS
#define SERIALCOMM_API __declspec(dllexport)
#else
#define SERIALCOMM_API __declspec(dllimport)
#endif


extern "C" SERIALCOMM_API BOOL Open(int port);
extern "C" SERIALCOMM_API void Close();
extern "C" SERIALCOMM_API BYTE ReadByte();
extern "C" SERIALCOMM_API int GetInputBufferCount();


