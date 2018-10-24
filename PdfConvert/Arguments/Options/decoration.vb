﻿Imports Microsoft.VisualBasic.CommandLine.Reflection

Namespace Arguments

    ''' <summary>
    ''' The page decoration options: Headers And Footer Options
    ''' </summary>
    ''' <remarks>
    ''' Footers And Headers:
    ''' Headers And footers can be added to the document by the --header-* And
    ''' --footer* arguments respectfully.  In header And footer text string supplied
    ''' to e.g. --header-left, the following variables will be substituted.
    '''
    ''' * [page]       Replaced by the number of the pages currently being printed
    ''' * [frompage]   Replaced by the number of the first page to be printed
    ''' * [topage]     Replaced by the number of the last page to be printed
    ''' * [webpage]    Replaced by the URL of the page being printed
    ''' * [section]    Replaced by the name of the current section
    ''' * [subsection] Replaced by the name of the current subsection
    ''' * [date]       Replaced by the current date in system local format
    ''' * [isodate]    Replaced by the current date in ISO 8601 extended format
    ''' * [time]       Replaced by the current time in system local format
    ''' * [title]      Replaced by the title of the of the current page object
    ''' * [doctitle]   Replaced by the title of the output document
    ''' * [sitepage]   Replaced by the number of the page in the current site being converted
    ''' * [sitepages]  Replaced by the number of pages in the current site being converted
    '''
    ''' As an example specifying --header-right "Page [page] of [toPage]", will result
    ''' in the text "Page x of y" where x Is the number of the current page And y Is
    ''' the number Of the last page, To appear In the upper left corner In the document.
    ''' </remarks>
    Public Class Decoration

#Region "Footers And Headers"

        ''' <summary>
        ''' Replaced by the number of the pages currently being printed
        ''' </summary>
        Public Const [page] = "[page]"
        ''' <summary>
        ''' Replaced by the number of the first page to be printed
        ''' </summary>
        Public Const [frompage] = "[frompage]"
        ''' <summary>
        ''' Replaced by the number Of the last page To be printed
        ''' </summary>
        Public Const [topage] = "[topage]"
        ''' <summary>
        ''' Replaced by the URL Of the page being printed
        ''' </summary>
        Public Const [webpage] = "[webpage]"
        ''' <summary>
        ''' Replaced by the name Of the current section
        ''' </summary>
        Public Const [section] = "[section]"
        ''' <summary>
        ''' Replaced by the name Of the current subsection
        ''' </summary>
        Public Const [subsection] = "[subsection]"
        ''' <summary>
        ''' Replaced by the current Date In system local format
        ''' </summary>
        Public Const [date] = "[date]"
        ''' <summary>
        ''' Replaced by the current Date In ISO 8601 extended format
        ''' </summary>
        Public Const [isodate] = "[isodate]"
        ''' <summary>
        ''' Replaced by the current time In system local format
        ''' </summary>
        Public Const [time] = "[time]"
        ''' <summary>
        ''' Replaced by the title Of the Of the current page Object
        ''' </summary>
        Public Const [title] = "[title]"
        ''' <summary>
        ''' Replaced by the title Of the output document
        ''' </summary>
        Public Const [doctitle] = "[doctitle]"
        ''' <summary>
        ''' Replaced by the number Of the page In the current site being converted
        ''' </summary>
        Public Const [sitepage] = "[sitepage]"
        ''' <summary>
        ''' Replaced by the number Of pages In the current site being converted
        ''' </summary>
        Public Const [sitepages] = "[sitepages]"

#End Region

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
        Public Property footerspacing As Double?

    End Class
End Namespace