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

namespace HoopsFast.AD
{
    /// <summary>
    /// Interaction logic for AD_GeneralOptions.xaml
    /// </summary>
    public partial class AD_GeneralOptions : Window
    {
        public AD_GeneralOptions()
        {
            InitializeComponent();


            chkEcho.IsChecked = Fast.oneTurbine.AD.Echo.value;
        }
    }
}
