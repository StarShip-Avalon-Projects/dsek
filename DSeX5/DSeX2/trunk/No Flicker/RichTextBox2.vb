Imports System.Windows.Forms
Imports System.Runtime.InteropServices

Public Class RichTextBox2
    Inherits RichTextBox


    <DllImport("user32.dll", CharSet:=CharSet.Auto)> _
    Private Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal msg As Integer, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
    End Function

    Private oldEventMask As IntPtr
    Private updating As Integer
    Private Const WM_USER As Integer = &H400
    Private Const WM_SETREDRAW As Integer = &HB
    Private Const EM_GETEVENTMASK As Integer = WM_USER + 59
    Private Const EM_SETEVENTMASK As Integer = WM_USER + 69
    ''' <summary>
    ''' Maintains performance while updating.
    ''' </summary>
    ''' <remarks>
    ''' <para>
    ''' It is recommended to call this method before doing
    ''' any major updates that you do not wish the user to
    ''' see. Remember to call EndUpdate When you are finished
    ''' with the update. Nested calls are supported.
    ''' </para>
    ''' <para>
    ''' Calling this method will prevent redrawing. It will
    ''' also setup the event mask of the underlying richedit
    ''' control so that no events are sent.
    ''' </para>
    ''' </remarks>
    Public Sub BeginUpdate()
        ' Deal with nested calls.
        updating += 1

        If updating > 1 Then
            Return
        End If

        ' Prevent the control from raising any events.
        oldEventMask = SendMessage(CType(New HandleRef(Me, Handle), IntPtr), EM_SETEVENTMASK, CType(0, IntPtr), CType(0, IntPtr))

        ' Prevent the control from redrawing itself.
        SendMessage(CType(New HandleRef(Me, Handle), IntPtr), WM_SETREDRAW, CType(0, IntPtr), CType(0, IntPtr))
    End Sub

    ''' <summary>
    ''' Resumes drawing and event handling.
    ''' </summary>
    ''' <remarks>
    ''' This method should be called every time a call is made
    ''' made to BeginUpdate. It resets the event mask to it's
    ''' original value and enables redrawing of the control.
    ''' </remarks>
    Public Sub EndUpdate()
        ' Deal with nested calls.
        updating -= 1

        If updating > 0 Then
            Return
        End If

        ' Allow the control to redraw itself.
        SendMessage(CType(New HandleRef(Me, Handle), IntPtr), WM_SETREDRAW, CType(1, IntPtr), CType(0, IntPtr))

        ' Allow the control to raise event messages.
        SendMessage(CType(New HandleRef(Me, Handle), IntPtr), EM_SETEVENTMASK, CType(0, IntPtr), oldEventMask)
    End Sub

    Public Sub New()
        MyBase.New()
        Me.DoubleBuffered = True
        UndoRedoHandler = New UndoRedoClass(Of RestorableItem)
    End Sub

    'Public Overloads Sub AppendText(ByRef str As String)
    '    MyBase.AppendText(str)
    'End Sub

    Public Sub AppendLine(ByRef str As String)
        MyBase.AppendText(str + vbLf)
    End Sub

    Public Event UndoEvent As EventHandler(Of UndoRedoEventArgs)
    Public Event RedoEvent As EventHandler(Of UndoRedoEventArgs)

#Region "Enums, Structures etc."
    Private Structure RestorableItem
        Public Property EditType As EditType
        Public Property Position As Integer
        Public Property Text As String
        Public Overrides Function ToString() As String
            Return "Position: " & Me.Position & " Action: " & Me.EditType.ToString & " Text: " & Me.Text
        End Function
    End Structure

    Private Enum EditType
        None
        Inserted
        Deleted
        BackSpace
        Copy
        Paste
        Cut
        Wizard_Insert
        Fix_Indents

    End Enum

    Private Enum WindowMessages
        WM_LBUTTONDOWN = &H201
        WM_RBUTTONDOWN = &H204
        WM_MBUTTONDOWN = &H207
        WM_KEYDOWN = &H100
        WM_KEYUP = &H101
        WM_CUT = &H300
        WM_PASTE = &H302

    End Enum
#End Region

    Dim WithEvents UndoRedoHandler As UndoRedoClass(Of RestorableItem)
    Dim IsTyping As Boolean
    Public Shadows ReadOnly Property CanUndo() As Boolean
        Get
            Return UndoRedoHandler.CanUndo
        End Get
    End Property

    Public Shadows ReadOnly Property CanRedo() As Boolean
        Get
            Return UndoRedoHandler.CanRedo
        End Get
    End Property

    Public Shadows Sub Paste()
        UpdateLastRestorableItem()
        MyBase.Paste()
    End Sub
    Public Shadows Sub Paste(ByRef o As DataFormats.Format)
        UpdateLastRestorableItem()
        MyBase.Paste(o)
    End Sub
    Public Shadows Sub Undo()
        UpdateLastRestorableItem()
        UndoRestorableItem(UndoRedoHandler.CurrentItem)
        UndoRedoHandler.Undo()
    End Sub
    Public Shadows Sub Redo()
        UndoRedoHandler.Redo()
        RedoRestorableItem(UndoRedoHandler.CurrentItem)
    End Sub

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        Select Case m.Msg
            Case WindowMessages.WM_LBUTTONDOWN, WindowMessages.WM_RBUTTONDOWN, WindowMessages.WM_MBUTTONDOWN
                UpdateLastRestorableItem()
            Case WindowMessages.WM_CUT
                AddRestorableItem(EditType.Cut, Me.SelectionStart, Me.SelectedText)
            Case WindowMessages.WM_PASTE
                Debug.WriteLine("WM_PASTE")
                If Me.SelectionLength > 0 Then      '' overtyping?
                    AddRestorableItem(EditType.Deleted, Me.SelectionStart, Me.SelectedText)
                End If
                AddRestorableItem(EditType.Paste, Me.SelectionStart, My.Computer.Clipboard.GetText)
            Case WindowMessages.WM_KEYDOWN
                Dim keyCode As Keys = CType(m.WParam, Keys) And Keys.KeyCode
                Select Case keyCode
                    Case Keys.Up, Keys.Down, Keys.Left, Keys.Right, Keys.PageUp, Keys.PageDown, Keys.Home, Keys.End, Keys.Control, Keys.Alt, Keys.Escape, Keys.F1 To Keys.F12
                        UpdateLastRestorableItem()

                    Case Keys.Enter
                        AddRestorableItem(EditType.Inserted, Me.SelectionStart, vbCrLf)
                        IsTyping = True

                        '' Uncomment the following lines to save at words instead of sentences.
                    Case Keys.Space
                        AddRestorableItem(EditType.Inserted, Me.SelectionStart, Space(1))
                        IsTyping = True

                    Case Keys.Back
                        If Me.SelectionLength = 0 Then
                            If Me.SelectionStart > 0 Then
                                AddRestorableItem(EditType.BackSpace, Me.SelectionStart - 1, Me.Text.Substring(Me.SelectionStart - 1, 1))
                            End If
                        Else
                            AddRestorableItem(EditType.Deleted, Me.SelectionStart, Me.SelectedText)
                        End If

                    Case Keys.Delete
                        If Me.SelectionLength = 0 Then
                            If Me.SelectionStart < Me.TextLength Then
                                AddRestorableItem(EditType.Deleted, Me.SelectionStart, Me.Text.Substring(Me.SelectionStart, 1))
                            End If
                        Else
                            AddRestorableItem(EditType.Deleted, Me.SelectionStart, Me.SelectedText)
                        End If

                    Case Else
                        If Not IsTyping Then
                            If Me.SelectionLength > 0 Then      '' overtyping?
                                AddRestorableItem(EditType.Deleted, Me.SelectionStart, Me.SelectedText)
                            End If
                            AddRestorableItem(EditType.Inserted, Me.SelectionStart, "")
                            IsTyping = True
                        End If
                End Select
            Case WM_SETREDRAW
        End Select

        MyBase.WndProc(m)
    End Sub

    Private Sub AddRestorableItem(editType As EditType, position As Integer, text As String)
        Trace.WriteLine("AddRestorableItem()")
        UpdateLastRestorableItem()
        With UndoRedoHandler
            If .CurrentItem.EditType = editType.Inserted AndAlso String.IsNullOrEmpty(.CurrentItem.Text) Then
                'reuse the current item
            Else
                UndoRedoHandler.AddItem(New RestorableItem)
            End If
            .CurrentItem.EditType = editType
            .CurrentItem.Position = position
            .CurrentItem.Text = text
        End With
    End Sub

    Public Property SelectedText2 As String
        Get
            Return MyBase.SelectedText
        End Get
        Set(value As String)
            If Not IsTyping Then
                If Me.SelectionLength > 0 Then      '' overtyping?
                    AddRestorableItem(EditType.Deleted, Me.SelectionStart, Me.SelectedText)
                End If
                AddRestorableItem(EditType.Inserted, Me.SelectionStart, value)
                'IsTyping = True
            End If
            MyBase.SelectedText = value
            'IsTyping = False
        End Set
    End Property
    Public Property SelectedRTF2 As String
        Get
            Return MyBase.SelectedRtf
        End Get
        Set(value As String)
            If Not IsTyping Then
                If Me.SelectionLength > 0 Then      '' overtyping?
                    AddRestorableItem(EditType.Deleted, Me.SelectionStart, Me.SelectedText)
                End If
                AddRestorableItem(EditType.Inserted, Me.SelectionStart, "")
                IsTyping = True
            End If
            MyBase.SelectedRtf = value
            'IsTyping = False
        End Set
    End Property
    Private Sub UpdateLastRestorableItem()
        Trace.WriteLine("UpdateLastRestorableItem()")
        If IsTyping Then
            With UndoRedoHandler.CurrentItem
                If .EditType = EditType.Inserted Then
                    'Try

                    .Text = Me.Text.Substring(.Position, Me.SelectionStart - .Position)
                    'Catch ex As Exception
                    'Dim x As New ErrorLogging(ex, Me)
                    'End Try
                End If
                IsTyping = False
            End With
        End If
    End Sub

    Private Sub UndoRestorableItem(ByVal restorableItem As RestorableItem)
        'NOTE: we need to do reverse of what is saved in the RestorableItem
        With restorableItem
            Me.SelectionStart = .Position
            Select Case .EditType
                Case EditType.Inserted
                    Me.SelectionLength = .Text.Length
                    Me.SelectedText = ""
                Case EditType.BackSpace
                    Me.SelectedText = .Text
                Case EditType.Deleted
                    Me.SelectedText = .Text
                Case EditType.Cut
                    Me.SelectedText = .Text
                Case EditType.Paste
                    Me.SelectionLength = .Text.Length
                    Me.SelectedText = ""
            End Select
        End With
    End Sub

    Private Sub RedoRestorableItem(ByVal restorableItem As RestorableItem)
        With restorableItem
            Me.SelectionStart = .Position
            Me.SelectionLength = 0
            Select Case .EditType
                Case EditType.Inserted
                    Me.SelectedText = .Text
                Case EditType.BackSpace
                    Me.SelectionLength = 1
                    Me.SelectedText = ""
                Case EditType.Deleted
                    Me.SelectionLength = .Text.Length
                    Me.SelectedText = ""
                Case EditType.Cut
                    Me.SelectionLength = .Text.Length
                    Me.SelectedText = ""
                Case EditType.Paste
                    Me.SelectedText = .Text
                Case EditType.Fix_Indents
                    Me.SelectedText = .Text
                Case EditType.Wizard_Insert
                    Me.SelectedText = .Text

            End Select
        End With
    End Sub

    Public Sub UnDoE() Handles UndoRedoHandler.UndoHappened
        RaiseEvent UndoEvent(Me, New UndoRedoEventArgs(UndoRedoHandler.CurrentItem))
    End Sub
    Public Sub ReDoE() Handles UndoRedoHandler.RedoHappened
        RaiseEvent UndoEvent(Me, New UndoRedoEventArgs(UndoRedoHandler.CurrentItem))
    End Sub
End Class


