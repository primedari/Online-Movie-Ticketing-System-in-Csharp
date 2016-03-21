using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTime.Business
{
    
    public class Entertainment
    {

    // declaring datamembers
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public string ShowTime { get; set; }
        public double Price { get; set; }
        public int TotalNumSeats { get; set; }


        // constructors
        public Entertainment()
        {
        }

        public Entertainment(DateTime date, string sTime, double price, int totalNoSeats)
        {
            Date = date;
            ShowTime = sTime;
            Price = price;
            TotalNumSeats = totalNoSeats;
        }

        public double UpdateNoSeats()
        {
            return 1;
        }

        public override string ToString()
        {
            return "Date: " + Date.Date.ToShortDateString() +" "+ ShowTime + " Price: " + Price + " Seats: " + TotalNumSeats;
        }
    }
}
