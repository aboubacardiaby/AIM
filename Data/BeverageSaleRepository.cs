using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class BeverageSaleRepository : IBeverageSaleRepository
    {
        private CoreDBEntities _coreDBEntities = new CoreDBEntities();
        public void AddBeverageSale(tblBeverageSale beverageSale)
        {
            try
            {
                
              

                string canEnd = beverageSale.CAN_END;
                if (!string.IsNullOrEmpty(canEnd) && canEnd.ToLower() == "end")
                {
                    string canend = canEnd.ToLower();
                    beverageSale.MaterialId = _coreDBEntities.tblMaterial_Handling.
                        Where(v => v.Material_Desc.ToLower().Contains("end")).
                        Select(v => v.MaterialId).FirstOrDefault();
                    var itemdesc = _coreDBEntities.tblProduct_Reference.Where(v => v.Item_Desc.Contains("End")).FirstOrDefault();
                    beverageSale.Item_Code = itemdesc.Item_Code;

                }
                else
                {
                    beverageSale.MaterialId = 1;
                    var itemdesc = _coreDBEntities.tblProduct_Reference.Where(v => v.Item_Desc.Contains("Pallet  Plastic Can")).FirstOrDefault();
                    beverageSale.Item_Code = itemdesc.Item_Code;

                }


                _coreDBEntities.tblBeverageSales.Add(beverageSale);
                _coreDBEntities.SaveChanges();
            }
            catch (Exception exec)
            {


            }
        }

        public void DeleteBeverage(int beverageId)
        {
            string sqlQuery = @"DELETE FROM [dbo].[tblBeverageSale]
                                WHERE [BeverageSaleId] =" + beverageId;
            int noOfRowUpdated = _coreDBEntities.Database.ExecuteSqlCommand(sqlQuery);

        }
        public List<tblMaterial_Handling> GetMaterial()
        {
            return _coreDBEntities.tblMaterial_Handling.ToList();
        }
        public List<tblBeverageSale> LoadData(string name, int startIndex, int count, string sorting)
        {
            IEnumerable<tblBeverageSale> query = _coreDBEntities.tblBeverageSales.ToList();

            if (string.IsNullOrEmpty(sorting) || sorting.Equals("Name ASC"))
            {
                query = query.OrderBy(p => p.Item_Code);
            }
            else if (sorting.Equals("Name DESC"))
            {
                query = query.OrderByDescending(p => p.Item_Code);
            }
            //else if (sorting.Equals("CustomerCode ASC"))
            //{
            //    query = query.OrderBy(p => p.CustomerCode);
            //}
            //else if (sorting.Equals("CustomerCode DESC"))
            //{
            //    query = query.OrderByDescending(p => p.CustomerCode);
            //}

            else
            {
                query = query.OrderBy(p => p.Item_Code); //Default!
            }

            return count > 0
                       ? query.Skip(startIndex).Take(count).ToList() //Paging
                       : query.ToList(); //No paging
        }

        public int BeveragedataCount()
        {
            return _coreDBEntities.tblBeverageSales.Count();
        }
        public List<tblBeverageSale> GetAllSaleData(string name)
        {
            return _coreDBEntities.tblBeverageSales.Where(n => n.Item_Code.Contains(name)).ToList();
        }

    }
}
