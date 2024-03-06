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

namespace HoopsFast.Fst
{
    /// <summary>
    /// Interaction logic for Fst_SimCon.xaml
    /// </summary>
    public partial class Fst_SimCon : Window
    {
        public Fst_SimCon()
        {
            InitializeComponent();

            lblFstEcho.Content = Fast.oneTurbine.fst.Echo.name;
            chkFstEcho_Value.IsChecked = Fast.oneTurbine.fst.Echo.value;
            lblFstEcho_Description.Content = Fast.oneTurbine.fst.Echo.description;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
