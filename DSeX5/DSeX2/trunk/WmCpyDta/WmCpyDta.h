// WmCpyDta.h : main header file for the WMCPYDTA DLL
//

#if !defined(AFX_WMCPYDTA_H__9D920745_F332_41C5_82FE_94EBE4BE9C81__INCLUDED_)
#define AFX_WMCPYDTA_H__9D920745_F332_41C5_82FE_94EBE4BE9C81__INCLUDED_

#if _MSC_VER >= 1000
#pragma once
#endif // _MSC_VER >= 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"		// main symbols

/////////////////////////////////////////////////////////////////////////////
// CWmCpyDtaApp
// See WmCpyDta.cpp for the implementation of this class
//
const int ciMaxFID = 4;
const int ciMaxName = 128;
const int ciMaxTag = 512;
const int ciMaxData = 2048;
const int cWM_TagDataMessageIdDefault = WM_APP + 1;


extern "C" 
{
	//Adornment or optional properties
	void WmCpyDta_SetEncrypt(char c);
	void WmCpyDta_SetSenderId(HWND hSenderId);
	HWND WmCpyDta_GetSenderId();
	void WmCpyDta_SetMessageId(WPARAM wMsgId);
	WPARAM WmCpyDta_GetMessageId();

	//Functions for sending / receiving the data structure
	void WmCpyDta_SendMessage_sTagData(HWND hReciever, HWND hSender,  
							  char szName[],  unsigned int szFID, char szTag[], char szData[] );

	BOOL WmCpyDta_GetMessage_sTagData(HWND hAcceptFrom, HWND hIdOfSender, const LPARAM lMsgStructure, 
		 char szName[],  unsigned int szFID, char szTag[], char szData[]);

	//constants calling receiving programs should know
	WPARAM WmCpyDta_BaseDefaultMsgId(){return (WPARAM)cWM_TagDataMessageIdDefault;}
	int WmCpyDta_MaxNameLen() { return ciMaxName;}
	int WmCpyDta_MaxTagLen() { return ciMaxTag;}
	int WmCpyDta_MaxDataLen() {return ciMaxData;}
	
}//end of exposed functions

class CWmCpyDtaApp : public CWinApp
{
public:
	CWmCpyDtaApp();

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CWmCpyDtaApp)
	//}}AFX_VIRTUAL

	//{{AFX_MSG(CWmCpyDtaApp)
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};


/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Developer Studio will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_WMCPYDTA_H__9D920745_F332_41C5_82FE_94EBE4BE9C81__INCLUDED_)
