using NativeLibSourceGeneratorShared;
using System;
using System.Runtime.InteropServices;

namespace Neurotech.CallibriUtils
{
    public static class CallibriUtilsLibNamePropvider
    {
        public const string LibName = "callibri_utils";
        public const string LibNameiOS = "__Internal";
        public const string LibNameWin32 = LibName + "-x86";
        public const string LibNameWin64 = LibName + "-x64";
        public const string LibNameWinArm = LibName + "-arm";
        public const string LibNameWinArm64 = LibName + "-arm64";
    }

    [NativeLib(CallibriUtilsLibNamePropvider.LibName, NativePlatformType.Default)]
    [NativeLib(CallibriUtilsLibNamePropvider.LibNameWin32, NativePlatformType.WinX86)]
    [NativeLib(CallibriUtilsLibNamePropvider.LibNameWin64, NativePlatformType.WinX64)]
    [NativeLib(CallibriUtilsLibNamePropvider.LibNameWinArm, NativePlatformType.WinArm)]
    [NativeLib(CallibriUtilsLibNamePropvider.LibNameWinArm64, NativePlatformType.WinArm64)]
    [NativeLib(CallibriUtilsLibNamePropvider.LibNameiOS, NativePlatformType.iOS)]
    [NativeLib(CallibriUtilsLibNamePropvider.LibName, NativePlatformType.AndroidARMv7,
        NativePlatformType.AndroidARMv8,
        NativePlatformType.AndroidX86,
        NativePlatformType.AndroidX64,
        NativePlatformType.OSX,
        NativePlatformType.LinuxX64,
        NativePlatformType.LinuxX86)]
    public interface ICallibriMathNative
    {
        IntPtr createCallibriMathLib(int sampling_rate, int data_window, int nwins_for_pressure_index);
        void freeCallibriMathLib(IntPtr mathPtr);
        void CallibriMathLibInitFilter(IntPtr mathPtr);
        void CallibriMathLibPushData(IntPtr mathPtr, [In,Out] double[] samples, int samplesCount);
	    void CallibriMathLibProcessDataArr(IntPtr mathPtr);
        double CallibriMathLibGetRR(IntPtr mathPtr);
        double CallibriMathLibGetPressureIndex(IntPtr mathPtr);
        double CallibriMathLibGetHR(IntPtr mathPtr);
        double CallibriMathLibGetModa(IntPtr mathPtr);
        double CallibriMathLibGetAmplModa(IntPtr mathPtr);
        double CallibriMathLibGetVariationDist(IntPtr mathPtr);
        bool CallibriMathLibInitialSignalCorrupted(IntPtr mathPtr);
        void CallibriMathLibResetDataProcess(IntPtr mathPtr);
        void CallibriMathLibSetRRchecked(IntPtr mathPtr);
        void CallibriMathLibSetPressureAverage(IntPtr mathPtr, int t);
        bool CallibriMathLibRRdetected(IntPtr mathPtr);
        void CallibriMathLibClearData(IntPtr mathPtr);
    }
}
