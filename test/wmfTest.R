require(Markdown2PDF);

imports "pdf" from "reportKit";

pdf::convertWmf("D:\GCModeller\src\R-sharp\test\syntax\graphics\demo-math-plot.png", `${@dir}/colorBar.pdf`);