using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckerCalculator
{
    class Source
    {
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

        public static float TravelTime()
        {
            int standardMPH = 50;

            Globals.hoursToTravel = (float)Math.Ceiling(Globals.miles / standardMPH);

            return Globals.hoursToTravel;
        }

        /*public float breakTime()
        {
            if (TravelTime() >= 14)
            {
                int driveBreak = 10;

                return TravelTime() + driveBreak;
            }
            else
            {
                return 0;
            }
        }*/

        public static int LoadingTime(int stopNum)
        {
            return stopNum * 3;
        }

        public static string DeliveryDay(double pickup, double miles, double nextMiles, int dayNum, int stopNum)
        {
            int newDay;

            float dropTime = (float)pickup + TravelTime() + TravelTime() + LoadingTime(stopNum);

            newDay = (int)((dropTime) / 24) + dayNum;

            if (newDay > 6)
            {
                newDay %= 7;
                string next = "next ";

                week nextWeek = (week)newDay;
                return next + nextWeek.ToString();
            }
            else
            {
                week finalDay = (week)newDay;
                return finalDay.ToString();
            }


        }

        public static float DeliveryTime(double pickup, double miles, double nextMiles, int stopNum)
        {
            float dropTime = (float)pickup + Globals.hoursToTravel + TravelTime() + LoadingTime(stopNum);

            return (int)((dropTime) % 24);
        }
    }
}
