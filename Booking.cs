using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace MovieTime.Business
{
    class Booking
    {

        //declaring datamembers of Booking

        public int BookingId { get; set; }
        public string MovieName { get; set; }
        public string CustName { get; set; }
        public string PhoneNum { get; set; }
        private int totalNumSeats;
        public DateTime Date { get; set; }
        public string ShowTime { get; set; }
        public double Price { get; set; }
        public string LastError { get; set; }

        public Booking() // default constructor
        {


        }
         // getter and setter for TotalNumSeats
        public int TotalNumSeats  
        {
            get
            {
                return totalNumSeats;
            }
            set
            {
                totalNumSeats = value;
                
            }
        }


        // Parameterized constructor
        public Booking(int bookid, string mname, string cusname, string phnum, int totalnumseats, DateTime date, string stime,double unitPrice)
        {

            BookingId = bookid;
            MovieName = mname;
            CustName = cusname;
            PhoneNum = phnum;
            TotalNumSeats = totalnumseats;
            Date = date;
            ShowTime = stime;
            
            CalculatePrice(unitPrice);
        }


        //Calculate price for tickets
        public double CalculatePrice(double unitPrice)
        {
          //  GetPrice(ShowTime);
            Price =  unitPrice * TotalNumSeats;
            return Price;
        }

        public override string ToString()
        {
            return "Booking Id" + BookingId + " MovieName" + MovieName + "Customer Name" + CustName + "PhoneNum" + PhoneNum + "Total No Seats" + TotalNumSeats + "Show Time" + ShowTime;
        }

        public double GetPrice(string ShowTime)
        {
            double price = 0.00;
           

            return price;
        }

        // Delete booking details from Booking table
            
        public int DeleteBooking(int bookid)
        {
            Connect aaConnect = new Connect();
            string sql = "Delete   from  [Booking]  where  [BookingId] =@BookingId";
            SqlParameter param1 = new SqlParameter("@BookingId", SqlDbType.Int);
            param1.Value = bookid;
            try
            {

                return aaConnect.ExecuteNonQuery(sql, CommandType.Text, param1);


            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return -1;
            }


        }

        // Delete name from Booking table

        public int DeleteName (string name)
        {
            Connect aaConnect = new Connect();
            string sql = "Delete from [Booking] where [CustomerName] =@CustomerName";
            SqlParameter param1 = new SqlParameter("@CustomerName", SqlDbType.NChar);
            param1.Value = name;
            try
            {

                return aaConnect.ExecuteNonQuery(sql, CommandType.Text, param1);


            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return -1;
            }


        }

        // Select query  to search 
        public void SearchBooking(int bookingId)
        {
            Connect aaConnect = new Connect();
            SqlParameter param = new SqlParameter("@BookingId", SqlDbType.Int);
            param.Value = bookingId;
            SqlDataReader ret = null;
            try
            {
                ret = (aaConnect.GetReader("SELECT * FROM Booking where BookingId = @BookingId", CommandType.Text, param));
              

                while (ret.Read())
                {
                    BookingId = int.Parse(ret[0].ToString());
                    MovieName = ret[1].ToString();
                    CustName = ret[2].ToString();
                    PhoneNum = ret[3].ToString();
                    TotalNumSeats = int.Parse(ret[4].ToString());
                    Date = DateTime.Parse(ret[5].ToString());
                    ShowTime = ret[6].ToString();
                    Price = Double.Parse(ret[7].ToString());
                    break;                  
                }
                ret.Close();
            }catch(Exception eo){
                throw new Exception(eo.Message);
            }
        }
        // // Delete phoneNumber from Booking table
        public int  DeletePhone (string num)
        {       
            Connect aaConnect = new Connect();
            string sql = "Delete  from  [Booking]  where  [PhoneNumber] = @Phonenumber";
            SqlParameter param1 = new SqlParameter("@PhoneNumber", SqlDbType.NChar);
            param1.Value = num;
            try
            {
                return aaConnect.ExecuteNonQuery(sql, CommandType.Text, param1);

            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return -1;
            }


        }

        //Insert the table Booking 

        public int AddBooking(Movie movie)
        {
            UpdateMovietable(movie,TotalNumSeats);

            Connect aaConnect = new Connect();
            string sql = "Insert INTO Booking values (@BookingId, @MovieName, @CustomerName, @PhoneNumber, @TotalSeats, @Date, @Showtime, @TotalPrice)";

            SqlParameter param1 = new SqlParameter("@BookingId", SqlDbType.Int);
            param1.Value = BookingId;
            SqlParameter param2 = new SqlParameter("@MovieName", SqlDbType.NChar);
            param2.Value = MovieName;
            SqlParameter param3 = new SqlParameter("@CustomerName", SqlDbType.NChar);
            param3.Value = CustName;
            SqlParameter param4 = new SqlParameter("@PhoneNumber", SqlDbType.NChar);
            param4.Value = PhoneNum;
            SqlParameter param5 = new SqlParameter("@TotalSeats", SqlDbType.Int);
            param5.Value = TotalNumSeats;
            SqlParameter param6 = new SqlParameter("@Date", SqlDbType.Date);
            param6.Value = Date;
            SqlParameter param7 = new SqlParameter("@Showtime", SqlDbType.NVarChar);
            param7.Value = ShowTime;
            SqlParameter param8 = new SqlParameter("@TotalPrice", SqlDbType.Decimal);
            param8.Value = Price;


            try
            {
                return aaConnect.ExecuteNonQuery(sql, CommandType.Text, param1, param2, param3, param4, param5, param6, param7, param8);
            }
            catch(Exception ep)
            {
                throw new Exception(ep.Message);
            
            }

        }

        //Update the table Movies
        private int UpdateMovietable(Movie movie, int seats)
        {
            Connect aaConnect = new Connect();
            int nSeats = movie.TotalNumSeats-seats;
            string sql = "UPDATE Movie SET NumofSeats = @NumofSeats Where MovieId = @MovieId";
            SqlParameter param1 = new SqlParameter("@NumofSeats", SqlDbType.Int);
            param1.Value = nSeats;
            SqlParameter param2 = new SqlParameter("@MovieId", SqlDbType.Int);
            param2.Value = movie.MovieId;

            try
            {
                return aaConnect.ExecuteNonQuery(sql, CommandType.Text, param1, param2);
            }
            catch (Exception ep)
            {
                throw new Exception(ep.Message);

            }

        }

        //Datatable

        public DataTable populate()
        {
            
            DataSet actorsDS = null;
            try
            {
                string sql = "select * from Booking";
                Connect aHollywoodConn = new Connect();

                actorsDS = new DataSet();
                actorsDS = aHollywoodConn.GetDataSet(sql);
                return actorsDS.Tables[0];
            }
            catch (Exception er)
            {
               throw new Exception(er.Message);
             
            }


        }

    }
}
