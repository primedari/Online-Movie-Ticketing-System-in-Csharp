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
    public class Connect
    {
        private SqlConnection connection;

        //Connection String

        string connectionString = ConfigurationManager.ConnectionStrings["MovieTimeDBConnectionString"].ToString();

        public SqlConnection GetConnection()
        {
           if (connection == null)
           connection = new SqlConnection(connectionString);
           return connection;
       }
       
        public void Open()  //Open Connection
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
        }
        public void Close()   //Close Connection
        {
            connection.Close();
        }

        public SqlDataReader GetReader(string procnameOrQuery, CommandType cmdType, SqlParameter param1 = null, SqlParameter param2 = null, SqlParameter param3 = null, SqlParameter param4 = null)
        {

            SqlCommand cmd = new SqlCommand(procnameOrQuery, this.GetConnection());
            cmd.CommandType = cmdType;


            if (param1 != null)
                cmd.Parameters.Add(param1);
            if (param2 != null)
                cmd.Parameters.Add(param2);
            if (param3 != null)
                cmd.Parameters.Add(param3);
            if (param4 != null)
                cmd.Parameters.Add(param4);

            try
            {
                this.Open();
                return cmd.ExecuteReader();

            }
            finally
            {
                cmd.Dispose();
            }

        }
        public DataTable GetTable(string procnameOrQuery, CommandType cmdType, SqlParameter param1 = null, SqlParameter param2 = null, SqlParameter param3 = null, SqlParameter param4 = null)
        {

            //Load a DataTable form the database using either query text or a stored procedure name.
            DataTable table = new DataTable();
            SqlDataReader reader = null;

            try
            {
                reader = GetReader(procnameOrQuery, cmdType, param1, param2, param3, param4);
                table.Load(reader);
                reader.Close();
                return table;

            }
            finally
            {
                this.Close();
            }

       }
        public object GetScalar(string procnameOrQuery, CommandType cmdType, SqlParameter param1 = null, SqlParameter param2 = null, SqlParameter param3 = null, SqlParameter param4 = null)
        {

            SqlCommand cmd = new SqlCommand(procnameOrQuery, this.GetConnection());
            cmd.CommandType = cmdType;

            //If there are query parameters, add them to the command

            if (param1 != null)
                cmd.Parameters.Add(param1);
            if (param2 != null)
                cmd.Parameters.Add(param2);
            if (param3 != null)
                cmd.Parameters.Add(param3);
            if (param4 != null)
                cmd.Parameters.Add(param4);

            try
            {
                this.Open();
                return cmd.ExecuteScalar();

            }
            finally
            {
                this.Close();
                cmd.Dispose();
            }
        }

        public int ExecuteNonQuery(string procnameOrQuery, CommandType cmdType, SqlParameter param1 = null, SqlParameter param2 = null, SqlParameter param3 = null, SqlParameter param4 = null,
            SqlParameter param5 = null, SqlParameter param6 = null, SqlParameter param7 = null, SqlParameter param8 = null)
        {

            SqlCommand cmd = new SqlCommand(procnameOrQuery, this.GetConnection());
            cmd.CommandType = cmdType;

            //If there are query parameters, add them to the command

            if (param1 != null)
                cmd.Parameters.Add(param1);
            if (param2 != null)
                cmd.Parameters.Add(param2);
            if (param3 != null)
                cmd.Parameters.Add(param3);
            if (param4 != null)
                cmd.Parameters.Add(param4);
            if (param5 != null)
                cmd.Parameters.Add(param5);
            if (param6 != null)
                cmd.Parameters.Add(param6);
            if (param7 != null)
                cmd.Parameters.Add(param7);
            if (param8 != null)
                cmd.Parameters.Add(param8);

            try
            {
                this.Open();
                return cmd.ExecuteNonQuery();

            }
            finally
            {
                this.Close();
                cmd.Dispose();
            }
        }
        public DataSet GetDataSet(string sql)
        {
            DataSet aDataset = new DataSet();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, this.GetConnection());
                this.Open();
                adapter.Fill(aDataset);
                return aDataset;
            }
            finally
            {
                this.Close();
            }
        }
    }
}
