using System.Reflection;
using System.Runtime.InteropServices;

namespace YogaSharp;

internal static class NativeLoader
{
    private static bool _isLoaded = false;
    private static readonly object _lock = new object();

    public static void Initialize()
    {
        if (_isLoaded) return;

        lock (_lock)
        {
            if (_isLoaded) return;
            NativeLibrary.SetDllImportResolver(typeof(NativeLoader).Assembly, DllImportResolver);
            _isLoaded = true;
        }
    }

    private static IntPtr DllImportResolver(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
    {
        if (libraryName == "yoga")
        {
            string os = "";
            string ext = "";
            string arch = RuntimeInformation.ProcessArchitecture.ToString().ToLower();

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                os = "win";
                ext = "dll";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                os = "linux";
                ext = "so";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                os = "osx";
                ext = "dylib";
            }
            else
            {
                throw new PlatformNotSupportedException("Unsupported OS for Yoga layout engine.");
            }

            // Map x86_64 to x64
            if (arch == "x86_64") arch = "x64";

            string artifactFolder = $"{os}-{arch}";
            string libraryFileName = os == "win" ? $"yoga.{ext}" : $"libyoga.{ext}";

            // Check next to assembly
            string assemblyDir = Path.GetDirectoryName(assembly.Location) ?? string.Empty;
            string runtimesPath = Path.Combine(assemblyDir, "runtimes", artifactFolder, libraryFileName);

            if (File.Exists(runtimesPath))
            {
                return NativeLibrary.Load(runtimesPath);
            }

            // Also check for flat structure if copied directly
            string flatPath = Path.Combine(assemblyDir, libraryFileName);
            if (File.Exists(flatPath))
            {
                return NativeLibrary.Load(flatPath);
            }

            // Fallback to default
            return NativeLibrary.Load(libraryName, assembly, searchPath);
        }

        return IntPtr.Zero;
    }
}