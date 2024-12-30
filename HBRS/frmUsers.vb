
Imports System.Data.OleDb

Public Class frmUsers

    ' عند تحميل النموذج، نقوم بملء الـ ListView مع السجلات من جدول tblUsersInfo
    Private Sub frmUsers_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' عرض السجلات في الـ ListView عند فتح النموذج
            display_users()
            TabControl1.SelectTab(0)
        Catch ex As Exception
            MsgBox("Error loading users: " & ex.Message, vbCritical, "Error")
        End Try
    End Sub

    ' عرض بيانات المستخدمين في الـ ListView
    Private Sub display_users()
        Try
            con.Open()
            Dim Dt As New DataTable("tblUser")
            Dim rs As New OleDbDataAdapter("SELECT * FROM tblUser", con)

            rs.Fill(Dt)
            lvUser.Items.Clear()

            For indx = 0 To Dt.Rows.Count - 1
                Dim lv As New ListViewItem
                lv.Text = Dt.Rows(indx).Item("id").ToString()  ' ID المستخدم
                lv.SubItems.Add(Dt.Rows(indx).Item("user_name").ToString())  ' اسم المستخدم

                ' عرض كلمة المرور كنجوم بدلاً من النص الفعلي
                Dim password As String = Dt.Rows(indx).Item("user_password").ToString()
                lv.SubItems.Add(New String("*"c, password.Length))  ' كلمات المرور كنجوم

                lvUser.Items.Add(lv)
            Next

            rs.Dispose()
        Catch ex As Exception
            MsgBox("Error displaying users: " & ex.Message, vbCritical, "Error")
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
    End Sub

    ' إضافة مستخدم جديد عند النقر على زر Save
    Private Sub bttnSave_Click(sender As Object, e As EventArgs) Handles bttnSave.Click
        ' التحقق إذا كانت الحقول فارغة
        Dim username As String = Trim(txtUserName.Text)
        Dim password As String = Trim(txtPassword.Text)

        If username = "" Or password = "" Then
            MsgBox("Please fill all fields", vbInformation, "Note")
            Return
        End If

        Try
            con.Open()
            Dim sqlq As String = "INSERT INTO tblUser(user_name,user_password)" + "VALUES('" & username & "','" & password & "');"
            ' استعلام مُعلّم لإدخال مستخدم جديد
            Dim add_user As New OleDb.OleDbCommand(sqlq, con)
            add_user.ExecuteNonQuery()
            add_user.Dispose()
            txtUserName.Clear()
            txtPassword.Clear()
            MsgBox("User added successfully!", vbInformation, "Note")
            con.Close()
            display_users()

        Catch ex As Exception
            MsgBox("Error adding user: " & ex.Message, vbCritical, "Error")
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
    End Sub



    ' عند النقر على زر Cancel
    Private Sub bttnCancel_Click(sender As Object, e As EventArgs) Handles bttnCancel.Click
        txtUserName.Clear()
        txtPassword.Clear()
    End Sub

End Class
