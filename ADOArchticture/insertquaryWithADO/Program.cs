using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace insertquaryWithADO
{
    public class Program
    {
        static void Main(string[] args)
        {
            var Configration = new ConfigurationBuilder().AddJsonFile("json.json").Build();
            var walletTOInsert = new Wallet
            {
                Holder = "salah",
                Balance = 4500,
            };
            SqlConnection connection = new SqlConnection(Configration.GetSection("connection").Value);

            var sql = "INSERT INTO WALLETS (Holder, Balance) VALUES " +
                 $"(@Holder, @Balance)";

            SqlParameter HolderParameter = new SqlParameter
            {
                ParameterName = "@Holder",
                SqlDbType = System.Data.SqlDbType.Text,
                Direction = System.Data.ParameterDirection.Input,
                Value = walletTOInsert.Holder,
            };
            SqlParameter BalanceParameter = new SqlParameter
            {
                ParameterName = "@Balance",
                SqlDbType= System.Data.SqlDbType.Decimal,
                Direction = System.Data.ParameterDirection.Input,
                Value = walletTOInsert.Balance,
            };

            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.Add(HolderParameter);
            command.Parameters.Add(BalanceParameter);   

            command.CommandType = System.Data.CommandType.Text;

            connection.Open();
            if(command.ExecuteNonQuery()>0 ) 
            {
               // walletTOInsert.Id = (int)command.ExecuteScalar();
                Console.WriteLine($"wallet {walletTOInsert} added successully");
            }
             
            connection.Close();

            Console.ReadKey();
        }
    }
}
