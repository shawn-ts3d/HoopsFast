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
    /// Interaction logic for Fst_InputFiles.xaml
    /// </summary>
    public partial class Fst_InputFiles : Window
    {
        public Fst_InputFiles()
        {
            InitializeComponent();

            lblFstEDFile.Content = Fast.oneTurbine.fst.EDFile.name;
            txtFstEDFile_value.Text = Fast.oneTurbine.fst.EDFile.value.ToString();
            lblFstEDFile_description.Content = Fast.oneTurbine.fst.EDFile.description;

            lblFstBDBldFile1.Content = Fast.oneTurbine.fst.BDBldFile1.name;
            txtFstBDBldFile1_value.Text = Fast.oneTurbine.fst.BDBldFile1.value.ToString();
            lblFstBDBldFile1_description.Content = Fast.oneTurbine.fst.BDBldFile1.description;

            lblFstBDBldFile2.Content = Fast.oneTurbine.fst.BDBldFile2.name;
            txtFstBDBldFile2_value.Text = Fast.oneTurbine.fst.BDBldFile2.value.ToString();
            lblFstBDBldFile2_description.Content = Fast.oneTurbine.fst.BDBldFile2.description;

            lblFstBDBldFile3.Content = Fast.oneTurbine.fst.BDBldFile3.name;
            txtFstBDBldFile3_value.Text = Fast.oneTurbine.fst.BDBldFile3.value.ToString();
            lblFstBDBldFile3_description.Content = Fast.oneTurbine.fst.BDBldFile3.description;

            lblFstInflowFile.Content = Fast.oneTurbine.fst.InflowFile.name;
            txtFstInflowFile_value.Text = Fast.oneTurbine.fst.InflowFile.value.ToString();
            lblFstInflowFile_description.Content = Fast.oneTurbine.fst.InflowFile.description;

            lblFstAeroFile.Content = Fast.oneTurbine.fst.AeroFile.name;
            txtFstAeroFile_value.Text = Fast.oneTurbine.fst.AeroFile.value.ToString();
            lblFstAeroFile_description.Content = Fast.oneTurbine.fst.AeroFile.description;

            lblFstServoFile.Content = Fast.oneTurbine.fst.ServoFile.name;
            txtFstServoFile_value.Text = Fast.oneTurbine.fst.ServoFile.value.ToString();
            lblFstServoFile_description.Content = Fast.oneTurbine.fst.ServoFile.description;

            lblFstHydroFile.Content = Fast.oneTurbine.fst.HydroFile.name;
            txtFstHydroFile_value.Text = Fast.oneTurbine.fst.HydroFile.value.ToString();
            lblFstHydroFile_description.Content = Fast.oneTurbine.fst.HydroFile.description;

            lblFstSubFile.Content = Fast.oneTurbine.fst.SubFile.name;
            txtFstSubFile_value.Text = Fast.oneTurbine.fst.SubFile.value.ToString();
            lblFstSubFile_description.Content = Fast.oneTurbine.fst.SubFile.description;

            lblFstMooringFile.Content = Fast.oneTurbine.fst.MooringFile.name;
            txtFstMooringFile_value.Text = Fast.oneTurbine.fst.MooringFile.value.ToString();
            lblFstMooringFile_description.Content = Fast.oneTurbine.fst.MooringFile.description;

            lblFstIceFile.Content = Fast.oneTurbine.fst.IceFile.name;
            txtFstIceFile_value.Text = Fast.oneTurbine.fst.IceFile.value.ToString();
            lblFstIceFile_description.Content = Fast.oneTurbine.fst.IceFile.description;

        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
