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
    /// Interaction logic for AD_DynamicBEMTOptions.xaml
    /// </summary>
    public partial class AD_DynamicBEMTOptions : Window
    {
        public AD_DynamicBEMTOptions()
        {
            InitializeComponent();

            lblADDBEMT_Mod.Content = Fast.oneTurbine.AD.DBEMT_Mod.name;
            cboADDBEMT_Mod_value.Items.Insert(0, "1=constant tau1");
            cboADDBEMT_Mod_value.Items.Insert(1, "2=time-dependent tau1");
            cboADDBEMT_Mod_value.Items.Insert(2, "3=constant tau1 with continuous formulation");
            cboADDBEMT_Mod_value.SelectedIndex = Fast.oneTurbine.AD.DBEMT_Mod.value - 1;
            lblADDBEMT_Mod_description.Content = Fast.oneTurbine.AD.DBEMT_Mod.description;

            lblADtau1_const.Content = Fast.oneTurbine.AD.tau1_const.name;
            txtADtau1_const_value.Text = Fast.oneTurbine.AD.tau1_const.value.ToString();
            lblADtau1_const_description.Content = Fast.oneTurbine.AD.tau1_const.description;
        }


        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            //update inputs
            if (Fast.oneTurbine.AD.DBEMT_Mod.value != cboADDBEMT_Mod_value.SelectedIndex + 1)
            {
                Fast.oneTurbine.AD.DBEMT_Mod.oldValue = Fast.oneTurbine.AD.DBEMT_Mod.value;
                Fast.oneTurbine.AD.DBEMT_Mod.value = cboADDBEMT_Mod_value.SelectedIndex + 1;
            }

            if (Fast.oneTurbine.AD.tau1_const.value != double.Parse(txtADtau1_const_value.Text))
            {
                Fast.oneTurbine.AD.tau1_const.oldValue = Fast.oneTurbine.AD.tau1_const.value;
                Fast.oneTurbine.AD.tau1_const.value = double.Parse(txtADtau1_const_value.Text);
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
