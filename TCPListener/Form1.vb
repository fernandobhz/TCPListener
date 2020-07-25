Imports System.IO
Imports System.Threading
Imports System.Net
Imports System.Net.Sockets

Public Class Form1

    Private Server As Sockets.TcpListener

    Private Async Sub Form1_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

        Dim Port As String = InputBox("Port", , "3128")

        Me.Text = String.Format("Listening port {0}", Port)

        Server = New Sockets.TcpListener(Net.IPAddress.Any, Port)
        Server.Start()

        Dim cA As Task(Of TcpClient) = Server.AcceptTcpClientAsync

        While Not cA.IsCompleted
            Application.DoEvents()
            Thread.Sleep(100)
            If Kabou Then Exit Sub
        End While

        Dim c As TcpClient = Await cA


        Dim s As NetworkStream = c.GetStream

        While True

            While Not s.DataAvailable
                Application.DoEvents()
                Thread.Sleep(100)
                If Kabou Then Exit Sub
            End While

            Do
                Dim BuffSize As Integer = 4096
                Dim Buffer As Byte() = New Byte(BuffSize - 1) {}
                Dim BytesRead As Integer

                BytesRead = s.Read(Buffer, 0, BuffSize)

                Me.TextBox1.AppendText(System.Text.Encoding.ASCII.GetString(Buffer).Substring(0, BytesRead))

                Application.DoEvents()
                Thread.Sleep(100)

                If Kabou Then Exit Sub
            Loop While s.DataAvailable

        End While

    End Sub

    Private Kabou As Boolean = False

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Kabou = True
    End Sub

End Class

