require(Markdown2PDF);

imports "pdf" from "reportKit";

pdf::convertWmf("D:\GCModeller\src\R-sharp\test\syntax\graphics\plot.wmf", `${@dir}/colorBar.pdf`);