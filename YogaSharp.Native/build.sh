#!/bin/bash
set -e

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" &> /dev/null && pwd)"
BUILD_DIR="$SCRIPT_DIR/build"
ARTIFACTS_DIR="$SCRIPT_DIR/artifacts"

mkdir -p "$BUILD_DIR"
cd "$BUILD_DIR"

# Detect OS and Architecture
OS="$(uname -s)"
ARCH="$(uname -m)"

ARTIFACT_FOLDER=""
LIB_EXT=""

if [ "$OS" = "Linux" ]; then
    ARTIFACT_FOLDER="linux-x64"
    LIB_EXT="so"
elif [ "$OS" = "Darwin" ]; then
    LIB_EXT="dylib"
    if [ "$ARCH" = "arm64" ]; then
        ARTIFACT_FOLDER="osx-arm64"
    else
        ARTIFACT_FOLDER="osx-x64"
    fi
else
    echo "Unsupported OS: $OS"
    exit 1
fi

echo "Building for $ARTIFACT_FOLDER..."

cmake -DCMAKE_BUILD_TYPE=Release ..
cmake --build . --config Release

mkdir -p "$ARTIFACTS_DIR/$ARTIFACT_FOLDER"
cp "libyoga.$LIB_EXT" "$ARTIFACTS_DIR/$ARTIFACT_FOLDER/libyoga.$LIB_EXT"

echo "Build complete. Artifacts in $ARTIFACTS_DIR/$ARTIFACT_FOLDER"
