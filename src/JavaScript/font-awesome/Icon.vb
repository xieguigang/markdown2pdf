#Region "Microsoft.VisualBasic::e24067b9265d9e0486625f22b802ffbb, G:/GCModeller/src/runtime/markdown2pdf/src/JavaScript/font-awesome//Icon.vb"

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

    '   Total Lines: 65
    '    Code Lines: 46
    ' Comment Lines: 9
    '   Blank Lines: 10
    '     File Size: 2.07 KB


    ' Class Icon
    ' 
    '     Properties: Color, Name, Preview, Style
    ' 
    '     Constructor: (+2 Overloads) Sub New
    '     Function: getClassName, (+2 Overloads) ToString
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.Imaging

''' <summary>
''' fontawesome ver5
''' </summary>
Public Class Icon

    Public Property Style As Styles
    Public Property Name As String
    Public Property Color As Color

    Public ReadOnly Property Preview As String
        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Get
            Return ToString()
        End Get
    End Property

    Sub New(icon As icons, Optional style As Styles = Styles.Solid, Optional color As Color = Nothing)
        Call Me.New(icon.Description, style, color)
    End Sub

    Sub New(name$, Optional style As Styles = Styles.Solid, Optional color As Color = Nothing)
        Me.Name = name
        Me.Style = style
        Me.Color = color
    End Sub

    <MethodImpl(MethodImplOptions.AggressiveInlining)>
    Public Overrides Function ToString() As String
        If Color.IsEmpty Then
            Return (<i class=<%= getClassName() %>></i>).ToString
        Else
            Return ToString(style:=$"color:{Color.ToHtmlColor};")
        End If
    End Function

    <MethodImpl(MethodImplOptions.AggressiveInlining)>
    Private Function getClassName() As String
        Return Style.Description & " " & Strings.LCase(Name)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="style">With CSS style values</param>
    ''' <returns></returns>
    ''' 
    <MethodImpl(MethodImplOptions.AggressiveInlining)>
    Public Overloads Function ToString(style As String) As String
        Return (<i class=<%= getClassName() %> style=<%= style %>></i>).ToString
    End Function

    <MethodImpl(MethodImplOptions.AggressiveInlining)>
    Public Shared Operator &(html$, fa As Icon) As String
        Return html & fa.ToString
    End Operator

    <MethodImpl(MethodImplOptions.AggressiveInlining)>
    Public Shared Operator &(fa As Icon, html$) As String
        Return fa.ToString & html
    End Operator
End Class
