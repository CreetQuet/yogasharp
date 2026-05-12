#!/bin/bash
set -e

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" &> /dev/null && pwd)"
BUILD_DIR="$SCRIPT_DIR/build"
ARTIFACTS_DIR="$SCRIPT_DIR/artifacts"

mkdir -p "$BUILD_DIR"
cd "$BUILD_DIR"

# Detect OS and Architecture
OS="$(uname -s)"
# Allow overriding architecture via argument (useful for CI cross-compilation)
ARCH="${1:-$(uname -m)}"

ARTIFACT_FOLDER=""
LIB_EXT=""
CMAKE_FLAGS="-DCMAKE_BUILD_TYPE=Release"

if [ "$OS" = "Linux" ]; then
    ARTIFACT_FOLDER="linux-x64"
    LIB_EXT="so"
elif [ "$OS" = "Darwin" ]; then
    LIB_EXT="dylib"
    if [ "$ARCH" = "arm64" ]; then
        ARTIFACT_FOLDER="osx-arm64"
        CMAKE_FLAGS="$CMAKE_FLAGS -DCMAKE_OSX_ARCHITECTURES=arm64"
    elif [ "$ARCH" = "x86_64" ] || [ "$ARCH" = "x64" ]; then
        ARTIFACT_FOLDER="osx-x64"
        ARCH="x86_64" # Normalize for CMake
        CMAKE_FLAGS="$CMAKE_FLAGS -DCMAKE_OSX_ARCHITECTURES=x86_64"
    else
        echo "Unsupported macOS Architecture: $ARCH"
        exit 1
    fi
else
    echo "Unsupported OS: $OS"
    exit 1
fi

echo "Building for $ARTIFACT_FOLDER ($ARCH)..."

# Clean previous build to avoid arch conflicts
rm -rf *
cmake $CMAKE_FLAGS ..
cmake --build . --config Release

mkdir -p "$ARTIFACTS_DIR/$ARTIFACT_FOLDER"
cp "libyoga.$LIB_EXT" "$ARTIFACTS_DIR/$ARTIFACT_FOLDER/libyoga.$LIB_EXT"

echo "Build complete. Artifacts in $ARTIFACTS_DIR/$ARTIFACT_FOLDER"
