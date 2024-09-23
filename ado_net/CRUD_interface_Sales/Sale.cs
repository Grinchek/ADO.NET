using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _CRUD_interface_Sales
{
    public class Sale
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int SellerId { get; set; }
        public int Summa { get; set; }
        public DateTime SaleDate { get; set; }
        public Sale() { }
        public Sale(int customerId,int sellerId,int suma, DateTime saleData)
        {
           this.CustomerId = customerId;
           this.SellerId = sellerId;
           this.Summa = suma;
           this.SaleDate = saleData;
        }

    }
}

