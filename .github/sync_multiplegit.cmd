@echo off

git pull local HEAD:master
git pull gitee HEAD:master

git push local HEAD:master
git push gitee HEAD:master

echo synchronization of this code repository job done!