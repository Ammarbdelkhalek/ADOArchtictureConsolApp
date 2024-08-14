using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace ADOArchticture
{
    public class Program
    {
        static void Main(string[] args)
        { 
            var Configration = new ConfigurationBuilder()
                .AddJsonFile("jsconfig1.json")
                .Build();

            SqlConnection connection = new SqlConnection(Configration.GetSection("connection").Value);
            var sql = "SELECT * FROM WALLETS";
            SqlCommand command = new SqlCommand(sql,connection);
            command.CommandType = CommandType.Text;
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            Wallet wallet;
            while (reader.Read())
            {
                wallet = new Wallet
                {
                   Id = reader.GetInt32(reader.GetOrdinal("Id")),
                  Holder = reader.GetString(reader.GetOrdinal("Holder")),
                  Balance = reader.GetDecimal(reader.GetOrdinal("Balance")),

                };
                Console.WriteLine(wallet);
            } 
            connection.Close();
            

            Console.ReadKey();

        }
    }
}

