@echo off

SET drive=%~d0
SET R_HOME=%drive%/GCModeller\src\R-sharp\App\net8.0
SET pkg=./Markdown2PDF.zip

%R_HOME%/Rscript.exe --build /src ../ /save %pkg%
%R_HOME%/R#.exe --install.packages %pkg%

pause