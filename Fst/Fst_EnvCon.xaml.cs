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
    /// Interaction logic for Fst_EnvCon.xaml
    /// </summary>
    public partial class Fst_EnvCon : Window
    {
        public Fst_EnvCon()
        {
            InitializeComponent();

            lblFstGravity.Content = Fast.oneTurbine.fst.Gravity.name;
            txtFstGravity_value.Text = Fast.oneTurbine.fst.Gravity.value.ToString();
            lblFstGravity_description.Content = Fast.oneTurbine.fst.Gravity.description;

            lblFstAirDens.Content = Fast.oneTurbine.fst.AirDens.name;
            txtFstAirDens_value.Text = Fast.oneTurbine.fst.AirDens.value.ToString();
            lblFstAirDens_description.Content = Fast.oneTurbine.fst.AirDens.description;

            lblFstWtrDens.Content = Fast.oneTurbine.fst.WtrDens.name;
            txtFstWtrDens_value.Text = Fast.oneTurbine.fst.WtrDens.value.ToString();
            lblFstWtrDens_description.Content = Fast.oneTurbine.fst.WtrDens.description;

            lblFstKinVisc.Content = Fast.oneTurbine.fst.KinVisc.name;
            txtFstKinVisc_value.Text = Fast.oneTurbine.fst.KinVisc.value.ToString();
            lblFstKinVisc_description.Content = Fast.oneTurbine.fst.KinVisc.description;

            lblFstSpdSound.Content = Fast.oneTurbine.fst.SpdSound.name;
            txtFstSpdSound_value.Text = Fast.oneTurbine.fst.SpdSound.value.ToString();
            lblFstSpdSound_description.Content = Fast.oneTurbine.fst.SpdSound.description;

            lblFstPatm.Content = Fast.oneTurbine.fst.Patm.name;
            txtFstPatm_value.Text = Fast.oneTurbine.fst.Patm.value.ToString();
            lblFstPatm_description.Content = Fast.oneTurbine.fst.Patm.description;

            lblFstPvap.Content = Fast.oneTurbine.fst.Pvap.name;
            txtFstPvap_value.Text = Fast.oneTurbine.fst.Pvap.value.ToString();
            lblFstPvap_description.Content = Fast.oneTurbine.fst.Pvap.description;

            lblFstWtrDpth.Content = Fast.oneTurbine.fst.WtrDpth.name;
            txtFstWtrDpth_value.Text = Fast.oneTurbine.fst.WtrDpth.value.ToString();
            lblFstWtrDpth_description.Content = Fast.oneTurbine.fst.WtrDpth.description;

            lblFstMSL2SWL.Content = Fast.oneTurbine.fst.MSL2SWL.name;
            txtFstMSL2SWL_value.Text = Fast.oneTurbine.fst.MSL2SWL.value.ToString();
            lblFstMSL2SWL_description.Content = Fast.oneTurbine.fst.MSL2SWL.description;

        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            //Update inputs
            if (Fast.oneTurbine.fst.Gravity.value != double.Parse(txtFstGravity_value.Text))
            {
                Fast.oneTurbine.fst.Gravity.oldValue = Fast.oneTurbine.fst.Gravity.value;
                Fast.oneTurbine.fst.Gravity.value = double.Parse(txtFstGravity_value.Text);
            }

            if (Fast.oneTurbine.fst.AirDens.value != double.Parse(txtFstAirDens_value.Text))
            {
                Fast.oneTurbine.fst.AirDens.oldValue = Fast.oneTurbine.fst.AirDens.value;
                Fast.oneTurbine.fst.AirDens.value = double.Parse(txtFstAirDens_value.Text);
            }

            if (Fast.oneTurbine.fst.WtrDens.value != double.Parse(txtFstWtrDens_value.Text))
            {
                Fast.oneTurbine.fst.WtrDens.oldValue = Fast.oneTurbine.fst.WtrDens.value;
                Fast.oneTurbine.fst.WtrDens.value = double.Parse(txtFstWtrDens_value.Text);
            }

            if (Fast.oneTurbine.fst.KinVisc.value != double.Parse(txtFstKinVisc_value.Text))
            {
                Fast.oneTurbine.fst.KinVisc.oldValue = Fast.oneTurbine.fst.KinVisc.value;
                Fast.oneTurbine.fst.KinVisc.value = double.Parse(txtFstKinVisc_value.Text);
            }

            if (Fast.oneTurbine.fst.SpdSound.value != double.Parse(txtFstSpdSound_value.Text))
            {
                Fast.oneTurbine.fst.SpdSound.oldValue = Fast.oneTurbine.fst.SpdSound.value;
                Fast.oneTurbine.fst.SpdSound.value = double.Parse(txtFstSpdSound_value.Text);
            }

            if (Fast.oneTurbine.fst.Patm.value != double.Parse(txtFstPatm_value.Text))
            {
                Fast.oneTurbine.fst.Patm.oldValue = Fast.oneTurbine.fst.Patm.value;
                Fast.oneTurbine.fst.Patm.value = double.Parse(txtFstPatm_value.Text);
            }

            if (Fast.oneTurbine.fst.Pvap.value != double.Parse(txtFstPvap_value.Text))
            {
                Fast.oneTurbine.fst.Pvap.oldValue = Fast.oneTurbine.fst.Pvap.value;
                Fast.oneTurbine.fst.Pvap.value = double.Parse(txtFstPvap_value.Text);
            }

            if (Fast.oneTurbine.fst.WtrDpth.value != double.Parse(txtFstWtrDpth_value.Text))
            {
                Fast.oneTurbine.fst.WtrDpth.oldValue = Fast.oneTurbine.fst.WtrDpth.value;
                Fast.oneTurbine.fst.WtrDpth.value = double.Parse(txtFstWtrDpth_value.Text);
            }

            if (Fast.oneTurbine.fst.MSL2SWL.value != double.Parse(txtFstMSL2SWL_value.Text))
            {
                Fast.oneTurbine.fst.MSL2SWL.oldValue = Fast.oneTurbine.fst.MSL2SWL.value;
                Fast.oneTurbine.fst.MSL2SWL.value = double.Parse(txtFstMSL2SWL_value.Text);
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
