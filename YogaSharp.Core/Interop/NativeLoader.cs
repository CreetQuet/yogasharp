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

        string rid = GetRuntimeIdentifier();
        string libraryFileName = GetLibraryFileName();
        
        var probeDirs = new List<string>();

        string assemblyDir = string.Empty;
        if (!string.IsNullOrEmpty(assembly.Location)) {
            assemblyDir = Path.GetDirectoryName(assembly.Location) ?? string.Empty;
        }

        if (!string.IsNullOrEmpty(assemblyDir)) {
            probeDirs.Add(assemblyDir);
        }


        string baseDir = AppContext.BaseDirectory;
        if (!string.IsNullOrEmpty(baseDir) && baseDir != assemblyDir) {
            probeDirs.Add(baseDir);
        }


        string cwd = Directory.GetCurrentDirectory();
        if (!string.IsNullOrEmpty(cwd) && cwd != assemblyDir && cwd != baseDir) {
            probeDirs.Add(cwd);
        }


        foreach (string dir in probeDirs) {
            string nugetPath = Path.Combine(dir, "runtimes", rid, "native", libraryFileName);
            if (TryLoad(nugetPath, out IntPtr handle)) return handle;


            string legacyPath = Path.Combine(dir, "runtimes", rid, libraryFileName);
            if (TryLoad(legacyPath, out handle)) return handle;


            string flatPath = Path.Combine(dir, libraryFileName);
            if (TryLoad(flatPath, out handle)) return handle;
        }


        string? nugetCache = GetNuGetPackagesPath();
        if (nugetCache != null) {
            string packageBase = Path.Combine(nugetCache, "yogasharp.core");
            if (Directory.Exists(packageBase)) {
                try {
                    var versionDirs = Directory.GetDirectories(packageBase)
                        .OrderByDescending(d => d)
                        .ToArray();

                    foreach (string versionDir in versionDirs) {
                        string cachePath = Path.Combine(versionDir, "runtimes", rid, "native", libraryFileName);
                        if (TryLoad(cachePath, out IntPtr handle)) return handle;
                    }
                } catch {
                }
            }
        }


        return NativeLibrary.Load(libraryName, assembly, searchPath);
    }

    private static bool TryLoad(string path, out IntPtr handle) {
        handle = IntPtr.Zero;
        if (!File.Exists(path)) return false;
        return NativeLibrary.TryLoad(path, out handle);
    }

    private static string GetRuntimeIdentifier() {
        string os;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
            os = "win";
        } else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) {
            os = "linux";
        } else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) {
            os = "osx";
        } else {
            throw new PlatformNotSupportedException("Unsupported OS for Yoga layout engine.");
        }

        string arch = RuntimeInformation.ProcessArchitecture switch {
            Architecture.X64 => "x64",
            Architecture.X86 => "x86",
            Architecture.Arm64 => "arm64",
            Architecture.Arm => "arm",
            _ => RuntimeInformation.ProcessArchitecture.ToString().ToLower()
        };

        return $"{os}-{arch}";
    }

    private static string GetLibraryFileName() {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) return "yoga.dll";
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) return "libyoga.dylib";
        return "libyoga.so";
    }

    private static string? GetNuGetPackagesPath() {
        string? nugetPackages = Environment.GetEnvironmentVariable("NUGET_PACKAGES");
        if (!string.IsNullOrEmpty(nugetPackages) && Directory.Exists(nugetPackages))
            return nugetPackages;

        string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        if (string.IsNullOrEmpty(home)) return null;

        string defaultPath = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
            ? Path.Combine(home, ".nuget", "packages")
            : Path.Combine(home, ".nuget", "packages");

        return Directory.Exists(defaultPath) ? defaultPath : null;
    }
}
