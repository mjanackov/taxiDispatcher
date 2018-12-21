using System;
using System.Collections.Generic;

namespace TaxiDispatcher.App
{
    public class Scheduler
    {
        protected Taxi taxi1 = new Taxi { Taxi_driver_id = 1, Taxi_driver_name = "Predrag", Taxi_company = "Naxi", Location = 1};
        protected Taxi taxi2 = new Taxi { Taxi_driver_id = 2, Taxi_driver_name = "Nenad", Taxi_company = "Naxi", Location = 4 };
        protected Taxi taxi3 = new Taxi { Taxi_driver_id = 3, Taxi_driver_name = "Dragan", Taxi_company = "Alfa", Location = 6 };
        protected Taxi taxi4 = new Taxi { Taxi_driver_id = 4, Taxi_driver_name = "Goran", Taxi_company = "Gold", Location = 7 };
        public Ride OrderRide(int location_from, int location_to, int ride_type, DateTime time)
        {
            #region FindingTheBestVehicle 

            Taxi min_taxi = taxi1;
            int min_distance = Math.Abs(taxi1.Location - location_from);

            if (Math.Abs(taxi2.Location - location_from) < min_distance)
            {
                min_taxi = taxi2;
                min_distance = Math.Abs(taxi2.Location - location_from);
            }

            if (Math.Abs(taxi3.Location - location_from) < min_distance)
            {
                min_taxi = taxi3;
                min_distance = Math.Abs(taxi3.Location - location_from);
            }

            if (Math.Abs(taxi4.Location - location_from) < min_distance)
            {
                min_taxi = taxi4;
                min_distance = Math.Abs(taxi4.Location - location_from);
            }

            if (min_distance > 15)
                throw new Exception("There are no available taxi vehicles!");

            #endregion

            #region CreatingRide

            Ride ride = new Ride();
            ride.Taxi_driver_id = min_taxi.Taxi_driver_id;
            ride.Location_from = location_from;
            ride.Location_to = location_to;
            ride.Taxi_driver_name = min_taxi.Taxi_driver_name;

            #endregion

            #region CalculatingPrice

            switch (min_taxi.Taxi_company)
            {
                case "Naxi":
                {
                    ride.Price = 10 * Math.Abs(location_from - location_to);
                    break;
                }
                case "Alfa":
                {
                    ride.Price = 15 * Math.Abs(location_from - location_to);
                    break;
                }
                case "Gold":
                {
                    ride.Price = 13 * Math.Abs(location_from - location_to);
                    break;
                }
                default:
                {
                    throw new Exception("Ilegal company");
                }
            }

            if (ride_type == Constants.InterCity)
            {
                ride.Price *= 2;
            }

            if (time.Hour < 6 || time.Hour > 22)
            {
                ride.Price *= 2;
            }

            #endregion

            Console.WriteLine("Ride ordered, price: " + ride.Price.ToString());
            return ride;
        }

        public void AcceptRide(Ride ride)
        {
            InMemoryRideDataBase.SaveRide(ride);

            if (taxi1.Taxi_driver_id == ride.Taxi_driver_id)
            {
                taxi1.Location = ride.Location_to;
            }

            if (taxi2.Taxi_driver_id == ride.Taxi_driver_id)
            {
                taxi2.Location = ride.Location_to;
            }

            if (taxi3.Taxi_driver_id == ride.Taxi_driver_id)
            {
                taxi3.Location = ride.Location_to;
            }

            if (taxi4.Taxi_driver_id == ride.Taxi_driver_id)
            {
                taxi4.Location = ride.Location_to;
            }

            Console.WriteLine("Ride accepted, waiting for driver: " + ride.Taxi_driver_name);
        }

        public List<Ride> GetRideList(int driver_id)
        {
            List<Ride> rides = new List<Ride>();
            List<int> ids = InMemoryRideDataBase.GetRide_Ids();
            foreach (int id in ids)
            {
                Ride ride = InMemoryRideDataBase.GetRide(id);
                if (ride.Taxi_driver_id == driver_id)
                    rides.Add(ride);
            }

            return rides;
        }

        public class Taxi
        {
            public int Taxi_driver_id { get; set; }
            public string Taxi_driver_name { get; set; }
            public string Taxi_company { get; set; }
            public int Location { get; set; }
        }

        public class Ride
        {
            public int Ride_id { get; set; }
            public int Location_from { get; set; }
            public int Location_to { get; set; }
            public int Taxi_driver_id { get; set; }
            public string Taxi_driver_name { get; set; }
            public int Price { get; set; }
        }
    }
}
