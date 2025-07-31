#!/usr/bin/env sh
set -eu

echo "Launching the 'Mind Trail' application"
cd /app/
dotnet MindTrail.WebHost.dll

exec "$@"