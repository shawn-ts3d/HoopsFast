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
    /// Interaction logic for Fst_SimCon.xaml
    /// </summary>
    public partial class Fst_SimCon : Window
    {
        public Fst_SimCon()
        {
            InitializeComponent();

            lblFstEcho.Content = Fast.oneTurbine.fst.Echo.name;
            chkFstEcho_value.IsChecked = Fast.oneTurbine.fst.Echo.value;
            lblFstEcho_description.Content = Fast.oneTurbine.fst.Echo.description;

            lblFstAbortLevel.Content = Fast.oneTurbine.fst.AbortLevel.name;
            cboFstAbortLevel_value.Items.Insert(0, "\"WARNING\"");
            cboFstAbortLevel_value.Items.Insert(1, "\"SEVERE\"");
            cboFstAbortLevel_value.Items.Insert(2, "\"FATAL\"");
            if (Fast.oneTurbine.fst.AbortLevel.value.ToUpper() == "\"WARNING\"")
            {
                cboFstAbortLevel_value.SelectedIndex = 0;
            }
            else if (Fast.oneTurbine.fst.AbortLevel.value.ToUpper() == "\"SEVERE\"")
            {
                cboFstAbortLevel_value.SelectedIndex = 1;
            }
            else if (Fast.oneTurbine.fst.AbortLevel.value.ToUpper() == "\"FATAL\"")
            {
                cboFstAbortLevel_value.SelectedIndex = 2;
            }
            else 
            {
                //error
            }
            lblFstAbortLevel_description.Content = Fast.oneTurbine.fst.AbortLevel.description;

            lblFstTMax.Content = Fast.oneTurbine.fst.TMax.name;
            txtFstTMax_value.Text = Fast.oneTurbine.fst.TMax.value.ToString();
            lblFstTMax_description.Content = Fast.oneTurbine.fst.TMax.description;

            lblFstDT.Content = Fast.oneTurbine.fst.DT.name;
            txtFstDT_value.Text = Fast.oneTurbine.fst.DT.value.ToString();
            lblFstDT_description.Content = Fast.oneTurbine.fst.DT.description;

            lblFstInterpOrder.Content = Fast.oneTurbine.fst.InterpOrder.name;
            cboFstInterpOrder_value.Items.Insert(0, "1=linear");
            cboFstInterpOrder_value.Items.Insert(1, "2=quadratic");
            if (Fast.oneTurbine.fst.InterpOrder.value == 1)
            {
                cboFstInterpOrder_value.SelectedIndex = 0;
            }
            else if (Fast.oneTurbine.fst.InterpOrder.value == 2)
            {
                cboFstInterpOrder_value.SelectedIndex = 1;
            }
            lblFstInterpOrder_description.Content = Fast.oneTurbine.fst.InterpOrder.description;

            lblFstNumCrctn.Content = Fast.oneTurbine.fst.NumCrctn.name;
            txtFstNumCrctn_value.Text = Fast.oneTurbine.fst.NumCrctn.value.ToString();
            lblFstNumCrctn_description.Content = Fast.oneTurbine.fst.NumCrctn.description;

            lblFstDT_UJac.Content = Fast.oneTurbine.fst.DT_UJac.name;
            txtFstDT_UJac_value.Text = Fast.oneTurbine.fst.DT_UJac.value.ToString();
            lblFstDT_UJac_description.Content = Fast.oneTurbine.fst.DT_UJac.description;

            lblFstUJacSclFact.Content = Fast.oneTurbine.fst.UJacSclFact.name;
            txtFstUJacSclFact_value.Text = Fast.oneTurbine.fst.UJacSclFact.value.ToString();
            lblFstUJacSclFact_description.Content = Fast.oneTurbine.fst.UJacSclFact.description;

        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            //Update inputs
            if (Fast.oneTurbine.fst.Echo.value != chkFstEcho_value.IsChecked.Value)
            {
                Fast.oneTurbine.fst.Echo.oldValue = Fast.oneTurbine.fst.Echo.value;
                Fast.oneTurbine.fst.Echo.value = chkFstEcho_value.IsChecked.Value;
            }

            if (Fast.oneTurbine.fst.AbortLevel.value.ToUpper() != cboFstAbortLevel_value.SelectedItem.ToString().ToUpper())
            {
                Fast.oneTurbine.fst.AbortLevel.oldValue = Fast.oneTurbine.fst.AbortLevel.value;
                Fast.oneTurbine.fst.AbortLevel.value = cboFstAbortLevel_value.SelectedItem.ToString().ToUpper();
            }

            if (Fast.oneTurbine.fst.TMax.value != double.Parse(txtFstTMax_value.Text))
            {
                Fast.oneTurbine.fst.TMax.oldValue = Fast.oneTurbine.fst.TMax.value;
                Fast.oneTurbine.fst.TMax.value = double.Parse(txtFstTMax_value.Text);
            }

            if (Fast.oneTurbine.fst.DT.value != double.Parse(txtFstDT_value.Text))
            {
                Fast.oneTurbine.fst.DT.oldValue = Fast.oneTurbine.fst.DT.value;
                Fast.oneTurbine.fst.DT.value = double.Parse(txtFstDT_value.Text);
            }

            if (Fast.oneTurbine.fst.InterpOrder.value != cboFstInterpOrder_value.SelectedIndex+1)
            {
                Fast.oneTurbine.fst.InterpOrder.oldValue = Fast.oneTurbine.fst.InterpOrder.value;
                Fast.oneTurbine.fst.InterpOrder.value = cboFstInterpOrder_value.SelectedIndex + 1;
            }

            if (Fast.oneTurbine.fst.NumCrctn.value != int.Parse(txtFstNumCrctn_value.Text))
            {
                Fast.oneTurbine.fst.NumCrctn.oldValue = Fast.oneTurbine.fst.NumCrctn.value;
                Fast.oneTurbine.fst.NumCrctn.value = int.Parse(txtFstNumCrctn_value.Text);
            }

            if (Fast.oneTurbine.fst.DT_UJac.value != double.Parse(txtFstDT_UJac_value.Text))
            {
                Fast.oneTurbine.fst.DT_UJac.oldValue = Fast.oneTurbine.fst.DT_UJac.value;
                Fast.oneTurbine.fst.DT_UJac.value = double.Parse(txtFstDT_UJac_value.Text);
            }

            if (Fast.oneTurbine.fst.UJacSclFact.value != double.Parse(txtFstUJacSclFact_value.Text))
            {
                Fast.oneTurbine.fst.UJacSclFact.oldValue = Fast.oneTurbine.fst.UJacSclFact.value;
                Fast.oneTurbine.fst.UJacSclFact.value = double.Parse(txtFstUJacSclFact_value.Text);
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
