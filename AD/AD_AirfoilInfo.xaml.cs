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
    /// Interaction logic for AD_AirfoilInfo.xaml
    /// </summary>
    public partial class AD_AirfoilInfo : Window
    {
        public AD_AirfoilInfo()
        {
            InitializeComponent();

            lblADAFTabMod.Content = Fast.oneTurbine.AD.AFTabMod.name;
            cboADAFTabMod_value.Items.Insert(0, "1=1D interpolation on AoA (first table only)");
            cboADAFTabMod_value.Items.Insert(1, "2=2D interpolation on AoA and Re");
            cboADAFTabMod_value.Items.Insert(2, "3=2D interpolation on AoA and UserProp");
            cboADAFTabMod_value.SelectedIndex = Fast.oneTurbine.AD.AFTabMod.value - 1;
            lblADAFTabMod_description.Content = Fast.oneTurbine.AD.AFTabMod.description;

            lblADInCol_Alfa.Content = Fast.oneTurbine.AD.InCol_Alfa.name;
            txtADInCol_Alfa_value.Text = Fast.oneTurbine.AD.InCol_Alfa.value.ToString();
            lblADInCol_Alfa_description.Content = Fast.oneTurbine.AD.InCol_Alfa.description;

            lblADInCol_Cl.Content = Fast.oneTurbine.AD.InCol_Cl.name;
            txtADInCol_Cl_value.Text = Fast.oneTurbine.AD.InCol_Cl.value.ToString();
            lblADInCol_Cl_description.Content = Fast.oneTurbine.AD.InCol_Cl.description;

            lblADInCol_Cd.Content = Fast.oneTurbine.AD.InCol_Cd.name;
            txtADInCol_Cd_value.Text = Fast.oneTurbine.AD.InCol_Cd.value.ToString();
            lblADInCol_Cd_description.Content = Fast.oneTurbine.AD.InCol_Cd.description;

            lblADInCol_Cm.Content = Fast.oneTurbine.AD.InCol_Cm.name;
            txtADInCol_Cm_value.Text = Fast.oneTurbine.AD.InCol_Cm.value.ToString();
            lblADInCol_Cm_description.Content = Fast.oneTurbine.AD.InCol_Cm.description;

            lblADInCol_Cpmin.Content = Fast.oneTurbine.AD.InCol_Cpmin.name;
            txtADInCol_Cpmin_value.Text = Fast.oneTurbine.AD.InCol_Cpmin.value.ToString();
            lblADInCol_Cpmin_description.Content = Fast.oneTurbine.AD.InCol_Cpmin.description;

            lblADNumAFfiles.Content = Fast.oneTurbine.AD.NumAFfiles.name;
            txtADNumAFfiles_value.Text = Fast.oneTurbine.AD.NumAFfiles.value.Count.ToString();
            lblADNumAFfiles_description.Content = Fast.oneTurbine.AD.NumAFfiles.description + "\n";
            for (int i = 0; i < Fast.oneTurbine.AD.NumAFfiles.value.Count - 1; i++)
            {
                lblADNumAFfiles_description.Content += Fast.oneTurbine.AD.NumAFfiles.value[i] + "\n";
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            //Update inputs
            if (Fast.oneTurbine.AD.AFTabMod.value != cboADAFTabMod_value.SelectedIndex + 1)
            {
                Fast.oneTurbine.AD.AFTabMod.oldValue = Fast.oneTurbine.AD.AFTabMod.value;
                Fast.oneTurbine.AD.AFTabMod.value = cboADAFTabMod_value.SelectedIndex + 1;
            }

            if (Fast.oneTurbine.AD.InCol_Alfa.value != int.Parse(txtADInCol_Alfa_value.Text))
            {
                Fast.oneTurbine.AD.InCol_Alfa.oldValue = Fast.oneTurbine.AD.InCol_Alfa.value;
                Fast.oneTurbine.AD.InCol_Alfa.value = int.Parse(txtADInCol_Alfa_value.Text);
            }

            if (Fast.oneTurbine.AD.InCol_Cl.value != int.Parse(txtADInCol_Cl_value.Text))
            {
                Fast.oneTurbine.AD.InCol_Cl.oldValue = Fast.oneTurbine.AD.InCol_Cl.value;
                Fast.oneTurbine.AD.InCol_Cl.value = int.Parse(txtADInCol_Cl_value.Text);
            }

            if (Fast.oneTurbine.AD.InCol_Cd.value != int.Parse(txtADInCol_Cd_value.Text))
            {
                Fast.oneTurbine.AD.InCol_Cd.oldValue = Fast.oneTurbine.AD.InCol_Cd.value;
                Fast.oneTurbine.AD.InCol_Cd.value = int.Parse(txtADInCol_Cd_value.Text);
            }

            if (Fast.oneTurbine.AD.InCol_Cm.value != int.Parse(txtADInCol_Cm_value.Text))
            {
                Fast.oneTurbine.AD.InCol_Cm.oldValue = Fast.oneTurbine.AD.InCol_Cm.value;
                Fast.oneTurbine.AD.InCol_Cm.value = int.Parse(txtADInCol_Cm_value.Text);
            }

            if (Fast.oneTurbine.AD.InCol_Cpmin.value != int.Parse(txtADInCol_Cpmin_value.Text))
            {
                Fast.oneTurbine.AD.InCol_Cpmin.oldValue = Fast.oneTurbine.AD.InCol_Cpmin.value;
                Fast.oneTurbine.AD.InCol_Cpmin.value = int.Parse(txtADInCol_Cpmin_value.Text);
            }

            //Add update for NumAFfiles
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
