
Imports System.Data.OleDb
Public Class frmSelectGuest
    ' خصائص لتخزين معلومات الضيف
    Public Property GuestID As Integer
    Public Property GuestFullName As String
    Private Sub frmSelectGuest_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        display_guest()
    End Sub
    Private Sub display_guest()
        con.Open()
        Dim Dt As New DataTable("tblGuest")
        Dim rs As OleDbDataAdapter

        rs = New OleDbDataAdapter("Select * from tblGuest WHERE Remarks = 'Available' ", con)

        rs.Fill(Dt)
        Dim indx As Integer
        lvGuest.Items.Clear()
        For indx = 0 To Dt.Rows.Count - 1
            Dim lv As New ListViewItem
            lv.Text = Dt.Rows(indx).Item("ID")
            lv.SubItems.Add(Dt.Rows(indx).Item("GuestFName"))
            lv.SubItems.Add(Dt.Rows(indx).Item("GuestMName"))
            lv.SubItems.Add(Dt.Rows(indx).Item("GuestLName"))
            lvGuest.Items.Add(lv)
        Next
        rs.Dispose()
        con.Close()
    End Sub

    Private Sub lvGuest_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvGuest.DoubleClick
        Me.GuestID = CInt(lvGuest.SelectedItems(0).SubItems(0).Text) ' GuestID
        Me.GuestFullName = lvGuest.SelectedItems(0).SubItems(1).Text ' GuestFullName
        Me.Close()
    End Sub

    Private Sub lvGuest_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles lvGuest.MouseDoubleClick
        'التحقق من وجود عنصر محدد في القائمة
        If lvGuest.SelectedItems.Count > 0 Then
            ' تخزين بيانات الضيف في الخصائص
            Me.GuestID = CInt(lvGuest.SelectedItems(0).SubItems(0).Text) ' GuestID
            Me.GuestFullName = lvGuest.SelectedItems(0).SubItems(1).Text ' GuestFullName
            ' إغلاق النموذج بعد تحديد الضيف
            Me.Close()
        End If
    End Sub


End Class