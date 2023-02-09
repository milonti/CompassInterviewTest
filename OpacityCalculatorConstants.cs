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
