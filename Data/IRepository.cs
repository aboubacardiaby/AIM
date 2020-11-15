using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
   
    public interface IRepository
    {
        List<tblCustomerReference> LoadCustomerReferencedata(int startIndex, int count, string sorting);
        int LoadCustomerReferencedataCount();
        tblCustomerReference AddCustomerReferenceCode(tblCustomerReference customerReference);
        List<tblCustomerReference> LoadFilteredCustomerbyName(string name, int startIndex, int count, string sorting);
        void UpdateCustomerCode(tblCustomerReference customerReference);
        List<tblCustomerReference> DeleteCustomerCode(long ReferenceId);
        List<tblAddress> GetAddressbyAddresid(long addresseid);
        TblCountry GetCountrybyCountryId(long countryId);

        List<TblCountry> GetCountries();
        List<AddressInfo> GetAddressbyCustomerCode(string customercode);
        List<tblContact> GetCustomerContact(string customerCode);
        bool IsCustomerExist(string customerCode);


    }
}
