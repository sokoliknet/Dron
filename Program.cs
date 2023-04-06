using DroneSimulation.Entity;
using DroneSimulation.Utils;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DroneTask
{
  
    class Program
    {
        static List<Tube> tubes = null;
        static void Main(string[] args)
        {
            /// read data from stations 
            tubes = File.ReadAllLines("C:\\DroneSimulation\\DataContext\\tube.csv")
                                          .Select(v => Tube.FromCsv(v))
                                          .ToList();

            DroneService dronService = new DroneService();
            // publisher -- subcriber
            // observable -- observer
            dronService.Publisher += Subscriber;

            Task.Run(() =>
            {
                dronService.FillData("5937.csv");
            });

            Task.Run(() =>
            {
                dronService.FillData("6043.csv");
            });

            Console.ReadLine();
        }


        static void Subscriber(Drone drone)
        {
            var coordCurrentDrone = new GeoCoordinate(drone.Latitude, drone.Longitude);

            // define nearesr station for current drone 
            var nearestStation = tubes.Select(x => new GeoCoordinate(x.Latitude, x.Longitude))
                                   .OrderBy(x => x.GetDistanceTo(coordCurrentDrone))
                                   .First();

            // a nearby station should be less than 350 meters from the drone's position
            var coordStation = new GeoCoordinate(nearestStation.Latitude, nearestStation.Longitude);
            var distance = coordStation.GetDistanceTo(coordCurrentDrone);

            if (distance <= 350) ShowInfo(drone, nearestStation);

        }

        private static void ShowInfo(Drone drone, GeoCoordinate nearestStation)
        {     
            var currentStation = tubes.Where(x => x.Latitude == nearestStation.Latitude && x.Longitude == nearestStation.Longitude).FirstOrDefault();

            Console.WriteLine("Drone ID: {0}  Time: {1}, Speed: {2}, Station: {3}, Conditions of Traffic: {4}", 
                               drone.Id, drone.Time, drone.Speed, currentStation.Station, typeof(TrafficoConditionsEnum).GetRandomEnumValue());

            if (drone.Time.Hour == 8 && drone.Time.Minute == 10 && drone.Time.Second == 0)
            {
                Console.WriteLine("Drones were SHUTDOWN");
                Thread.CurrentThread.Abort();
            }
        }
    }


    public class DroneService
    {
        public delegate void FillhMethod(Drone search);
        public event FillhMethod Publisher = null;
        public void FillData(string file)
        {
            try
            {
                List<Drone> values = File.ReadAllLines("C:\\DroneSimulation\\DataContext\\" + file)
                                       .Select(v => Drone.FromCsv(v))
                                       .ToList();

                foreach (var d in values)
                {
                    Publisher(d);
                }

            }
            catch (System.Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }
        }
    }
}
