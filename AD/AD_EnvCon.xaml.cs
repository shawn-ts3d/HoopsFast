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
    /// Interaction logic for AD_EnvCon.xaml
    /// </summary>
    public partial class AD_EnvCon : Window
    {
        public AD_EnvCon()
        {
            InitializeComponent();

            lblADAirDens.Content = Fast.oneTurbine.AD.AirDens.name;
            txtADAirDens_value.Text = Fast.oneTurbine.AD.AirDens.value.ToString();
            lblADAirDens_description.Content = Fast.oneTurbine.AD.AirDens.description;

            lblADKinVisc.Content = Fast.oneTurbine.AD.KinVisc.name;
            txtADKinVisc_value.Text = Fast.oneTurbine.AD.KinVisc.value.ToString();
            lblADKinVisc_description.Content = Fast.oneTurbine.AD.KinVisc.description;

            lblADSpdSound.Content = Fast.oneTurbine.AD.SpdSound.name;
            txtADSpdSound_value.Text = Fast.oneTurbine.AD.SpdSound.value.ToString();
            lblADSpdSound_description.Content = Fast.oneTurbine.AD.SpdSound.description;

            lblADPatm.Content = Fast.oneTurbine.AD.Patm.name;
            txtADPatm_value.Text = Fast.oneTurbine.AD.Patm.value.ToString();
            lblADPatm_description.Content = Fast.oneTurbine.AD.Patm.description;

            lblADPvap.Content = Fast.oneTurbine.AD.Pvap.name;
            txtADPvap_value.Text = Fast.oneTurbine.AD.Pvap.value.ToString();
            lblADPvap_description.Content = Fast.oneTurbine.AD.Pvap.description;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            //update inputs
            if (Fast.oneTurbine.AD.AirDens.value != double.Parse(txtADAirDens_value.Text))
            {
                Fast.oneTurbine.AD.AirDens.oldValue = Fast.oneTurbine.AD.AirDens.value;
                Fast.oneTurbine.AD.AirDens.value = double.Parse(txtADAirDens_value.Text);
            }

            if (Fast.oneTurbine.AD.KinVisc.value != double.Parse(txtADKinVisc_value.Text))
            {
                Fast.oneTurbine.AD.KinVisc.oldValue = Fast.oneTurbine.AD.KinVisc.value;
                Fast.oneTurbine.AD.KinVisc.value = double.Parse(txtADKinVisc_value.Text);
            }

            if (Fast.oneTurbine.AD.SpdSound.value != double.Parse(txtADSpdSound_value.Text))
            {
                Fast.oneTurbine.AD.SpdSound.oldValue = Fast.oneTurbine.AD.SpdSound.value;
                Fast.oneTurbine.AD.SpdSound.value = double.Parse(txtADSpdSound_value.Text);
            }

            if (Fast.oneTurbine.AD.Patm.value != double.Parse(txtADPatm_value.Text))
            {
                Fast.oneTurbine.AD.Patm.oldValue = Fast.oneTurbine.AD.Patm.value;
                Fast.oneTurbine.AD.Patm.value = double.Parse(txtADPatm_value.Text);
            }

            if (Fast.oneTurbine.AD.Pvap.value != double.Parse(txtADPvap_value.Text))
            {
                Fast.oneTurbine.AD.Pvap.oldValue = Fast.oneTurbine.AD.Pvap.value;
                Fast.oneTurbine.AD.Pvap.value = double.Parse(txtADPvap_value.Text);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
