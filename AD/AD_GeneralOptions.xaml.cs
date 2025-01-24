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

            lblADEcho.Content = Fast.oneTurbine.AD.Echo.name;
            chkADEcho_value.IsChecked = Fast.oneTurbine.AD.Echo.value;
            lblADEcho_description.Content = Fast.oneTurbine.AD.Echo.description;

            lblADDTAero.Content = Fast.oneTurbine.AD.DTAero.name;
            txtADDTAero_value.Text = Fast.oneTurbine.AD.DTAero.value.ToString();
            lblADDTAero_description.Content = Fast.oneTurbine.AD.DTAero.description;

            lblADWakeMod.Content = Fast.oneTurbine.AD.Wake_Mod.name;
            cboADWakeMod_value.Items.Insert(0, "0=none");
            cboADWakeMod_value.Items.Insert(1, "1=BEMT");
            cboADWakeMod_value.Items.Insert(2, "2=DBEMT");
            cboADWakeMod_value.Items.Insert(3, "3=OLAF");
            cboADWakeMod_value.SelectedIndex = Fast.oneTurbine.AD.Wake_Mod.value;
            lblADWakeMod_description.Content = Fast.oneTurbine.AD.Wake_Mod.description;

            lblADAFAeroMod.Content = Fast.oneTurbine.AD.AFAeroMod.name;
            cboADAFAeroMod_value.Items.Insert(0, "1=steady model");
            cboADAFAeroMod_value.Items.Insert(1, "2=Beddoes-Leishman unsteady model");
            cboADAFAeroMod_value.SelectedIndex = Fast.oneTurbine.AD.AFAeroMod.value - 1;
            lblADAFAeroMod_description.Content = Fast.oneTurbine.AD.AFAeroMod.description;

            lblADTwrPotent.Content = Fast.oneTurbine.AD.TwrPotent.name;
            cboADTwrPotent_value.Items.Insert(0, "0=none");
            cboADTwrPotent_value.Items.Insert(1, "1=baseline potential flow");
            cboADTwrPotent_value.Items.Insert(2, "2=potential flow with Bak correction");
            cboADTwrPotent_value.SelectedIndex = Fast.oneTurbine.AD.TwrPotent.value;
            lblADTwrPotent_description.Content = Fast.oneTurbine.AD.TwrPotent.description;

            lblADTwrShadow.Content = Fast.oneTurbine.AD.TwrShadow.name;
            cboADTwrShadow_value.Items.Insert(0, "0=none");
            cboADTwrShadow_value.Items.Insert(1, "1=Powles model");
            cboADTwrShadow_value.Items.Insert(2, "2=Eames model");
            cboADTwrShadow_value.SelectedIndex = Fast.oneTurbine.AD.TwrShadow.value;
            lblADTwrShadow_description.Content = Fast.oneTurbine.AD.TwrShadow.description;

            lblADTwrAero.Content = Fast.oneTurbine.AD.TwrAero.name;
            chkADTwrAero_value.IsChecked = Fast.oneTurbine.AD.TwrAero.value;
            lblADTwrAero_description.Content = Fast.oneTurbine.AD.TwrAero.description;

            lblADFrozenWake.Content = Fast.oneTurbine.AD.FrozenWake.name;
            chkADFrozenWake_value.IsChecked = Fast.oneTurbine.AD.FrozenWake.value;
            lblADFrozenWake_description.Content = Fast.oneTurbine.AD.FrozenWake.description;

            lblADCavitCheck.Content = Fast.oneTurbine.AD.CavitCheck.name;
            chkADCavitCheck_value.IsChecked = Fast.oneTurbine.AD.CavitCheck.value;
            lblADCavitCheck_description.Content = Fast.oneTurbine.AD.CavitCheck.description;

            lblADBuoyancy.Content = Fast.oneTurbine.AD.Buoyancy.name;
            chkADBuoyancy_value.IsChecked = Fast.oneTurbine.AD.Buoyancy.value;
            lblADBuoyancy_description.Content = Fast.oneTurbine.AD.Buoyancy.description;

            lblADCompAA.Content = Fast.oneTurbine.AD.CompAA.name;
            chkADCompAA_value.IsChecked = Fast.oneTurbine.AD.CompAA.value;
            lblADCompAA_description.Content = Fast.oneTurbine.AD.CompAA.description;

            lblADAA_InputFile.Content = Fast.oneTurbine.AD.AA_InputFile.name;
            txtADAA_InputFile_value.Text = Fast.oneTurbine.AD.AA_InputFile.value;
            lblADAA_InputFile_description.Content = Fast.oneTurbine.AD.AA_InputFile.description;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            //Update inputs
            if (Fast.oneTurbine.AD.Echo.value != chkADEcho_value.IsChecked.Value)
            {
                Fast.oneTurbine.AD.Echo.oldValue = Fast.oneTurbine.AD.Echo.value;
                Fast.oneTurbine.AD.Echo.value = chkADEcho_value.IsChecked.Value;
            }

            if (Fast.oneTurbine.AD.DTAero.value != double.Parse(txtADDTAero_value.Text))
            {
                Fast.oneTurbine.AD.DTAero.oldValue = Fast.oneTurbine.AD.DTAero.value;
                Fast.oneTurbine.AD.DTAero.value = double.Parse(txtADDTAero_value.Text);
            }

            if (Fast.oneTurbine.AD.Wake_Mod.value != cboADWakeMod_value.SelectedIndex)
            {
                Fast.oneTurbine.AD.Wake_Mod.oldValue = Fast.oneTurbine.AD.Wake_Mod.value;
                Fast.oneTurbine.AD.Wake_Mod.value = cboADWakeMod_value.SelectedIndex;
            }

            if (Fast.oneTurbine.AD.AFAeroMod.value != cboADAFAeroMod_value.SelectedIndex + 1)
            {
                Fast.oneTurbine.AD.AFAeroMod.oldValue = Fast.oneTurbine.AD.AFAeroMod.value;
                Fast.oneTurbine.AD.AFAeroMod.value = cboADAFAeroMod_value.SelectedIndex + 1;
            }

            if (Fast.oneTurbine.AD.TwrPotent.value != cboADTwrPotent_value.SelectedIndex)
            {
                Fast.oneTurbine.AD.TwrPotent.oldValue = Fast.oneTurbine.AD.TwrPotent.value;
                Fast.oneTurbine.AD.TwrPotent.value = cboADTwrPotent_value.SelectedIndex;
            }

            if (Fast.oneTurbine.AD.TwrShadow.value != cboADTwrShadow_value.SelectedIndex)
            {
                Fast.oneTurbine.AD.TwrShadow.oldValue = Fast.oneTurbine.AD.TwrShadow.value;
                Fast.oneTurbine.AD.TwrShadow.value = cboADTwrShadow_value.SelectedIndex;
            }

            if (Fast.oneTurbine.AD.TwrAero.value != chkADTwrAero_value.IsChecked.Value)
            {
                Fast.oneTurbine.AD.TwrAero.oldValue = Fast.oneTurbine.AD.TwrAero.value;
                Fast.oneTurbine.AD.TwrAero.value = chkADTwrAero_value.IsChecked.Value;
            }

            if (Fast.oneTurbine.AD.FrozenWake.value != chkADFrozenWake_value.IsChecked.Value)
            {
                Fast.oneTurbine.AD.FrozenWake.oldValue = Fast.oneTurbine.AD.FrozenWake.value;
                Fast.oneTurbine.AD.FrozenWake.value = chkADFrozenWake_value.IsChecked.Value;
            }

            if (Fast.oneTurbine.AD.CavitCheck.value != chkADCavitCheck_value.IsChecked.Value)
            {
                Fast.oneTurbine.AD.CavitCheck.oldValue = Fast.oneTurbine.AD.CavitCheck.value;
                Fast.oneTurbine.AD.CavitCheck.value = chkADCavitCheck_value.IsChecked.Value;
            }

            if (Fast.oneTurbine.AD.Buoyancy.value != chkADBuoyancy_value.IsChecked.Value)
            {
                Fast.oneTurbine.AD.Buoyancy.oldValue = Fast.oneTurbine.AD.Buoyancy.value;
                Fast.oneTurbine.AD.Buoyancy.value = chkADBuoyancy_value.IsChecked.Value;
            }

            if (Fast.oneTurbine.AD.CompAA.value != chkADCompAA_value.IsChecked.Value)
            {
                Fast.oneTurbine.AD.CompAA.oldValue = Fast.oneTurbine.AD.CompAA.value;
                Fast.oneTurbine.AD.CompAA.value = chkADCompAA_value.IsChecked.Value;
            }

            if (Fast.oneTurbine.AD.AA_InputFile.value != txtADAA_InputFile_value.Text)
            {
                Fast.oneTurbine.AD.AA_InputFile.oldValue = Fast.oneTurbine.AD.AA_InputFile.value;
                Fast.oneTurbine.AD.AA_InputFile.value = txtADAA_InputFile_value.Text;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
