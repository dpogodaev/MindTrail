@echo off

set version=%1
if [%version%]==[] set version=1.0.0.0
echo Version: %version%

docker build --network host ../.. -t mind-trail -f ./Dockerfile --build-arg version=%version%