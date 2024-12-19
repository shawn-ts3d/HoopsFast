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
        public Plot(List<double> dataX, List<List<double>> dataY)
        {
            InitializeComponent();

            foreach (var data in dataY)
            {
                WpfPlot1.Plot.Add.Scatter(dataX, data, WpfPlot1.Plot.Add.GetNextColor());
            }
            WpfPlot1.Refresh();
        }
    }
}
