using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace insertUsingStoreProcedure
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var wallet = new Wallet { 
                Holder = "Kareem",
                Balance = 90000,
            };
            var configuration = new ConfigurationBuilder().AddJsonFile("json.json").Build();
            SqlConnection connection = new SqlConnection(configuration.GetSection("connection").Value);
            //var sql = "INSERT INTO Wallets(Holder,Balance) VALUES (@Holder,@Balance);" +
              //  "SELECT CAST(scope_identity() as int);";

            SqlParameter HolderParameter = new SqlParameter
            {
                ParameterName ="@Holder",
                SqlDbType = System.Data.SqlDbType.Text,
                Value = wallet.Holder,
                Direction = System.Data.ParameterDirection.Input,

            };
            SqlParameter BalanceParameter = new SqlParameter
            {
                ParameterName = "@Balance",
                SqlDbType = System.Data.SqlDbType.Decimal,
                Value = wallet.Balance,
                Direction = System.Data.ParameterDirection.Input,

            };

            SqlCommand command = new SqlCommand("AddWallet", connection);
            command.Parameters.Add(HolderParameter);
            command.Parameters.Add(BalanceParameter);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            connection.Open();
            if (command.ExecuteNonQuery() > 0)//return a number of rows affected;
            {
                Console.WriteLine($"wallet for {wallet.Holder} added successully");
            }
            else
            {
                Console.WriteLine($"ERROR: wallet for {wallet .Holder} was not added");
            }

            connection.Close();
            Console. ReadKey ();

        }
    }
}
