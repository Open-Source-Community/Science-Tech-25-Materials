using System.Data.SqlClient;
using System.Data;
namespace ADO.NET.Example
{
    internal class Program
    {
        static void Main(string[] args)
        {

            List<int> nums = new List<int>() {1,2,3,4,5 };

            var list = from a in nums select a;


            // select a from nums


                       #region Create new Connection

            string connectionString = "Data Source=HADEER;" +
                                      "Initial Catalog=CourseStudentDB;" +
                                      "Integrated Security = SSPI; " +
                                      "TrustServerCertificate = True";
            
            SqlConnection connection = new SqlConnection(connectionString);

            #endregion

            #region Sql command

            string query = "SELECT Id, FirstName, LastName FROM Students";

            SqlCommand command = new SqlCommand(query, connection);

            #endregion

            #region Execute sql command

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine($"ID: {reader.GetInt32("Id")}, Name: {reader.GetString("FirstName")} {reader.GetString("LastName")}");
            }

            connection.Close(); 
            #endregion

        }
    }


}
