Public Class ProgressFrm

    Private Delegate Sub UpdateProgressDelegate(ByVal msg As String, ByVal percentage As Integer)

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        PictureBox1.Image = DSeX.My.Resources.Resources._128x128
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Sub New(ByRef pic As Image)
        InitializeComponent()
        PictureBox1.Image = pic
    End Sub


    Public Sub UpdateProgress(ByVal msg As String, ByVal percentage As Integer)
        If Me.InvokeRequired Then
            Me.Invoke(New UpdateProgressDelegate(AddressOf UpdateProgress), New Object() {msg, percentage})
        Else
            Me.Label1.Text = msg
            If percentage >= Me.Status.Minimum AndAlso percentage <= Me.Status.Maximum Then
                Me.Status.Value = percentage
                If percentage = Me.Status.Maximum Then
                    Me.Dispose()
                End If
            End If
        End If
    End Sub

    Private Sub ProgressFrm_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.ControlBox = False
        Me.Text = ""
        Me.TopLevel = True
    End Sub

    Private Sub frmSplashScreen_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        ' Draw a Black Border around the Borderless Form:
        Dim rc As New Rectangle(0, 0, Me.ClientRectangle.Width - 1, Me.ClientRectangle.Height - 1)
        e.Graphics.DrawRectangle(Pens.Black, rc)
    End Sub

End Class