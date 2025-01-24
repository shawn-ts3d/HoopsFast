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
    /// Interaction logic for AD_BEMTOptions.xaml
    /// </summary>
    public partial class AD_BEMTOptions : Window
    {
        public AD_BEMTOptions()
        {
            InitializeComponent();

            lblADSkewMod.Content = Fast.oneTurbine.AD.Skew_Mod.name;
            cboADSkewMod_value.Items.Insert(0, "1=uncoupled");
            cboADSkewMod_value.Items.Insert(1, "2=Pitt/Peters");
            cboADSkewMod_value.Items.Insert(2, "3=coupled");
            cboADSkewMod_value.SelectedIndex = Fast.oneTurbine.AD.Skew_Mod.value - 1;
            lblADSkewMod_description.Content = Fast.oneTurbine.AD.Skew_Mod.description;

            lblADSkewModFactor.Content = Fast.oneTurbine.AD.SkewRedistrFactor.name;
            txtADSkewModFactor_value.Text = Fast.oneTurbine.AD.SkewRedistrFactor.value.ToString();
            lblADSkewModFactor_description.Content = Fast.oneTurbine.AD.SkewRedistrFactor.description;

            lblADTipLoss.Content = Fast.oneTurbine.AD.TipLoss.name;
            chkADTipLoss_value.IsChecked = Fast.oneTurbine.AD.TipLoss.value;
            lblADTipLoss_description.Content = Fast.oneTurbine.AD.TipLoss.description;

            lblADHubLoss.Content = Fast.oneTurbine.AD.HubLoss.name;
            chkADHubLoss_value.IsChecked = Fast.oneTurbine.AD.HubLoss.value;
            lblADHubLoss_description.Content = Fast.oneTurbine.AD.HubLoss.description;

            lblADTanInd.Content = Fast.oneTurbine.AD.TanInd.name;
            chkADTanInd_value.IsChecked = Fast.oneTurbine.AD.TanInd.value;
            lblADTanInd_description.Content = Fast.oneTurbine.AD.TanInd.description;

            lblADAIDrag.Content = Fast.oneTurbine.AD.AIDrag.name;
            chkADAIDrag_value.IsChecked = Fast.oneTurbine.AD.AIDrag.value;
            lblADAIDrag_description.Content = Fast.oneTurbine.AD.AIDrag.description;

            lblADTIDrag.Content = Fast.oneTurbine.AD.TIDrag.name;
            chkADTIDrag_value.IsChecked = Fast.oneTurbine.AD.TIDrag.value;
            lblADTIDrag_description.Content = Fast.oneTurbine.AD.TIDrag.description;

            lblADIndToler.Content = Fast.oneTurbine.AD.IndToler.name;
            txtADIndToler_value.Text = Fast.oneTurbine.AD.IndToler.value.ToString();
            lblADIndToler_description.Content = Fast.oneTurbine.AD.IndToler.description;

            lblADMaxIter.Content = Fast.oneTurbine.AD.MaxIter.name;
            txtADMaxIter_value.Text = Fast.oneTurbine.AD.MaxIter.value.ToString();
            lblADMaxIter_description.Content = Fast.oneTurbine.AD.MaxIter.description;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            //update inputs
            if (Fast.oneTurbine.AD.Skew_Mod.value != cboADSkewMod_value.SelectedIndex + 1)
            {
                Fast.oneTurbine.AD.Skew_Mod.oldValue = Fast.oneTurbine.AD.Skew_Mod.value;
                Fast.oneTurbine.AD.Skew_Mod.value = cboADSkewMod_value.SelectedIndex + 1;
            }

            if (Fast.oneTurbine.AD.SkewRedistrFactor.value != double.Parse(txtADSkewModFactor_value.Text))
            {
                Fast.oneTurbine.AD.SkewRedistrFactor.oldValue = Fast.oneTurbine.AD.SkewRedistrFactor.value;
                Fast.oneTurbine.AD.SkewRedistrFactor.value = double.Parse(txtADSkewModFactor_value.Text);
            }

            if (Fast.oneTurbine.AD.TipLoss.value != chkADTipLoss_value.IsChecked.Value)
            {
                Fast.oneTurbine.AD.TipLoss.oldValue = Fast.oneTurbine.AD.TipLoss.value;
                Fast.oneTurbine.AD.TipLoss.value = chkADTipLoss_value.IsChecked.Value;
            }

            if (Fast.oneTurbine.AD.HubLoss.value != chkADHubLoss_value.IsChecked.Value)
            {
                Fast.oneTurbine.AD.HubLoss.oldValue = Fast.oneTurbine.AD.HubLoss.value;
                Fast.oneTurbine.AD.HubLoss.value = chkADHubLoss_value.IsChecked.Value;
            }

            if (Fast.oneTurbine.AD.TanInd.value != chkADTanInd_value.IsChecked.Value)
            {
                Fast.oneTurbine.AD.TanInd.oldValue = Fast.oneTurbine.AD.TanInd.value;
                Fast.oneTurbine.AD.TanInd.value = chkADTanInd_value.IsChecked.Value;
            }

            if (Fast.oneTurbine.AD.AIDrag.value != chkADAIDrag_value.IsChecked.Value)
            {
                Fast.oneTurbine.AD.AIDrag.oldValue = Fast.oneTurbine.AD.AIDrag.value;
                Fast.oneTurbine.AD.AIDrag.value = chkADAIDrag_value.IsChecked.Value;
            }

            if (Fast.oneTurbine.AD.TIDrag.value != chkADTIDrag_value.IsChecked.Value)
            {
                Fast.oneTurbine.AD.TIDrag.oldValue = Fast.oneTurbine.AD.TIDrag.value;
                Fast.oneTurbine.AD.TIDrag.value = chkADTIDrag_value.IsChecked.Value;
            }

            if (Fast.oneTurbine.AD.IndToler.value != double.Parse(txtADIndToler_value.Text))
            {
                Fast.oneTurbine.AD.IndToler.oldValue = Fast.oneTurbine.AD.IndToler.value;
                Fast.oneTurbine.AD.IndToler.value = double.Parse(txtADIndToler_value.Text);
            }

            if (Fast.oneTurbine.AD.MaxIter.value != double.Parse(txtADMaxIter_value.Text))
            {
                Fast.oneTurbine.AD.MaxIter.oldValue = Fast.oneTurbine.AD.MaxIter.value;
                Fast.oneTurbine.AD.MaxIter.value = int.Parse(txtADMaxIter_value.Text);
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
