require(Markdown2PDF);

imports "htmlReport" from "reportKit";

setwd(@dir);

htmlTemplate(url = "./headersTest.html")
|> pageHeaders()
|> flush(outputdir = "./demo_headers/")
;