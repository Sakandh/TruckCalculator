using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckerCalculator
{
    // member type for the days of the week
    enum week { MON, TUE, WED, THUR, FRI, SAT, SUN };

    class Program
    {
        static void Main(string[] args)
        {
            // runs the program until the user types "done"
            string exit = "done";
            do
            {
                // user input the starting day, M-F, checks day entered is valid
                Console.WriteLine("Enter departure day: ");
                string day = Console.ReadLine().ToUpper();
                int dayNum = DayInt(day);
                if (dayNum == -1)
                {
                    Console.WriteLine("Invalid day");
                    continue;
                }

                // user input the starting time in 24 hr format, must be under 24
                Console.WriteLine("Enter departure time: ");
                double pickupTime = Convert.ToInt32(Console.ReadLine());
                if (pickupTime > 24)
                {
                    Console.WriteLine("Invalid time");
                    continue;
                }

                // user input the number of stops until delivery
                Console.WriteLine("Enter number of stops: ");
                int stopNum = Convert.ToInt32(Console.ReadLine());

                // user input the distance between each stops in miles and adds all together for a total
                double miles = 0;
                for (int i = 0; i < stopNum; i++)
                {
                    Console.WriteLine("Enter distance (miles) to stop #" + (i+1) + " : ");
                    miles += Convert.ToInt32(Console.ReadLine());
                }

                // returns the calculation based on the inputs
                CalculateResponse(miles, stopNum, pickupTime, dayNum);
                
            }
            while (Console.ReadLine() != exit);
        }

        // the end message that calls all the calculation functions
        public static void CalculateResponse(double miles, int stopNum, double pickupTime, int dayNum)
        {
            Console.WriteLine("\nIt will take " + TotalTravelTime(miles, stopNum) + " hours.");
            Console.WriteLine("Final estimated delivery on " + DeliveryDay(pickupTime, miles, dayNum, stopNum)
                + " at " + DeliveryTime(pickupTime, miles, stopNum) + ".");
        }

        // the entered day corresponds with the week enum values 0-6
        public static int DayInt(string day)
        {
            if (day == "MONDAY" || day == "MON")
                return (int)week.MON;
            else if (day == "TUESDAY" || day == "TUE")
                return (int)week.TUE;
            else if (day == "WEDNESDAY" || day == "WED")
                return (int)week.WED;
            else if (day == "THURSDAY" || day == "THUR")
                return (int)week.THUR;
            else if (day == "FRIDAY" || day == "FRI")
                return (int)week.FRI;
            else if (day == "SATURDAY" || day == "SAT")
                return (int)week.SAT;
            else if (day == "SUNDAY" || day == "SUN")
                return (int)week.SUN;
            else
                return -1;
        }

        // the entered total miles divided by the standard mph, 50
        //plus the loading time (3) times the number of stops
        public static float TravelTime(double miles, int stopNum)
        {
            int standardMPH = 50;
            int loadTime = 3;

            float travelLoadTime = (float)Math.Ceiling(miles / standardMPH) + (loadTime * stopNum);
            
            return travelLoadTime;
        }

        // takes the travel time with load time divides by 14 to get the number of breaks 
        // and add that to the total travel time
        public static int TotalTravelTime(double miles, int stopNum)
        {
            int breakTime = 10;

            int breakNum = (int)(TravelTime(miles, stopNum) / 14) * breakTime;
            int breakTravelTime = (int)TravelTime(miles, stopNum) + breakNum;

            return breakTravelTime;
        }

        // adds the total travel time to the start time then divide by 24 to get the number of days
        // adding the days of travel to the start day number to get the delivery day
        // checks to see if final day is over 6, then adds the remaining days to the next week
        public static string DeliveryDay(double pickup, double miles, int dayNum, int stopNum)
        {
            int newDay;

            float dropTime = (float)pickup + TotalTravelTime(miles, stopNum);

            newDay = (int)((dropTime) / 24) + dayNum;

            if (newDay > 6)
            {
                newDay %= 7;
                string next = "next ";

                week nextWeek = (week)newDay;
                string nextWeekDay = next + nextWeek.ToString();

                return nextWeekDay;
            }
            else
            {
                week finalDay = (week)newDay;
                return finalDay.ToString();
            }
        }

        // adds the total travel time to the start time then does modulus to get the remainder time from days
        public static float DeliveryTime(double pickup, double miles, int stopNum)
        {
            float dropTime = (float)pickup + TotalTravelTime(miles, stopNum);
            int finalDropTime = (int)((dropTime) % 24);

            return finalDropTime;
        }
    }
}