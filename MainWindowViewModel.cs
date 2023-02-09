using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace CompassInterviewTest
{
    public class MainWindowViewModel
    {
        //Defining placeholder default value to avoid null value exceptions
        private OpacityCalculatorConstants.MaterialType _chosenMaterialType = OpacityCalculatorConstants.MaterialType.Air;
        public OpacityCalculatorConstants.MaterialType ChosenMaterialType { get => _chosenMaterialType; set => _chosenMaterialType = value;}
        
        //Defining placeholder default value to avoid null value exceptions
        private double _chosenFrequency = OpacityCalculatorConstants.MaximumFrequency;
        public double ChosenFrequency { 
            //Convert to exahertz in getter and setter
            get => _chosenFrequency / 1.0E18; 
            set => _chosenFrequency = value * 1.0E18;
        }
        
        //Defining placeholder default value to avoid null value exceptions
        private double _chosenIntensity = OpacityCalculatorConstants.MaximumIntensity;
        public double ChosenIntensity { get => _chosenIntensity; set => _chosenIntensity = value;}
        
        //Defining placeholder default value to avoid null value exceptions
        private double _chosenDistance = OpacityCalculatorConstants.MaximumDistance;
        public double ChosenDistance { get => _chosenDistance; set => _chosenDistance = value;}
    }
}
