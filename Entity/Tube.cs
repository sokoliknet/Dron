using DroneSimulation.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace DroneSimulation.Entity
{


    class Tube
    {
        public string Station { set; get; }
        public double Latitude { set; get; }
        public double Longitude { set; get; }
        
        public static Tube FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            Tube tube = new Tube();
            tube.Station = Convert.ToString(values[0].RemoverStrs(new[] { "'", "\"" }));
            tube.Latitude = Convert.ToDouble(values[1]);
            tube.Longitude = Convert.ToDouble(values[2]);
          
            return tube;
        }
    }
}
