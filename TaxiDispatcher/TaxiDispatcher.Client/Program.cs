using System;
using TaxiDispatcher.App;

namespace TaxiDispatcher.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Scheduler scheduler = new Scheduler();

            ScheduleRide(scheduler, 5, 0, Constants.City, new DateTime(2018, 1, 1, 23, 0, 0));
            ScheduleRide(scheduler, 0, 12, Constants.InterCity, new DateTime(2018, 1, 1, 9, 0, 0));
            ScheduleRide(scheduler, 5, 0, Constants.City, new DateTime(2018, 1, 1, 11, 0, 0));
            ScheduleRide(scheduler, 35, 12, Constants.City, new DateTime(2018, 1, 1, 11, 0, 0));
            
            Console.WriteLine("Driver with ID = 2 earned today:");
            int total = 0;
            foreach (Ride r in scheduler.GetDriverRidesList(2))
            {
                total += r.Price;
                Console.WriteLine("Price: " + r.Price);
            }

            Console.WriteLine("Total: " + total);
            Console.ReadLine();
        }
        
        private static void ScheduleRide(Scheduler scheduler, int locationFrom, int locationTo, int rideType, DateTime time)
        {
            Console.WriteLine(string.Format("Ordering ride from {0} to {1} ...", locationFrom.ToString(), locationTo.ToString()));
            Ride ride = scheduler.OrderRide(locationFrom, locationTo, rideType, time);
            if(ride == null)
                Console.WriteLine("There are no available taxi vehicles!");
            else
                scheduler.AcceptRide(ride);
            Console.WriteLine("");
        }
    }
}
