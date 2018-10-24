Imports Microsoft.VisualBasic.CommandLine.Reflection

Namespace Arguments

    ''' <summary>
    ''' The page decoration options: Headers And Footer Options
    ''' </summary>
    Public Class Decoration

        ''' <summary>
        ''' Centered footer/header text
        ''' </summary>
        ''' <returns></returns>
        <Argv("-center", CLITypes.String)>
        Public Property footercenter As String

        ''' <summary>
        ''' Set footer/header font name (default Arial)
        ''' </summary>
        ''' <returns></returns>
        <Argv("-font-name", CLITypes.String)>
        Public Property footerfontname As String

        ''' <summary>
        ''' Set footer/header font size (default 12)
        ''' </summary>
        ''' <returns></returns>
        ''' 
        <Argv("-font-size", CLITypes.Double)>
        Public Property footerfontsize As Double?

        ''' <summary>
        ''' Adds a html footer/header
        ''' </summary>
        ''' <returns></returns>
        <Argv("-html", CLITypes.File)>
        Public Property footerhtml As String

        ''' <summary>
        ''' Left aligned footer/header text
        ''' </summary>
        ''' <returns></returns>
        ''' 
        <Argv("-left", CLITypes.String)>
        Public Property footerleft As String

        ''' <summary>
        ''' Display line above the footer/header
        ''' </summary>
        ''' <returns></returns>
        ''' 
        <Argv("-line", CLITypes.Boolean)>
        Public Property footerline As Boolean

        ''' <summary>
        ''' Right aligned footer/header text
        ''' </summary>
        ''' <returns></returns>
        <Argv("-right", CLITypes.String)>
        Public Property footerright As String

        ''' <summary>
        ''' Spacing between footer/header and content in mm (default 0)
        ''' </summary>
        ''' <returns></returns>
        <Argv("-spacing", CLITypes.Double)>
        Public Property footerspacing As Double

    End Class

End Namespace