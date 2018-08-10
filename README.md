&ensp;&ensp;++*This open source repository is developed and maintained by the JYTEK company, Shanghai, China, for the purpose of facilitating the development of the test and measurement using C# programming language. The project follows the GNU GPL V3.0 license and provides many commonly used T&M GUIs, utilities, data manipulations, and algorithms.*++

# SeeSharpTools Introduction

&ensp;&ensp;SeeSharpTools is a collection of open source .NET libraries provided by JYTEK. It helps developers build Measurement and Test application in an easier and efficient way. These libraries are distributed in seventeen **dll** files(dynamic link library). The library package information is listed below:

| Namespce                | Introduction                                                             |
|-------------------------|--------------------------------------------------------------------------|
| SeeSharpTools.JY.ArrayUtility       | Array calculation and Array Manipulation functions                |
| SeeSharpTools.JY.DSP.Fundamental    | Common waveform generation and spectrum algorithms                |
| SeeSharpTools.JY.DSP.FilterMCR      | Filter functions based on matlab runtime engine(MCR)              |
| SeeSharpTools.JY.DSP.SoundVibration | Sound vibration algorithms                                        |
| SeeSharpTools.JY.DSP.Utility        | Common DSP algorithms                                             |
| SeeSharpTools.JY.DSP.Utility.Fundamental| Beta version of fundmental dsp algorithms                     |
| SeeSharpTools.JY.Statistics         | Statistic algorithms                                              |
| SeeSharpTools.JY.GUI                | Common winform GUI controls for Test and Measurement industries   |
| SeeSharpTools.JY.Graph3D            | 3D data visualization winfrom GUI controls                        |
| SeeSharpTools.JY.Localization       | Winform application UI localization                               |
| SeeSharpTools.JY.File               | Data persistance by file(csv/bin/wvf)                             |
| SeeSharpTools.JY.Report             | Report and Log components                                         |
| SeeSharpTools.JY.ThreadSafeQueue    | Thread safe queue containers                                      |
| SeeSharpTools.JY.Sensors            | Sensor value convert functions                                    |
| SeeSharpTools.JY.TCP                | Data transfer by web socket                                       |
| SeeSharpTools.JY.Database           | Database operating utilities                                      |
| SeeSharpTools.JY.Audio              | Audio test Related waveform generation and related algorithms(Beta)|

&ensp;&ensp;Detailed introduction of these libraries is maintained by "SeeSharpTools User Mannual". This document is located at repository gitpage: [https://seesharpopensource.github.io/SeeSharpTools/](https://seesharpopensource.github.io/SeeSharpTools/). Developers can get the latest release of SeeSharpTools and other software dependencies from JYTEK weibsite: [http://www.jytek.com/seesharptools](http://www.jytek.com/seesharptools).

***
# Library Introductions
---
### SeeSharpTools.JY.ArrayUtility


This library contains some useful functions in array calculation and array manipulation. The classes in this library is listed below:

- **ArrayCalculation:** *[static class]* provides array calculation algorithms including: array add/substract/multiply, array offset calculation, basic statistic algorithms like rms/average/sum/abs.
- **ArrayManipulation:** *[static class]* provides array manipulation utilities including: get array subset, two dimentional-array transpose and etc.

---
### SeeSharpTools.JY.DSP.Fundamental


This library provides waveform generation function and basic DSP algorithms. The classes in this library is listed below:

- **Generation:** *[static class]* Provides waveform data generation function including: sine wave generation, square wave generation, uniform white noise generation and ramp wave generation.
- **Spectrum:** *[static class]* Provides spectrum calculation algorithm.*This algorithm is based on MKL. MKL should be installed before using Spectrum function. Developers can get the mkl installer from [JYTEK website](http://www.jytek.com/seesharptools).*

---
### SeeSharpTools.JY.DSP.FilterMCR
This library provides common filter functions. The algorithm is base on MCR(Matlab compiler runtime). Developer need to install **'MCR(Matlab compiler runtime R2017a)'** before using this library. The classes in this library is listed below:

- **IIRFilter:** Provides IIR(Infinite Impulse Response) Filter functions including: Low-pass filter, High-pass filter, Band-pass filter, Band-stop filter.
- **FIRFilter:** Provides FIR(Finite Impulse Response) Filter functions including: Low-pass filter, High-pass filter, Band-pass filter, Band-stop filter and Kaiser Window filter.
- **JYSpectrum:** *[static class]* Provides Spectrum calculation algorithm.

---
### SeeSharpTools.JY.DSP.SoundVibration
This library provides some audio analyze algorithms. Some of these algorithms are based on 'SeeSharpTools.JY.DSP.Fundamental'. The classes in this library is listed below:

- **HarmonicAnalyzer:** *[static class]* Provides THD and Fundmental Frequency calculation algorithm.

###SeeSharpTools.JY.DSP.Utility
This library provides some useful DSP algorithms. Some of these algorithms are based on 'SeeSharpTools.JY.DSP.Fundamental'. The classes in this library is listed below:

- **HarmonicAnalysis:** *[static calss]* Provides harmonic related parameters calculation including THD, THD+N, SNR, SNRAD, NoiseFloor, ENOB and etc.
- **PeakSpectrum:** *[static class]* Provides waveform Peak Frequency and Peak Amplitude calculation algorithms.
- **Phase:** *[static class]* Provides Phase Shift calculation algorithm between two related waveform.
- **SignlaProcessing:** *[static class]* Provides common data check functions and interpolation algorithms.
- **SquarewaveMeasurements:** *[static class]* Provides common sqaure wave measurement algorithms.
- **Synchronizer:** *[static class]* Provides synchronize function for Asynchronize data acquisition.

---
### SeeSharpTools.JY.DSP.Utility.Fundamental
This library provides some cross-platform DSP algorithms. The classes in this library is listed below:

- **Spectrum:** *[static class]* Provides cross-platform Spectrum calculation algorithms.

---
### SeeSharpTools.JY.Statistics
This library provides some statistic algorithms. The classes in this library is listed below:

- **Statistics:** *[static class]* Provides common statistic algorithms including: Maximum, Minimum, Mean, RMS, StandardDeviation, Variance, Skewness, Kurtosis, Histogram.

---
### SeeSharpTools.JY.GUI
This library provides some useful winform GUI controls for measurement and test industries. These GUI controls can be used in Windows and Linux environment. The picture below shows the appearance of some controls. The display effect of all the controls are shown in "SeeSharpTools User Mannual".
![avatar](media/jytek_controls.png)
The classes in this library is listed below:

- **AquaGauge:** Instrumental dashboard control used to show single analog value.
- **PressureGauge:** Instrumental dashboard control used to show single analog value.
- **Tank:** Value display control with tank style.
- **Thermometer** Value display control with thermometer appearance.
- **SevenSegment:** Value display control with standard seven-segment nixie tube style.
- **SegmentBright:** Digital tube style value display control with more configurable appearance.
- **ButtonSwitch:** Toggle switch control for boolean value input. This control support multiple display style.
- **IndustrySwitch:** Industry switch control for boolean value input. This control support multiple display style.
- **EasyButton:** Button control with preset images that more intuitive than original winform button. The preset images is configurable.
- **EasyChart:** Data Visualization control based on MS Chart. Easy to use and capable of showing substantial data. Abandoned and no longer maintained.
- **EasyChartX:** Evolving version of Data visualization control(EasyChart). The improvment including: Higher efficiency, Optimized interface, More custimization parameters, More functions.
- **StripChart:** Data logger graph with scroll style.
- **Slide:** Slide control used for analog value input.
- **GaugeLinear:** Slide control with adaptive scale and better appearance.
- **KnobControl:** Knob control for analog value input.
- **LED:** LED light control for boolean value output. This control support multiple display style.
- **LedArrow:** LED light control with arrow style. This control support multiple display style.
- **LedMatrixControl:** Control for text display with dot array style.
- **JYArray:** Control array for array data input/output.
- **PathControl:** Directory selection or input control.
- **ScrollingText:** Text display control with special scrolling effect.
- **ViewController** Control for GUI control linkage. The control can switch property "Enable" and "Visible" for now.