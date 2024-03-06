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
    /// Interaction logic for AD_Tower.xaml
    /// </summary>
    public partial class AD_Tower : Window
    {

        List<AD.TowerNode> towerNodes = new List<AD.TowerNode>();

        public AD_Tower()
        {
            InitializeComponent();
            
            foreach (var towerNode in Fast.oneTurbine.AD.NumTwrNds.value)
            {
                towerNodes.Add(new AD.TowerNode() { Id = towerNode.Key, TwrElev = towerNode.Value[0], TwrDiam = towerNode.Value[1], TwrCd = towerNode.Value[2], TwrTI = towerNode.Value[3] });
            }

            dgTowerNodes.ItemsSource = towerNodes;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            Fast.oneTurbine.AD.NumTwrNds.oldValue = Fast.oneTurbine.AD.NumTwrNds.value;
            Fast.oneTurbine.AD.NumTwrNds.value = new Dictionary<int, List<double>> { };
            foreach (var towerNode in towerNodes)
            {
                List<double> valueList = new List<double> { towerNode.TwrElev, towerNode.TwrDiam, towerNode.TwrCd, towerNode.TwrTI };
                Fast.oneTurbine.AD.NumTwrNds.value.Add(towerNode.Id, valueList);
            }

            //Update visualization (temporary, need improvements)
            Hoops.ADKey = null;
            Hoops.ADTowerKey = null;
            (Application.Current.MainWindow as MainWindow).VisAD.Execute(Application.Current.MainWindow as MainWindow);
            ////////////////////////////////////////////////////
            

            this.Close();

        }
    }

}
