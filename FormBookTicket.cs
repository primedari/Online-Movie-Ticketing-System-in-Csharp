using MovieTime.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovieTime
{
    public partial class FormBookTicket : Form
    {


        Booking abook;   //declaring object for class Booking
        List<Movie> movieList = new List<Movie>();  // Generic ArrayList instantization
        
        public FormBookTicket()
        {
            InitializeComponent();
        }


        // Book Ticket for users
        private void bookButton_Click(object sender, EventArgs e)
        {

            if (null != movieListBox.SelectedItem && !movieListBox.SelectedItem.Equals("")) // checking for null
            {
                try
                {

                    // passing the values from form fields to variables
                    int bid = int.Parse(bookingIdTextBox.Text); 
                    string name = customerNameTextBox.Text;
                    int seats = int.Parse(noOfSeatsTextBox.Text);
                    string phNo = phoneNumberTextBox.Text;
                    Movie selMovie = movieList[movieListBox.SelectedIndex];
                    string moviename = selMovie.MovieName;
                    DateTime dat = selMovie.Date;
                    string stime = selMovie.ShowTime;
                    Double unitprice = selMovie.Price;
                   
                    abook = new Booking(bid, moviename, name, phNo, seats, dat, stime, unitprice); // 

                    int r = abook.AddBooking(selMovie); // adds the movie to the method AddBooking in Booking Class

                    if (r == -1)  // checks if there is any bookings added
                    {
                        throw new Exception("Error while adding customer"); // throws Exception while adding customers
                    }
                    else
                    {
                        // Display the data in MessageBox

                        MessageBox.Show("\tBook Id:" + abook.BookingId + " \n\tMovieName:" + abook.MovieName
                    + " \n\tCustomer:" + abook.CustName
                    + " \n\tDate:" + abook.Date
                    + "  ShowTime:" + abook.ShowTime
                    +" \n\tAmount Due:"+abook.Price.ToString("c"), "Ticket Booked");
                    }
                }
                catch (Exception ey) { MessageBox.Show(ey.Message); }  // Catching Exceptions
            }
            else
            {
                MessageBox.Show("Select A Movie to Book Tickets", "Movie Not selected");
            }
        }
        

        //Look through the databse for the search inputs
        private void searchButton_Click(object sender, EventArgs e)
        {

            string movieName = movieNameTextBox.Text; // movie name search string
            string language = languageTextBox.Text;     // language search string
            DateTime date = movieDateDateTimePicker.Value.Date;  // Date search string
            DataTable dt = new DataTable();
            Movie movie = new Movie();  // Object instantization for Movie class

            try
            {
                dt = movie.PopulateMovieListBox(movieName, language, date);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)   //iterate through the datatable
                    {
                      
                        //  passing the time , price ,movie id nad numof seats  to data table rows
                        
                        DateTime dat = DateTime.Parse(dt.Rows[i]["Date"].ToString());
                        Double price = Double.Parse(dt.Rows[i]["Price"].ToString());
                        int mId = int.Parse(dt.Rows[i]["MovieId"].ToString());
                        int num = int.Parse(dt.Rows[i]["NumofSeats"].ToString());

                        //passing the table  values to Movie parameterised Constructor
                        movie = new Movie(mId, dt.Rows[i]["MovieName"].ToString() , dat 
                            , dt.Rows[i]["Showtime"].ToString(), dt.Rows[i]["Language"].ToString(), price, num);


                        movieList.Add(movie);   // adding movie objects to list
                     
                    }
                }
                    else
                    movieListBox.Items.Add("Records not Found");  // 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Clear Buttons
        private void clearButton_Click(object sender, EventArgs e)
        {
            movieListBox.Items.Clear();
            movieNameTextBox.Text = "";
            languageTextBox.Text = "";
            bookingIdTextBox.Text = "";
            customerNameTextBox.Text = "";
            noOfSeatsTextBox.Text = "";
            phoneNumberTextBox.Text = "";
        }

        private void searchGroupBox_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            movieListBox.Items.Clear();    // Clear the ListBox

            //Linq query showing students with status 1 only
            var query = (from movie in movieList
                         where movie.TotalNumSeats > 0 
                         orderby movie.Date
                         select movie);

            movieList = new List<Movie>();
            foreach (Movie a in query)      
            {
                movieList.Add(a);
                movieListBox.Items.Add(a.ToString());

            }
        }
    }
}
