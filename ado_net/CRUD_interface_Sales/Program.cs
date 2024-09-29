using Microsoft.Win32.SafeHandles;
using data_acess;
using System.Diagnostics;
namespace _CRUD_interface_Sales
{
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
