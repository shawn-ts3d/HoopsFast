using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FASTInputDll;

namespace HoopsFast
{
    public static class Hoops
    {
        public static HPS.Model Model { get; set; }
        public static HPS.SegmentKey HDKey { get; set; }
        public static HPS.SegmentKey HDJointsKey { get; set; }
        public static HPS.SegmentKey HDMembersKey { get; set; }

        public static HPS.SegmentKey ADKey { get; set; }
        public static HPS.SegmentKey ADTowerKey { get; set; }
        public static HPS.SegmentKey ADBladeKey { get; set; }
        public static HPS.SegmentKey ADBladeKey2 { get; set; }
        public static HPS.SegmentKey ADBladeKey3 { get; set; }

        public static HPS.SegmentKey EDKey { get; set; }
        public static HPS.SegmentKey EDRotorKey { get; set; }

        public static HPS.SegmentKey SDKey { get; set; }
        public static HPS.SegmentKey SDJointsKey { get; set; }
        public static HPS.SegmentKey SDMembersKey { get; set; }

        public static HPS.SegmentKey MDKey { get; set; }
        public static HPS.SegmentKey MDLinesKey { get; set; }

        public static HPS.CameraKit DefaultCamera { get; set; }

        enum HDJointUserData
        {
            JointID,
            JointAxID
        }

        public static void CreateNewHoopsModel()
        {
            Model = null;
            HDKey = HDJointsKey = HDMembersKey = null;
            ADKey = ADTowerKey = ADBladeKey = ADBladeKey2 = ADBladeKey3 = null;
            EDKey = EDRotorKey = null;
            SDKey = SDJointsKey = SDMembersKey = null;
            MDKey = MDLinesKey = null;
        }




        public static void CreateHDModel(TurbineData oneTurbine)
        {
            HDKey = Model.GetSegmentKey().Subsegment();

            if (HDJointsKey == null)
            {
                CreateHDJoints(oneTurbine);
            }
            else
            {
                
            }

            if (HDMembersKey == null)
            {
                CreateHDMembers(oneTurbine);
            }
            else
            {
                
            }
        }

        private static void CreateHDJoints(TurbineData oneTurbine)
        {
            HDJointsKey = HDKey.Subsegment();

            for (int i = 0; i < oneTurbine.HD.NJoints.value.Count - 1; i++)
            {
                HPS.SphereKit sphereKit = new HPS.SphereKit();
                KeyValuePair<int, List<double>> entry = oneTurbine.HD.NJoints.value.ElementAt(i);
                float Jointxi = (float)entry.Value[0];
                float Jointyi = (float)entry.Value[1];
                float Jointzi = (float)entry.Value[2];

                sphereKit.SetCenter(new HPS.Point(Jointxi, Jointyi, Jointzi));
                sphereKit.SetRadius(0.1f);
                sphereKit.SetBasis(new HPS.Vector(0, 1, 0), new HPS.Vector(1, 0, 0));

                byte[] myData;
                //UserData - JointID
                string JointID = entry.Key.ToString();
                myData = Encoding.ASCII.GetBytes(JointID);
                sphereKit.SetUserData(new IntPtr((int)HDJointUserData.JointID), (ulong)myData.Length, myData);

                //UserData - JointAxID
                string JointAxID = entry.Value[3].ToString();
                myData = Encoding.ASCII.GetBytes(JointAxID);
                sphereKit.SetUserData(new IntPtr((int)HDJointUserData.JointAxID), (ulong)myData.Length, myData);

                HDJointsKey.InsertSphere(sphereKit);
            }
        }

        private static void CreateHDMembers(TurbineData oneTurbine)
        {
            HDMembersKey = HDKey.Subsegment();

            for (int i = 0; i < oneTurbine.HD.NMembers.value.Count; i++)
            {
                HPS.CylinderKit cylinderKit = new HPS.CylinderKit();
                KeyValuePair<int, List<double>> entry = oneTurbine.HD.NMembers.value.ElementAt(i);
                int jointID1 = (int)entry.Value[0];
                int jointID2 = (int)entry.Value[1];
                int propSetID = (int)entry.Value[2];

                List<double> joint1 = oneTurbine.HD.NJoints.value[jointID1];
                List<double> joint2 = oneTurbine.HD.NJoints.value[jointID2];
                List<double> propSet = oneTurbine.HD.NPropSets.value[propSetID];

                HPS.Point[] points = new HPS.Point[2];
                points[0] = new HPS.Point((float)joint1[0], (float)joint1[1], (float)joint1[2]);
                points[1] = new HPS.Point((float)joint2[0], (float)joint2[1], (float)joint2[2]);
                cylinderKit.SetPoints(points);

                float[] radii = new float[2];
                radii[0] = (float)propSet[0] / 2;
                radii[1] = (float)propSet[0] / 2;
                cylinderKit.SetRadii(radii);

                cylinderKit.SetCaps(HPS.Cylinder.Capping.Both);

                HDMembersKey.InsertCylinder(cylinderKit);
            }
        }

        public static void CreateADModel(TurbineData oneTurbine)
        {
            ADKey = Model.GetSegmentKey().Subsegment();

            if (ADTowerKey == null)
            {
                CreateADTower(oneTurbine);
                CreateADBlades(oneTurbine);
            }
            else
            {

            }
        }

        private static void CreateADTower(TurbineData oneTurbine)
        {
            ADTowerKey = ADKey.Subsegment();
            HPS.CylinderKit cylinderKit = new HPS.CylinderKit();
            HPS.Point[] points = new HPS.Point[oneTurbine.AD.NumTwrNds.value.Count];
            float[] radii = new float[oneTurbine.AD.NumTwrNds.value.Count];

            for (int i = 0; i < oneTurbine.AD.NumTwrNds.value.Count; i++)
            {
                KeyValuePair<int, List<double>> entry = oneTurbine.AD.NumTwrNds.value.ElementAt(i);
                points[i] = new HPS.Point(0.0f, 0.0f, (float)entry.Value[0]);
                radii[i] = (float)entry.Value[1] / 2;
            }

            cylinderKit.SetPoints(points);
            cylinderKit.SetRadii(radii);
            cylinderKit.SetCaps(HPS.Cylinder.Capping.Both);

            ADTowerKey.InsertCylinder(cylinderKit);
        }

        private static void CreateADBlades(TurbineData oneTurbine)
        {
            ADBladeKey = ADKey.Subsegment();

            List<HPS.Point[]> PointArray = new List<HPS.Point[]>();

            for (int i = 1; i <= oneTurbine.AD.ADBlInput1.NumBlNds.value.Count; i++)    
            {
                PointArray.Add(GetPointArray(
                    oneTurbine.AD.AFfilesInput.value[(int)oneTurbine.AD.ADBlInput1.NumBlNds.value[i][6]-1], 
                    (float)oneTurbine.AD.ADBlInput1.NumBlNds.value[i][5],
                    (float)oneTurbine.AD.ADBlInput1.NumBlNds.value[i][0]));
            }

            for (int k = 0; k < PointArray.Count - 1; k++)
            {
                ADBladeKey.Subsegment().InsertPolygon(PointArray[k]);

                HPS.Point[] surface = new HPS.Point[PointArray[k].Length * 2];
                for (int index = 0; index < PointArray[k].Length; index++)
                {
                    surface[index] = PointArray[k][index];
                    surface[index + PointArray[k].Length] = PointArray[k + 1][index];
                }

                for (int index = 0; index < PointArray[k].Length; index++)
                {
                    int[] faceList = { 4, index, index + 1, index + 1 + PointArray[k].Length, index + PointArray[k].Length };
                    ADBladeKey.InsertShell(surface, faceList);
                }
            }

            HPS.Vector vector = new HPS.Vector(0.0f, 0.0f, 1.0f);
            ADBladeKey.GetModellingMatrixControl().RotateOffAxis(vector, 0.0f);


            if (oneTurbine.AD.ADBlFile2.value == oneTurbine.AD.ADBlFile1.value)
            {
                ADBladeKey2 = ADKey.Subsegment();
                ADBladeKey.CopyTo(ADBladeKey2);
            }
            else
            {
                //create Blade 2 if different from Blade 1
            }

            if (oneTurbine.AD.ADBlFile3.value == oneTurbine.AD.ADBlFile1.value)
            {
                ADBladeKey3 = ADKey.Subsegment();
                ADBladeKey.CopyTo(ADBladeKey3);
            }
            else
            {
                //create Blade 3 if different from Blade 1
            }

            ADBladeKey.GetModellingMatrixControl().Translate(
                (float)(oneTurbine.ED.NacCMxn.value * 2.0 + oneTurbine.ED.HubRad.value), 
                0.0f, 
                (float)(oneTurbine.ED.TowerHt.value + oneTurbine.ED.NacCMzn.value));

            vector = new HPS.Vector(1.0f, 0.0f, 0.0f);
            ADBladeKey2.GetModellingMatrixControl().RotateOffAxis(vector, 120.0f);
            ADBladeKey2.GetModellingMatrixControl().Translate(
               (float)(oneTurbine.ED.NacCMxn.value * 2.0 + oneTurbine.ED.HubRad.value), 
                0.0f,
                (float)(oneTurbine.ED.TowerHt.value + oneTurbine.ED.NacCMzn.value));

            ADBladeKey3.GetModellingMatrixControl().RotateOffAxis(vector, -120.0f);
            ADBladeKey3.GetModellingMatrixControl().Translate(
               (float)(oneTurbine.ED.NacCMxn.value * 2.0 + oneTurbine.ED.HubRad.value),
                0.0f,
               (float)(oneTurbine.ED.TowerHt.value + oneTurbine.ED.NacCMzn.value));


            //ADBladeKey.GetModellingMatrixControl().Translate(0.0f, 0.0f, (float)oneTurbine.ED.HubRad.value);
            //ADBladeKey2.GetModellingMatrixControl().Translate(0.0f, -(float)oneTurbine.ED.HubRad.value, 0.0f);
            //ADBladeKey3.GetModellingMatrixControl().Translate(0.0f, (float)oneTurbine.ED.HubRad.value, 0.0f);
        }

        private static HPS.Point[] GetPointArray(ADAfFileInput af, float chord, float z)
        {
            var size = af.NumCoords.value.Count;
            HPS.Point[] PointArray = new HPS.Point[size - 2];   
            
            //the first value is x-y coordinate of airfoil reference
            //the last point is the same as the first one
            for (int i = 1; i < size-1; i++)
            {
                float x = (float)af.NumCoords.value[i+1][0] * chord * 1.0f;
                float y = (float)af.NumCoords.value[i+1][1] * chord * 1.0f;
                PointArray[i-1] = new HPS.Point(x, y, z);
            }

            return PointArray;
        }

        public static void CreateEDModel(TurbineData oneTurbine)
        {
            EDKey = Model.GetSegmentKey().Subsegment();

            if (EDRotorKey == null)
            {
                CreateEDRotor(oneTurbine);
            }
            else
            {

            }
        }

        private static void CreateEDRotor(TurbineData oneTurbine)
        {
            EDRotorKey = EDKey.Subsegment();
            var twrTopNode = oneTurbine.AD.NumTwrNds.value.ElementAt(oneTurbine.AD.NumTwrNds.value.Count - 1);

            //naccalle
            HPS.CylinderKit cylinderKit = new HPS.CylinderKit();       
            HPS.Point[] points = new HPS.Point[2];
            points[0] = new HPS.Point(
                (float)oneTurbine.ED.NacCMxn.value * 2.0f,
                0.0f,
                (float)(twrTopNode.Value[0] + oneTurbine.ED.NacCMzn.value));
            points[1] = new HPS.Point(
                (float)-twrTopNode.Value[1],
                0.0f,
                (float)(twrTopNode.Value[0] + oneTurbine.ED.NacCMzn.value));
            cylinderKit.SetPoints(points);

            float[] radii = new float[2];
            radii[0] = (float)oneTurbine.ED.HubRad.value;
            radii[1] = (float)oneTurbine.ED.HubRad.value;
            cylinderKit.SetRadii(radii);
            cylinderKit.SetCaps(HPS.Cylinder.Capping.Both);

            //rotor
            HPS.CylinderKit cylinderKit2 = new HPS.CylinderKit();
            HPS.Point[] points2 = new HPS.Point[2];
            points2[0] = new HPS.Point(
                (float)oneTurbine.ED.NacCMxn.value * 2.0f,
                0.0f,
                (float)(twrTopNode.Value[0] + oneTurbine.ED.NacCMzn.value));
            points2[1] = new HPS.Point(
                (float)(oneTurbine.ED.NacCMxn.value * 2.0 + oneTurbine.ED.HubRad.value),
                0.0f,
                (float)(twrTopNode.Value[0] + oneTurbine.ED.NacCMzn.value));
            cylinderKit2.SetPoints(points2);

            float[] radii2 = new float[2];
            radii2[0] = (float)oneTurbine.ED.HubRad.value;
            radii2[1] = (float)oneTurbine.ED.HubRad.value;
            cylinderKit2.SetRadii(radii2);
            cylinderKit2.SetCaps(HPS.Cylinder.Capping.Both);

            HPS.SphereKit sphereKit = new HPS.SphereKit();
            sphereKit.SetCenter(new HPS.Point(
                (float)(oneTurbine.ED.NacCMxn.value * 2.0 + oneTurbine.ED.HubRad.value * 2.0),
                0.0f,
                (float)(twrTopNode.Value[0] + oneTurbine.ED.NacCMzn.value)));
            sphereKit.SetRadius((float)oneTurbine.ED.HubRad.value * 2.0f);
            sphereKit.SetBasis(new HPS.Vector(0, 1, 0), new HPS.Vector(1, 0, 0));

            //insert
            EDRotorKey.InsertCylinder(cylinderKit);
            EDRotorKey.InsertCylinder(cylinderKit2);
            EDRotorKey.InsertSphere(sphereKit);
        }

        public static void CreateSDModel(TurbineData oneTurbine)
        {
            SDKey = Model.GetSegmentKey().Subsegment();

            if (SDJointsKey == null)
            {
                CreateSDJoints(oneTurbine);
                CreateSDMembers(oneTurbine);
            }
            else
            {

            }
        }


        private static void CreateSDJoints(TurbineData oneTurbine)
        {
            SDJointsKey = SDKey.Subsegment();

            for (int i = 0; i < oneTurbine.SD.NJoints.value.Count - 1; i++)
            {
                HPS.SphereKit sphereKit = new HPS.SphereKit();
                KeyValuePair<int, List<double>> entry = oneTurbine.SD.NJoints.value.ElementAt(i);
                float Jointxi = (float)entry.Value[0];
                float Jointyi = (float)entry.Value[1];
                float Jointzi = (float)entry.Value[2];

                sphereKit.SetCenter(new HPS.Point(Jointxi, Jointyi, Jointzi));
                sphereKit.SetRadius(0.1f);
                sphereKit.SetBasis(new HPS.Vector(0, 1, 0), new HPS.Vector(1, 0, 0));

                SDJointsKey.InsertSphere(sphereKit);
            }
        }

        private static void CreateSDMembers(TurbineData oneTurbine)
        {
            SDMembersKey = SDKey.Subsegment();

            for (int i = 0; i < oneTurbine.SD.NMembers.value.Count; i++)
            {
                HPS.CylinderKit cylinderKit = new HPS.CylinderKit();
                KeyValuePair<int, List<int>> entry = oneTurbine.SD.NMembers.value.ElementAt(i);
                int jointID1 = entry.Value[0];
                int jointID2 = entry.Value[1];
                int propSetID = entry.Value[2];

                List<double> joint1 = oneTurbine.SD.NJoints.value[jointID1];
                List<double> joint2 = oneTurbine.SD.NJoints.value[jointID2];
                List<double> propSet = oneTurbine.SD.NPropSets.value[propSetID];

                HPS.Point[] points = new HPS.Point[2];
                points[0] = new HPS.Point((float)joint1[0], (float)joint1[1], (float)joint1[2]);
                points[1] = new HPS.Point((float)joint2[0], (float)joint2[1], (float)joint2[2]);
                cylinderKit.SetPoints(points);

                float[] radii = new float[2];
                radii[0] = (float)propSet[3] / 2;
                radii[1] = (float)propSet[3] / 2;
                cylinderKit.SetRadii(radii);

                cylinderKit.SetCaps(HPS.Cylinder.Capping.Both);

                SDMembersKey.InsertCylinder(cylinderKit);
            }
        }

        public static void CreateMDModel(TurbineData oneTurbine)
        {
            MDKey = Model.GetSegmentKey().Subsegment();

            if (MDLinesKey == null)
            {
                CreateMDLines(oneTurbine);
            }
            else
            {

            }
        }

        private static void CreateMDLines(TurbineData oneTurbine)
        {
            MDLinesKey = MDKey.Subsegment();

            foreach (var item in oneTurbine.MD.NLines_values)
            {
                HPS.Point[] pointArray = new HPS.Point[2];
                int connect_1 = (int)item.Value[2];
                int connect_2 = (int)item.Value[3];
                pointArray[0] = new HPS.Point((float)oneTurbine.MD.NConnects_values[connect_1][0], (float)oneTurbine.MD.NConnects_values[connect_1][1], (float)oneTurbine.MD.NConnects_values[connect_1][2]);
                pointArray[1] = new HPS.Point((float)oneTurbine.MD.NConnects_values[connect_2][0], (float)oneTurbine.MD.NConnects_values[connect_2][1], (float)oneTurbine.MD.NConnects_values[connect_2][2]);
                
                MDLinesKey.InsertLine(pointArray);
            }
        }


        static public void HideEverything()
        {
            if (HDKey != null)
                HDKey.GetVisibilityControl().SetFaces(false);
            if (ADKey != null)
                Hoops.ADKey.GetVisibilityControl().SetFaces(false);
            if (SDKey != null)
                Hoops.SDKey.GetVisibilityControl().SetFaces(false);
            if (EDKey != null)
                Hoops.EDKey.GetVisibilityControl().SetFaces(false);
            if (MDKey != null)
                Hoops.MDKey.GetVisibilityControl().SetLines(false);
        }



    }
}
