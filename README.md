# Mathematical library for working with ECG data from the Callibri sensor.
 
The main functionality is the calculation of cardio-interval lengths, heart rate and Stress Index (SI).

During the first 6 seconds the algorithm is learning, if no 5 RR-intervals are found in the signal 
5 RR-intervals are not found, the training is repeated. Further work with the library is iterative (adding new data, calculating indicators).

## Initialization
### Determine the basic parameters

1. Raw signal sampling frequency. Integer type. The allowed values are 250 or 1000.
2. Data processing window size. Integer type. Valid values of sampling_rate / 4 or sampling_rate / 2.
3. Number of windows to calculate SI. Integer type. Allowable values [20...50].
4. The averaging parameter of the IN calculation. Default value is 6.

### Creating a library instance
Firstly you need to determine lybrary parameters and then put them to library. Tne next step is initialize the filters. In the current version the filters are built-in and clearly defined: Butterworth 2nd order BandPass 5_15 Hz.

You can initialize averaging for SI calculation. It is optional value.

##### C#
```csharp
// 1. Raw signal sampling frequency
int samplingRate = 250;
// 2. Data processing window size
int dataWindow = samplingRate / 2;
// 3. Number of windows to calculate SI
int nwinsForPressureIndex = 30;

CallibriMath math = new CallibriMath(samplingRate, dataWindow, nwinsForPressureIndex);
// Filters are initialized into the constructor

// optional
// 4. The averaging parameter of the IN calculation. Default value is 6.
int pressureIndexAverage = 6;
math.SetPressureAverage(pressureIndexAverage);
```

## Initializing a data array for transfer to the library:
The size of the transmitted array has to be of a certain length:
- 25 values for a signal frequency of 250 Hz 
- 100 values for a signal frequency of 1000 Hz

##### C#
```csharp
double[] rawData = new double[25];
// or
double[] rawData = new double[100];
```

## Optional functions (not necessary for the library to work)
Check for initial signal corruption:

##### C#
```csharp
if(math.InitialSignalCorrupted()){
    // Signal corrupted!!!
}
```
### Work with the library
1. Adding and process data:

##### C#
```csharp
math.PushData(samples);
```

2. Getting the results:

##### C#
```csharp
if (math.RRdetected)
// check for a new peak in the signal
{               
    // RR-interval length
    double rr = math.RR;
    // HR     
    double hr = math.HR;
    // SI
    double pi = math.PressureIndex;
    // Moda
    double moda = math.Moda;
    // Amplitude of mode
    double amplModa = math.AmplModa;
    // Variation range
    double variationDist = math.VariationDist;
    math.SetRRchecked();	
}
```
## Finishing work with the library

##### C#
```csharp
math.Dispose();
```