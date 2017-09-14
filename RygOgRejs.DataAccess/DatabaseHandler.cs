using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RygOgRejs.Entities;
using System.Data;
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

        public DataView GetAllFromTable(string table)
        {
            string query = $"SELECT * FROM {table};";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    dt.Load(reader);
                }
            }
            catch (ObjectDisposedException) { throw; }
            catch (InvalidOperationException) { throw; }
            catch (InvalidCastException) { throw; }
            catch (SqlException) { throw; }
            catch (System.IO.IOException) { throw; }

            return dt.AsDataView();
        }

        public void CascadeInsert(Journey journey, Payer payer, Transaction transaction)
        {
            string strDepartureDate = journey.DepartureDate.ToString("yyyyMMdd");
            int bitFirstClass = 0;
            if (journey.IsFirstClass) { bitFirstClass = 1; }
            string amount = transaction.Amount + "";
            amount = amount.Replace(',', '.');
            string strTimeStamp = transaction.TimeStamp.ToString("yyyyMMdd");

            string query = $"EXEC dbo.CascadeInsert '{journey.Destination}', '{strDepartureDate}', {journey.Adults}, {journey.Children}, {bitFirstClass}, {journey.LuggageAmount}, '{payer.FirstName}', '{payer.LastName}', '{payer.Ssn}', {amount}, '{strTimeStamp}'";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();

                    command.Prepare();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public decimal[] GetTodayTotals()
        {
            decimal journeysSold = 0;
            decimal firstClasses = 0;
            decimal standard = 0;
            decimal travelers = 0;
            decimal adults = 0;
            decimal children = 0;
            decimal sales = 0;

            string query = "SELECT IsFirstClass, Adults, Children, Amount FROM Transactions " +
                "JOIN Journeys ON Transactions.JourneyId = Journeys.JourneyId " +
                "WHERE YEAR(TimeStamps) = YEAR(CURRENT_TIMESTAMP) " +
                "AND MONTH(TimeStamps) = MONTH(CURRENT_TIMESTAMP) " +
                "AND DAY(TimeStamps) = DAY(CURRENT_TIMESTAMP)";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();

                    command.Prepare();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        if (reader.GetFieldValue<bool>(0))
                            firstClasses++;
                        else
                            standard++;

                        adults += reader.GetFieldValue<int>(1);
                        children += reader.GetFieldValue<int>(2);

                        sales += reader.GetFieldValue<decimal>(3);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            journeysSold = firstClasses + standard;
            travelers = adults + children;

            return new decimal[7] { journeysSold, firstClasses, standard, travelers, adults, children, sales };
        }
    }
}
