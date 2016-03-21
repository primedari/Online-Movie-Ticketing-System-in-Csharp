using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace MovieTime.Business
{
   
    public class Movie : Entertainment
    {
        public int MovieId { get; set; }
        public string MovieName { get; set; }
        public string Language { get; set; }
        public string LastError { get; set; }
        Connect dbConnect = new Connect();

        public Movie()
        {
        }

        public Movie(
            int movieid, string mname,
            DateTime date, string stime,
            string lang,
            double price, int tnoseats)
            : base(date, stime, price, tnoseats)
        {
            MovieId = movieid;
            MovieName = mname;
            Language = lang;
        }

        public override string ToString()
        {
            return "Name: " + MovieName + base.ToString();
        }

        public DataTable PopulateMovieListBox(string movieName, string language, DateTime date)
        {
            string sql = "Select * FROM [Movie] WHERE MovieName = @MovieName or Language = @Language or Date = @Date";
            SqlParameter param1 = new SqlParameter("@MovieName", SqlDbType.NChar);
            param1.Value = movieName;
            SqlParameter param2 = new SqlParameter("@Language", SqlDbType.NChar);
            param2.Value = language;
            SqlParameter param3 = new SqlParameter("@Date", SqlDbType.Date);
            param3.Value = date;
            try
            {
                return dbConnect.GetTable(sql, CommandType.Text, param1, param2, param3);
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return null;
            }
        }
    }
}
