using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_acess.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int SellerId { get; set; }
        public int Summa { get; set; }
        public DateTime SaleDate { get; set; }
        public Sale() { }
        public Sale(int customerId, int sellerId, int suma, DateTime saleData)
        {
            CustomerId = customerId;
            SellerId = sellerId;
            Summa = suma;
            SaleDate = saleData;
        }

    }
}

