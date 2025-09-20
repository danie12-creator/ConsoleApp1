Imports System.Data.SqlClient
Imports System.Windows.Forms

Public Class LogInForm
    Dim con As New SqlConnection("Server=(localdb)\MSSQLLocalDB;Database=UpperviewDB;Trusted_Connection=True;")

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Then
            MessageBox.Show("All fields are required", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim sql As String = "SELECT [AccountType] FROM [Account] WHERE [Username]=@user AND [Password]=@pass"

        Using cmd As New SqlCommand(sql, con)
            cmd.Parameters.AddWithValue("@user", TextBox1.Text.Trim())
            cmd.Parameters.AddWithValue("@pass", TextBox2.Text.Trim())

            Try
                con.Open()
                Dim result As Object = cmd.ExecuteScalar()

                If result IsNot Nothing Then
                    Dim accountType As String = result.ToString().ToUpper()  ' convert to uppercase

                    Select Case accountType
                        Case "ADMIN"
                            MessageBox.Show("Welcome Admin", "Login Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Dim adminMain As New adminDashboard
                            adminMain.Show()
                            Me.Hide()
                        Case "RECEPTIONIST"
                            MessageBox.Show("Welcome Receptionist", "Login Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Dim userMain As New RecepDashboard
                            userMain.Show()
                            Me.Hide()
                        Case Else
                            MessageBox.Show("Unknown user type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Select
                Else
                    MessageBox.Show("Invalid username or password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    TextBox1.Clear()
                    TextBox2.Clear()
                End If
            Catch ex As Exception
                MessageBox.Show("Database error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                con.Close()
            End Try
        End Using
    End Sub
    Private Sub TextBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            Button1.PerformClick()
        End If
    End Sub
End Class
