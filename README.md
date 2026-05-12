# YogaSharp

A cross-platform native layout engine integration layer for a future declarative UI framework. This wraps Facebook's Yoga engine (version **3.2.1**).

## Architecture

This solution contains two main components:
1. **`YogaSharp.Native`**: A CMake project that compiles Facebook Yoga into a cross-platform shared library (`.dll`, `.so`, `.dylib`).
2. **`YogaSharp`**: A .NET 8 class library that provides safe, managed P/Invoke wrappers (`ILayoutNode`, `ILayoutEngine`) over the Yoga C API.

## 1. Native Build Instructions

### Prerequisites
- CMake 3.13+
- C++20 compatible compiler (MSVC, GCC, Clang)
- Bash (Linux/macOS) or Command Prompt (Windows)

### Building for Linux or macOS
Run the `build.sh` script inside the `YogaSharp.Native` directory.
```bash
cd YogaSharp.Native
chmod +x build.sh
./build.sh
```
This automatically detects your OS and architecture (e.g. `linux-x64`, `osx-arm64`) and copies the resulting `libyoga.so` or `libyoga.dylib` to `YogaSharp.Native/artifacts/<os>-<arch>/`.

### Building for Windows
Run the `build_windows.bat` script inside the `YogaSharp.Native` directory.
```cmd
cd YogaSharp.Native
build_windows.bat
```
This builds for `win-x64` and places `yoga.dll` in `YogaSharp.Native/artifacts/win-x64/`.

## 2. Managed .NET Wrapper

The .NET layer is located in `YogaSharp`. 

- Targets **.NET 8.0**
- Hides all raw `IntPtr` and raw Yoga C-API calls behind `ILayoutNode` and `ILayoutEngine`.
- Handles automatic disposal of child node trees via safe `IDisposable` patterns.
- Automatically resolves the correct platform binary at runtime using `NativeLibrary.SetDllImportResolver`.

## 3. Example Usage

```csharp
using YogaSharp;

using var engine = new YogaLayoutEngine();

using var root = engine.CreateNode();
root.Width = 500;
root.Height = 500;
root.FlexDirection = FlexDirection.Column;

using var child = engine.CreateNode();
child.FlexGrow = 1;

root.AddChild(child);

// Computes the layout with Yoga
root.CalculateLayout();

Console.WriteLine($"Child layout: {child.Layout.X}, {child.Layout.Y}, {child.Layout.Width}, {child.Layout.Height}");
```

## 4. Packaging and Distribution

The `YogaSharp.csproj` is configured to automatically package the native artifacts into the NuGet structure. 
To build the NuGet package:

```bash
cd YogaSharp
dotnet pack -c Release
```

This will produce a `YogaSharp.1.0.0.nupkg` which includes the correct `runtimes/` directory for `win-x64`, `linux-x64`, `osx-x64`, and `osx-arm64`. Any project consuming this NuGet package will automatically copy the correct native binary for their platform during execution.
