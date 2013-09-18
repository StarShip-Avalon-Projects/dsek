Imports DSeX.IniFile
Imports FastColoredTextBoxNS

Module MainModule
    Private m_Mutex As System.Threading.Mutex

    Public KeysIni As IniFile = New IniFile
    Public ini As IniFile = New IniFile
    Public EditSettings As ConfigStructs.EditSettings = New ConfigStructs.EditSettings


    Public DS_String_Style As TextStyle = New TextStyle(Brushes.Brown, Nothing, FontStyle.Italic)
    Public DS_Str_Var_Style As TextStyle = New TextStyle(New SolidBrush(Color.Green), Nothing, FontStyle.Regular)
    Public DS_Num_Var_Style As TextStyle = New TextStyle(New SolidBrush(Color.Green), Nothing, FontStyle.Regular)
    Public DS_Comment_Style As TextStyle = New TextStyle(New SolidBrush(Color.Green), Nothing, FontStyle.Regular)
    Public DS_Default_Style As TextStyle = New TextStyle(New SolidBrush(Color.Green), Nothing, FontStyle.Regular)
    Public DS_Num_Style As TextStyle = New TextStyle(New SolidBrush(Color.Green), Nothing, FontStyle.Regular)
    Public DS_Line_ID_Style As TextStyle = New TextStyle(New SolidBrush(Color.Green), Nothing, FontStyle.Regular)
    Public DS_Header_Style As TextStyle = New TextStyle(New SolidBrush(Color.Green), Nothing, FontStyle.Bold)
    Public DS_Footer_Style As TextStyle = New TextStyle(New SolidBrush(Color.Green), Nothing, FontStyle.Regular)

    '<MTAThread()> _
    'Public Sub Main(ByVal cmdArgs() As String)
    '    ' Create the Mutex class
    '    m_Mutex = New System.Threading.Mutex(False, "~DSEX~")
    '    ' Attempt to lock the Mutex
    '    If m_Mutex.WaitOne(3, True) Then ' We locked it! We are the first instance!!!
    '        Application.EnableVisualStyles()
    '        Application.DoEvents()
    '        Application.Run(New MS_Edit())

    '    Else ' Not the first instance!!!
    '        ' Raise the StartUpEvent through the SingletonCommunicator not as a NewInstance
    '        'Dim event_args As New StartUpEventArgs(False, args)
    '        'UseInstanceChannel(event_args)
    '    End If

    'End Sub

End Module
