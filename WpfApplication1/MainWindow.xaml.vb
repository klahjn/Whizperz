Imports System.Net.Sockets
Imports System.Security
Imports System.Security.Cryptography
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Text.RegularExpressions
Imports System.Text
Class MainWindow
    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        label.Content = GetIPv4Address.ToString
        comboBox.SelectedIndex = 0
        button_Copy.IsEnabled = False
    End Sub
    Private Function GetIPv4Address() As String
        GetIPv4Address = String.Empty
        Dim strHostName As String = System.Net.Dns.GetHostName()
        Dim iphe As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(strHostName)

        For Each ipheal As System.Net.IPAddress In iphe.AddressList
            If ipheal.AddressFamily = System.Net.Sockets.AddressFamily.InterNetwork Then
                GetIPv4Address = ipheal.ToString()
            End If
        Next
    End Function
    Private Sub button_Click(sender As Object, e As RoutedEventArgs) Handles button.Click
        listBox.Items.Add("Inkrypted: " + Apple(textBox.Text)) : textBox.Tag = Apple(textBox.Text) : button_Copy.IsEnabled = True
        If checkBox1.IsChecked = True Then My.Computer.Clipboard.SetText(Apple(textBox.Text))
        'textBox.Clear()
    End Sub
    Private Function Apple(ByVal apricot As String) As String
        Dim passPhrase As String = comboBox.Text + textBox1.Text '"yourPassPhrase"
        If checkBox.IsChecked = True Then passPhrase = passPhrase + Date.Now.Day.ToString
        Dim saltValue As String = "3v3ry0n3 L0v35 t0 k33p 53cr3t5 : ™™™™™™™™¢©Œ©" + StrReverse(comboBox.Text) + comboBox.Text + StrReverse(comboBox.Text)
        Dim hashAlgorithm As String = "SHA512"
        Dim passwordIterations As Integer = 2
        Dim initVector As String = "@1B2c3D4e5F6g7H8"
        Dim keySize As Integer = 256
        Dim initVectorBytes As Byte() = Encoding.ASCII.GetBytes(initVector)
        Dim saltValueBytes As Byte() = Encoding.ASCII.GetBytes(saltValue)
        Dim plainTextBytes As Byte() = Encoding.UTF8.GetBytes(apricot)
        Dim password As New PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations)
        Dim keyBytes As Byte() = password.GetBytes(keySize \ 8)
        Dim symmetricKey As New RijndaelManaged()
        symmetricKey.Mode = CipherMode.CBC
        Dim encryptor As ICryptoTransform = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes)
        Dim memoryStream As New MemoryStream()
        Dim cryptoStream As New CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write)
        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length)
        cryptoStream.FlushFinalBlock()
        Dim cipherTextBytes As Byte() = memoryStream.ToArray()
        memoryStream.Close()
        cryptoStream.Close()
        Dim cipherText As String = Convert.ToBase64String(cipherTextBytes)
        Return cipherText
    End Function
    Private Function Banana(ByVal brie As String) As String
        On Error GoTo wormhole
        Dim passPhrase As String = comboBox.Text + textBox1.Text
        If checkBox.IsChecked = True Then passPhrase = passPhrase + Date.Now.Day.ToString
        Dim saltValue As String = "3v3ry0n3 L0v35 t0 k33p 53cr3t5 : ™™™™™™™™¢©Œ©" + StrReverse(comboBox.Text) + comboBox.Text + StrReverse(comboBox.Text)
        Dim hashAlgorithm As String = "SHA512"
        Dim passwordIterations As Integer = 2
        Dim initVector As String = "@1B2c3D4e5F6g7H8"
        Dim keySize As Integer = 256
        Dim initVectorBytes As Byte() = Encoding.ASCII.GetBytes(initVector)
        Dim saltValueBytes As Byte() = Encoding.ASCII.GetBytes(saltValue)
        Dim cipherTextBytes As Byte() = Convert.FromBase64String(brie)
        Dim password As New PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations)
        Dim keyBytes As Byte() = password.GetBytes(keySize \ 8)
        Dim symmetricKey As New RijndaelManaged()
        symmetricKey.Mode = CipherMode.CBC
        Dim decryptor As ICryptoTransform = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes)
        Dim memoryStream As New MemoryStream(cipherTextBytes)
        Dim cryptoStream As New CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read)
        Dim plainTextBytes As Byte() = New Byte(cipherTextBytes.Length - 1) {}
        Dim decryptedByteCount As Integer = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length)
        memoryStream.Close()
        cryptoStream.Close()
        Dim plainText As String = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount)
        Return plainText
wormhole:
        'MsgBox("brrr!!  Wrong!!")
    End Function
    Private Sub button_Copy_Click(sender As Object, e As RoutedEventArgs) Handles button_Copy.Click
        If textBox.Tag = "" Then button_Copy.IsEnabled = False : Exit Sub
        If Banana(textBox.Tag) <> "" Then listBox.Items.Add("dekripted: " + Banana(textBox.Tag))
    End Sub
    Private Sub listBox_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles listBox.MouseDoubleClick
        If listBox.SelectedItem.ToString = "" Then Exit Sub
        Dim monkey As String = My.Computer.Clipboard.GetText
        monkey = Replace(monkey, "Inkripted: ", "") : monkey = Replace(monkey, "dekripted: ", "")
        My.Computer.Clipboard.SetText(monkey)
    End Sub
    Private Sub button1_Click(sender As Object, e As RoutedEventArgs) Handles button1.Click
        If Banana(textBox2.Text) = "" Then Exit Sub
        listBox.Items.Add("dekripted: " + Banana(textBox2.Text))
        If checkBox1.IsChecked = True Then My.Computer.Clipboard.SetText(Banana(textBox2.Text))
        textBox2.Clear()
    End Sub
    Private Sub comboBox_DropDownOpened(sender As Object, e As EventArgs) Handles comboBox.DropDownOpened
    End Sub
End Class
