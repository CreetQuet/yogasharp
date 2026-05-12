using System.Reflection;
using System.Runtime.InteropServices;

namespace YogaSharp.Core.Interop;

internal static class NativeLoader {
    private static bool _isLoaded;
    private static readonly object _lock = new();

    public static void Initialize() {
        if (_isLoaded) return;
        lock (_lock) {
            if (_isLoaded) return;
            NativeLibrary.SetDllImportResolver(typeof(NativeLoader).Assembly, DllImportResolver);
            _isLoaded = true;
        }
    }

    private static IntPtr DllImportResolver(string libraryName, Assembly assembly, DllImportSearchPath? searchPath) {
        if (libraryName != "yoga") return IntPtr.Zero;

        string os;
        string ext;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
            os = "win";
            ext = "dll";
        } else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) {
            os = "linux";
            ext = "so";
        } else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) {
            os = "osx";
            ext = "dylib";
        } else {
            throw new PlatformNotSupportedException("Unsupported OS for Yoga layout engine.");
        }

        string arch = RuntimeInformation.ProcessArchitecture.ToString().ToLower();
        if (arch == "x86_64") arch = "x64";
        if (arch == "aarch64") arch = "arm64";

        string artifactFolder = $"{os}-{arch}";
        string libraryFileName = os == "win" ? $"yoga.{ext}" : $"libyoga.{ext}";

        string assemblyDir = Path.GetDirectoryName(assembly.Location) ?? string.Empty;

        // Try exact runtime path
        string runtimesPath = Path.Combine(assemblyDir, "runtimes", artifactFolder, "native", libraryFileName);
        if (File.Exists(runtimesPath)) return NativeLibrary.Load(runtimesPath);

        // Try fallback runtime path
        runtimesPath = Path.Combine(assemblyDir, "runtimes", artifactFolder, libraryFileName);
        if (File.Exists(runtimesPath)) return NativeLibrary.Load(runtimesPath);

        // Try flat copy
        string flatPath = Path.Combine(assemblyDir, libraryFileName);
        if (File.Exists(flatPath)) return NativeLibrary.Load(flatPath);

        // Fallback to runtime default mechanism
        return NativeLibrary.Load(libraryName, assembly, searchPath);
    }
}
