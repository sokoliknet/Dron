using DroneSimulation.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace DroneSimulation.Entity
{
    public class Drone
    {

        public int Id { set; get; }
        public double Latitude { set; get; }
        public double Longitude { set; get; }
        public DateTime Time { set; get; }

        public readonly int Speed = 50 ;
        public static Drone FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            Drone drone = new Drone();
            drone.Id = Convert.ToInt32(values[0]);
            drone.Latitude = Convert.ToDouble(values[1].RemoverStrs(new[] { "'", "\"" }));
            drone.Longitude = Convert.ToDouble(values[2].RemoverStrs(new[] { "'", "\"" }));
            drone.Time = Convert.ToDateTime(values[3].RemoverStrs(new[] { "'", "\"" }));

            return drone;
        }
    }
}
