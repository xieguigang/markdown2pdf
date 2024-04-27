#Region "Microsoft.VisualBasic::67552911f65ee2c416346ab5aa13513b, G:/GCModeller/src/runtime/markdown2pdf/src/ReportBuilder//EMailMsg.vb"

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

    '   Total Lines: 16
    '    Code Lines: 12
    ' Comment Lines: 0
    '   Blank Lines: 4
    '     File Size: 541 B


    ' Module EMailMsg
    ' 
    '     Function: GetMessage
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Text

Public Module EMailMsg

    Public Function GetMessage(Title As String, Message As String, User As String, Link As String, LinkTitle As String) As String
        Dim html As New StringBuilder(My.Resources.readmail)

        Call html.Replace("{Title}", Title)
        Call html.Replace("{UserName}", User)
        Call html.Replace("{Message}", Message)
        Call html.Replace("{LinkTitle}", LinkTitle)
        Call html.Replace("{Link}", Link)

        Return html.ToString
    End Function
End Module
