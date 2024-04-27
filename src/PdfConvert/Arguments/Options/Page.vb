#Region "Microsoft.VisualBasic::2609fa5dfde163f4e93582134c508620, G:/GCModeller/src/runtime/markdown2pdf/src/PdfConvert//Arguments/Options/Page.vb"

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

    '   Total Lines: 212
    '    Code Lines: 64
    ' Comment Lines: 118
    '   Blank Lines: 30
    '     File Size: 7.15 KB


    '     Enum handlers
    ' 
    '         [skip], abort, ignore
    ' 
    '  
    ' 
    ' 
    ' 
    '     Class Page
    ' 
    '         Properties: allow, bypassproxyfor, cachedir, checkboxcheckedsvg, checkboxsvg
    '                     cookies, customheader, debugjavascript, disableexternallinks, disableinternallinks
    '                     disablejavascript, enableforms, enabletocbacklinks, encoding, javascriptdelay
    '                     keeprelativelinks, loaderrorhandling, loadmediaerrorhandling, minimumfontsize, nobackground
    '                     noimages, pageoffset, runscript, userstylesheet, viewportsize
    '                     windowstatus, zoom
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports Microsoft.VisualBasic.CommandLine.Reflection

Namespace Arguments

    Public Enum handlers
        abort
        ignore
        [skip]
    End Enum

    ''' <summary>
    ''' Page Options
    ''' </summary>
    Public Class Page

        ''' <summary>
        ''' Allow the file or files from the specified folder to be loaded (repeatable)
        ''' </summary>
        ''' <returns></returns>
        <Argv("--allow", CLITypes.File)>
        Public Property allow As String

        ''' <summary>
        ''' Do not print background
        ''' </summary>
        ''' <returns></returns>
        <Argv("--no-background", CLITypes.Boolean)>
        Public Property nobackground As Boolean

        ''' <summary>
        ''' Bypass proxy for host (repeatable)
        ''' </summary>
        ''' <returns></returns>
        <Argv("--bypass-proxy-for")>
        Public Property bypassproxyfor As String

        ''' <summary>
        ''' Web cache directory
        ''' </summary>
        ''' <returns></returns>
        <Argv("--cache-dir", CLITypes.File)>
        Public Property cachedir As String

        ''' <summary>
        ''' Use this SVG file when rendering checked checkboxes
        ''' </summary>
        ''' <returns></returns>
        <Argv("--checkbox-checked-svg", CLITypes.File)>
        Public Property checkboxcheckedsvg As String

        ''' <summary>
        ''' Use this SVG file when rendering unchecked checkboxes
        ''' </summary>
        ''' <returns></returns>
        <Argv("--checkbox-svg", CLITypes.File)>
        Public Property checkboxsvg As String

        ''' <summary>
        ''' Set an additional cookie (repeatable), value should be url encoded.
        ''' (命令行程序会自动进行url转义编码)
        ''' </summary>
        ''' <returns></returns>
        ''' 
        <Argv("--cookie", CLITypes.Undefined)>
        Public Property cookies As Dictionary(Of String, String)

        ''' <summary>
        ''' Set an additional HTTP header (repeatable)
        ''' </summary>
        ''' <returns></returns>
        <Argv("--custom-header", CLITypes.Undefined)>
        Public Property customheader As Dictionary(Of String, String)

        ''' <summary>
        ''' Show javascript debugging output
        ''' </summary>
        ''' <returns></returns>
        <Argv("--debug-javascript", CLITypes.Boolean)>
        Public Property debugjavascript As Boolean

        ''' <summary>
        ''' Set the default text encoding, for input
        ''' </summary>
        ''' <returns></returns>
        <Argv("--encoding", CLITypes.String)>
        Public Property encoding As String

        ''' <summary>
        ''' Do not make links to remote web pages
        ''' </summary>
        ''' <returns></returns>
        <Argv("--disable-external-links", CLITypes.Boolean)>
        Public Property disableexternallinks As Boolean

        ''' <summary>
        ''' Specify how to handle pages that fail to load: abort, ignore Or skip (default abort)
        ''' </summary>
        ''' <returns></returns>
        <Argv("--load-error-handling", CLITypes.String)>
        Public Property loaderrorhandling As handlers?

        ''' <summary>
        ''' Specify how to handle media files that fail To load: abort, ignore Or skip (default ignore)
        ''' </summary>
        ''' <returns></returns>
        <Argv("--load-media-error-handling", CLITypes.String)>
        Public Property loadmediaerrorhandling As handlers?

        ''' <summary>
        ''' Turn HTML form fields into pdf form fields
        ''' </summary>
        ''' <returns></returns>
        <Argv("--enable-forms", CLITypes.Boolean)>
        Public Property enableforms As Boolean

        ''' <summary>
        ''' Do not load or print images
        ''' </summary>
        ''' <returns></returns>
        ''' 
        <Argv("--no-images", CLITypes.Boolean)>
        Public Property noimages As Boolean

        ''' <summary>
        ''' Do not make local links
        ''' </summary>
        ''' <returns></returns>
        <Argv("--disable-internal-links", CLITypes.Boolean)>
        Public Property disableinternallinks As Boolean

        ''' <summary>
        ''' Do not allow web pages to run javascript
        ''' </summary>
        ''' <returns></returns>
        ''' 
        <Argv("--disable-javascript", CLITypes.Boolean)>
        Public Property disablejavascript As Boolean

        ''' <summary>
        ''' Wait some milliseconds for javascript finish (default 200)
        ''' </summary>
        ''' <returns></returns>
        <Argv("--javascript-delay", CLITypes.Integer)>
        Public Property javascriptdelay As Integer?

        ''' <summary>
        ''' Keep relative external links as relative external links
        ''' </summary>
        ''' <returns></returns>
        ''' 
        <Argv("--keep-relative-links", CLITypes.Boolean)>
        Public Property keeprelativelinks As Boolean

        ''' <summary>
        ''' Minimum font size
        ''' </summary>
        ''' <returns></returns>
        <Argv("--minimum-font-size", CLITypes.Integer)>
        Public Property minimumfontsize As Integer?

        ''' <summary>
        ''' Set the starting page number (default 0)
        ''' </summary>
        ''' <returns></returns>
        <Argv("--page-offset", CLITypes.Integer)>
        Public Property pageoffset As Integer?

        ''' <summary>
        ''' Run this additional javascript after the page is done loading (repeatable)
        ''' </summary>
        ''' <returns></returns>
        <Argv("--run-script", CLITypes.Undefined)>
        Public Property runscript As String()

        ''' <summary>
        ''' Link from section header to toc
        ''' </summary>
        ''' <returns></returns>
        ''' 
        <Argv("--enable-toc-back-links", CLITypes.Boolean)>
        Public Property enabletocbacklinks As Boolean

        ''' <summary>
        ''' Specify a user style sheet, to load with every page
        ''' </summary>
        ''' <returns></returns>
        <Argv("--user-style-sheet", CLITypes.File)>
        Public Property userstylesheet As String

        ''' <summary>
        ''' Set viewport size if you have custom scrollbars Or css attribute overflow 
        ''' to emulate window size
        ''' </summary>
        ''' <returns></returns>
        <Argv("--viewport-size", CLITypes.String)>
        Public Property viewportsize As String

        ''' <summary>
        ''' Wait until window.status is equal to this String before rendering page
        ''' </summary>
        ''' <returns></returns>
        <Argv("--window-status", CLITypes.String)>
        Public Property windowstatus As String

        ''' <summary>
        ''' Use this zoom factor (default 1)
        ''' </summary>
        ''' <returns></returns>
        <Argv("--zoom", CLITypes.Double)>
        Public Property zoom As Double?
    End Class
End Namespace
