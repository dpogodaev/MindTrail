#!/usr/bin/env sh
set -eu

echo "Launching the 'Thought Guide' application"
cd /app/
dotnet ThoughtGuide.WebHost.dll

exec "$@"