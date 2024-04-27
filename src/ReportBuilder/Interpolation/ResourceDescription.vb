#Region "Microsoft.VisualBasic::b5ae741d01bdcaac0b98af74affd9d62, G:/GCModeller/src/runtime/markdown2pdf/src/ReportBuilder//Interpolation/ResourceDescription.vb"

    ' Author:
    ' 
    '       xieguigang (I@xieguigang.me)
    ' 
    ' Copyright (c) 2021 R# language
    ' 
    ' 
    ' MIT License
    ' 
    ' 
    ' Permission is hereby granted, free of charge, to any person obtaining a copy
    ' of this software and associated documentation files (the "Software"), to deal
    ' in the Software without restriction, including without limitation the rights
    ' to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    ' copies of the Software, and to permit persons to whom the Software is
    ' furnished to do so, subject to the following conditions:
    ' 
    ' The above copyright notice and this permission notice shall be included in all
    ' copies or substantial portions of the Software.
    ' 
    ' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    ' IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    ' FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    ' AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    ' LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    ' OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    ' SOFTWARE.



    ' /********************************************************************************/

    ' Summaries:


    ' Code Statistics:

    '   Total Lines: 93
    '    Code Lines: 77
    ' Comment Lines: 0
    '   Blank Lines: 16
    '     File Size: 2.95 KB


    ' Class ResourceDescription
    ' 
    '     Properties: html, image, options, styles, table
    '                 text, type
    ' 
    '     Function: FillMetadata, getResourceValue, ToString
    ' 
    ' Enum ResourceTypes
    ' 
    '     html, image, table, text
    ' 
    '  
    ' 
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Text
Imports Microsoft.VisualBasic.MIME.Html.Language.CSS

Public Class ResourceDescription

    Public Property image As String
    Public Property table As String
    Public Property styles As CSSFile
    Public Property text As String
    Public Property html As String
    Public Property options As Dictionary(Of String, Object)

    Public ReadOnly Property type As ResourceTypes
        Get
            If Not text.StringEmpty Then
                Return ResourceTypes.text
            ElseIf Not html.StringEmpty Then
                Return ResourceTypes.html
            ElseIf Not table.StringEmpty Then
                Return ResourceTypes.table
            Else
                Return ResourceTypes.image
            End If
        End Get
    End Property

    Public Function FillMetadata(meta As Dictionary(Of String, String)) As ResourceDescription
        Dim text As New StringBuilder(If(Me.text, ""))
        Dim image As New StringBuilder(If(Me.image, ""))
        Dim table As New StringBuilder(If(Me.table, ""))
        Dim html As New StringBuilder(If(Me.html, ""))
        Dim opts As New Dictionary(Of String, Object)
        Dim contentText As String

        For Each key As String In meta.Keys
            contentText = meta(key)
            key = $"${{{key}}}"

            Call text.Replace(key, contentText)
            Call image.Replace(key, contentText)
            Call table.Replace(key, contentText)
            Call html.Replace(key, contentText)
        Next

        For Each key As String In options.Keys
            Dim value As Object = options(key)

            If TypeOf value Is String Then
                Dim str As New StringBuilder(DirectCast(value, String))

                For Each name As String In meta.Keys
                    Call str.Replace($"${{{name}}}", meta(name))
                Next

                value = str
            End If

            Call opts.Add(key, value)
        Next

        Return New ResourceDescription With {
            .text = text.ToString,
            .image = image.ToString,
            .table = table.ToString,
            .html = html.ToString,
            .styles = styles,
            .options = opts
        }
    End Function

    Public Function getResourceValue() As String
        Select Case type
            Case ResourceTypes.image : Return image
            Case ResourceTypes.table : Return table
            Case ResourceTypes.text : Return text
            Case ResourceTypes.html : Return html
            Case Else
                Throw New NotImplementedException(type.ToString)
        End Select
    End Function

    Public Overrides Function ToString() As String
        Return $"[{type.Description}] {getResourceValue()}"
    End Function

End Class

Public Enum ResourceTypes
    text
    image
    table
    html
End Enum
