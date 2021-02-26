// Rs232c1.h: interface for the CRs232c class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_RS232C1_H__E7B3A3F1_7A1D_412F_AD62_F38396E8EBC7__INCLUDED_)
#define AFX_RS232C1_H__E7B3A3F1_7A1D_412F_AD62_F38396E8EBC7__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

class CRs232c  
{
private:
	HANDLE m_hComm;
public:
	BOOL SendString(LPCSTR lpszData);
	BOOL Send(char* lpszData, int nCount);
	void DiscardOutBuffer();
	void DiscardInBuffer();
	int GetInputBufferCount();
	BOOL ReadBytes(char* lpszBuffer, int nCount, int& nReadCount);
	BOOL WriteBytes(char* lpszData, int nCount, int& nWritedBytes);
	void Uninit();
	BOOL Init(int nBaudRate, short nPort);
	CRs232c();
	virtual ~CRs232c();

};

#endif // !defined(AFX_RS232C1_H__E7B3A3F1_7A1D_412F_AD62_F38396E8EBC7__INCLUDED_)
