@echo off

REM git remote remove local 
REM git remote add local http://192.168.0.232:8848/xieguigang/markdown2pdf.git
REM git remote add biodeep http://git.biodeep.cn/xieguigang/markdown2pdf.git

git pull gitlink HEAD
git pull gitee HEAD
git pull biodeep HEAD

git push gitlink HEAD
git push gitee HEAD
git push biodeep HEAD

echo synchronization of this code repository job done!