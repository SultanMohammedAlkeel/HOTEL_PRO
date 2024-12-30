Imports System.Data.OleDb
Public Class frmSelectRoom
    Public Property RoomNumber As Integer
    Public Property RoomType As String
    Public Property RoomRate As Decimal
    Private Sub frmSelectRoom_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        display_room()
    End Sub
    Private Sub display_room()
        con.Open()
        Dim Dt As New DataTable("tblRoom")
        Dim rs As OleDbDataAdapter

        rs = New OleDbDataAdapter("Select * from tblRoom WHERE Status = 'Available'", con)

        rs.Fill(Dt)
        Dim indx As Integer
        lvRoom.Items.Clear()
        For indx = 0 To Dt.Rows.Count - 1
            Dim lv As New ListViewItem
            lv.Text = Dt.Rows(indx).Item("RoomNumber")
            lv.SubItems.Add(Dt.Rows(indx).Item("RoomType"))
            lv.SubItems.Add(Dt.Rows(indx).Item("RoomRate"))
            lv.SubItems.Add(Dt.Rows(indx).Item("NoofBeds"))
            lvRoom.Items.Add(lv)
        Next
        rs.Dispose()
        con.Close()
    End Sub

    Private Sub lvRoom_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvRoom.DoubleClick
        frmCheckin.txtRoomNumber.Text = lvRoom.SelectedItems(0).Text
        frmCheckin.txtRoomType.Text = lvRoom.SelectedItems(0).SubItems(1).Text
        frmCheckin.txtRoomRate.Text = lvRoom.SelectedItems(0).SubItems(2).Text
        frmCheckin.lblNoOfbeds.Text = lvRoom.SelectedItems(0).SubItems(3).Text

        frmReserve.txtRoomNumber.Text = lvRoom.SelectedItems(0).Text
        frmReserve.txtRoomType.Text = lvRoom.SelectedItems(0).SubItems(1).Text
        frmReserve.txtRoomRate.Text = lvRoom.SelectedItems(0).SubItems(2).Text
        frmReserve.lblNoOfbeds.Text = lvRoom.SelectedItems(0).SubItems(3).Text

        Me.Close()
    End Sub
    'Private Sub lvRoom_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvRoom.DoubleClick
    '    If frmCheckin Is Nothing Then
    '        frmCheckin = New frmCheckin()
    '    End If

    '    If frmReserve Is Nothing Then
    '        frmReserve = New frmReserve()
    '    End If
    '    ' التحقق من وجود عنصر محدد في القائمة
    '    If lvRoom.SelectedItems.Count > 0 Then
    '        frmCheckin.txtRoomNumber.Text = lvRoom.SelectedItems(0).Text
    '        frmCheckin.txtRoomType.Text = lvRoom.SelectedItems(0).SubItems(1).Text
    '        frmCheckin.txtRoomRate.Text = lvRoom.SelectedItems(0).SubItems(2).Text
    '        frmCheckin.lblNoOfbeds.Text = lvRoom.SelectedItems(0).SubItems(3).Text

    '        frmReserve.txtRoomNumber.Text = lvRoom.SelectedItems(0).Text
    '        frmReserve.txtRoomType.Text = lvRoom.SelectedItems(0).SubItems(1).Text
    '        frmReserve.txtRoomRate.Text = lvRoom.SelectedItems(0).SubItems(2).Text
    '        frmReserve.lblNoOfbeds.Text = lvRoom.SelectedItems(0).SubItems(3).Text
    '        Me.Close()
    '    Else
    '        MessageBox.Show("Please select a room from the list.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End If
    'End Sub


    Private Sub lvRoom_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lvRoom.SelectedIndexChanged
        ' عندما يختار المستخدم غرفة من القائمة
        If lvRoom.SelectedItems.Count > 0 Then
            ' تخزين البيانات في الخصائص
            Me.RoomNumber = CInt(lvRoom.SelectedItems(0).SubItems(0).Text) ' RoomNumber من البيانات المختارة
            Me.RoomType = lvRoom.SelectedItems(0).SubItems(1).Text ' RoomType من البيانات المختارة
            Me.RoomRate = CDec(lvRoom.SelectedItems(0).SubItems(2).Text) ' RoomRate من البيانات المختارة

            ' إغلاق النموذج الفرعي بعد تحديد الغرفة
            Me.Close()
        End If
    End Sub

End Class