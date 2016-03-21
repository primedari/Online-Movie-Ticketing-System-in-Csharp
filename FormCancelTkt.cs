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

    public partial class FormCancelTkt : Form
    {

        Booking abook = new Booking(); // instantization of Booking 
        public FormCancelTkt()
        {
            InitializeComponent();
        }


        // Cancel tickets

        private void cancelButton_Click(object sender, EventArgs e)
        {

            if (bookingIdTextBox.Text != "")      // check if the booking Id textbox is not null
            {
                int bookid = Convert.ToInt32(bookingIdTextBox.Text);
                abook.SearchBooking(bookid);
                MessageBox.Show("Book Id:"+abook.BookingId+" \nMovieName:"+abook.MovieName
                    +" \nCustomer:"+abook.CustName
                    +" \nDate:"+abook.Date
                    +" ShowTime:"+abook.ShowTime,"Confirm Delete");
                int count = abook.DeleteBooking(bookid); // passing the details to DeleteBooking method of Booking class and storing the count.
               

            }
            else
            {

                if (nameTextBox.Text != "")
                {
                    string name = nameTextBox.Text;
                    int count = abook.DeleteName(name);

                    MessageBox.Show(count + ": Ticket Cancellation confirmed");
                }


                else
                {

                    if (phoneNumberTextBox.Text != "")
                    {
                        string num = phoneNumberTextBox.Text;
                        int count = abook.DeletePhone(num);
                        MessageBox.Show(count + ":Ticket Cancellation confirmed");
                    }
                }
            }
        }
    }
}
