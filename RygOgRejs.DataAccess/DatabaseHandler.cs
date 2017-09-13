using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RygOgRejs.Entities;
using System.Data.SqlClient;

namespace RygOgRejs.DataAccess
{
    public class DatabaseHandler
    {
        private readonly string connectionString;

        public DatabaseHandler(string connectionString)
        {
            if (String.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException(nameof(connectionString));

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                }
            }
            catch (ArgumentException) { throw; }
            catch (InvalidOperationException) { throw; }
            catch (SqlException) { throw; }

            this.connectionString = connectionString;
        }


    }
}
