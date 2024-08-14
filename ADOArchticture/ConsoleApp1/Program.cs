using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var Configration = new ConfigurationBuilder().AddJsonFile("json.json").Build();
            SqlConnection connection = new SqlConnection(Configration.GetSection("connection").Value);
            var sql = "DELETE FROM Wallets " +
                 $"WHERE Id = @Id";

            SqlParameter IdParameter = new SqlParameter
            {
                ParameterName = "@Id",
                SqlDbType = System.Data.SqlDbType.Int,
                Direction = System.Data.ParameterDirection.Input,
                Value = 1,
            };

            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.Add(IdParameter);
            command.CommandType = System.Data.CommandType.Text;
            connection.Open();
            if (command.ExecuteNonQuery() > 0)
            {
                Console.WriteLine("the frist row deleted sucessfully");
            }

            connection.Close();

            Console.ReadKey();
        }
    }
}
