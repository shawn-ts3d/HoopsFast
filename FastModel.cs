using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.IO;
using FASTInputDll;

namespace HoopsFast
{
    public class FstModel
    {
        public HDModel hdModel { get; set; }
        public SDModel sdModel { get; set; }
        public MDModel mdModel { get; set; }
        public ADModel adModel { get; set; }
        public EDModel edModel { get; set; }

        public string fstFile { get; set; }
        public string filePath { get; set; }
        public string EDFile { get; set; }
        public string HydroFile { get; set; }
        public string AeroFile { get; set; }
        public string MooringFile { get; set; }
        public string SubFile { get; set; }
        public string ServoFile { get; set; }

        public int? CompElast { get; set; }
        public int? CompAero { get; set; }
        public int? CompHydro { get; set; }
        public int? CompMooring { get; set; }
        public int? CompSub { get; set; }
        public int? CompServo { get; set; }

        public bool convertElast { get; set; }
        public bool convertAero { get; set; }
        public bool convertHydro { get; set; }
        public bool convertSub { get; set; }
        public bool convertMooring { get; set; }
        public bool convertServo { get; set; }

        public FstModel()
        {
            hdModel = new HDModel();
            sdModel = new SDModel();
            mdModel = new MDModel();
            adModel = new ADModel();

            convertElast = false;
            convertAero = false;
            convertHydro = false;
            convertSub = false;
            convertMooring = false;
        }

        public void ParseFstInputFile(string fileName_fst, string status, ref FstInput fst)
        {
            var filePath = System.IO.Path.GetDirectoryName(fileName_fst);
            fst.ParseFstInput(fileName_fst, status);

            fstFile = fileName_fst;
            CompElast = fst.CompElast.value;
            CompAero = fst.CompAero.value;
            CompHydro = fst.CompHydro.value;
            CompMooring = fst.CompMooring.value;
            CompSub = fst.CompSub.value;
            CompServo = fst.CompServo.value;
            EDFile = Static_Methods.GetFileFullPath(filePath, fst.EDFile.value);
            HydroFile = Static_Methods.GetFileFullPath(filePath, fst.HydroFile.value);
            AeroFile = Static_Methods.GetFileFullPath(filePath, fst.AeroFile.value);
            MooringFile = Static_Methods.GetFileFullPath(filePath, fst.MooringFile.value);
            SubFile = Static_Methods.GetFileFullPath(filePath, fst.SubFile.value);
            ServoFile = Static_Methods.GetFileFullPath(filePath, fst.ServoFile.value);
        }

    }

    public class HDModel
    {
        int NJoints { get; set; }
        int NMembers { get; set; }
        int NPropSets { get; set; }
        public IDictionary<int, Vector3> jointList { get; set; }
        public IDictionary<int, List<int>> memberList { get; set; }
        public IDictionary<int, List<double>> sectionList { get; set; }
        string hdFile { get; set; }

        public HDModel()
        {
            NJoints = 0;
            NMembers = 0;
            NPropSets = 0;
            jointList = new Dictionary<int, Vector3> { };
            memberList = new Dictionary<int, List<int>> { };
            sectionList = new Dictionary<int, List<double>> { };
            hdFile = "";
        }

        public void ParseHDInputFile(string fileName_HD, string status, FstInput fst)
        {
            hdFile = fileName_HD;
            string filePath = Path.GetDirectoryName(hdFile);
            HDInput HD = new HDInput();
            HD.ParseHDInput(hdFile, status, fst);

            NJoints = HD.NJoints.value.Count;
            foreach (var item in HD.NJoints.value)
            {
                Vector3 jointCoord = new Vector3((float)item.Value[0], (float)item.Value[1], (float)item.Value[2]);
                jointList.Add(item.Key, jointCoord);
            }

            NMembers = HD.NMembers.value.Count;
            foreach (var item in HD.NMembers.value)
            {
                int jointID1 = (int)item.Value[0];
                int jointID2 = (int)item.Value[1];
                int sectID1 = (int)item.Value[2];
                int sectID2 = (int)item.Value[3];
                var memberProp = new List<int> { jointID1, jointID2, sectID1, sectID2 };
                memberList.Add(item.Key, memberProp);
            }

            NPropSets = HD.NPropSets.value.Count;
            sectionList = HD.NPropSets.value;
        }
    }

    public class SDModel
    {
        int NJoints { get; set; }
        int NMembers { get; set; }
        int NPropSets { get; set; }
        public IDictionary<int, Vector3> jointList { get; set; }
        public IDictionary<int, List<int>> memberList { get; set; }
        public IDictionary<int, List<double>> sectionList { get; set; }
        string sdFile { get; set; }

        public SDModel()
        {
            NJoints = 0;
            NMembers = 0;
            NPropSets = 0;
            jointList = new Dictionary<int, Vector3> { };
            memberList = new Dictionary<int, List<int>> { };
            sectionList = new Dictionary<int, List<double>> { };
            sdFile = "";
        }

        public void ParseSDInputFile(string fileName_SD, string status, FstInput fst)
        {
            sdFile = fileName_SD;
            SDInput SD = new SDInput();
            SD.ParseSDInput(sdFile, status, fst);

            NJoints = SD.NJoints.value.Count;
            foreach (var item in SD.NJoints.value)
            {
                int jointID = item.Key;
                float jointX = (float)item.Value[0];
                float jointY = (float)item.Value[1];
                float jointZ = (float)item.Value[2];
                Vector3 jointCoord = new Vector3(jointX, jointY, jointZ);

                jointList.Add(jointID, jointCoord);
            }

            NMembers = SD.NMembers.value.Count;
            foreach (var item in SD.NMembers.value)
            {
                int memberID = item.Key;
                int jointID1 = item.Value[0];
                int jointID2 = item.Value[1];
                int sectID1 = item.Value[2];
                int sectID2 = item.Value[2];

                var memberProp = new List<int> { jointID1, jointID2, sectID1, sectID2 };

                memberList.Add(memberID, memberProp);
            }

            NPropSets = SD.NPropSets.value.Count;
            foreach (var item in SD.NPropSets.value)
            {
                int sectionID = item.Key;
                double diameter = item.Value[3];
                double thickness = item.Value[4];
                var sectionProp = new List<double> { diameter, thickness };

                sectionList.Add(sectionID, sectionProp);
            }
        }
    }

    public class MDModel
    {
        public enum ConnectType
        {
            None,
            Fixed,
            Vessel
        }

        int NConnects { get; set; }
        public IDictionary<int, Vector3> connectList { get; set; }
        public IDictionary<int, Vector3> connectFixedList { get; set; }
        public IDictionary<int, Vector3> connectVesselList { get; set; }
        int NLines { get; set; }
        public IDictionary<int, List<int>> lineNodeList { get; set; }
        string mdFile { get; set; }

        public MDModel()
        {
            NConnects = 0;
            connectList = new Dictionary<int, Vector3> { };
            connectFixedList = new Dictionary<int, Vector3> { };
            connectVesselList = new Dictionary<int, Vector3> { };
            NLines = 0;
            lineNodeList = new Dictionary<int, List<int>> { };
            mdFile = "";
        }

        public void ParseMDInputFile(string fileName_MD, string status, FstInput fst)
        {
            mdFile = fileName_MD;
            MDInput MD = new MDInput();
            MD.ParseMDInput(mdFile, status, fst);

            NConnects = MD.NConnects.value;
            foreach (var item in MD.NConnects_Type)
            {
                int connectID = item.Key;
                ConnectType connectType = GetConnectType(item.Value);
                var valueList = MD.NConnects_values[connectID];
                float connectX = (float)valueList[0];
                float connectY = (float)valueList[1];
                float connectZ = (float)valueList[2];
                Vector3 connectCoord = new Vector3(connectX, connectY, connectZ);

                connectList.Add(connectID, connectCoord);
                if (connectType == ConnectType.Fixed)
                {
                    connectFixedList.Add(connectID, connectCoord);
                }
                if (connectType == ConnectType.Vessel)
                {
                    connectVesselList.Add(connectID, connectCoord);
                }
            }

            NLines = MD.NLines.value;
            foreach (var item in MD.NLines_values)
            {
                int lineID = item.Key;
                int lineNodeAnch = (int)item.Value[2];
                int lineNodeFair = (int)item.Value[3];

                var lineNodes = new List<int> { lineNodeAnch, lineNodeFair };
                lineNodeList.Add(lineID, lineNodes);
            }
        }



        private ConnectType GetConnectType(string strType)
        {
            ConnectType connectType = ConnectType.None;
            if (strType == "Fixed")
            {
                connectType = ConnectType.Fixed;
            }
            else if (strType == "Vessel")
            {
                connectType = ConnectType.Vessel;
            }
            else
            {
                //error, not supported yet
            }
            return connectType;
        }
    }

    public class ADModel
    {
        int NumTwrNds { get; set; }
        public IDictionary<int, List<double>> twrNodeList { get; set; }
        string adFile { get; set; }
        string ADBlFile1 { get; set; }
        string ADBlFile2 { get; set; }
        string ADBlFile3 { get; set; }

        public IDictionary<int, List<double>> adBldProp1 { get; set; }
        public IDictionary<int, List<double>> adBldProp2 { get; set; }
        public IDictionary<int, List<double>> adBldProp3 { get; set; }


        public ADModel()
        {
            NumTwrNds = 0;
            twrNodeList = new Dictionary<int, List<double>> { };
            adFile = "";
            ADBlFile1 = "";
            ADBlFile2 = "";
            ADBlFile3 = "";
            adBldProp1 = new Dictionary<int, List<double>> { };
            adBldProp2 = new Dictionary<int, List<double>> { };
            adBldProp3 = new Dictionary<int, List<double>> { };
        }

        public void ParseADInputFile(string fileName_AD, string status, FstInput fst)
        {
            adFile = fileName_AD;
            string filePath = Path.GetDirectoryName(adFile);
            ADInput AD = new ADInput();
            AD.ParseADInput(adFile, status, fst);

            NumTwrNds = AD.NumTwrNds.value.Count;
            twrNodeList = AD.NumTwrNds.value;
            ADBlFile1 = Static_Methods.GetFileFullPath(filePath, AD.ADBlFile1.value);
            ADBlFile2 = Static_Methods.GetFileFullPath(filePath, AD.ADBlFile2.value);
            ADBlFile3 = Static_Methods.GetFileFullPath(filePath, AD.ADBlFile3.value);

            adBldProp1 = ParseADBladeFile(ADBlFile1, AD);
            adBldProp2 = ParseADBladeFile(ADBlFile2, AD);
            adBldProp3 = ParseADBladeFile(ADBlFile3, AD);
        }

        private static IDictionary<int, List<double>> ParseADBladeFile(string BldFile, ADInput AD)
        {
            IDictionary<int, List<double>> bldProp = new Dictionary<int, List<double>> { };
            ADBlFileInput ADBlFile = new ADBlFileInput();
            var status = "";
            ADBlFile.ParseADBlFileInput(BldFile, status, AD);

            bldProp = ADBlFile.NumBlNds.value;
            return bldProp;
        }


    }

    public class EDModel
    {
        string edFile { get; set; }
        public int NumBl { get; set; }
        public double HubRad { get; set; }
        public double NacCMxn { get; set; }
        public double NacCMyn { get; set; }
        public double NacCMzn { get; set; }
        string BldFile1 { get; set; }
        string BldFile2 { get; set; }
        string BldFile3 { get; set; }

        public EDModel()
        {
            edFile = "";
            NumBl = 0;
            HubRad = 0.0;
            NacCMxn = 0.0;
            NacCMyn = 0.0;
            NacCMzn = 0.0;
        }

        public void ParseEDInputFile(string fileName_ED, string status, FstInput fst)
        {
            edFile = fileName_ED;
            string filePath = Path.GetDirectoryName(edFile);
            EDInput ED = new EDInput();
            ED.ParseEDInput(edFile, status, fst);
                
            NumBl = ED.NumBl.value;
            HubRad = ED.HubRad.value;
            NacCMxn = ED.NacCMxn.value;
            NacCMyn = ED.NacCMyn.value;
            NacCMzn = ED.NacCMzn.value;
            BldFile1 = Static_Methods.GetFileFullPath(filePath, ED.BldFile1.value);
            BldFile2 = Static_Methods.GetFileFullPath(filePath, ED.BldFile2.value);
            BldFile3 = Static_Methods.GetFileFullPath(filePath, ED.BldFile3.value);
        }
    }



    public static class FastModuleNum
    {
        public const int Module_Unknown = -1;      // Unknown[-]
        public const int Module_None = 0;      //No module selected[-]
        public const int Module_Glue = 1;      //Glue code[-]
        public const int Module_IfW = 2;      //InflowWind[-]
        public const int Module_OpFM = 3;      //OpenFOAM[-]
        public const int Module_ED = 4;      //ElastoDyn[-]
        public const int Module_BD = 5;      //BeamDyn[-]
        public const int Module_AD14 = 6;      //AeroDyn14[-]
        public const int Module_AD = 7;      //AeroDyn[-]
        public const int Module_SrvD = 8;      //ServoDyn[-]
        public const int Module_HD = 9;      //HydroDyn[-]
        public const int Module_SD = 10;      //SubDyn[-]
        public const int Module_ExtPtfm = 11;      //External Platform Loading MCKF[-]
        public const int Module_MAP = 12;      //MAP(Mooring Analysis Program) [-]
        public const int Module_FEAM = 13;      //FEAMooring[-]
        public const int Module_MD = 14;      //MoorDyn[-]
        public const int Module_Orca = 15;      //OrcaFlex integration(HD/Mooring) [-]
        public const int Module_IceF = 16;      //IceFloe[-]
        public const int Module_IceD = 17;      //IceDyn[-]
        public const int NumModules = 17;      //The number of modules available in FAST[-]
        public const int MaxNBlades = 3;   
    }



}
