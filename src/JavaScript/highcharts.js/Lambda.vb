#Region "Microsoft.VisualBasic::5afb0f2bfe43f5fe73a06c4feb00f413, G:/GCModeller/src/runtime/markdown2pdf/src/JavaScript/highcharts.js//Lambda.vb"

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

    '   Total Lines: 88
    '    Code Lines: 62
    ' Comment Lines: 9
    '   Blank Lines: 17
    '     File Size: 2.85 KB


    ' Class LambdaWriter
    ' 
    '     Function: CanConvert, ReadJson, StripLambda
    ' 
    '     Sub: WriteJson
    ' 
    ' Class Lambda
    ' 
    '     Properties: [function], args
    ' 
    '     Function: ToString
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Reflection
Imports System.Text
Imports Newtonsoft.Json
Imports r = System.Text.RegularExpressions.Regex

''' <summary>
''' 
''' </summary>
''' <remarks>
''' https://stackoverflow.com/questions/11934487/custom-json-serialization-of-class
''' </remarks>
Public Class LambdaWriter : Inherits JsonConverter

    Public Overrides Sub WriteJson(writer As JsonWriter, value As Object, serializer As JsonSerializer)
        If value Is Nothing Then
            Call serializer.Serialize(writer, Nothing)
        End If

        Dim lambda$ = Nothing
        Dim properties = value _
            .GetType _
            .GetProperties _
            .Where(Function(p)
                       Return p.PropertyType Is GetType(Lambda)
                   End Function)

        Call writer.WriteStartObject()

        For Each [property] As PropertyInfo In properties
            ' write property name
            Call writer.WritePropertyName([property].Name)
            ' let the serializer serialize the value itself
            ' (so this converter will work with any other type, Not just int)
            Call lambda.InlineCopy(TryCast([property].GetValue(value, Nothing), Lambda)?.ToString())
            Call serializer.Serialize(writer, lambda)
        Next

        Call writer.WriteEndObject()
    End Sub

    Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As Object, serializer As JsonSerializer) As Object
        Throw New NotImplementedException()
    End Function

    Public Overrides Function CanConvert(objectType As Type) As Boolean
        If objectType Is GetType(Lambda) Then
            Return True
        Else
            Return False
        End If
    End Function

    Const LambdaPattern$ = Lambda.DeliStart & ".+?" & Lambda.DeliEnds

    Public Shared Function StripLambda(json As String) As String
        Dim matches = r.Matches(json, LambdaPattern, RegexICSng).ToArray
        Dim out As New StringBuilder(json)
        Dim replaceValue$

        For Each match As String In matches
            replaceValue = match _
                .Replace(Lambda.DeliStart, "") _
                .Replace(Lambda.DeliEnds, "") _
                .Replace("\r", vbCr) _
                .Replace("\n", vbLf)
            match = $"""{match}"""

            out.Replace(match, replaceValue)
        Next

        Return out.ToString
    End Function
End Class

Public Class Lambda

    Public Property args As String()
    Public Property [function] As String

    Friend Const DeliStart$ = ";<<<"
    Friend Const DeliEnds$ = ";>>>>"

    Public Overrides Function ToString() As String
        Return $"{DeliStart} function({args.JoinBy(", ")}) {{
    {[function]}
}} {DeliEnds}"
    End Function
End Class
