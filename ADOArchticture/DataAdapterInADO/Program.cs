using insertUsingStoreProcedure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAdapterInADO
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var Configration = new ConfigurationBuilder()
                .AddJsonFile("json.json")
                .Build();

            SqlConnection connection = new SqlConnection(Configration.GetSection("connection").Value);
            var sql = "SELECT * FROM WALLETS";
            connection.Open();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, connection);
            DataTable d1 = new DataTable();
            dataAdapter.Fill(d1);
            connection.Close();
            foreach(DataRow dr in d1.Rows)
            {
                var wallet = new Wallet
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    Holder = Convert.ToString(dr["Holder"]),
                    Balance = Convert.ToDecimal(dr["Balance"])
                };
                Console.WriteLine(wallet);
            }

            Console.ReadKey(); 

        }
    }
}
