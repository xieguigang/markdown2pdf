#Region "Microsoft.VisualBasic::e9c45af00c17dcc94ac5b96cbe5ed6c7, G:/GCModeller/src/runtime/markdown2pdf/src/JavaScript/d3.js//Network/JSON/v3.vb"

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

    '   Total Lines: 76
    '    Code Lines: 46
    ' Comment Lines: 15
    '   Blank Lines: 15
    '     File Size: 2.56 KB


    '     Class Regulation
    ' 
    '         Properties: MotifFamily, ORF_ID, Regulator
    ' 
    '         Function: ToString
    ' 
    '         Structure out
    ' 
    '             Properties: links, nodes
    ' 
    '             Function: ToString
    ' 
    '         Class node
    ' 
    '             Properties: color, group, ID, name, size
    '                         type, value
    ' 
    '             Function: ToString
    ' 
    '             Sub: Assign
    ' 
    '         Class link
    ' 
    '             Properties: source, target, value
    ' 
    '             Function: ToString
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports Microsoft.VisualBasic.ComponentModel
Imports Microsoft.VisualBasic.ComponentModel.Collection.Generic
Imports Microsoft.VisualBasic.Data.Framework.StorageProvider.Reflection
Imports Microsoft.VisualBasic.Serialization.JSON

Namespace Network.JSON

    Public Class Regulation

        <Column("ORF ID")> Public Property ORF_ID As String
        Public Property MotifFamily As String
        Public Property Regulator As String

        Public Overrides Function ToString() As String
            Return Me.GetJson
        End Function
    End Class

    Namespace v3

        ''' <summary>
        ''' helper class for json text generation 
        ''' </summary>
        Public Structure out

            ''' <summary>
            ''' 网络之中的节点对象
            ''' </summary>
            ''' <returns></returns>
            Public Property nodes As node()
            ''' <summary>
            ''' 节点之间的边链接
            ''' </summary>
            ''' <returns></returns>
            Public Property links As link(Of Integer)()

            Public Overrides Function ToString() As String
                Return Me.GetJson
            End Function
        End Structure

        Public Class node : Implements IAddressOf, INamedValue

            Public Property name As String Implements INamedValue.Key
            Public Property group As String
            Public Property size As Double
            Public Property type As String
            Public Property ID As Integer Implements IAddressOf.Address
            Public Property color As String
            Public Property value As Dictionary(Of String, String)

            Public Sub Assign(address As Integer) Implements IAddress(Of Integer).Assign
                ID = address
            End Sub

            Public Overrides Function ToString() As String
                Return Me.GetJson
            End Function
        End Class

        ''' <summary>
        ''' 在d3.js v3之中，边的连接使用的是节点集合之中的下标数值
        ''' 但是在d3.js v4之中，边的连接则可以直接使用节点的``id``属性来表示了
        ''' </summary>
        Public Class link(Of T)

            Public Property source As T
            Public Property target As T
            Public Property value As Double

            Public Overrides Function ToString() As String
                Return Me.GetJson
            End Function
        End Class
    End Namespace
End Namespace
