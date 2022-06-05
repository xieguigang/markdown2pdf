require(Markdown2PDF);

imports "htmlReport" from "reportKit";

setwd(@dir);

htmlTemplate(url = "./headersTest.html")
|> pageHeaders()
|> countFigures()
|> countTables()
|> flush(outputdir = "./demo_headers/")
;