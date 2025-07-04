#!/usr/bin/env bash
# Pack and publish UntitledRpgLogic package to GitHub Packages
set -e

# -- Project List --
LIBRARY_PROJECTS=(
  "UntitledRpgLogic.Core"
  "UntitledRpgLogic.Extensions.Common"
  "UntitledRpgLogic.Extensions.Godot"
  "UntitledRpgLogic.Extensions.Logging"
  "UntitledRpgLogic.Infrastructure.Configuration"
  "UntitledRpgLogic.Infrastructure.Data"
  "UntitledRpgLogic.Resources"
  "UntitledRpgLogic.Services"
  "UntitledRpgLogic.StateMachines"
)
  
# The directory where the final published files will be placed.
OUTPUT_DIR="./publish"

# The solution file.
SOLUTION_FILE="./UntitledRpgLogic.sln"

# The build configuration.
CONFIGURATION="Release"

# --- Script ---
echo "🚀 Starting library publish process..."

# 1. Clean the previous publish directory.
echo "🧹 Cleaning old publish directory..."
if [ -d "$OUTPUT_DIR" ]; then
    rm -rf "$OUTPUT_DIR"
fi
mkdir -p "$OUTPUT_DIR"

# 2. Build the entire solution first.
echo "🏗️ Building the entire solution..."
dotnet build "$SOLUTION_FILE" -c "$CONFIGURATION" --no-restore

# 3. Package it up
echo "📦 Packing all libraries..."
dotnet pack "$SOLUTION_FILE" --configuration "$CONFIGURATION" --output "$OUTPUT_DIR" --no-build


# 4. Publish each library project into the same output directory.
echo "📦 Publishing libraries..."
for project in "${LIBRARY_PROJECTS[@]}"; do
    echo "   -> Publishing $project"
    
    NUPKG=$(find "$OUTPUT_DIR" -type f -name "$project*.nupkg" ! -name "*.symbols.nupkg" ! -name "*.snupkg" -print0 | 
    xargs -0 ls -t | head -n 1)
    
    if [[ -z "$NUPKG" ]]; then
      echo "No $project .nupkg file found!"
    else
      echo "Pushing $NUPKG to GitHub Packages..."
      dotnet nuget push "$NUPKG" --source "github"
    fi
done

echo "----------------------------------------"
echo "✅ All packages published successfully!"
echo "----------------------------------------"
