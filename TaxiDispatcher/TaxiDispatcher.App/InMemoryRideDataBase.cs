using System.Collections.Generic;
using static TaxiDispatcher.App.Scheduler;

namespace TaxiDispatcher.App
{
    public static class InMemoryRideDataBase
    {
        public static Dictionary<int, Ride> Rides = new Dictionary<int, Ride>();

        public static void SaveRide(Ride ride)
        {
            Rides.Add(getNextRideId(), ride);
        }

        public static int getNextRideId()
        {
            return Rides.Count + 1;
        }

        public static List<Ride> GetRides()
        {
            return new List<Ride>(Rides.Values);
        }
    }
}
