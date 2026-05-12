@echo off
setlocal

set SCRIPT_DIR=%~dp0
set BUILD_DIR=%SCRIPT_DIR%build
set ARTIFACTS_DIR=%SCRIPT_DIR%artifacts

if not exist "%BUILD_DIR%" mkdir "%BUILD_DIR%"
cd "%BUILD_DIR%"

echo Building for win-x64...

cmake -A x64 -DCMAKE_BUILD_TYPE=Release ..
cmake --build . --config Release

if not exist "%ARTIFACTS_DIR%\win-x64" mkdir "%ARTIFACTS_DIR%\win-x64"
copy /Y "Release\yoga.dll" "%ARTIFACTS_DIR%\win-x64\yoga.dll"

echo Build complete. Artifacts in %ARTIFACTS_DIR%\win-x64
