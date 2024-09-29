using Microsoft.Win32.SafeHandles;
using System.Data.SqlClient;
using System.Diagnostics;
namespace _CRUD_interface_Sales
{
    public class Sales
    {
        private SqlConnection conn;
        private string connectionString = @"Data Source=DESKTOP-A1447MR\SQLEXPRESS;Initial Catalog=Sales;Integrated Security=True";
        public Sales()
        {
            conn = new SqlConnection(connectionString);
            conn.Open();
            Console.WriteLine("Connected");
        }
        ~Sales()
        {
            Console.WriteLine("Disconnected");
            conn.Close();
        }
        private int FindIdBySellerName(string fullName)
        {
            string employeeCmdText = @"select * from Sellers";
            SqlDataReader employeeReader = null;
            SqlCommand employeeCommand = new SqlCommand(employeeCmdText, conn);
            int wantedId = 0;
            employeeReader = employeeCommand.ExecuteReader();

            if (employeeReader.HasRows == true)
            {
                while (employeeReader.Read())
                {
                    if (employeeReader["FullName"].ToString() == fullName)
                    {
                        wantedId = int.Parse(employeeReader["Id"].ToString());
                    }

                }
            }
            employeeReader.Close();
            return wantedId;
        }
  
        public List<Sale> GetAllSales()
        {
            string cmdText = @"select * from Sales";

            SqlCommand command = new SqlCommand(cmdText, conn);

            SqlDataReader reader = command.ExecuteReader();

            List<Sale> sales = new List<Sale>();

            ////// відображаємо всі значення кожного рядка
            while (reader.Read())
            {
                sales.Add(new Sale()
                {
                    Id = (int)reader[0],
                    CustomerId = (int)reader[1],
                    SellerId = (int)reader[2],
                    Summa = (int)reader[3],
                    SaleDate = (DateTime)reader[4]

                });
            }

            reader.Close();
            return sales;
        }
        public void CreateSale(Sale sale)
        {
            string cmdText = $@"INSERT INTO Sales
                              VALUES (@CustomerId, 
                                      @SellerId, 
                                      @Summa, 
                                      @SalesDate)";

            SqlCommand command = new SqlCommand(cmdText, conn);
            command.Parameters.AddWithValue("CustomerId", sale.CustomerId);
            command.Parameters.AddWithValue("SellerId", sale.SellerId);
            command.Parameters.AddWithValue("Summa", sale.Summa);
            command.Parameters.AddWithValue("SalesDate", sale.SaleDate);

            command.CommandTimeout = 5; 

            int rows = command.ExecuteNonQuery();

            Console.WriteLine(rows + " rows affected!");
        }
        public void ShowSellByPeriod(DateTime StartDate, DateTime EndDate)
        {

            string cmdText = @"SELECT * FROM Sales WHERE SalesDate >= @StartDate AND SalesDate <= @EndDate";

            SqlDataReader salesReader = null;

            SqlCommand salesCommand = new SqlCommand(cmdText, conn);
            salesCommand.Parameters.AddWithValue("@StartDate", StartDate);
            salesCommand.Parameters.AddWithValue("@EndDate", EndDate);


            salesReader = salesCommand.ExecuteReader();
            if (salesReader.HasRows == true)
            {
                while (salesReader.Read())
                {
                    Console.WriteLine(string.Format("{0} - {1} - {2} - {3} - {4}",
                    salesReader["Id"].ToString(),
                    salesReader["CustomerId"].ToString(),
                    salesReader["SellerId"].ToString(),
                    salesReader["Summa"].ToString(),
                    salesReader["SalesDate"].ToString()));
                }

            }

            salesReader.Close();

        }
        public void ShowLastSaleBySellerName(string fullName)
        {

            List<Sale> salesList = GetAllSales();
            List<Sale> salesListBySellerName = new List<Sale>();
            foreach (Sale sales in salesList)
            {
                if (sales.SellerId == FindIdBySellerName(fullName))
                {
                    salesListBySellerName.Add(sales);
                }
            }
            DateTime maxDate = salesListBySellerName[0].SaleDate;
            foreach (Sale sales in salesListBySellerName)
            {
                if (sales.SaleDate > maxDate)
                {
                    maxDate = sales.SaleDate;
                }


            }

            foreach (Sale sales in salesListBySellerName)
            {
                if (sales.SaleDate == maxDate)
                {
                    Console.WriteLine($"{fullName} - {sales.SaleDate}");
                }
            }

        }
        public void DeleteSeller(int id)
        {
            string cmdText = $@"delete Sellers where Id = {id}";
            SqlCommand command = new SqlCommand(cmdText, conn);
            command.ExecuteNonQuery();
        }
        public void DeleteCustomer(int id)
        {
            string cmdText = $@"delete Customers where Id = {id}";
            SqlCommand command = new SqlCommand(cmdText, conn);
            command.ExecuteNonQuery();
        }
        public void ShowTop1Seller()
        {
            string query = @"
                SELECT TOP 1 Sellers.Id, Sellers.Fullname, SUM(Sales.Summa) AS TotalSales
                FROM Sales
                INNER JOIN Sellers ON Sales.SellerId = Sellers.Id
                GROUP BY Sellers.Id, Sellers.Fullname
                ORDER BY TotalSales desc ;";

            using (SqlCommand command = new SqlCommand(query, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int sellerId = reader.GetInt32(0);
                        string Fullname = reader.GetString(1);
                        decimal totalSales = reader.GetInt32(2);

                        Console.WriteLine($"The best seller: {Fullname}");
                        Console.WriteLine($"Total amount of sales: {totalSales}");
                    }
                    else
                    {
                        Console.WriteLine("Продавців не знайдено.");
                    }
                }
            }
        }





    }
    internal class Program
    {
        static void Main(string[] args)
        {
            //Sale saleForCreate = new Sale();
            //saleForCreate.CustomerId = 9;
            //saleForCreate.SellerId = 7;
            //saleForCreate.Summa = 2300;
            //saleForCreate.SaleDate = new DateTime(2024, 01, 12);
            //shop.CreateSale(saleForCreate);
            Sales shop = new Sales();
            shop.ShowTop1Seller();
            //DateTime StartDate = new DateTime(2022, 06, 26);
            //DateTime EndDate = new DateTime(2024, 01, 12);
            //shop.ShowSellByPeriod(StartDate, EndDate);
            //shop.ShowLastSaleBySellerName("Arabela Shilton");
            
          

        }
    }
}
