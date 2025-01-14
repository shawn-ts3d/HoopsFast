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
    /// Interaction logic for Fst_Output.xaml
    /// </summary>
    public partial class Fst_Output : Window
    {
        public Fst_Output()
        {
            InitializeComponent();

            lblFstSumPrint.Content = Fast.oneTurbine.fst.SumPrint.name;
            chkFstSumPrint_value.IsChecked = Fast.oneTurbine.fst.SumPrint.value;
            lblFstSumPrint_description.Content = Fast.oneTurbine.fst.SumPrint.description;

            lblFstSttsTime.Content = Fast.oneTurbine.fst.SttsTime.name;
            txtFstSttsTime_value.Text = Fast.oneTurbine.fst.SttsTime.value.ToString();
            lblFstSttsTime_description.Content = Fast.oneTurbine.fst.SttsTime.description;

            lblFstChkptTime.Content = Fast.oneTurbine.fst.ChkptTime.name;
            txtFstChkptTime_value.Text = Fast.oneTurbine.fst.ChkptTime.value.ToString();
            lblFstChkptTime_description.Content = Fast.oneTurbine.fst.ChkptTime.description;

            lblFstDT_Out.Content = Fast.oneTurbine.fst.DT_Out.name;
            txtFstDT_Out_value.Text = Fast.oneTurbine.fst.DT_Out.value.ToString();
            lblFstDT_Out_description.Content = Fast.oneTurbine.fst.DT_Out.description;

            lblFstTStart.Content = Fast.oneTurbine.fst.TStart.name;
            txtFstTStart_value.Text = Fast.oneTurbine.fst.TStart.value.ToString();
            lblFstTStart_description.Content = Fast.oneTurbine.fst.TStart.description;

            lblFstOutFileFmt.Content = Fast.oneTurbine.fst.OutFileFmt.name;
            cboFstOutFileFmt_value.Items.Insert(0, "0: uncompressed binary [<RootName>.outb]");
            cboFstOutFileFmt_value.Items.Insert(1, "1: text file [<RootName>.out]");
            cboFstOutFileFmt_value.Items.Insert(2, "2: binary file [<RootName>.outb]");
            cboFstOutFileFmt_value.Items.Insert(3, "3: both 1 and 2");
            cboFstOutFileFmt_value.Items.Insert(4, "4: uncompressed binary [<RootName>.outb");
            cboFstOutFileFmt_value.Items.Insert(5, "5: both 1 and 4");
            cboFstOutFileFmt_value.SelectedIndex = Fast.oneTurbine.fst.OutFileFmt.value;
            lblFstOutFileFmt_description.Content = Fast.oneTurbine.fst.OutFileFmt.description;

            lblFstTabDelim.Content = Fast.oneTurbine.fst.TabDelim.name;
            chkFstTabDelim_value.IsChecked = Fast.oneTurbine.fst.TabDelim.value;
            lblFstTabDelim_description.Content = Fast.oneTurbine.fst.TabDelim.description;

            lblFstOutFmt.Content = Fast.oneTurbine.fst.OutFmt.name;
            txtFstOutFmt_value.Text = Fast.oneTurbine.fst.OutFmt.value.ToString();
            lblFstOutFmt_description.Content = Fast.oneTurbine.fst.OutFmt.description;

        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            //Update inputs
            if (Fast.oneTurbine.fst.SumPrint.value != chkFstSumPrint_value.IsChecked.Value)
            {
                Fast.oneTurbine.fst.SumPrint.oldValue = Fast.oneTurbine.fst.SumPrint.value;
                Fast.oneTurbine.fst.SumPrint.value = chkFstSumPrint_value.IsChecked.Value;
            }

            if (Fast.oneTurbine.fst.SttsTime.value != double.Parse(txtFstSttsTime_value.Text))
            {
                Fast.oneTurbine.fst.SttsTime.oldValue = Fast.oneTurbine.fst.SttsTime.value;
                Fast.oneTurbine.fst.SttsTime.value = double.Parse(txtFstSttsTime_value.Text);
            }

            if (Fast.oneTurbine.fst.ChkptTime.value != double.Parse(txtFstChkptTime_value.Text))
            {
                Fast.oneTurbine.fst.ChkptTime.oldValue = Fast.oneTurbine.fst.ChkptTime.value;
                Fast.oneTurbine.fst.ChkptTime.value = double.Parse(txtFstChkptTime_value.Text);
            }

            if (Fast.oneTurbine.fst.DT_Out.value != double.Parse(txtFstDT_Out_value.Text))
            {
                Fast.oneTurbine.fst.DT_Out.oldValue = Fast.oneTurbine.fst.DT_Out.value;
                Fast.oneTurbine.fst.DT_Out.value = double.Parse(txtFstDT_Out_value.Text);
            }

            if (Fast.oneTurbine.fst.TStart.value != double.Parse(txtFstTStart_value.Text))
            {
                Fast.oneTurbine.fst.TStart.oldValue = Fast.oneTurbine.fst.TStart.value;
                Fast.oneTurbine.fst.TStart.value = double.Parse(txtFstTStart_value.Text);
            }

            if (Fast.oneTurbine.fst.OutFileFmt.value != cboFstOutFileFmt_value.SelectedIndex)
            {
                Fast.oneTurbine.fst.OutFileFmt.oldValue = Fast.oneTurbine.fst.OutFileFmt.value;
                Fast.oneTurbine.fst.OutFileFmt.value = cboFstOutFileFmt_value.SelectedIndex;
            }

            if (Fast.oneTurbine.fst.TabDelim.value != chkFstTabDelim_value.IsChecked.Value)
            {
                Fast.oneTurbine.fst.TabDelim.oldValue = Fast.oneTurbine.fst.TabDelim.value;
                Fast.oneTurbine.fst.TabDelim.value = chkFstTabDelim_value.IsChecked.Value;
            }

            if (Fast.oneTurbine.fst.OutFmt.value != txtFstOutFmt_value.Text.ToUpper())
            {
                Fast.oneTurbine.fst.OutFmt.oldValue = Fast.oneTurbine.fst.OutFmt.value;
                Fast.oneTurbine.fst.OutFmt.value = txtFstOutFmt_value.Text.ToUpper();
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
