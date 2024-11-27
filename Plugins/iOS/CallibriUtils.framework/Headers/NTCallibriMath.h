
#ifndef NTCallibriMath_h
#define NTCallibriMath_h

#include <Foundation/Foundation.h>
#include <Foundation/NSArray.h>

#include "common_api.h"

@interface NTCallibriMath : NSObject {
    CallibriMathLib* mathptr;
}

-(id) initWithSamplingRate:(int) samplingRate andDataWindow:(int) data_window andNWinsForPressure:(int) nwins_for_pressure_index;
-(void) pushData:(NSArray<NSNumber*>*) data;

@property (NS_NONATOMIC_IOSONLY, readonly) double RR;
@property (NS_NONATOMIC_IOSONLY, readonly) double PressureIndex;
@property (NS_NONATOMIC_IOSONLY, readonly) double HR;
@property (NS_NONATOMIC_IOSONLY, readonly) double Moda;
@property (NS_NONATOMIC_IOSONLY, readonly) double AmplModa;
@property (NS_NONATOMIC_IOSONLY, readonly) double VariationDist;
-(bool) initialSignalCorrupted;
-(void) resetDataProcess;
-(void) setRRchecked;
-(void) setPressureAverage:(int)t;
@property (NS_NONATOMIC_IOSONLY, readonly) BOOL rrDetected;
-(void) clearData;
@end

#endif /* NTCallibriMath_h */
