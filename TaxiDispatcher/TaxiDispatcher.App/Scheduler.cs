using System;
using System.Collections.Generic;

namespace TaxiDispatcher.App
{
    public class Scheduler
    {
        protected List<Taxi> availableTaxies = new List<Taxi>();
        
        public Scheduler()
        {
            InitAvailableTaxies();
        }

        public Ride OrderRide(int locationFrom, int locationTo, int rideType, DateTime time)
        {
            Taxi bestVehicle = FindTheBestVehicle(locationFrom);

            if(bestVehicle == null)
                return null;

            Ride ride = new Ride(locationFrom, locationTo, bestVehicle, rideType, time);

            Console.WriteLine(string.Format("Ride ordered, price: {0}", ride.Price.ToString()));
            return ride;
        }

        public void AcceptRide(Ride ride)
        {
            InMemoryRideDataBase.SaveRide(ride);

            ride.Vehicle.UpdateLocation(ride.LocationTo);

            Console.WriteLine(string.Format("Ride accepted, waiting for driver: {0}", ride.Vehicle.DriverName));
        }

        public List<Ride> GetDriverRidesList(int driverId)
        {
            List<Ride> rides = new List<Ride>();
            foreach (Ride ride in InMemoryRideDataBase.GetRides())
            {
                if (ride.Vehicle.DriverId == driverId)
                    rides.Add(ride);
            }

            return rides;
        }

        private void InitAvailableTaxies()
        {
            availableTaxies.Add(new Taxi(1, "Predrag", "Naxi", 10, 1));
            availableTaxies.Add(new Taxi(2, "Nenad", "Naxi", 10, 4));
            availableTaxies.Add(new Taxi(3, "Dragan", "Alfa", 15, 6));
            availableTaxies.Add(new Taxi(4, "Goran", "Gold", 13, 7));
        }

        private Taxi FindTheBestVehicle(int locationFrom)
        {
            Taxi minTaxi = availableTaxies[0];
            int minDistance = minTaxi.GetDistance(locationFrom);

            foreach(Taxi taxi in availableTaxies)
            {
                int currentDistance = taxi.GetDistance(locationFrom);
                if(currentDistance < minDistance)
                    {
                        minDistance = currentDistance;
                        minTaxi = taxi;
                    }
            }

            if (minDistance > 15)
                return null;

            return minTaxi;
        }
    }

    public class Taxi
    {
        public int DriverId { get; set; }
        public string DriverName { get; set; }
        public TaxiCompany Company { get; set; }
        public int Location { get; set; }

        public Taxi(int driverId, string driverName, string companyName, int companyPrice, int location)
        {
            this.DriverId = driverId;
            this.DriverName = driverName;
            this.Company = new TaxiCompany(companyName, companyPrice);
            this.Location = location;
        }

        public void UpdateLocation(int locationTo)
        {
            this.Location = locationTo;
        }

        public int GetDistance(int locationFrom)
        {
            return Math.Abs(Location - locationFrom);
        }
    }

    public class Ride
    {
        public int RideId { get; set; }
        public int LocationFrom { get; set; }
        public int LocationTo { get; set; }
        public Taxi Vehicle { get; set; }
        public int Price { get; private set; }

        public Ride(int locationFrom, int locationTo, Taxi vehicle, int rideType, DateTime time)
        {
            this.LocationFrom = locationFrom;
            this.LocationTo = locationTo;
            this.Vehicle = vehicle;
            CalculatePrice(rideType, time);
        }

        private void CalculatePrice(int rideType, DateTime time)
        {
            Price = Vehicle.Company.Price * Math.Abs(LocationFrom - LocationTo);

            if (rideType == Constants.InterCity)
            {
                Price *= 2;
            }

            if (time.Hour < 6 || time.Hour > 22)
            {
                Price *= 2;
            }
        }
    }

    public class TaxiCompany
    {
        public string Name;
        public int Price;

        public TaxiCompany(string companyName, int companyPrice)
        {
            Name = companyName;
            Price = companyPrice;
        }

    }
}
