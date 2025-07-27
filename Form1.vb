Imports System.ComponentModel
Imports System.Security.Policy
Imports System.Threading

Public Class Form1

#Region "ログ"

    'Currently operating data 現在稼働中のデータ
    Private stLog As ST_LOG

#End Region

#Region "delegates デリゲート共有"

    'Drop backup file
    Private Function dlg_getTargetFile() As String
        If file1.InvokeRequired Then
            Return CType(file1.Invoke(New Func(Of String)(AddressOf dlg_getTargetFile)), String)
        Else
            Return file1.Text
        End If
    End Function

    Private Sub dlg_setTargetFile(value As String)
        If file1.InvokeRequired Then
            file1.Invoke(New Action(Of String)(AddressOf dlg_setTargetFile), value)
        Else
            file1.Text = value
        End If
    End Sub


    'Drop backup file
    Private Function dlg_getBkfolder() As String
        If file1.InvokeRequired Then
            Return CType(bkfolder1.Invoke(New Func(Of String)(AddressOf dlg_getBkfolder)), String)
        Else
            Return bkfolder1.Text
        End If
    End Function

    Private Sub dlg_setBkfolder(value As String)
        If file1.InvokeRequired Then
            bkfolder1.Invoke(New Action(Of String)(AddressOf dlg_setBkfolder), value)
        Else
            bkfolder1.Text = value
        End If
    End Sub


    'Max Revisions
    Private Function dlg_getMaxRev() As Integer
        If maxrev1.InvokeRequired Then
            Return CType(bkfolder1.Invoke(New Func(Of Integer)(AddressOf dlg_getMaxRev)), Integer)
        Else
            Return Val(maxrev1.Text)
        End If
    End Function

    Private Sub dlg_setMaxRev(value As Integer)
        If maxrev1.InvokeRequired Then
            maxrev1.Invoke(New Action(Of Integer)(AddressOf dlg_setMaxRev), value)
        Else
            maxrev1.Text = value.ToString
        End If
    End Sub

    'Interval mins.
    Private Function dlg_getInterval() As Integer
        If min1.InvokeRequired Then
            Return CType(bkfolder1.Invoke(New Func(Of Integer)(AddressOf dlg_getInterval)), Integer)
        Else
            Return Val(min1.Text)
        End If
    End Function

    Private Sub dlg_setInterval(value As Integer)
        If min1.InvokeRequired Then
            min1.Invoke(New Action(Of Integer)(AddressOf dlg_setInterval), value)
        Else
            min1.Text = value.ToString
        End If
    End Sub

    'msg
    Private Sub dlg_setMsg(value As String)
        If msg1.InvokeRequired Then
            msg1.Invoke(New Action(Of String)(AddressOf dlg_setMsg), value)
        Else
            msg1.Text = value
        End If
    End Sub

    'button text
    Private Sub dlg_setButtonText(value As String)
        If Button1.InvokeRequired Then
            Button1.Invoke(New Action(Of String)(AddressOf dlg_setButtonText), value)
        Else
            Button1.Text = value
        End If
    End Sub


#End Region

#Region "Main Thread メインスレッド "

    Private Function threadUpload(ByVal worker As System.ComponentModel.BackgroundWorker, ByVal e As System.ComponentModel.DoWorkEventArgs) As Long

        Dim result As Long = 0

        Do
            Thread.Sleep(1000)

            If stLog.go = False Then
                Continue Do
            End If

            'Remaining time until the next execution 次回実行までの残り時間
            Dim ts As TimeSpan = stLog.nextDate - DateTime.Now
            If ts.TotalSeconds > 0 Then
                'dlg_setButtonText(stLog.nextDate.ToString("HH:mm:ss"))
                dlg_setButtonText(ts.ToString("hh\:mm\:ss"))
                Continue Do
            End If

            'Check the existence of backup files バックアップファイルの存在確認
            If System.IO.File.Exists(stLog.targetFile) = False Then
                'If the file does not exist ファイルが存在しない場合
                stLog.go = False
                dlg_setButtonText("Start")
                Continue Do
            End If

            'Get the update time of the backup file. バックアップファイルの更新日時を取得
            Dim lastUpdate As DateTime = System.IO.File.GetLastWriteTime(stLog.targetFile)
            'Compare with the last update date and time 前回の更新日時と比較
            If lastUpdate = stLog.lastUpdate Then
                'If it has not been updated, do nothing and set the next execution time. 更新されていない場合は何もしないで次の実行時間を設定
                stLog.nextDate = DateTime.Now.AddMinutes(stLog.intervalMin)
                Continue Do
            End If

            'Perform backup. 更新されている場合はバックアップを実行
            'Generate backup file name. バックアップファイル名を生成
            Dim backupFileName As String = System.IO.Path.Combine(stLog.backupPath, $"{System.IO.Path.GetFileNameWithoutExtension(stLog.targetFile)}_{DateTime.Now:yyyyMMdd_HHmmss}{System.IO.Path.GetExtension(stLog.targetFile)}")
            Try
                'copy the target file to the backup folder. ターゲットファイルをバックアップフォルダにコピー
                System.IO.File.Copy(stLog.targetFile, backupFileName)
                'Display message.  バックアップ成功のメッセージを表示
                dlg_setMsg($"{DateTime.Now:H:mm:ss}")
                'recording ログに記録
                stLog.lastUpdate = lastUpdate
                log_addLog(stLog)
            Catch ex As Exception
                'MessageBox.Show("Backup failed: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

            'Check if the number of files in the backup folder exceeds maxRevision.
            'バックアップフォルダのファイル数がmaxRevisionを超えているか確認
            Dim files As String() = System.IO.Directory.GetFiles(stLog.backupPath)
            If files.Length > stLog.maxRevision Then
                'Delete the oldest file based on the update date. 更新日の一番古いファイルを削除
                Dim filesToDelete As String() = files.OrderBy(Function(f) System.IO.File.GetLastWriteTime(f)).Take(files.Length - stLog.maxRevision + 1).ToArray()
                For Each file In filesToDelete
                    Try
                        System.IO.File.Delete(file)
                    Catch ex As Exception
                        '削除失敗のログ出力など必要ならここに記述
                    End Try
                Next
            End If

            'Set the next execution time. 次回の実行時間を設定
            stLog.nextDate = DateTime.Now.AddMinutes(stLog.intervalMin)

        Loop

        Return result

    End Function


    'Main thread startup. メインスレッド起動
    Private Sub BackgroundWorker1_DoWork(sender As Object, e As DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Dim objWorker As System.ComponentModel.BackgroundWorker = CType(sender, System.ComponentModel.BackgroundWorker)
        e.Result = threadUpload(objWorker, e)

    End Sub

    '============================================================
    '   Thread process finished.スレッド処理終了
    '============================================================
    Private Sub backgroundworker1_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        If Not (e.Error Is Nothing) Then
            'MessageBox.Show(e.Error.Message)
        ElseIf e.Cancelled Then
        Else
        End If
    End Sub

#End Region

#Region "onload"

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Loading log files. ログファイルの読み込み
        log_read()

        stLog.init()

        'Start BackgroundWorker1
        BackgroundWorker1.RunWorkerAsync()

    End Sub


#End Region

#Region "Content processing コンテンツ処理"

    Private Sub resetContents(szfile As String)

        Dim sttemp As ST_LOG

        If log_getLog(szfile, sttemp) >= 0 Then
            dlg_setBkfolder(sttemp.backupPath)
            dlg_setMaxRev(sttemp.maxRevision)
            dlg_setInterval(sttemp.intervalMin)
        Else
            dlg_setBkfolder("")
            dlg_setMaxRev(100)
            dlg_setInterval(10)
        End If
        dlg_setMsg("")

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        'If currently in operation, stop. 現在稼働中なら停止
        If stLog.go = True Then
            stLog.go = False
            dlg_setButtonText("Start")
            Return
        End If

        'save contents
        Dim st As ST_LOG

        st.init()
        st.targetFile = dlg_getTargetFile()
        st.backupPath = dlg_getBkfolder()
        st.intervalMin = dlg_getInterval()
        st.maxRevision = dlg_getMaxRev()
        st.lastUpdate = DateTime.Now
        st.nextDate = DateTime.Now.AddMinutes(st.intervalMin)

        'Confirm existence of target file. targetFile存在確認
        If System.IO.File.Exists(st.targetFile) = False Then
            MessageBox.Show("Target file does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        'targetFileの更新日時を取得. targetFileの更新日時を取得
        st.lastUpdate = System.IO.File.GetLastWriteTime(st.targetFile)

        'Confirm existence of backupPath. backupPath存在確認
        If st.backupPath = "" Then
            MessageBox.Show("Backup folder is not set.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        If System.IO.Directory.Exists(st.backupPath) = False Then
            'confirm the creation. 無いので作成確認する
            If MessageBox.Show("Backup folder does not exist. Create it?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Try
                    System.IO.Directory.CreateDirectory(st.backupPath)
                Catch ex As Exception
                    MessageBox.Show("Failed to create backup folder: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End Try
            Else
                Return
            End If
        End If

        'Max Revisions check.
        If st.maxRevision < 1 Then
            MessageBox.Show("Max Revisions must be at least 1.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        'Interval mins check.
        If st.intervalMin < 1 Then
            MessageBox.Show("Interval mins must be at least 1.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        'Save the settings to the log
        log_addLog(st)
        stLog.copy(st)

        'start the process
        stLog.go = True


    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Process.Start("EXPLORER.EXE", dlg_getBkfolder())
    End Sub


    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        'form2を表示する
        Dim frm As New Form2
        frm.ShowDialog()
        'form2の内容を取得して、現在のログに反映する
        Dim idx As Integer = frm.getSelectedIndex()
        frm.Dispose()

        If idx < 0 Then
            'cancel
            Return
        End If

        dlg_setTargetFile(g_log(idx).targetFile)
        resetContents(g_log(idx).targetFile)

    End Sub



#End Region

#Region "d&d process. d&d処理"

    Private Sub file1_DragDrop(sender As Object, e As DragEventArgs) Handles file1.DragDrop

        'Stores the path of the dragged file/folder.
        Dim strFileName As String() = CType(e.Data.GetData(DataFormats.FileDrop, False), String())
        'The existence of the directory is checked, and only if it exists is the path displayed in the text box.
        If System.IO.File.Exists(strFileName(0)) = True Then
            dlg_setTargetFile(strFileName(0))
            resetContents(strFileName(0))
        Else
            resetContents("")
        End If

    End Sub

    Private Sub file1_DragEnter(sender As Object, e As DragEventArgs) Handles file1.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) = True Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub bkfolder1_DragDrop(sender As Object, e As DragEventArgs) Handles bkfolder1.DragDrop

        'Stores the path of the dragged file/folder.
        Dim strFileName As String() = CType(e.Data.GetData(DataFormats.FileDrop, False), String())
        'The existence of the directory is checked, and only if it exists is the path displayed in the text box.
        If System.IO.Directory.Exists(strFileName(0).ToString) = True Then
            dlg_setBkfolder(strFileName(0).ToString)
        Else
            dlg_setBkfolder("")
        End If


    End Sub

    Private Sub bkfolder1_DragEnter(sender As Object, e As DragEventArgs) Handles bkfolder1.DragEnter

        If e.Data.GetDataPresent(DataFormats.FileDrop) = True Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If

    End Sub




#End Region

End Class
