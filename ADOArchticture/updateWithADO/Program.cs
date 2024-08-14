using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace updateWithADO
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var Configration = new ConfigurationBuilder().AddJsonFile("json.json").Build();
            SqlConnection connection = new SqlConnection(Configration.GetSection("connection").Value);
            var sql = "UPDATE Wallets SET Holder = @Holder, Balance = @Balance " +
                $"WHERE Id = @Id";
            SqlParameter HolderParameter = new SqlParameter
            {
                ParameterName = "@Holder",
                SqlDbType = System.Data.SqlDbType.Text,
                Direction = System.Data.ParameterDirection.Input,
                Value = "mohamed mostafa",
            };
            SqlParameter BalanceParameter = new SqlParameter
            {
                ParameterName = "@Balance",
                SqlDbType = System.Data.SqlDbType.Decimal,
                Direction = System.Data.ParameterDirection.Input,
                Value = 12.0,
            };
            SqlParameter IdParameter = new SqlParameter
            {
                ParameterName = "@Id",
                SqlDbType = System.Data.SqlDbType.Int,
                Direction = System.Data.ParameterDirection.Input,
                Value = 1,
            };

            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.Add(HolderParameter);
            command.Parameters.Add(BalanceParameter);
            command.Parameters.Add(IdParameter);
            command.CommandType = System.Data.CommandType.Text;
            connection.Open();
            if(command.ExecuteNonQuery() >0) 
            {
                Console.WriteLine("the frist row updated sucessfully");
            }
            
            connection.Close();

            Console.ReadKey();

        }
    }
}
