Public Class adminDashboard
    Public Shared adminMain
    Private Sub adminDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Button1.Enabled = False

        Dim CheckinMain As New CheckInPanel


        AddHandler CheckinMain.FormClosed,
            Sub()
                Button1.Enabled = True
            End Sub

        CheckinMain.Show()

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

    End Sub


End Class