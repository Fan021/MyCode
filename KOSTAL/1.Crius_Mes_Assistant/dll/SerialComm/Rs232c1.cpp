// Rs232c1.cpp: implementation of the CRs232c class.
//
//////////////////////////////////////////////////////////////////////

#include "stdafx.h"
#include "Rs232c1.h"
#include <stdio.h>

#ifdef _DEBUG
#undef THIS_FILE
static char THIS_FILE[]=__FILE__;
#define new DEBUG_NEW
#endif

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CRs232c::CRs232c()
{

}

CRs232c::~CRs232c()
{

}

BOOL CRs232c::Init(int nBaudRate, short nPort)
{
	long nError = 0;
	//-----------Open Port-----------
	char szComm[MAX_PATH] = {0};
	sprintf(szComm,"\\\\.\\com%d",nPort);
	m_hComm = ::CreateFile(szComm,GENERIC_READ|GENERIC_WRITE,NULL,NULL,OPEN_EXISTING,
		NULL,NULL);
	if(m_hComm == INVALID_HANDLE_VALUE || m_hComm == NULL)
	{
		nError = GetLastError();  
		return FALSE;
	}

	//-------------set timeout---------------
	COMMTIMEOUTS to;
	to.ReadIntervalTimeout = 50;
	to.ReadTotalTimeoutConstant = 50;
	to.WriteTotalTimeoutConstant = 50;
	to.WriteTotalTimeoutMultiplier = 5;
	to.ReadTotalTimeoutMultiplier = 5;
	if(!::SetCommTimeouts(m_hComm,&to))
	{
		CloseHandle(m_hComm);
		MessageBox(NULL, TEXT("1"), TEXT("1"), MB_OK | MB_ICONERROR);
		return FALSE;
	}
	
	//---------set baudrate------------
	/*DCB dcb;
	GetCommState(m_hComm, &dcb);
	dcb.BaudRate = nBaudRate;
	dcb.Parity = NOPARITY;
	dcb.StopBits = ONESTOPBIT;
	dcb.ByteSize = 8;

	if(!::SetCommState(m_hComm,&dcb))
	{
		int n =::GetLastError();
		TCHAR szText[100];
		memset(szText, 0, 100*sizeof(TCHAR));

		_stprintf(szText, "%d", n);

		MessageBox(NULL, _T("2"),szText, MB_OK | MB_ICONERROR);
		CloseHandle(m_hComm);
		return FALSE;
	}*/
	
	return TRUE;
}

void CRs232c::Uninit()
{
	::CloseHandle(m_hComm);
}


BOOL CRs232c::WriteBytes(char *lpszData, int nCount,int& nWritedBytes)
{
	return ::WriteFile(m_hComm,lpszData,nCount,(unsigned long*)&nWritedBytes,NULL);
}


BOOL CRs232c::ReadBytes(char *lpszBuffer, int nCount, int& nReadCount)
{
	return ::ReadFile(m_hComm, lpszBuffer,nCount,(unsigned long*)&nReadCount,NULL);
}



int CRs232c::GetInputBufferCount()
{
	DWORD dwError = 0;
	COMSTAT st = {0};
	
	::ClearCommError(m_hComm, &dwError, &st);
	return st.cbInQue;
}

void CRs232c::DiscardInBuffer()
{
	::PurgeComm(m_hComm,PURGE_RXCLEAR|PURGE_RXABORT);
}

void CRs232c::DiscardOutBuffer()
{
	::PurgeComm(m_hComm,PURGE_TXCLEAR|PURGE_TXABORT);
}

BOOL CRs232c::SendString(LPCSTR lpszData)
{
	if(lpszData == NULL) 
		return TRUE;
	
	int nLen = strlen(lpszData);
	int nWrited = 0;
    return	::WriteFile(m_hComm, lpszData,nLen,(unsigned long*)&nWrited,NULL);
}

BOOL CRs232c::Send( char* lpszData, int nCount )
{
	if(lpszData == NULL) 
		return TRUE;
	
	int nWrited = 0;
    if(!::WriteFile(m_hComm, lpszData,nCount,(unsigned long*)&nWrited,NULL))
	{
		int nErr = ::GetLastError();
		return FALSE;
	}
	if(nWrited != nCount)
	{
		int nErr = ::GetLastError();
		return FALSE;
	}

	return TRUE;
}