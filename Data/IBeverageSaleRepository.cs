using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public  interface IBeverageSaleRepository
    {
        void AddBeverageSale(tblBeverageSale beverageSale);

        List<tblBeverageSale> LoadData(string name, int startIndex, int count, string sorting);
        List<tblMaterial_Handling> GetMaterial();
        void DeleteBeverage(int beverageId);
        int BeveragedataCount();
        List<tblBeverageSale> GetAllSaleData(string name);
    }
}
