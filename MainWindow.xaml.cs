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

        private void PlotDistanceButton_OnClick(object sender, RoutedEventArgs e)
        {
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
        }
    }
}
