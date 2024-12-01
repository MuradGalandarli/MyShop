using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Enums
{
    public class OrderEnum
    {
        public enum OrderStatus
        {
            InStock = 1,      // Ürün stokta mevcut
            OutOfStock = 2,       // Ürün stokta yok
            AddedToCart = 3,      // Ürün sepete eklendi
            Discontinued = 4,     // Ürün artık satışta değil
            Delete = 5,           // Sifaris silindi
            WasSold = 6,          // Satildi
            Cancellation = 7,    // legv edildi
            NotAvailable = 8,     // Mövcud deyil
            BuyError = 9,      // satın alma xətası
            Error = 10,        // Gozlenilmeyen xeta
            NotAddedToCart = 11  //  Sebete elave edilmeyib
        }

    }
}
