using System.Collections.Generic;
using static TaxiDispatcher.App.Scheduler;

namespace TaxiDispatcher.App
{
    public static class InMemoryRideDataBase
    {
        public static List<Ride> Rides = new List<Ride>();

        public static void SaveRide(Ride ride)
        {
            int max_id = Rides.Count == 0 ? 0 : Rides[0].Ride_id;
            foreach (Ride r in Rides)
            {
                if (r.Ride_id > max_id)
                    max_id = r.Ride_id;
            }

            ride.Ride_id = max_id + 1;
            Rides.Add(ride);
        }

        public static Ride GetRide(int id)
        {
            Ride ride = Rides[0];
            bool found = ride.Ride_id == id;
            int current = 1;
            while (!found)
            {
                ride = Rides[current];
                found = ride.Ride_id == id;
                current += 1;
            }

            return ride;
        }

        public static List<int> GetRide_Ids()
        {
            List<int> ids = new List<int>();
            foreach (Ride ride in Rides)
            {
                ids.Add(ride.Ride_id);
            }

            return ids;
        }
    }
}
