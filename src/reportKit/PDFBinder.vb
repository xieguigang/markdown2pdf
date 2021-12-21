' 
'  * Copyright 2009-2011 Joern Schou-Rode
'  * 
'  * This file is part of PDFBinder.
'  *
'  * PDFBinder is free software: you can redistribute it and/or modify
'  * it under the terms of the GNU General Public License as published by
'  * the Free Software Foundation, either version 3 of the License, or
'  * (at your option) any later version.
'  * PDFBinder is distributed in the hope that it will be useful,
'  * but WITHOUT ANY WARRANTY without even the implied warranty of
'  * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'  * GNU General Public License for more details.
'  * 
'  * You should have received a copy of the GNU General Public License
'  * along with PDFBinder.  If not, see <http:'www.gnu.org/licenses/>.
'  */

Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Class PDFBinder : Implements IDisposable

    ReadOnly _document As Document
    ReadOnly _pdfCopy As PdfCopy

    Private disposedValue As Boolean

    Public Sub New(outputFilePath As String)
        Dim outputStream = outputFilePath.Open(
            mode:=FileMode.OpenOrCreate,
            doClear:=True,
            [readOnly]:=False
        )

        _document = New Document()
        _pdfCopy = New PdfCopy(_document, outputStream)
        _document.Open()
    End Sub

    Public Sub AddFile(ByVal fileName As String)
        Dim reader As New PdfReader(fileName)

        For i As Integer = 1 To reader.NumberOfPages Step i + 1
            Dim size = reader.GetPageSizeWithRotation(i)
            _document.SetPageSize(size)
            _document.NewPage()

            Dim page = _pdfCopy.GetImportedPage(reader, i)
            _pdfCopy.AddPage(page)
        Next

        reader.Close()
    End Sub

    Public Shared Function TestSourceFile(ByVal fileName As String) As SourceTestResult
        Try
            Dim reader As PdfReader = New PdfReader(fileName)
            Dim ok As Boolean = Not reader.IsEncrypted() OrElse (reader.Permissions AndAlso PdfWriter.AllowAssembly) = PdfWriter.AllowAssembly

            Call reader.Close()

            Return If(ok, SourceTestResult.Ok, SourceTestResult.Protected)
        Catch
            Return SourceTestResult.Unreadable
        End Try
    End Function

    Public Enum SourceTestResult
        Ok
        Unreadable
        [Protected]
    End Enum

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: 释放托管状态(托管对象)
                Call _document.Close()
            End If

            ' TODO: 释放未托管的资源(未托管的对象)并重写终结器
            ' TODO: 将大型字段设置为 null
            disposedValue = True
        End If
    End Sub

    ' ' TODO: 仅当“Dispose(disposing As Boolean)”拥有用于释放未托管资源的代码时才替代终结器
    ' Protected Overrides Sub Finalize()
    '     ' 不要更改此代码。请将清理代码放入“Dispose(disposing As Boolean)”方法中
    '     Dispose(disposing:=False)
    '     MyBase.Finalize()
    ' End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        ' 不要更改此代码。请将清理代码放入“Dispose(disposing As Boolean)”方法中
        Dispose(disposing:=True)
        GC.SuppressFinalize(Me)
    End Sub
End Class