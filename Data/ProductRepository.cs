using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class ProductRepository : IProductRepository
    {
        private CoreDBEntities _coreDBEntities = new CoreDBEntities();
        public List<tblProduct> GetProduct(int startIndex, int count, string sorting)
        {
             
            IEnumerable<tblProduct> query = _coreDBEntities.tblProducts.ToList(); ;
            if (string.IsNullOrEmpty(sorting) || sorting.Equals("Description ASC"))
            {
                query = query.OrderBy(p => p.Description);
            }
            else if (sorting.Equals("Item_Group DESC"))
            {
                query = query.OrderByDescending(p => p.Item_Group);
            }
            else if (sorting.Equals("Item_Number ASC"))
            {
                query = query.OrderBy(p => p.Item_Number);
            }
            else if (sorting.Equals("Item_Number DESC"))
            {
                query = query.OrderByDescending(p => p.Item_Number);
            }

            else
            {
                query = query.OrderBy(p => p.Description); //Default!
            }

            return count > 0
                       ? query.Skip(startIndex).Take(count).ToList() //Paging
                       : query.ToList(); //No paging
        }
        public int GetProductCount()
        {
            return _coreDBEntities.tblProducts.Count();
        }
        public List<tblProduct> GetProductList()
        {
            return _coreDBEntities.tblProducts.ToList();
        }
        public tblProduct AddProduct(tblProduct product)
        {
            _coreDBEntities.tblProducts.Add(product);
            _coreDBEntities.SaveChanges();
            return product;
        }
        public tblProduct UpdateProduct(tblProduct product)
        {
            string sqlQuery = @" UPDATE [dbo].[tblProduct]
                           SET [Item_Number] = '" +product.Item_Number + @"'
                              ,[Item_Group] = '" + product.Item_Group + @"'
                              ,[Description] = '" + product.Description + @"'
                              ,[Customer_Group] = '" + product.Customer_Group + @"'
                              ,[Material_Type] = '" + product.Material_Type + @"'
                              ,[Colors] = '" + product.Colors + @"'
                              ,[Weight] = '" + product.Weight + @"'
                              ,[Dimension] = '" + product.Dimension + @"'
                              ,[Grouping] = '" +product.Grouping + 
                             @"' WHERE ProductId ='" + product.ProductId +@"'";
            int noOfRowUpdated = _coreDBEntities.Database.ExecuteSqlCommand(sqlQuery);
            if (noOfRowUpdated >0)
            {
                return product;
            }

            return null;
        }

        public void DeleteProduct(int prodid)
        {
            string sqlQuery = @"DELETE FROM[dbo].[tblProduct]" +
                 @" WHERE productId =" + prodid;
            int noOfRowUpdated = _coreDBEntities.Database.ExecuteSqlCommand(sqlQuery);
        }
    }
}
