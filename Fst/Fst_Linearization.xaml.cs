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
    /// Interaction logic for Fst_Linearization.xaml
    /// </summary>
    public partial class Fst_Linearization : Window
    {
        public Fst_Linearization()
        {
            InitializeComponent();

            lblFstLinearize.Content = Fast.oneTurbine.fst.Linearize.name;
            chkFstLinearize_value.IsChecked = Fast.oneTurbine.fst.Linearize.value;
            lblFstLinearize_description.Content = Fast.oneTurbine.fst.Linearize.description;

            lblFstCalcSteady.Content = Fast.oneTurbine.fst.CalcSteady.name;
            chkFstCalcSteady_value.IsChecked = Fast.oneTurbine.fst.CalcSteady.value;
            lblFstCalcSteady_description.Content = Fast.oneTurbine.fst.CalcSteady.description;

            lblFstTrimCase.Content = Fast.oneTurbine.fst.TrimCase.name;
            cboFstTrimCase_value.Items.Insert(0, "1:yaw");
            cboFstTrimCase_value.Items.Insert(1, "2:torque");
            cboFstTrimCase_value.Items.Insert(2, "3:pitch");
            cboFstTrimCase_value.SelectedIndex = Fast.oneTurbine.fst.TrimCase.value-1;
            lblFstTrimCase_description.Content = Fast.oneTurbine.fst.TrimCase.description;

            lblFstTrimTol.Content = Fast.oneTurbine.fst.TrimTol.name;
            txtFstTrimTol_value.Text = Fast.oneTurbine.fst.TrimTol.value.ToString();
            lblFstTrimTol_description.Content = Fast.oneTurbine.fst.TrimTol.description;

            lblFstTrimGain.Content = Fast.oneTurbine.fst.TrimGain.name;
            txtFstTrimGain_value.Text = Fast.oneTurbine.fst.TrimGain.value.ToString();
            lblFstTrimGain_description.Content = Fast.oneTurbine.fst.TrimGain.description;

            lblFstTwr_Kdmp.Content = Fast.oneTurbine.fst.Twr_Kdmp.name;
            txtFstTwr_Kdmp_value.Text = Fast.oneTurbine.fst.Twr_Kdmp.value.ToString();
            lblFstTwr_Kdmp_description.Content = Fast.oneTurbine.fst.TrimGain.description;

            lblFstBld_Kdmp.Content = Fast.oneTurbine.fst.Bld_Kdmp.name;
            txtFstBld_Kdmp_value.Text = Fast.oneTurbine.fst.Bld_Kdmp.value.ToString();
            lblFstBld_Kdmp_description.Content = Fast.oneTurbine.fst.Bld_Kdmp.description;

            lblFstNLinTimes.Content = Fast.oneTurbine.fst.NLinTimes.name;
            txtFstNLinTimes_value.Text = Fast.oneTurbine.fst.NLinTimes.value.ToString();
            lblFstNLinTimes_description.Content = Fast.oneTurbine.fst.NLinTimes.description;

            lblFstLinInputs.Content = Fast.oneTurbine.fst.LinInputs.name;
            cboFstLinInputs_value.Items.Insert(0, "0=none");
            cboFstLinInputs_value.Items.Insert(1, "1=standard");
            cboFstLinInputs_value.Items.Insert(2, "2=all module inputs (debug)");
            cboFstLinInputs_value.SelectedIndex = Fast.oneTurbine.fst.LinInputs.value;
            lblFstLinInputs_description.Content = Fast.oneTurbine.fst.LinInputs.description;

            lblFstLinOutputs.Content = Fast.oneTurbine.fst.LinOutputs.name;
            cboFstLinOutputs_value.Items.Insert(0, "0=none");
            cboFstLinOutputs_value.Items.Insert(1, "1=from OutList(s)");
            cboFstLinOutputs_value.Items.Insert(2, "2=all module outputs (debug)");
            cboFstLinOutputs_value.SelectedIndex = Fast.oneTurbine.fst.LinOutputs.value;
            lblFstLinOutputs_description.Content = Fast.oneTurbine.fst.LinOutputs.description;

            lblFstLinOutJac.Content = Fast.oneTurbine.fst.LinOutJac.name;
            chkFstLinOutJac_value.IsChecked = Fast.oneTurbine.fst.LinOutJac.value;
            lblFstLinOutJac_description.Content = Fast.oneTurbine.fst.LinOutJac.description;

            lblFstLinOutMod.Content = Fast.oneTurbine.fst.LinOutMod.name;
            chkFstLinOutMod_value.IsChecked = Fast.oneTurbine.fst.LinOutMod.value;
            lblFstLinOutMod_description.Content = Fast.oneTurbine.fst.LinOutMod.description;

        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            //Update inputs
            if (Fast.oneTurbine.fst.Linearize.value != chkFstLinearize_value.IsChecked.Value)
            {
                Fast.oneTurbine.fst.Linearize.oldValue = Fast.oneTurbine.fst.Linearize.value;
                Fast.oneTurbine.fst.Linearize.value = chkFstLinearize_value.IsChecked.Value;
            }

            if (Fast.oneTurbine.fst.CalcSteady.value != chkFstCalcSteady_value.IsChecked.Value)
            {
                Fast.oneTurbine.fst.CalcSteady.oldValue = Fast.oneTurbine.fst.CalcSteady.value;
                Fast.oneTurbine.fst.CalcSteady.value = chkFstCalcSteady_value.IsChecked.Value;
            }

            if (Fast.oneTurbine.fst.TrimCase.value != cboFstTrimCase_value.SelectedIndex+1)
            {
                Fast.oneTurbine.fst.TrimCase.oldValue = Fast.oneTurbine.fst.TrimCase.value;
                Fast.oneTurbine.fst.TrimCase.value = cboFstTrimCase_value.SelectedIndex+1;
            }

            if (Fast.oneTurbine.fst.TrimTol.value != double.Parse(txtFstTrimTol_value.Text))
            {
                Fast.oneTurbine.fst.TrimTol.oldValue = Fast.oneTurbine.fst.TrimTol.value;
                Fast.oneTurbine.fst.TrimTol.value = double.Parse(txtFstTrimTol_value.Text);
            }

            if (Fast.oneTurbine.fst.TrimGain.value != double.Parse(txtFstTrimGain_value.Text))
            {
                Fast.oneTurbine.fst.TrimGain.oldValue = Fast.oneTurbine.fst.TrimGain.value;
                Fast.oneTurbine.fst.TrimGain.value = double.Parse(txtFstTrimGain_value.Text);
            }

            if (Fast.oneTurbine.fst.Twr_Kdmp.value != double.Parse(txtFstTwr_Kdmp_value.Text))
            {
                Fast.oneTurbine.fst.Twr_Kdmp.oldValue = Fast.oneTurbine.fst.Twr_Kdmp.value;
                Fast.oneTurbine.fst.Twr_Kdmp.value = double.Parse(txtFstTwr_Kdmp_value.Text);
            }

            if (Fast.oneTurbine.fst.Bld_Kdmp.value != double.Parse(txtFstBld_Kdmp_value.Text))
            {
                Fast.oneTurbine.fst.Bld_Kdmp.oldValue = Fast.oneTurbine.fst.Bld_Kdmp.value;
                Fast.oneTurbine.fst.Bld_Kdmp.value = double.Parse(txtFstBld_Kdmp_value.Text);
            }

            if (Fast.oneTurbine.fst.NLinTimes.value != int.Parse(txtFstNLinTimes_value.Text))
            {
                Fast.oneTurbine.fst.NLinTimes.oldValue = Fast.oneTurbine.fst.NLinTimes.value;
                Fast.oneTurbine.fst.NLinTimes.value = int.Parse(txtFstBld_Kdmp_value.Text);
            }

            if (Fast.oneTurbine.fst.LinInputs.value != cboFstLinInputs_value.SelectedIndex)
            {
                Fast.oneTurbine.fst.LinInputs.oldValue = Fast.oneTurbine.fst.LinInputs.value;
                Fast.oneTurbine.fst.LinInputs.value = cboFstLinInputs_value.SelectedIndex;
            }

            if (Fast.oneTurbine.fst.LinOutputs.value != cboFstLinOutputs_value.SelectedIndex)
            {
                Fast.oneTurbine.fst.LinOutputs.oldValue = Fast.oneTurbine.fst.LinOutputs.value;
                Fast.oneTurbine.fst.LinOutputs.value = cboFstLinOutputs_value.SelectedIndex;
            }

            if (Fast.oneTurbine.fst.LinOutJac.value != chkFstLinOutJac_value.IsChecked.Value)
            {
                Fast.oneTurbine.fst.LinOutJac.oldValue = Fast.oneTurbine.fst.LinOutJac.value;
                Fast.oneTurbine.fst.LinOutJac.value = chkFstLinOutJac_value.IsChecked.Value;
            }

            if (Fast.oneTurbine.fst.LinOutMod.value != chkFstLinOutMod_value.IsChecked.Value)
            {
                Fast.oneTurbine.fst.LinOutMod.oldValue = Fast.oneTurbine.fst.LinOutMod.value;
                Fast.oneTurbine.fst.LinOutMod.value = chkFstLinOutMod_value.IsChecked.Value;
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
