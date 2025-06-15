#!/usr/bin/env bash
# Pack and publish UntitledRpgLogic package to GitHub Packages

START_DIR=$(pwd)
cd UntitledRpgLogic || exit 1
dotnet pack --configuration Release

# Find the most recently modified UntitledRpg.Logic*.nupkg file (excluding .snupkg)
NUPKG=$(find ./bin/Release -type f -name "UntitledRpg.Logic*.nupkg" ! -name "*.symbols.nupkg" ! -name "*.snupkg" -print0 | xargs -0 ls -t | head -n 1)

if [[ -z "$NUPKG" ]]; then
  echo "No UntitledRpg.Logic .nupkg file found!"
  cd "$START_DIR" || exit 1
  exit 1
fi

echo "Pushing $NUPKG to GitHub Packages..."
dotnet nuget push "$NUPKG" --source "github"

cd "$START_DIR" || exit 1
