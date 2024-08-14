using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionInto_ADO
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var Configration = new ConfigurationBuilder().AddJsonFile("json.json").Build();
            SqlConnection connection = new SqlConnection(Configration.GetSection("connection").Value);
            SqlCommand Command = connection.CreateCommand();
            Command.CommandType = System.Data.CommandType.Text;

            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();
            Command.Transaction = transaction;
            try
            {
                Command.CommandText = "UPDATE Wallets Set Balance = Balance - 1000 Where Id = 2";
                Command.ExecuteNonQuery();

                Command.CommandText = "UPDATE Wallets Set Balance = Balance + 1000 Where Id = 3";
                Command.ExecuteNonQuery();
                transaction.Commit();
                Console.WriteLine("transaction completed");
            }
            catch  
            {
                try
                {
                    transaction.Rollback();

                }
                catch 
                {
                }
               

            }
            finally 
            {
               connection.Close();
            }
        }
    }
}
