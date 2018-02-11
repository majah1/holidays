using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace WebApplication2.Models
{
    public class DbBroker
    {
        SqlConnection conn;

        public DbBroker()
        {
            conn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = 'C:\Users\Maja\Documents\Visual Studio 2015\Projects\WebApplication2\WebApplication2\App_Data\Holidays.mdf'; Integrated Security = True");
               
}

        public int Insert(string Name, DateTime StartsAt, DateTime EndsAt, bool OccurusAnually)
        {

            conn.Open();
            SqlCommand sqlCommand = conn.CreateCommand();
            sqlCommand.CommandType = System.Data.CommandType.Text;
            Debug.WriteLine(StartsAt);
            sqlCommand.CommandText = "INSERT into Holidays (Name, StartsAt, EndsAt, OccursAnnualy) VALUES ( @Name, @StartsAt, @EndsAt, @OccursAnnualy)";
            sqlCommand.Parameters.AddWithValue("@Name", Name); 
            sqlCommand.Parameters.AddWithValue("@StartsAt", StartsAt);
            sqlCommand.Parameters.AddWithValue("@EndsAt", EndsAt);
            sqlCommand.Parameters.AddWithValue("@OccursAnnualy", Convert.ToInt32(OccurusAnually));

            try
            {
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
            }
            conn.Close();
            return 1;

        }


        public int Update(string Name, DateTime StartsAt, DateTime EndsAt, bool OccurusAnually, int Id)
        {

            conn.Open();
            SqlCommand sqlCommand = conn.CreateCommand();
            sqlCommand.CommandType = System.Data.CommandType.Text;
            Debug.WriteLine(StartsAt);
            sqlCommand.CommandText = "UPDATE Holidays SET Name=@Name, StartsAt=@StartsAt, EndsAt=@EndsAt, OccursAnnualy=@OccursAnnualy WHERE id=@id";
            sqlCommand.Parameters.AddWithValue("@Name", Name);
            sqlCommand.Parameters.AddWithValue("@StartsAt", StartsAt);
            sqlCommand.Parameters.AddWithValue("@EndsAt", EndsAt);
            sqlCommand.Parameters.AddWithValue("@OccursAnnualy", Convert.ToInt32(OccurusAnually));
            sqlCommand.Parameters.AddWithValue("@id", Id);

            try
            {
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
            }
            conn.Close();
            return 1;

        }


        public List<Holiday> SelectAll()
        {

            conn.Open();
            SqlCommand sqlCommand = conn.CreateCommand();
            sqlCommand.CommandType = System.Data.CommandType.Text;

            SqlDataReader reader;
            sqlCommand.CommandText = "SELECT Name, OccursAnnualy, StartsAt, EndsAt,Id  FROM Holidays";
            reader = sqlCommand.ExecuteReader();

            List<Holiday> newHoliday = new List<Holiday>();

            while (reader.Read())
            {
                int occursAnually = (int)(byte) reader.GetByte(1);
                bool annual = (occursAnually == 1) ? true : false;
                Holiday createNew = new Holiday(reader.GetString(0),annual, reader.GetDateTime(2), reader.GetDateTime(3),5,reader.GetInt32(4));
                newHoliday.Add(createNew);
            }

            conn.Close();

            return newHoliday;

        }

        public Holiday SelectOne(int Id)
        {
            conn.Open();
            SqlCommand sqlCommand = conn.CreateCommand();
            sqlCommand.CommandType = System.Data.CommandType.Text;

            SqlDataReader reader;
            sqlCommand.CommandText = "SELECT Name, OccursAnnualy, StartsAt, EndsAt,Id  FROM Holidays WHERE id=@id ";
            sqlCommand.Parameters.AddWithValue("@id", Id);
            reader = sqlCommand.ExecuteReader();

            Holiday selected = new Holiday("",false,DateTime.Now,DateTime.Now,0,0);

            while (reader.Read())
            {
                int occursAnually = (int)(byte)reader.GetByte(1);
                bool annual = (occursAnually == 1) ? true : false;
                selected = new Holiday(reader.GetString(0), annual, reader.GetDateTime(2), reader.GetDateTime(3), 5, reader.GetInt32(4));
            }

            conn.Close();

            return selected;


        }

        public int Delete(int Id) {
            conn.Open();
            SqlCommand sqlCommand = conn.CreateCommand();
            sqlCommand.CommandType = System.Data.CommandType.Text;
            sqlCommand.CommandText = "DELETE FROM Holidays WHERE id=@id";
            sqlCommand.Parameters.AddWithValue("@id", Id);
            sqlCommand.ExecuteNonQuery();
            conn.Close();
            return 1;
        }
    }
}