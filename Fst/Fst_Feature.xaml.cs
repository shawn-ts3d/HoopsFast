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
    /// Interaction logic for Fst_Feature.xaml
    /// </summary>
    public partial class Fst_Feature : Window
    {
        public Fst_Feature()
        {
            InitializeComponent();


            lblFstCompElast.Content = Fast.oneTurbine.fst.CompElast.name;
            cboFstCompElast_value.Items.Insert(0, "1=ElastoDyn");
            cboFstCompElast_value.Items.Insert(1, "2=ElastoDyn + BeamDyn for blades");
            cboFstCompElast_value.SelectedIndex = Fast.oneTurbine.fst.CompElast.value - 1;
            lblFstCompElast_description.Content = Fast.oneTurbine.fst.CompElast.description;

            lblFstCompInflow.Content = Fast.oneTurbine.fst.CompInflow.name;
            cboFstCompInflow_value.Items.Insert(0, "0=still air");
            cboFstCompInflow_value.Items.Insert(1, "1=InflowWind");
            cboFstCompInflow_value.Items.Insert(2, "2=external from OpenFOAM");
            cboFstCompInflow_value.SelectedIndex = Fast.oneTurbine.fst.CompInflow.value;
            lblFstCompInflow_description.Content = Fast.oneTurbine.fst.CompInflow.description;

            lblFstCompAero.Content = Fast.oneTurbine.fst.CompAero.name;
            cboFstCompAero_value.Items.Insert(0, "0=None");
            cboFstCompAero_value.Items.Insert(1, "1=AeroDyn v14");
            cboFstCompAero_value.Items.Insert(2, "2=AeroDyn v15");
            cboFstCompAero_value.SelectedIndex = Fast.oneTurbine.fst.CompAero.value;
            lblFstCompAero_description.Content = Fast.oneTurbine.fst.CompAero.description;

            lblFstCompServo.Content = Fast.oneTurbine.fst.CompServo.name;
            cboFstCompServo_value.Items.Insert(0, "0=None");
            cboFstCompServo_value.Items.Insert(1, "1=ServoDyn");
            cboFstCompServo_value.SelectedIndex = Fast.oneTurbine.fst.CompServo.value;
            lblFstCompServo_description.Content = Fast.oneTurbine.fst.CompServo.description;

            lblFstCompSeaSt.Content = Fast.oneTurbine.fst.CompSeaSt.name;
            cboFstCompSeaSt_value.Items.Insert(0, "0=None");
            cboFstCompSeaSt_value.Items.Insert(1, "1=SeaState");
            cboFstCompSeaSt_value.SelectedIndex = Fast.oneTurbine.fst.CompSeaSt.value;
            lblFstCompSeaSt_description.Content = Fast.oneTurbine.fst.CompSeaSt.description;

            lblFstCompHydro.Content = Fast.oneTurbine.fst.CompHydro.name;
            cboFstCompHydro_value.Items.Insert(0, "0=None");
            cboFstCompHydro_value.Items.Insert(1, "1=HydroDyn");
            cboFstCompHydro_value.SelectedIndex = Fast.oneTurbine.fst.CompHydro.value;
            lblFstCompHydro_description.Content = Fast.oneTurbine.fst.CompHydro.description;

            lblFstCompSub.Content = Fast.oneTurbine.fst.CompSub.name;
            cboFstCompSub_value.Items.Insert(0, "0=None");
            cboFstCompSub_value.Items.Insert(1, "1=SubDyn");
            cboFstCompSub_value.Items.Insert(2, "2=External Platform MCKF");
            cboFstCompSub_value.SelectedIndex = Fast.oneTurbine.fst.CompSub.value;
            lblFstCompSub_description.Content = Fast.oneTurbine.fst.CompSub.description;

            lblFstCompMooring.Content = Fast.oneTurbine.fst.CompMooring.name;
            cboFstCompMooring_value.Items.Insert(0, "0=None");
            cboFstCompMooring_value.Items.Insert(1, "1=MAP++");
            cboFstCompMooring_value.Items.Insert(2, "2=FEAMooring");
            cboFstCompMooring_value.Items.Insert(3, "3=MoorDyn");
            cboFstCompMooring_value.Items.Insert(4, "4=OrcaFlex");
            cboFstCompMooring_value.SelectedIndex = Fast.oneTurbine.fst.CompMooring.value;
            lblFstCompMooring_description.Content = Fast.oneTurbine.fst.CompMooring.description;

            lblFstCompIce.Content = Fast.oneTurbine.fst.CompIce.name;
            cboFstCompIce_value.Items.Insert(0, "0=None");
            cboFstCompIce_value.Items.Insert(1, "1=IceFloe");
            cboFstCompIce_value.Items.Insert(2, "2=IceDyn");
            cboFstCompIce_value.SelectedIndex = Fast.oneTurbine.fst.CompIce.value;
            lblFstCompIce_description.Content = Fast.oneTurbine.fst.CompIce.description;

            lblFstMHK.Content = Fast.oneTurbine.fst.MHK.name;
            cboFstMHK_value.Items.Insert(0, "0=Not an MHK turbine");
            cboFstMHK_value.Items.Insert(1, "1=Fixed MHK turbine");
            cboFstMHK_value.Items.Insert(2, "2=Floating MHK turbine");
            cboFstMHK_value.SelectedIndex = Fast.oneTurbine.fst.MHK.value;
            lblFstMHK_description.Content = Fast.oneTurbine.fst.MHK.description;

        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            //Update inputs
            if (Fast.oneTurbine.fst.CompElast.value != cboFstCompElast_value.SelectedIndex + 1)
            {
                Fast.oneTurbine.fst.CompElast.oldValue = Fast.oneTurbine.fst.CompElast.value;
                Fast.oneTurbine.fst.CompElast.value = cboFstCompElast_value.SelectedIndex + 1;
            }

            if (Fast.oneTurbine.fst.CompInflow.value != cboFstCompInflow_value.SelectedIndex)
            {
                Fast.oneTurbine.fst.CompInflow.oldValue = Fast.oneTurbine.fst.CompInflow.value;
                Fast.oneTurbine.fst.CompInflow.value = cboFstCompInflow_value.SelectedIndex;
            }

            if (Fast.oneTurbine.fst.CompAero.value != cboFstCompAero_value.SelectedIndex)
            {
                Fast.oneTurbine.fst.CompAero.oldValue = Fast.oneTurbine.fst.CompAero.value;
                Fast.oneTurbine.fst.CompAero.value = cboFstCompAero_value.SelectedIndex;
            }

            if (Fast.oneTurbine.fst.CompServo.value != cboFstCompServo_value.SelectedIndex)
            {
                Fast.oneTurbine.fst.CompServo.oldValue = Fast.oneTurbine.fst.CompServo.value;
                Fast.oneTurbine.fst.CompServo.value = cboFstCompServo_value.SelectedIndex;
            }

            if (Fast.oneTurbine.fst.CompSeaSt.value != cboFstCompSeaSt_value.SelectedIndex)
            {
                Fast.oneTurbine.fst.CompSeaSt.oldValue = Fast.oneTurbine.fst.CompSeaSt.value;
                Fast.oneTurbine.fst.CompSeaSt.value = cboFstCompSeaSt_value.SelectedIndex;
            }

            if (Fast.oneTurbine.fst.CompHydro.value != cboFstCompHydro_value.SelectedIndex)
            {
                Fast.oneTurbine.fst.CompHydro.oldValue = Fast.oneTurbine.fst.CompHydro.value;
                Fast.oneTurbine.fst.CompHydro.value = cboFstCompHydro_value.SelectedIndex;
            }

            if (Fast.oneTurbine.fst.CompSub.value != cboFstCompSub_value.SelectedIndex)
            {
                Fast.oneTurbine.fst.CompSub.oldValue = Fast.oneTurbine.fst.CompSub.value;
                Fast.oneTurbine.fst.CompSub.value = cboFstCompSub_value.SelectedIndex;
            }

            if (Fast.oneTurbine.fst.CompMooring.value != cboFstCompMooring_value.SelectedIndex)
            {
                Fast.oneTurbine.fst.CompMooring.oldValue = Fast.oneTurbine.fst.CompMooring.value;
                Fast.oneTurbine.fst.CompMooring.value = cboFstCompMooring_value.SelectedIndex;
            }

            if (Fast.oneTurbine.fst.CompIce.value != cboFstCompIce_value.SelectedIndex)
            {
                Fast.oneTurbine.fst.CompIce.oldValue = Fast.oneTurbine.fst.CompIce.value;
                Fast.oneTurbine.fst.CompIce.value = cboFstCompIce_value.SelectedIndex;
            }

            if (Fast.oneTurbine.fst.MHK.value != cboFstMHK_value.SelectedIndex)
            {
                Fast.oneTurbine.fst.MHK.oldValue = Fast.oneTurbine.fst.MHK.value;
                Fast.oneTurbine.fst.MHK.value = cboFstMHK_value.SelectedIndex;
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
