Imports System.IO

Module log

#Region "log globals"

    Public Structure ST_LOG
        Dim targetFile As String    'バックアップ対象ファイル フルパス
        Dim backupPath As String   'バックアップ保存先パス
        Dim intervalMin As Integer  'バックアップ間隔（分）
        Dim maxRevision As Integer '最大保存数
        Dim lastUpdate As DateTime  'ファイルの最終更新日時
        Dim nextDate As DateTime    '次のバックアップ日時
        Dim go As Boolean           'バックアップ実行フラグ　　startボタンでon
        Public Sub init()
            targetFile = ""
            backupPath = ""
            intervalMin = 10
            maxRevision = 100
            lastUpdate = New DateTime()
            nextDate = New DateTime()
            go = False
        End Sub
        Public Sub copy(ByRef st As ST_LOG)

            init()

            targetFile = st.targetFile
            backupPath = st.backupPath
            intervalMin = st.intervalMin
            maxRevision = st.maxRevision
            lastUpdate = st.lastUpdate
            nextDate = st.nextDate

        End Sub

    End Structure

    Public g_log() As ST_LOG
    Public g_logs As Integer = 0

#End Region

#Region "log io"

    '============================================================
    '   read
    '============================================================
    Public Function log_read() As Boolean

        Dim sztemp() As String

        'reading file.log
        g_logs = 0

        Try

            Using StreamReader As New System.IO.StreamReader(My.Application.Info.DirectoryPath + "\file.log", System.Text.Encoding.GetEncoding(932))
                While Not StreamReader.EndOfStream
                    Dim line As String = StreamReader.ReadLine()
                    sztemp = line.Split(","c)
                    If sztemp.Length < 5 Then
                        Return False
                    End If
                    ReDim Preserve g_log(g_logs + 1)
                    g_log(g_logs).init()
                    g_log(g_logs).targetFile = sztemp(0)
                    g_log(g_logs).backupPath = sztemp(1)
                    g_log(g_logs).intervalMin = Val(sztemp(2))
                    g_log(g_logs).maxRevision = Val(sztemp(3))
                    g_log(g_logs).lastUpdate = DateTime.Parse(sztemp(4))
                    g_logs += 1
                End While
                StreamReader.Close()
            End Using

        Catch ex As Exception

        End Try


        Return True

    End Function

    '============================================================
    '   Write to a file. ファイルに書き込む 
    '============================================================
    Public Sub log_write()

        Try

            Using StreamWriter As New System.IO.StreamWriter(My.Application.Info.DirectoryPath + "\file.log", False, System.Text.Encoding.GetEncoding(932))
                For i As Integer = 0 To g_logs - 1
                    StreamWriter.WriteLine(g_log(i).targetFile + "," +
                                       g_log(i).backupPath + "," +
                                       g_log(i).intervalMin.ToString() + "," +
                                       g_log(i).maxRevision.ToString() + "," +
                                       g_log(i).lastUpdate.ToString("yyyy/MM/dd HH:mm:ss"))
                Next
                StreamWriter.Close()
            End Using

        Catch ex As Exception

        End Try

    End Sub

    Public Function log_getLog(ByVal targetFile As String, ByRef st As ST_LOG) As Integer
        For i As Integer = 0 To g_logs - 1
            If g_log(i).targetFile = targetFile Then
                st.copy(g_log(i))
                Return i
            End If
        Next
        Return -1
    End Function

    Public Sub log_addLog(st As ST_LOG)

        Dim sttemp As ST_LOG
        Dim idx As Integer = log_getLog(st.targetFile, sttemp)

        If idx >= 0 Then
            '既に存在する場合は更新
            g_log(idx).copy(st)
        Else
            '新規追加
            ReDim Preserve g_log(g_logs + 1)
            g_log(g_logs).copy(st)
            g_logs += 1
        End If

        log_write()

    End Sub

#End Region



End Module
