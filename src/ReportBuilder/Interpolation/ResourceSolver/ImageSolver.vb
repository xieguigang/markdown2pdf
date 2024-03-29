﻿Imports Microsoft.VisualBasic.Net.Http
Imports any = Microsoft.VisualBasic.Scripting

Public Class ImageSolver : Inherits ResourceSolver

    Public Sub New(res As ResourceDescription)
        MyBase.New(res)
    End Sub

    Public Overrides Function GetHtml(workdir As String) As String
        Dim filepath As String = getfile(workdir)
        Dim html As Boolean = resource.options.TryGetValue("html", [default]:=False)
        Dim isDataUri As Boolean = False

        If filepath.FileExists Then
            ' 20220425
            ' try to convert the image data to base64 data URI
            ' to fix the file path error in wkhtmltopdf
            filepath = New DataURI(filepath).ToString
            isDataUri = True
        End If

        If html Then
            If isDataUri OrElse filepath.FileExists Then
                Return $"<img style='{resource.styles("*")?.CSSValue}' src='{filepath}' />"
            Else
                Return any.ToString(resource.options.TryGetValue("missing", [default]:=""))
            End If
        Else
            Return filepath
        End If
    End Function

    ''' <summary>
    ''' there is a bug in wkhtmltopdf application when generates 
    ''' html page contains image file with non-ascii chars, the 
    ''' file path of the image will not be recognized in wkhtmltopdf 
    ''' application.
    ''' 
    ''' try to fix this error with the url encode of the non-ascii
    ''' chars.
    ''' </summary>
    ''' <param name="filepath"></param>
    ''' <returns></returns>
    Public Shared Function encodeLocalURL(filepath As String) As String
        Dim tokens As String() = Strings.Trim(filepath) _
            .StringSplit("[/\\]") _
            .Select(Function(str) str.UrlEncode) _
            .ToArray
        Dim newPath As String = tokens.JoinBy("/").StringReplace("[/]{2,}", "/")

        Return newPath
    End Function

    Private Function getfile(workdir As String) As String
        If resource.image.FileExists Then
            Return resource.image
        Else
            Return $"{workdir}/{resource.image}"
        End If
    End Function
End Class