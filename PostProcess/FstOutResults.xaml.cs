using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
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

namespace HoopsFast.PostProcess
{
    /// <summary>
    /// Interaction logic for FstOutResults.xaml
    /// </summary>
    public partial class FstOutResults : Window
    {
        IDictionary<string, string> unit = new Dictionary<string, string>();
        IDictionary<string, List<double>> values = new Dictionary<string, List<double>>();

        List<OutParameter> listOutParameters = new List<OutParameter>();

        public FstOutResults(string outFile)
        {
            InitializeComponent();

            ParseOutFile(outFile);

            //Import all the OpenFAST out parameters. This only needs to be done once.
            if (Fast.outParameterList == null)
            {
                Fast.outParameterList = new Dictionary<string, string>();
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage xlPackage = new ExcelPackage(new FileInfo(@"..\..\..\..\Resources\OutListParameters.xlsx")))
                {
                    for (int i = 1; i < xlPackage.Workbook.Worksheets.Count-3; i++)
                    {
                        var myWorksheet = xlPackage.Workbook.Worksheets[i]; //select sheet here
                        var totalRows = myWorksheet.Dimension.End.Row;
                        var totalColumns = myWorksheet.Dimension.End.Column;

                        if (totalColumns > 4)
                        {
                            for (int rowNum = 1; rowNum <= totalRows; rowNum++) //select starting row here
                            {
                                if (myWorksheet.Cells[rowNum, 1].Text.Trim() == "" && myWorksheet.Cells[rowNum, 2].Text.Trim() != "")
                                {
                                    if (!Fast.outParameterList.ContainsKey(myWorksheet.Cells[rowNum, 2].Text.Trim()))
                                    {
                                        Fast.outParameterList[myWorksheet.Cells[rowNum, 2].Text.Trim()] = myWorksheet.Cells[rowNum, 4].Text.Trim();
                                    }
                                        
                                    var otherNames = myWorksheet.Cells[rowNum, 3].Text.Trim();
                                    if (otherNames != "")
                                    {
                                        var listOtherNames = otherNames.Split(',').ToList();
                                        foreach (var name in listOtherNames)
                                        {
                                            if (!Fast.outParameterList.ContainsKey(name))
                                            {
                                                Fast.outParameterList[name] = myWorksheet.Cells[rowNum, 4].Text.Trim();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            LoadData();
        }

        private void ParseOutFile(string outFile)
        {
            var lines = System.IO.File.ReadAllLines(outFile);

            int valueLineNum = 0;
            string[] parameters = { };
            string[] unitArray = { };

            for (int i = 0; i < lines.Length; i++)
            {
                string[] oneInput = lines[i].Split('\t');

                if (oneInput[0] == "Time")
                {
                    parameters = oneInput;
                    unitArray = lines[i+1].Split('\t');
                    valueLineNum = i + 2;
                    break;
                }
            }

            for (int i = 0; i < unitArray.Length; i++)
            {
                unit[parameters[i]] = unitArray[i];
                values[parameters[i]] = new List<double>();
            }

            for (int i = valueLineNum; i < lines.Length; i++)
            {
                string[] oneValue = lines[i].Split('\t');
                for (int j = 0; j < parameters.Length; j++)
                {
                    List<double> valueVector = values[parameters[j]];
                    double value = double.Parse(oneValue[j]);
                    valueVector.Add(value);
                    values[parameters[j]] = valueVector;
                }
            }
        }

        private void LoadData()
        {
            for (int i = 0; i < unit.Count; i++)
            {
                OutParameter oneParameter = new OutParameter();     
                oneParameter.name = unit.ElementAt(i).Key;
                oneParameter.unit = unit.ElementAt(i).Value;
                
                if (oneParameter.name == "Time")
                {
                    oneParameter.selected = true;
                }
                else
                {
                    oneParameter.selected = false;
                    if (Fast.outParameterList.ContainsKey(oneParameter.name))
                    {
                        oneParameter.description = Fast.outParameterList[oneParameter.name];
                    }
                    else
                    {
                        oneParameter.description = "";
                    }      
                }

                listOutParameters.Add(oneParameter);
                //dataGridOutResults.Items.Add(oneParameter);              
            }


            dataGridOutResults.ItemsSource = listOutParameters;
            return;
        }

        private void btnPlot_Click(object sender, RoutedEventArgs e)
        {
            List<double> timeData = values["Time"];
            List<List<double>> data = new List<List<double>>();
            List<string> parameterNames = new List<string>();
            List<string> parameterUnits = new List<string>();

            foreach (var parameter in listOutParameters)
            {
                if (parameter.selected && parameter.name != "Time")
                {
                    data.Add(values[parameter.name]);
                    parameterNames.Add(parameter.name);
                    parameterUnits.Add(parameter.unit);
                }
            }

            Plot onePlot = new Plot(parameterNames, parameterUnits, timeData, data);
            onePlot.ShowDialog();
        }
    }


    public class OutParameter
    {
        public bool selected { get; set; }
        public string name { get; set; }
        public string unit { get; set; }
        public string description { get; set; }
       
    }
}
