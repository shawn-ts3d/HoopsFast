using ScottPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HoopsFast.PostProcess
{
    /// <summary>
    /// Interaction logic for Plot.xaml
    /// </summary>
    public partial class Plot : Window
    {
        public Plot(List<string> names, List<string> units, List<double> dataX, List<List<double>> dataY)
        {
            InitializeComponent();

            WpfPlot1.Plot.XLabel("Time (s)", 24);
            WpfPlot1.Plot.YLabel("Value", 24);

            for (int i = 0; i < dataY.Count; i++)
            {
                var signal = WpfPlot1.Plot.Add.Scatter(dataX, dataY[i]);              
                signal.LegendText = names[i] + " " + units[i];
                signal.MarkerSize = 0;
                signal.LineWidth = 2;
            }

            WpfPlot1.Refresh();
        }
    }
}
