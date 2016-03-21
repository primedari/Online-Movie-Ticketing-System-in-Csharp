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
    public partial class FormViewReport : Form
    {
        DataTable dt = null;     //datatable dt is initialized to null
        Booking abook = new Booking();  // Booking instantization
        public FormViewReport()
        {
            InitializeComponent();
        }

        private void FormViewReport_Load(object sender, EventArgs e)
        {
            
          
             dt = abook.populate();      // passing the details to method populate of the booking class
            displayDataGrid.DataSource = abook.populate();  // displaying the data in datagrid
          
        }


      
       
    }
}
