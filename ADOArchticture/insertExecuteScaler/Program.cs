using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace insertExecuteScaler
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var configration = new ConfigurationBuilder().AddJsonFile("jsconfig1.json").Build();
            var wallet = new Wallet
            {
                Holder = "omar",
                Balance = 6500,
            };

            SqlConnection connection = new SqlConnection(configration.GetSection("connection").Value);
            var sql = "INSERT INTO Wallets(Holder,Balance) VALUES (@Holder,@Balance);" +
                "SELECT CAST(scope_identity() as int);";

            SqlParameter HolderParameter = new SqlParameter
            {
                ParameterName = "@Holder",
                Direction = System.Data.ParameterDirection.Input,
                SqlDbType = System.Data.SqlDbType.Text,
                Value =  wallet.Holder,

            };
            SqlParameter BalanceParameter = new SqlParameter
            {
                ParameterName = "@Balance",
                Direction = System.Data.ParameterDirection.Input,
                SqlDbType = System.Data.SqlDbType.Decimal,
                Value = wallet.Balance,
            };

            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.Add(HolderParameter);
            command.Parameters.Add(BalanceParameter);
            command.CommandType = System.Data.CommandType.Text;
            
            connection.Open();
            wallet.Id = (int)command.ExecuteScalar();
            Console.WriteLine($"wallet {wallet.Holder} added successully");

            connection.Close();
 
            Console.ReadKey();

        }
    }
}
