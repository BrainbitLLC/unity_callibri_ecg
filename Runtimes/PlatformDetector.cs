#if UNITY_IOS
#define __IOS__
#endif

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace NativeLibSourceGeneratorShared
{
    public enum NativePlatformType
    {
        WinX86,
        WinX64,
        WinArm,
        WinArm64,
        OSX,
        iOS,
        AndroidARMv7,
        AndroidARMv8,
        AndroidX86,
        AndroidX64,
        LinuxX86,
        LinuxX64,
        LinuxArm,
        LinuxArm64,
        Default
    }
    public static class Platform
    {
        private static NativePlatformType? _platformType;
        public static NativePlatformType Type
        {
            get
            {
                if (_platformType == null)
                {
                    _platformType = DetectPlatform();
                }

                return _platformType.Value;
            }
        }
        private static NativePlatformType DetectPlatform()
        {
#if __IOS__
            return NativePlatformType.iOS;
#elif UNITY_5_3_OR_NEWER
            switch (UnityEngine.Application.platform)
            {
                case UnityEngine.RuntimePlatform.WindowsEditor:
                case UnityEngine.RuntimePlatform.WindowsPlayer:
                    {
                        return DetectWindowsArch();
                    }
                case UnityEngine.RuntimePlatform.LinuxEditor:
                case UnityEngine.RuntimePlatform.LinuxPlayer:
                    {
                        return DetectLinuxArch();
                    }
                case UnityEngine.RuntimePlatform.Android:
                    {
                        return DetectAndroidArch();
                    }
                case UnityEngine.RuntimePlatform.OSXEditor:
                case UnityEngine.RuntimePlatform.OSXPlayer:
                    {
                        return NativePlatformType.OSX;
                    }
                case UnityEngine.RuntimePlatform.IPhonePlayer:
                    {
                        return NativePlatformType.iOS;
                    }
            }
#elif NET5_0_OR_GREATER
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return DetectWindowsArch();
            }
            else if (OperatingSystem.IsAndroid())
            {
                return DetectAndroidArch();
            }
            else if (OperatingSystem.IsIOS())
            {
                return PlatformType.iOS;
            }
            else if (OperatingSystem.IsMacOS())
            {
                return PlatformType.OSX;
            }
            else if (OperatingSystem.IsLinux())
            {
                return DetectLinuxArch();
            }
#elif WINDOWS_UWP
            return DetectWindowsArch();
#elif __MACOS__
            return NativePlatformType.OSX;
#elif __ANDROID__
            return DetectAndroidArch();
#else
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return DetectWindowsArch();
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return CheckiOS();
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                if (CheckIsAndroid())
                {
                    return GetAndroidABI();
                }
                else
                {
                    // TODO: Пока нет возможности для IL2CPP определить Android или нет, но при Mono сборке код выше корректен
                    return DetectAndroidArch();
                }
            }
#endif
            throw new PlatformNotSupportedException("Current platform is not supported by the SDK");
        }
#if !__IOS__
        private static NativePlatformType DetectWindowsArch()
        {
            switch (RuntimeInformation.ProcessArchitecture)
            {
                case Architecture.X86:
                    return NativePlatformType.WinX86;
                case Architecture.X64:
                    return NativePlatformType.WinX64;
                case Architecture.Arm:
                    return NativePlatformType.WinArm;
                case Architecture.Arm64:
                    return NativePlatformType.WinArm64;
                default:
                    throw new PlatformNotSupportedException("Current architecture is not supported by the SDK for Windows.");
            }
        }

        private static NativePlatformType DetectLinuxArch()
        {
            switch (RuntimeInformation.ProcessArchitecture)
            {
                case Architecture.X86:
                    return NativePlatformType.LinuxX86;
                case Architecture.X64:
                    return NativePlatformType.LinuxX64;
                case Architecture.Arm:
                    return NativePlatformType.LinuxArm;
                case Architecture.Arm64:
                    return NativePlatformType.LinuxArm64;
                default:
                    throw new PlatformNotSupportedException("Current architecture is not supported by the SDK for Linux.");
            }
        }
        private static NativePlatformType DetectAndroidArch()
        {
            switch (RuntimeInformation.ProcessArchitecture)
            {
                case Architecture.X86:
                    return NativePlatformType.AndroidX86;
                case Architecture.X64:
                    return NativePlatformType.AndroidX64;
                case Architecture.Arm:
                    return NativePlatformType.AndroidARMv7;
                case Architecture.Arm64:
                    return NativePlatformType.AndroidARMv8;
                default:
                    throw new PlatformNotSupportedException("Current architecture is not supported by the SDK for Android.");
            }
        }
        private static bool CheckIsAndroid()
        {
            using (var process = new System.Diagnostics.Process())
            {
                process.StartInfo.FileName = "getprop";
                process.StartInfo.Arguments = "ro.build.user";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                try
                {
                    process.Start();
                    var output = process.StandardOutput.ReadToEnd();
                    return !string.IsNullOrEmpty(output);
                }
                catch
                {
                    return false;
                }
            }
        }

        private static NativePlatformType GetAndroidABI()
        {
            using (var process = new System.Diagnostics.Process())
            {
                process.StartInfo.FileName = "getprop";
                process.StartInfo.Arguments = "ro.product.cpu.abi";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                try
                {
                    process.Start();
                    var output = process.StandardOutput.ReadToEnd();
                    if (output.Trim().Equals("arm64-v8a")) return NativePlatformType.AndroidARMv8;
                    if (output.Trim().Equals("armeabi-v7a")) return NativePlatformType.AndroidARMv7;
                    if (output.Trim().Equals("x86")) return NativePlatformType.AndroidX86;
                    if (output.Trim().Equals("x86_64")) return NativePlatformType.AndroidX64;
                    throw new InvalidOperationException($"Unrecognized platform string: {output}");
                }
                catch (Exception e)
                {
                    throw new InvalidOperationException($"Can't determine Android ABI: {e.Message}");
                }
            }
        }
#endif
        private static NativePlatformType CheckiOS()
        {
            var pLen = Marshal.AllocHGlobal(sizeof(int));
            sysctlbyname("hw.machine", IntPtr.Zero, pLen, IntPtr.Zero, 0);

            var length = Marshal.ReadInt32(pLen);

            var pStr = Marshal.AllocHGlobal(length);
            sysctlbyname("hw.machine", pStr, pLen, IntPtr.Zero, 0);

            var hardwareStr = Marshal.PtrToStringAnsi(pStr);

            Console.WriteLine("[CheckSystem] Hardware is " + hardwareStr);

            Marshal.FreeHGlobal(pLen);
            Marshal.FreeHGlobal(pStr);

            if (hardwareStr.Contains("iPhone") || hardwareStr.Contains("iPad"))
                return NativePlatformType.iOS;
            else
                return NativePlatformType.OSX;
        }
        [DllImport("libc")]
        static internal extern int sysctlbyname([MarshalAs(UnmanagedType.LPStr)] string property, IntPtr output, IntPtr oldLen, IntPtr newp, uint newlen);
    }
}
