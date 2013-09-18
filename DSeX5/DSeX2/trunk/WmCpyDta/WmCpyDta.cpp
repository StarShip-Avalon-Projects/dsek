// WmCpyDta.cpp : Defines the initialization routines for the DLL.
//

#include "stdafx.h"
#include "WmCpyDta.h"

#ifdef DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

//
//	Note!
//
//		If this DLL is dynamically linked against the MFC
//		DLLs, any functions exported from this DLL which
//		call into MFC must have the AFX_MANAGE_STATE macro
//		added at the very beginning of the function.
//
//		For example:
//
//		extern "C" BOOL PASCAL EXPORT ExportedFunction()
//		{
//			AFX_MANAGE_STATE(AfxGetStaticModuleState());
//			// normal function body here
//		}
//
//		It is very important that this macro appear in each
//		function, prior to any calls into MFC.  This means that
//		it must appear as the first statement within the 
//		function, even before any object variable declarations
//		as their constructors may generate calls into the MFC
//		DLL.
//
//		Please see MFC Technical Notes 33 and 58 for additional
//		details.
//

/////////////////////////////////////////////////////////////////////////////
// CWmCpyDtaApp

BEGIN_MESSAGE_MAP(CWmCpyDtaApp, CWinApp)
	//{{AFX_MSG_MAP(CWmCpyDtaApp)
		// NOTE - the ClassWizard will add and remove mapping macros here.
		//    DO NOT EDIT what you see in these blocks of generated code!
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CWmCpyDtaApp construction

CWmCpyDtaApp::CWmCpyDtaApp()
{
	// TODO: add construction code here,
	// Place all significant initialization in InitInstance
}

/////////////////////////////////////////////////////////////////////////////
// The one and only CWmCpyDtaApp object

CWmCpyDtaApp theApp;
//----------------------------------------------
//These three global variables represent options that are not required to send a message from
//one process to another. However, I encourage you to read the descriptions below and decide
//for yourself if you want to define these options.
char gcEncryptedChar = ' ';  //no encryption
HWND ghSenderId = NULL;		 //do not care who the sender is
WPARAM gWM_MessageId = 0;	 //receiver has only one use for incoming message (struct sTagData)


struct sTagData
{//this is the structure that will be sent (copied) by the WM_COPYDATA

private:
	char szName[ciMaxName];
	char szTag[ciMaxTag];
	char szData[ciMaxData];
	 unsigned int szFID;
//		char bytes[sizeof szFID] ;
		//if you add more elements to this structure be aware that the structure must
	//be a const size.  You can not add classes or pointers. You can only add value types
	//not reference types.

public:
	sTagData()
	{
		memset(this, NULL, sizeof(sTagData));
	}

private:
	void Encrypt(char szBuff[], int iBuffLen, char key)
	{
		//One consideration for messages is that they are not very secure.  They can be viewed by
		//programs such as Spy.  This is a bitwise encrypt, which is only slightly more secure
		//than nothing at all.  Call it once to encrypt and again to unencrypt.

		for(int i =0; i < iBuffLen - 1; i++)
			szBuff[i] = szBuff[i] ^ key;


	}//Encrypt

public:

	void SetTagData(char* pszName,  unsigned int pszFID, char* pszTag, char* pszData, char cKey)
	{//Set the data in the structure
		if(strlen(pszName) < ciMaxName)
			strcpy(szName, pszName);
		else
		{
			strcpy(szName, "Tag too long");
			ASSERT(FALSE);
		}
		szFID = pszFID;
		         

		if(strlen(pszTag) < ciMaxTag)
			strcpy(szTag, pszTag);
		else
		{
			strcpy(szTag, "Tag too long");
			ASSERT(FALSE);
		}

		if(strlen(pszData) < ciMaxData)
			strcpy(szData, pszData);
		else
		{
			strcpy(szData, "Data too long");
			ASSERT(FALSE);
		}
		if(cKey != ' ')
		{//if key set then make data harder for the human eye to read
			Encrypt(szName, ciMaxName, cKey);
			Encrypt(szTag, ciMaxTag, cKey);
			Encrypt(szData, ciMaxData, cKey);
		}
	}//SetTagData

	char* GetName(char cKey)
	{//Set the tag value from the structure
		if(cKey != ' ')
			Encrypt(szName, ciMaxName, cKey);
		return szName;
	}//GetTag
    int GetFID(char cKey)
	{
		return szFID;
	}//GetTag
	char* GetTag(char cKey)
	{//Set the tag value from the structure
		if(cKey != ' ')
			Encrypt(szTag, ciMaxTag, cKey);
		return szTag;
	}//GetTag

	char* GetData(char cKey)
	{//Set the tag value from the structure
		if(cKey != ' ')
			Encrypt(szData, ciMaxData, cKey);
		return szData;
	}//GetData

};//sTagData
//-------------------------------------------------------


void WmCpyDta_SetEncrypt(char c)
{//pass a character such as 'a' to encrypt / unecrypt your messages
 //the sender and receiver programs must both pass the same character
	gcEncryptedChar = c;
}//WmCpyDta_SetEncrypt


void WmCpyDta_SetSenderId(HWND hSenderId)
{
//There are two reasons we might care about what process is sending our exe's messages.
 //	1.  We may want to send a message back.
 // 2.  Some other poorly coded process might HWND_BROADCAST a WM_COPYDATA message.  If this happens
 //		we should ignore the message.
 //---------------
 //The .Net C# examples show how to get this address, in the event you care about issue #1
 //for the most part I just care about issue #2.

 //I will just use this made up number to make #2 highly improbable.
 //If your really concerned about this use a GUID or tightly couple your programs so the 
 //receiver knows the handle of all sender processes.

	ghSenderId = hSenderId;
}//WmCpyDta_SetEncrypt

HWND WmCpyDta_GetSenderId()
{
	HWND hSenderId = ghSenderId; //use an explicit value set by calling program
	if(!hSenderId)
		hSenderId = (HWND)553311;//assign some arbitrary default value
	return hSenderId;
}//WmCpyDta_GenericSenderId


void WmCpyDta_SetMessageId(WPARAM wMsgId)
{
	/*The COPYDATASTRUCTURE has member dwData.  This is so we can set up a switch statement
	in the receiver process.  If your receiver has more than one use for the message 
	structure then you will need to define unique message ids for each.

	//Note this dll is not allowing you to send multiple formats of messages.
	//Without your personal modifications it is limited to sTagData structures.

	LRESULT WindowProc(UINT message, WPARAM wParam, LPARAM lParam)
	{
		if this WM_COPYDATA message has wMsgId == x then do process 1
		else if this WM_COPYDATA message has wMsgId == x+1 then do process 2
	}
	*/
	gWM_MessageId = wMsgId;
}//WmCpyDta_SetDataMessageId

WPARAM WmCpyDta_GetMessageId()
{
	return (WPARAM) gWM_MessageId;
}//WmCpyDta_TagDataMessageId



extern "C" void WmCpyDta_SendMessage_sTagData(HWND hReceiver, HWND hSender,  
									char szName[],  unsigned int szFID, char szTag[], char szData[])
{
	//send the structure sTagData to process hReceiver
	sTagData sDataToSend; //this is the sturture we will copy to another process
	sDataToSend.SetTagData(szName, szFID, szTag,  szData, gcEncryptedChar);

	WPARAM wMsgId = gWM_MessageId;
	if(!wMsgId)
		wMsgId = WmCpyDta_BaseDefaultMsgId(); //the user has not explicity set a message id

	COPYDATASTRUCT ds;
	ds.dwData = wMsgId; //a value telling the receiving app how to use this data structure
	ds.cbData = sizeof(sTagData);  //the length of data lpData points to
	ds.lpData = &sDataToSend;

	SendMessage(hReceiver, WM_COPYDATA, (WPARAM)WmCpyDta_GetSenderId(), (LPARAM)&ds);

}//WmCpyDta_SendMessages_sTagData


extern "C"	BOOL WmCpyDta_GetMessage_sTagData(HWND hAcceptFrom, HWND hIdOfSender,
									 const LPARAM lMsg, char szName[],  unsigned int szFID, char szTag[], char szData[])
{
	//convert the message received from structure sTagData into strings
	BOOL bReadMessage(FALSE); //assume this WM_COPYDATA message will not be read by this dll.
	if(!hAcceptFrom)//if caller does not specify who to accept from, then only accept messages -
		hAcceptFrom = WmCpyDta_GetSenderId();  //sent by this dll.

	if(!hIdOfSender)//if the sender did not send it's id, only accept messages sent via this dll.
		hIdOfSender = WmCpyDta_GetSenderId();


	if(hAcceptFrom == hIdOfSender)
	{
			sTagData* sDataReceived;
			COPYDATASTRUCT* ds = (COPYDATASTRUCT*)lMsg;
			if(ds->dwData == WmCpyDta_GetMessageId())
			{
				bReadMessage = TRUE; //this message was read
				sDataReceived = (sTagData*)ds->lpData;//the text that was sent by the sender
				strcpy(szName, sDataReceived->GetName(gcEncryptedChar));
				szFID = sDataReceived->GetFID(gcEncryptedChar);
				strcpy(szTag, sDataReceived->GetTag(gcEncryptedChar));
				strcpy(szData, sDataReceived->GetData(gcEncryptedChar));
			}
		
	}
	return bReadMessage;
}//WmCpyDta_GetMessage






