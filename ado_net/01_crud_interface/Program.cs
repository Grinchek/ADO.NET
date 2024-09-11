using System.Data.SqlClient;
using System.Diagnostics;

namespace _01_crud_interface
{
    public class SportShopDb
    {
        private SqlConnection conn;
        private string connectionString = @"workstation id=Shomiakom2.mssql.somee.com;packet size=4096;
                                        user id=Grincchik_SQLLogin_1;
                                        pwd=gstgjnpeqp;
                                        data source=Shomiakom2.mssql.somee.com;persist security info=False;
                                        initial catalog=Shomiakom2;
                                        TrustServerCertificate=True";
        public SportShopDb()
        {
            conn = new SqlConnection(connectionString);
            conn.Open();
        }
        ~SportShopDb()
        {
            conn.Close();
        }
        public void Create(Product product)
        {
            string cmdText = $@"INSERT INTO Products
                              VALUES ('{product.Name}', 
                                      '{product.Type}', 
                                       {product.Quantity}, 
                                       {product.Price}, 
                                      '{product.Producer}', 
                                       {product.CostPrice})";

            SqlCommand command = new SqlCommand(cmdText, conn);
            command.CommandTimeout = 5; // default - 30sec

            //// ExecuteNonQuery - виконує команду яка не повертає результат 
            ///(insert, update, delete...),
            ////але метод повертає кількітсь рядків, які були задіяні
            int rows = command.ExecuteNonQuery();

            Console.WriteLine(rows + " rows affected!");
        }
        public void CreateSale(Sales sales)
        {
            string cmdText = $@"INSERT INTO Sales
                              VALUES ({sales.ProductId}, 
                                      {sales.Price}, 
                                      {sales.Quantity}, 
                                      {sales.EmployeeId}, 
                                      {sales.ClientId}
                                      {sales.SaleDate}";

            SqlCommand command = new SqlCommand(cmdText, conn);
            command.CommandTimeout = 5; // default - 30sec

            //// ExecuteNonQuery - виконує команду яка не повертає результат 
            ///(insert, update, delete...),
            ////але метод повертає кількітсь рядків, які були задіяні
            int rows = command.ExecuteNonQuery();

            Console.WriteLine(rows + " rows affected!");
        }
        public List<Product> GetAll()
        {
            string cmdText = @"select * from Products";

            SqlCommand command = new SqlCommand(cmdText, conn);

            SqlDataReader reader = command.ExecuteReader();

            List<Product> products = new List<Product>();

            ////// відображаємо всі значення кожного рядка
            while (reader.Read())
            {
                products.Add(new Product()
                {
                    Id = (int)reader[0],
                    Name = (string)reader[1],
                    Type = (string)reader[2],
                    Quantity = (int)reader[3],
                    CostPrice = (int)reader[4],
                    Producer = (string)reader[5],
                    Price = (int)reader[6]
                });
            }

            reader.Close();
            return products;

        }
        public List<Sales> GetAllSales()
        {
            string cmdText = @"select * from Sales";

            SqlCommand command = new SqlCommand(cmdText, conn);

            SqlDataReader reader = command.ExecuteReader();

            List<Sales> sales = new List<Sales>();

            ////// відображаємо всі значення кожного рядка
            while (reader.Read())
            {
                sales.Add(new Sales()
                {
                    Id = (int)reader[0],
                    ProductId = (int)reader[1],
                    Price = (int)reader[2],
                    Quantity = (int)reader[3],
                    EmployeeId = (int)reader[4],
                    ClientId = (int)reader[5],

                });
            }

            reader.Close();
            return sales;
        }
        public List<Sales> GetSalesByPeriod(DateTime startDate,DateTime endDate)
        {
            string cmdText = @"select * from Sales";

            SqlCommand command = new SqlCommand(cmdText, conn);

            SqlDataReader reader = command.ExecuteReader();

            List<Sales> sales = new List<Sales>();

            ////// відображаємо всі значення кожного рядка
            while (reader.Read())
            {
                sales.Add(new Sales()
                {
                    Id = (int)reader[0],
                    ProductId = (int)reader[1],
                    Price = (int)reader[2],
                    Quantity = (int)reader[3],
                    EmployeeId = (int)reader[4],
                    ClientId = (int)reader[5],

                });
            }

            reader.Close();
            return sales;
        }
        public Product GetById(int id)
        {
            #region Execute Reader
            string cmdText = $@"select * from Products where Id = {id}";

            SqlCommand command = new SqlCommand(cmdText, conn);

            SqlDataReader reader = command.ExecuteReader();

            Product product = new Product();

            while (reader.Read())
            {

                product.Id = (int)reader[0];
                product.Name = (string)reader[1];
                product.Type = (string)reader[2];
                product.Quantity = (int)reader[3];
                product.CostPrice = (int)reader[4];
                product.Producer = (string)reader[5];
                product.Price = (int)reader[6];

            }
            reader.Close();
            return product;
            #endregion

        }

        public void Update(Product product)
        {
            string cmdText = $@"UPDATE Products
                              SET Name ='{product.Name}', 
                                  TypeProduct ='{product.Type}', 
                                  Quantity ={product.Quantity}, 
                                  CostPrice ={product.CostPrice}, 
                                  Producer ='{product.Producer}', 
                                  Price ={product.Price}
                                  where Id = {product.Id}";

            SqlCommand command = new SqlCommand(cmdText, conn);
            command.CommandTimeout = 5; // default - 30sec

            command.ExecuteNonQuery();

        }
        public void Delete(int id)
        {
            string cmdText = $@"delete Products where Id = {id}";
            SqlCommand command = new SqlCommand(cmdText, conn);
            command.ExecuteNonQuery();
        }

    }
    internal class Program
    {
        static void Main(string[] args)
        {

            SportShopDb db = new SportShopDb();

            Product pr = new Product()
            {
                Name = "Stanga",
                Type = "Equipment",
                Quantity = 33,
                Price = 3333,
                Producer = "China",
                CostPrice = 4444
            };
            // db.Create(pr);
            var products = db.GetAll();
            foreach (var product in products)
            {
                Console.WriteLine(product);
            }
            db.Delete(39);

            Product forUpdate = db.GetById(1);
            forUpdate.Price = 2000;
            forUpdate.CostPrice = 5000;

            db.Update(forUpdate);





        }
    }
}