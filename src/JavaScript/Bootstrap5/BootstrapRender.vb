Imports Microsoft.VisualBasic.MIME.text.markdown

Public Class BootstrapRender : Inherits HtmlRender

    Public Overrides Function Image(url As String, altText As String, title As String) As String
        Dim img As String = MyBase.Image(url, altText, title)

        If Not image_url_router Is Nothing Then
            url = image_url_router(url)
        End If

        Return $"<a href=""{url}"" class=""bs-lightbox"">{img}</a>"
    End Function

End Class
