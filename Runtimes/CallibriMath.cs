using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Neurotech.CallibriUtils
{
    public class CallibriMath : IDisposable
    {
        private IntPtr _mathPtr;

        public CallibriMath(int sampling_rate, int data_window, int nwins_for_pressure_index)
        {
            _mathPtr = CallibriMathNative.Inst.createCallibriMathLib(sampling_rate,data_window, nwins_for_pressure_index);
        }

        public void InitFilter()=>  CallibriMathNative.Inst.CallibriMathLibInitFilter(_mathPtr);
        public void PushData(double[] samples) => CallibriMathNative.Inst.CallibriMathLibPushData(_mathPtr, samples, samples.Length);
        public void ProcessDataArr() => CallibriMathNative.Inst.CallibriMathLibProcessDataArr(_mathPtr);
        public double GetRR() => CallibriMathNative.Inst.CallibriMathLibGetRR(_mathPtr);
        public double GetPressureIndex() => CallibriMathNative.Inst.CallibriMathLibGetPressureIndex(_mathPtr);
        public double GetHR()=> CallibriMathNative.Inst.CallibriMathLibGetHR(_mathPtr);
        public double GetModa() => CallibriMathNative.Inst.CallibriMathLibGetModa(_mathPtr);
        public double GetAmplModa() => CallibriMathNative.Inst.CallibriMathLibGetAmplModa(_mathPtr);
        public double GetVariationDist()=> CallibriMathNative.Inst.CallibriMathLibGetVariationDist(_mathPtr);
        public bool InitialSignalCorrupted()=> CallibriMathNative.Inst.CallibriMathLibInitialSignalCorrupted(_mathPtr);
        public void ResetDataProcess() => CallibriMathNative.Inst.CallibriMathLibResetDataProcess(_mathPtr);
        public void SetRRchecked()=> CallibriMathNative.Inst.CallibriMathLibSetRRchecked(_mathPtr);
        public void SetPressureAverage(int t)=> CallibriMathNative.Inst.CallibriMathLibSetPressureAverage(_mathPtr, t);
        public bool RRdetected()=> CallibriMathNative.Inst.CallibriMathLibRRdetected(_mathPtr);
        public void ClearData() => CallibriMathNative.Inst.CallibriMathLibClearData(_mathPtr);

        public void Dispose()
        {
            if(_mathPtr != IntPtr.Zero)
            {
                CallibriMathNative.Inst.freeCallibriMathLib(_mathPtr);
                _mathPtr = IntPtr.Zero;
            }
        }
    }
}
