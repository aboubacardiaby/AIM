using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
   public interface IProductRepository
   {
        List<tblProduct> GetProduct(int startIndex, int count, string sorting);
        int GetProductCount();
        tblProduct UpdateProduct(tblProduct product);
        void DeleteProduct(int prodid);
        tblProduct AddProduct(tblProduct product);
        List<tblProduct> GetProductList();
   }
}
