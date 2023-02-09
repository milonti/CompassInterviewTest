using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Wpf;
using OxyPlot.Series;

namespace CompassInterviewTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _viewModel;
        
        public MainWindow()
        {
            _viewModel = new MainWindowViewModel();

            DataContext = _viewModel;
            InitializeComponent();
        }

        private void UpdateCalculatedIntensity()
        {
            //Todo B. Modify this variable assignment to calculate the intensity of the light using OpacityDistanceFunction based on the chosen values in the viewModel and set it to calculatedIntensityLabel.Content
            //Todo (Make sure to convert the ChosenFrequency value to exahertz by multiplying it by 1.0E18)
            calculatedIntensityLabel.Content = _viewModel.ChosenIntensity;
        }

        //Todo F. Modify this function to set an error message to errorMessage and return false if any of the input variables are outside of the ranges specified in OpacityCalculatorConstants
        //Todo (Make sure to convert the ChosenFrequency value to exahertz by multiplying it by 1.0E18)
        private bool ValidateInputVariables(out string errorMessage)
        {
            errorMessage = "";
            return true;
        }
        
        private void PlotFrequencyButton_OnClick(object sender, RoutedEventArgs e)
        {
            //Todo G. Call ValidateInputVariables(out var errorMessage) and display error message to errorMessageLabel and return if input variable validation fails
            //Todo     Also use PlotView.Model = newPlotModel() to clear the plot if validation fails
            //Todo     Assign "N/A" to the calculatedIntensityLabel if validation fails
            //Todo     Make sure to clear the error message if there is no error
                
            var newModel = new PlotModel()
            {
                Title = $"Intensity through {_viewModel.ChosenDistance}m of {_viewModel.ChosenMaterialType.ToString()} across frequency range"
            };
            var xAxis = new LinearAxis
            {
                Title = "Frequency",
                Position = AxisPosition.Bottom,
                IsZoomEnabled = false,
                IsPanEnabled = false,
                FontSize = 12,
                TickStyle = TickStyle.Outside,
                TitleFontSize = 16,
            };
            newModel.Axes.Add(xAxis);

            //Todo D. Similarly to how its done in PlotDistanceButton_OnClick, add a function series to the newModel variable
            //Todo     This function series will plot the calculated intensity from the minimum frequency to the maximum frequency, incrementing by the frequency increment constant. All of these are given the OpacityCalculatorConstants.cs
            //Todo     The function will use the input variable as frequency rather than the ChosenFrequency variable
            
            PlotView.Model = newModel;
            
            UpdateCalculatedIntensity();
        }
        private void PlotDistanceButton_OnClick(object sender, RoutedEventArgs e)
        {
            //Todo H. (Identical to G) Call ValidateInputVariables(out var errorMessage) and display error message to errorMessageLabel and return if input variable validation fails
            //Todo     Also use PlotView.Model = newPlotModel() to clear the plot if validation fails
            //Todo     Assign "N/A" to the calculatedIntensityLabel if validation fails
            //Todo     Make sure to clear the error message if there is no error
                
            var newModel = new PlotModel()
            {
                Title = $"Intensity of {_viewModel.ChosenFrequency}EHz radiation through {_viewModel.ChosenMaterialType.ToString()} across to {_viewModel.ChosenDistance}m"
            };
            var xAxis = new LinearAxis
            {
                Title = "Distance",
                Position = AxisPosition.Bottom,
                IsZoomEnabled = false,
                IsPanEnabled = false,
                FontSize = 12,
                TickStyle = TickStyle.Outside,
                TitleFontSize = 16,
            };
            newModel.Axes.Add(xAxis);
            
            //Plot calculated intensity from the minimum distance to the ChosenDistance variable
            newModel.Series.Add( 
                //Convert Chosen Frequency to exahertz
                new FunctionSeries(x => OpacityCalculatorConstants.OpacityDistanceFunction(_viewModel.ChosenMaterialType, _viewModel.ChosenIntensity, _viewModel.ChosenFrequency * 1.0E18, x),
                OpacityCalculatorConstants.MinimumDistance,
                _viewModel.ChosenDistance,
                (_viewModel.ChosenDistance - OpacityCalculatorConstants.MinimumDistance) / OpacityCalculatorConstants.DistanceIncrement));

            PlotView.Model = newModel;
            
            UpdateCalculatedIntensity();
        }
    }
}
