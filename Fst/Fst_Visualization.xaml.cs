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
    /// Interaction logic for Fst_Visualization.xaml
    /// </summary>
    public partial class Fst_Visualization : Window
    {
        public Fst_Visualization()
        {
            InitializeComponent();

            lblFstWrVTK.Content = Fast.oneTurbine.fst.WrVTK.name;
            cboFstWrVTK_value.Items.Insert(0, "0=none");
            cboFstWrVTK_value.Items.Insert(1, "1=initialization data only");
            cboFstWrVTK_value.Items.Insert(2, "2=animation");
            cboFstWrVTK_value.Items.Insert(2, "3=mode shapes");
            cboFstWrVTK_value.SelectedIndex = Fast.oneTurbine.fst.WrVTK.value;
            lblFstWrVTK_description.Content = Fast.oneTurbine.fst.WrVTK.description;

            lblFstVTK_type.Content = Fast.oneTurbine.fst.VTK_type.name;
            cboFstVTK_type_value.Items.Insert(0, "1=surfaces");
            cboFstVTK_type_value.Items.Insert(1, "2=basic meshes (lines/points)");
            cboFstVTK_type_value.Items.Insert(2, "3=all meshes (debug)");
            cboFstVTK_type_value.SelectedIndex = Fast.oneTurbine.fst.VTK_type.value-1;
            lblFstVTK_type_description.Content = Fast.oneTurbine.fst.VTK_type.description;

            lblFstVTK_fields.Content = Fast.oneTurbine.fst.VTK_fields.name;
            chkFstVTK_fields_value.IsChecked = Fast.oneTurbine.fst.VTK_fields.value;
            lblFstVTK_fields_description.Content = Fast.oneTurbine.fst.VTK_fields.description;

            lblFstVTK_fps.Content = Fast.oneTurbine.fst.VTK_fps.name;
            txtFstVTK_fps_value.Text = Fast.oneTurbine.fst.VTK_fps.value.ToString();
            lblFstVTK_fps_description.Content = Fast.oneTurbine.fst.VTK_fps.description;

        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            //Update inputs
            if (Fast.oneTurbine.fst.WrVTK.value != cboFstWrVTK_value.SelectedIndex)
            {
                Fast.oneTurbine.fst.WrVTK.oldValue = Fast.oneTurbine.fst.WrVTK.value;
                Fast.oneTurbine.fst.WrVTK.value = cboFstWrVTK_value.SelectedIndex;
            }

            if (Fast.oneTurbine.fst.VTK_type.value != cboFstVTK_type_value.SelectedIndex+1)
            {
                Fast.oneTurbine.fst.VTK_type.oldValue = Fast.oneTurbine.fst.VTK_type.value;
                Fast.oneTurbine.fst.VTK_type.value = cboFstVTK_type_value.SelectedIndex+1;
            }

            if (Fast.oneTurbine.fst.VTK_fields.value != chkFstVTK_fields_value.IsChecked.Value)
            {
                Fast.oneTurbine.fst.VTK_fields.oldValue = Fast.oneTurbine.fst.VTK_fields.value;
                Fast.oneTurbine.fst.VTK_fields.value = chkFstVTK_fields_value.IsChecked.Value;
            }

            if (Fast.oneTurbine.fst.VTK_fps.value != double.Parse(txtFstVTK_fps_value.Text))
            {
                Fast.oneTurbine.fst.VTK_fps.oldValue = Fast.oneTurbine.fst.VTK_fps.value;
                Fast.oneTurbine.fst.VTK_fps.value = double.Parse(txtFstVTK_fps_value.Text);
            }







            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
