﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FASTInputDll;

namespace HoopsFast
{
    public static class Fast
    {
        public static TurbineData oneTurbine { get; set; }
        public static FstModel fstModel { set; get; }
        public static FstInput fstInput { set; get; }
        public static string fileName_fst { set; get; }
        public static string status { set; get; }
    }

}


namespace HoopsFast.AD
{
    public class TowerNode
    {
        public int Id { get; set; }
        public double TwrElev { get; set; }
        public double TwrDiam { get; set; }
        public double TwrCd { get; set; }
        public double TwrTI { get; set; }
    }
}



