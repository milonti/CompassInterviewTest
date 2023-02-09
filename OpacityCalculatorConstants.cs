using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CompassInterviewTest
{
    public class OpacityCalculatorConstants
    {
        public enum MaterialType
        {
            [Description("Air")]Air,
            [Description("Water")]Water,
            [Description("Carbon")]Carbon,
            [Description("Alumninum")]Aluminum
        }
        
        private const double PlankConstant = 6.626E-34;
        private const double ElementalCharge = 1.602E-19;
        
        //Frequency to electron volts
        public static Func<double, double> FrequencyToElectronVolts = 
            v => v * PlankConstant / ElementalCharge;
        
        public const double MinimumDistance = 0;
        public const double MaximumDistance = 1000;
        public const double DistanceIncrement = 100.0;
        
        public const double MinimumIntensity = 1;
        public const double MaximumIntensity = 1000;
        
        public const double MinimumFrequency = 2.41774826E19;
        public const double MaximumFrequency = 1.20887413E20;
        public const double FrequencyIncrement = 100.0;
        
        private const int ElectronVoltsA = 100000;
        private const int ElectronVoltsB = 200000;
        private const int ElectronVoltsC = 500000;
        
        //Linear Attenuation Coefficient Table (MaterialType, ElectronVolts) => LinearAttenuationCoefficient
        private static Dictionary<(MaterialType, int), double> LinearAttenuationCoefficientTable = new Dictionary<(MaterialType, int), double>()
        {
            {(MaterialType.Air, ElectronVoltsA), 0.0195},
            {(MaterialType.Air, ElectronVoltsB), 0.0159},
            {(MaterialType.Air, ElectronVoltsC), 0.0112},
            {(MaterialType.Water, ElectronVoltsA), 16.7},
            {(MaterialType.Water, ElectronVoltsB), 13.6},
            {(MaterialType.Water, ElectronVoltsC), 9.7},
            {(MaterialType.Carbon, ElectronVoltsA), 33.5},
            {(MaterialType.Carbon, ElectronVoltsB), 27.4},
            {(MaterialType.Carbon, ElectronVoltsC), 19.6},
            {(MaterialType.Aluminum, ElectronVoltsA), 43.5},
            {(MaterialType.Aluminum, ElectronVoltsB), 32.4},
            {(MaterialType.Aluminum, ElectronVoltsC), 22.7},
        };
        
        //Todo E. Redefine this function to do the following:
        //Todo     1. Calculate and return the linearly approximated fx
        //Linear Interpolation Function
        //x0: Lower interval bound
        //x1: Upper interval bound
        //fx0: Function output of lower interval bound
        //fx1: Function output of upper interval bound
        private static readonly Func<double, double, double, double, double, double> LinearInterpolation = (x0, x1, fx0, fx1, x) => (fx1 - fx0) / 2;

        //Todo C. Redefine this function to do the following:
        //Todo     1. Calculate the electron volts value from the frequency
        //Todo     2. Find which 2 of the ElectronVolt constants the calculated value lies between
        //Todo     3. Retrieve the LinearAttenuationCoefficient values for those 2 ElectronVolt constant values for the materialType parameter
        //Todo     4. Use LinearInterpolation Func to return an estimate of the LinearAttenuationCoefficient
        //Mass Attenuation Function
        //v:  Frequency of light
        private static readonly Func<MaterialType, double, double> LinearAttenuationFunction = (materialType, v) => LinearAttenuationCoefficientTable[(materialType, ElectronVoltsB)];
        
        //Opacity Distance Function
        //i0: Intensity of light at initial point
        //v:  Frequency
        //x:  Distance from light source
        public static Func<MaterialType, double, double, double, double> OpacityDistanceFunction = 
            (materialType, i0, v, x) => i0 * Math.Exp(-1 * LinearAttenuationFunction(materialType, v) * x);
    }
}
