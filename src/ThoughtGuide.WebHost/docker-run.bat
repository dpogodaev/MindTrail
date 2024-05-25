@echo off

set launchPort=%1
if [%launchPort%]==[] set launchPort=12345
echo Application URL: http://localhost:%launchPort%/swagger

set configFile=%2
if [%configFile%]==[] set configFile=thought-guide.env
echo Configuration file: %configFile%

docker run -it -p:%launchPort%:80 --env-file %configFile% thought-guide