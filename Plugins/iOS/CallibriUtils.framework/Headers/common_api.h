#ifndef COMMON_API_H
#define COMMON_API_H

#include "lib_export.h"
#include "stddef.h"

#ifdef __cplusplus
extern "C"
{
#endif

	typedef struct _CallibriMathLib CallibriMathLib;

	SDK_SHARED CallibriMathLib* createCallibriMathLib(int sampling_rate, int data_window, int nwins_for_pressure_index);
	
	SDK_SHARED void freeCallibriMathLib(CallibriMathLib*);

	SDK_SHARED void CallibriMathLibInitFilter(CallibriMathLib* callibriMathLibPtr);

	SDK_SHARED void CallibriMathLibPushData(CallibriMathLib* callibriMathLibPtr, const double* samples, size_t samplesCount);

	SDK_SHARED void CallibriMathLibProcessDataArr(CallibriMathLib* callibriMathLibPtr);

	SDK_SHARED double CallibriMathLibGetRR(CallibriMathLib* callibriMathLibPtr);

	SDK_SHARED double CallibriMathGetLastRpeakIndex(CallibriMathLib* callibriMathLibPtr);

	SDK_SHARED double CallibriMathLibGetPressureIndex(CallibriMathLib* callibriMathLibPtr);

	SDK_SHARED double CallibriMathLibGetHR(CallibriMathLib* callibriMathLibPtr);

	SDK_SHARED double CallibriMathLibGetModa(CallibriMathLib* callibriMathLibPtr);

	SDK_SHARED double CallibriMathLibGetAmplModa(CallibriMathLib* callibriMathLibPtr);

	SDK_SHARED double CallibriMathLibGetVariationDist(CallibriMathLib* callibriMathLibPtr);

	SDK_SHARED bool CallibriMathLibInitialSignalCorrupted(CallibriMathLib* callibriMathLibPtr);

	SDK_SHARED void CallibriMathLibResetDataProcess(CallibriMathLib* callibriMathLibPtr);

	SDK_SHARED void CallibriMathLibSetRRchecked(CallibriMathLib* callibriMathLibPtr);

	SDK_SHARED void CallibriMathLibSetPressureAverage(CallibriMathLib* callibriMathLibPtr, int t);
	
	SDK_SHARED bool CallibriMathLibRRdetected(CallibriMathLib* callibriMathLibPtr);

	SDK_SHARED void CallibriMathLibClearData(CallibriMathLib* callibriMathLibPtr);

#ifdef __cplusplus
}
#endif

#endif // COMMON_API_H
