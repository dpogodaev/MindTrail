@echo off

set launchPort=%1
if [%launchPort%]==[] set launchPort=12345
echo Application URL: http://localhost:%launchPort%/swagger

set configFile=%2
if [%configFile%]==[] set configFile=mind-trail.env
echo Configuration file: %configFile%

docker run -it -p:%launchPort%:8080 --env-file %configFile% mind-trail