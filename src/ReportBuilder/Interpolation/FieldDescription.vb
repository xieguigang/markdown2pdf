#Region "Microsoft.VisualBasic::296ea2df6d6a3e82690abf0132e82a5f, G:/GCModeller/src/runtime/markdown2pdf/src/ReportBuilder//Interpolation/FieldDescription.vb"

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

    '   Total Lines: 89
    '    Code Lines: 70
    ' Comment Lines: 8
    '   Blank Lines: 11
    '     File Size: 3.13 KB


    ' Class FieldDescription
    ' 
    '     Properties: [alias], format, ordinal, referenceName
    ' 
    '     Function: (+2 Overloads) ParseDescription, parseFieldOrdinals, ToString
    ' 
    ' /********************************************************************************/

#End Region

Imports Microsoft.VisualBasic.My.JavaScript
Imports any = Microsoft.VisualBasic.Scripting

Public Class FieldDescription

    ''' <summary>
    ''' the field reference name in the csv table file
    ''' </summary>
    ''' <returns></returns>
    Public Property referenceName As String
    Public Property format As String
    Public Property ordinal As Integer
    ''' <summary>
    ''' the display title name
    ''' </summary>
    ''' <returns></returns>
    Public Property [alias] As String

    Public Overrides Function ToString() As String
        Return $"[{ordinal}]{referenceName}"
    End Function

    Public Overloads Function ToString(isHeader As Boolean, str As String) As String
        If isHeader Then
            If [alias].StringEmpty Then
                Return str
            Else
                Return [alias]
            End If
        ElseIf format.StringEmpty Then
            Return str
        Else
            Return Val(str).ToString(format)
        End If
    End Function

    Friend Shared Iterator Function parseFieldOrdinals(fieldNames As Object(), names As String()) As IEnumerable(Of FieldDescription)
        For Each name As Object In fieldNames
            If TypeOf name Is String Then
                Yield ParseDescription(DirectCast(name, String), names)
            ElseIf TypeOf name Is JavaScriptObject Then
                Yield ParseDescription(DirectCast(name, JavaScriptObject), names)
            Else
                Throw New NotImplementedException(name.GetType.FullName)
            End If
        Next
    End Function

    Public Shared Function ParseDescription(field As JavaScriptObject, names As String()) As FieldDescription
        Dim refStr As String = field.GetNames.First
        Dim opt As JavaScriptObject = field(refStr)
        Dim aliasName As String = any.ToString(opt("alias"))
        Dim format As String = any.ToString(opt("format"))

        Return New FieldDescription With {
            .referenceName = refStr,
            .ordinal = names.IndexOf(.referenceName),
            .[alias] = aliasName,
            .format = format
        }
    End Function

    Public Shared Function ParseDescription(name As String, names As String()) As FieldDescription
        Dim i As Integer = names.IndexOf(name)

        If i = -1 Then
            Dim format As String = name.Match("[:][GF]\d+", RegexICSng)

            If format.StringEmpty Then
                Return New FieldDescription With {
                    .referenceName = name,
                    .ordinal = -1
                }
            Else
                Return New FieldDescription With {
                    .referenceName = name.Replace(format, ""),
                    .ordinal = names.IndexOf(.referenceName),
                    .format = format.Trim(":"c)
                }
            End If
        Else
            Return New FieldDescription With {
                .referenceName = name,
                .ordinal = i
            }
        End If
    End Function

    Public Shared Narrowing Operator CType(field As FieldDescription) As Integer
        Return field.ordinal
    End Operator

End Class

