﻿#Region "Microsoft.VisualBasic::3241a149aaeacee92d1d3abc42ece7e7, markdown2pdf\ReportBuilder\HTML\HTMLReport.vb"

' Author:
' 
'       asuka (amethyst.asuka@gcmodeller.org)
'       xie (genetics@smrucc.org)
'       xieguigang (xie.guigang@live.com)
' 
' Copyright (c) 2018 GPL3 Licensed
' 
' 
' GNU GENERAL PUBLIC LICENSE (GPL3)
' 
' 
' This program is free software: you can redistribute it and/or modify
' it under the terms of the GNU General Public License as published by
' the Free Software Foundation, either version 3 of the License, or
' (at your option) any later version.
' 
' This program is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
' GNU General Public License for more details.
' 
' You should have received a copy of the GNU General Public License
' along with this program. If not, see <http://www.gnu.org/licenses/>.



' /********************************************************************************/

' Summaries:

'     Class HTMLReport
' 
'         Properties: directory, HtmlFiles, templates
' 
'         Constructor: (+1 Overloads) Sub New
' 
'         Function: CopyTemplate, (+2 Overloads) Replace, ToString
' 
'         Sub: (+2 Overloads) Dispose, Save
' 
' 
' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.ComponentModel
Imports Microsoft.VisualBasic.FileIO
Imports Microsoft.VisualBasic.Language.UnixBash
Imports any = Microsoft.VisualBasic.Scripting

Namespace HTML

    ''' <summary>
    ''' A report template directory
    ''' </summary>
    Public Class HTMLReport : Implements IDisposable, IDataIndex

        ''' <summary>
        ''' the multiple report page
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>
        ''' 这个主要是为了兼容PC端的HTML报告和移动端的HTML报告所设置了
        ''' 因为PC端的HTML报告可能就只有一个index.html
        ''' 但是对于移动端，由于设备屏幕比较小以及为了方便在内容分区之间跳转，所以HTML报告往往会被按照内容分区分为多个html文档构成的
        ''' </remarks>
        Public ReadOnly Property templates As Dictionary(Of String, TemplateHandler)

        ''' <summary>
        ''' html报告的根目录，报告也将会被保存在这个文件夹之中
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property directory As String

        ''' <summary>
        ''' get counts of the template html pages number
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property pages As Integer
            Get
                Return templates.Count
            End Get
        End Property

        Dim minify As Boolean = True
        Dim json As New Dictionary(Of String, String)

        ''' <summary>
        ''' 获取所有<see cref="templates"/>模板的文件路径
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property HtmlFiles As String()
            <MethodImpl(MethodImplOptions.AggressiveInlining)>
            Get
                Return templates _
                    .Values _
                    .Select(Function(handler) handler.path) _
                    .ToArray
            End Get
        End Property

        ''' <summary>
        ''' 利用这个属性进行字符串替换的时候。模板之中的占位符的格式应该为``{$key_name}``
        ''' </summary>
        ''' <param name="name">在这里只需要输入``key_name``字符串即可</param>
        Default Public WriteOnly Property assign(name As String) As String
            Set(value As String)
                json(name) = value

                For Each template In templates.Values
                    template.builder(name) = value
                Next
            End Set
        End Property

        Sub New(directory$,
                Optional searchLevel As SearchOption = SearchOption.SearchTopLevelOnly,
                Optional minify As Boolean = False)

            Me.templates = (ls - l - {"*.html", "*.htm"} << searchLevel <= directory) _
                .ToDictionary(Function(path) path.BaseName,
                              Function(path)
                                  Return New TemplateHandler(path)
                              End Function)
            Me.minify = minify
            Me.directory = directory.GetDirectoryFullPath
        End Sub

        Sub New(page As TemplateHandler, Optional minify As Boolean = False)
            Dim singleKey As String = page.path.BaseName

            Me.templates = New Dictionary(Of String, TemplateHandler) From {{singleKey, page}}
            Me.minify = minify
            Me.directory = page.path.ParentPath
        End Sub

        ''' <summary>
        ''' the given <paramref name="name"/> parameter should be the
        ''' <see cref="BaseName(String, Boolean)"/> of the template page
        ''' file path
        ''' </summary>
        ''' <param name="name"></param>
        ''' <returns>
        ''' this function returns nothing if the given page is not found
        ''' </returns>
        Public Function GetPageByName(name As String) As TemplateHandler
            Return templates.Values _
                .Where(Function(page) page.path.BaseName = name) _
                .FirstOrDefault
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Sub SetByIndex(index As String, value As Object) Implements IDataIndex.SetByIndex
            If value Is Nothing Then
                Me(index) = ""
            ElseIf value.GetType.IsArray Then
                Me(index) = any.ToString(DirectCast(value, Array).GetValue(Scan0))
            Else
                Me(index) = any.ToString(value)
            End If
        End Sub

        Public Function GetByIndex(index As String, [default] As Object) As Object Implements IDataIndex.GetByIndex
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' 这个函数与<see cref="assign(String)"/>属性不同的是，这个是直接执行字符串替换，
        ''' 而<see cref="assign(String)"/>属性则是会将<paramref name="find"/>占位符拓展为
        ''' ``{$<paramref name="find"/>}``
        ''' </summary>
        ''' <param name="find">在模板之中的占位符</param>
        ''' <param name="value">将要替换为这个字符串的值</param>
        ''' <returns></returns>
        Public Function Replace(find$, value$) As HTMLReport
            For Each template In templates.Values
                Call template.builder.Replace(find, value)
            Next

            json(find.Trim("$"c)) = value

            Return Me
        End Function

        ''' <summary>
        ''' 这个方法与<see cref="Replace(String, String)"/>的功能一致，
        ''' 只不过这个方法更加方便于XML或者HTML语法的使用
        ''' </summary>
        ''' <param name="find$"></param>
        ''' <param name="value"></param>
        ''' <returns></returns>
        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Function Replace(find$, value As XElement) As HTMLReport
            Return Replace(find, value.ToString)
        End Function

        Public Overrides Function ToString() As String
            Return directory
        End Function

        Public Function Save() As HTMLReport
            For Each template In templates.Values
                Call template.Flush(minify:=minify)
            Next

            Return Me
        End Function

        Public Function ExportJSON() As Dictionary(Of String, String)
            Return json
        End Function

        Public Function Save(outputdir As String) As HTMLReport
            Dim newName As String
            Dim sourceFolder As String = Me.directory.GetDirectoryFullPath

            For Each template As TemplateHandler In templates.Values
                If Not outputdir.StringEmpty Then
                    newName = template.path _
                        .GetFullPath _
                        .Replace(sourceFolder, "")
                    newName = $"{outputdir}/{newName}"
                Else
                    newName = template.path
                End If

                Call template.Flush(
                    minify:=minify,
                    path:=newName
                )
            Next

            Return Me
        End Function

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).
                    Call Save()
                End If

                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO: set large fields to null.
            End If
            disposedValue = True
        End Sub

        ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
        'Protected Overrides Sub Finalize()
        '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        '    Dispose(False)
        '    MyBase.Finalize()
        'End Sub

        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
            Dispose(True)
            ' TODO: uncomment the following line if Finalize() is overridden above.
            ' GC.SuppressFinalize(Me)
        End Sub
#End Region

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Shared Operator &(dir As HTMLReport, fileName As String) As String
            Return (dir.directory & "/" & fileName).Replace("\", "/").StringReplace("[/]+", "/")
        End Operator

        ''' <summary>
        ''' Copy template files to a specific report output folder and then create a html report handler
        ''' </summary>
        ''' <param name="source">模板源文件夹</param>
        ''' <param name="output">生成报告文件的目标文件夹</param>
        Public Shared Function CopyTemplate(source$, output$,
                                            Optional searchLevel As SearchOption = SearchOption.SearchTopLevelOnly,
                                            Optional minify As Boolean = False) As HTMLReport
            With New Directory(source) _
                .CopyTo(target:=output) _
                .ToArray

                Return New HTMLReport(output, searchLevel, minify)
            End With
        End Function
    End Class
End Namespace
