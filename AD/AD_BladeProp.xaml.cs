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
    /// Interaction logic for AD_BladeProp.xaml
    /// </summary>
    public partial class AD_BladeProp : Window
    {
        public AD_BladeProp()
        {
            InitializeComponent();

            lblADUseBlCm.Content = Fast.oneTurbine.AD.UseBlCm.name;
            chkADUseBlCm_value.IsChecked = Fast.oneTurbine.AD.UseBlCm.value;
            lblADUseBlCm_description.Content = Fast.oneTurbine.AD.UseBlCm.description;

            lblADADBlFile1.Content = Fast.oneTurbine.AD.ADBlFile1.name;
            txtADADBlFile1_value.Text = Fast.oneTurbine.AD.ADBlFile1.value;
            lblADADBlFile1_description.Content = Fast.oneTurbine.AD.ADBlFile1.description;

            lblADADBlFile2.Content = Fast.oneTurbine.AD.ADBlFile2.name;
            txtADADBlFile2_value.Text = Fast.oneTurbine.AD.ADBlFile2.value;
            lblADADBlFile2_description.Content = Fast.oneTurbine.AD.ADBlFile2.description;

            lblADADBlFile3.Content = Fast.oneTurbine.AD.ADBlFile3.name;
            txtADADBlFile3_value.Text = Fast.oneTurbine.AD.ADBlFile3.value;
            lblADADBlFile3_description.Content = Fast.oneTurbine.AD.ADBlFile3.description;
        }



        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            //update inputs
            if (Fast.oneTurbine.AD.UseBlCm.value != chkADUseBlCm_value.IsChecked.Value)
            {
                Fast.oneTurbine.AD.UseBlCm.oldValue = Fast.oneTurbine.AD.UseBlCm.value;
                Fast.oneTurbine.AD.UseBlCm.value = chkADUseBlCm_value.IsChecked.Value;
            }

            //update blade file (read only now)
            //
            //

            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
