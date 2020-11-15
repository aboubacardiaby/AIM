using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Repository: IRepository
    {
        private CoreDBEntities _coreDBEntities = new CoreDBEntities();
        #region Customer Code
        public tblCustomerReference AddCustomerReferenceCode(tblCustomerReference customerReference)
        {
            try
            {
               
                customerReference.CreatedBy = " Application";
                customerReference.CreateDate = DateTime.Now;
                _coreDBEntities.tblCustomerReferences.Add(customerReference);
                _coreDBEntities.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {


            }
            return customerReference;
        }
        public void UpdateCustomerCode(tblCustomerReference customerReference)
        {
            string sqlQuery = @"UPDATE [dbo].[tblCustomerReference]
                             SET [Name] ='" + customerReference.Name + @"',[CustomerCode] ='" + customerReference.CustomerCode +
                             @"' WHERE ReferenceId =" + customerReference.ReferenceId;
            int noOfRowUpdated = _coreDBEntities.Database.ExecuteSqlCommand(sqlQuery);
            
           
        }

        public List<tblCustomerReference> DeleteCustomerCode(long referenceId)
        {
            List<tblCustomerReference> list = new List<tblCustomerReference>();
            string sqlQuery = @"DELETE FROM [dbo].[tblCustomerReference]  " +
                             @" WHERE ReferenceId =" + referenceId;
            int noOfRowUpdated = _coreDBEntities.Database.ExecuteSqlCommand(sqlQuery);
            if (noOfRowUpdated > 0)
            {
                 list = _coreDBEntities.tblCustomerReferences.Where(b => b.ReferenceId == referenceId).ToList();
            }

            return list;
        }
        public List<tblCustomerReference> LoadCustomerReferencedata(int startIndex, int count, string sorting)
        {
            try
            {
              
              //  var res = _coreDBEntities.tblCustomerReferences.SelectMany(b=>b.tblAddress)
                IEnumerable<tblCustomerReference> query = _coreDBEntities.tblCustomerReferences.ToList();

                if (string.IsNullOrEmpty(sorting) || sorting.Equals("Name ASC"))
                {
                    query = query.OrderBy(p => p.Name);
                }
                else if (sorting.Equals("Name DESC"))
                {
                    query = query.OrderByDescending(p => p.Name);
                }
                else if (sorting.Equals("CustomerCode ASC"))
                {
                    query = query.OrderBy(p => p.CustomerCode);
                }
                else if (sorting.Equals("CustomerCode DESC"))
                {
                    query = query.OrderByDescending(p => p.CustomerCode);
                }

                else
                {
                    query = query.OrderBy(p => p.Name); //Default!
                }

                return count > 0
                           ? query.Skip(startIndex).Take(count).ToList() //Paging
                           : query.ToList(); //No paging
            }
            catch (Exception exc)
            {

                var qu = exc.InnerException + exc.StackTrace;
            }

            return null;
           
             
        }
        public int LoadCustomerReferencedataCount()
        {
           return _coreDBEntities.tblCustomerReferences.Count();
        }

        public List<tblCustomerReference> LoadFilteredCustomerbyName(string name,int startIndex, int count, string sorting)
        {
            IEnumerable<tblCustomerReference> query = _coreDBEntities.tblCustomerReferences.Where(n=>n.Name.Contains(name));
            if (string.IsNullOrEmpty(sorting) || sorting.Equals("Name ASC"))
            {
                query = query.OrderBy(p => p.Name);
            }
            else if (sorting.Equals("Name DESC"))
            {
                query = query.OrderByDescending(p => p.Name);
            }
            else if (sorting.Equals("CustomerCode ASC"))
            {
                query = query.OrderBy(p => p.CustomerCode);
            }
            else if (sorting.Equals("CustomerCode DESC"))
            {
                query = query.OrderByDescending(p => p.CustomerCode);
            }

            else
            {
                query = query.OrderBy(p => p.Name); //Default!
            }

            return count > 0
                       ? query.Skip(startIndex).Take(count).ToList() //Paging
                       : query.ToList(); //No paging
        }
        #endregion  
        
        #region Address
        public List<tblAddress>GetAddressbyAddresid(long addresseid)
        {
            return _coreDBEntities.tblAddresses.Where(u => u.AddressId == addresseid).ToList();
        }

        public List<AddressInfo> GetAddressbyCustomerCode(string customercode)
        {
            List<AddressInfo> infos = new List<AddressInfo>();
            var query = from cust in _coreDBEntities.tblCustomerReferences
                        join ad in _coreDBEntities.tblAddresses on cust.CustomerCode equals ad.CustomerCode
                        where cust.CustomerCode == customercode
                        select new
                        {
                            cust.CustomerCode,
                            ad.AddressType,
                            ad.Address1,
                            ad.Address2,
                            ad.City,
                            ad.CountryId,
                            ad.Province,
                            ad.PostalCode,
                            ad.AddressId,
                           
                          
                        };

            foreach (var item in query)
            {
                infos.Add(new AddressInfo()
                {
                    Address1 = item.Address1,
                    Address2 = item.Address2,
                    AddressType = item.AddressType,
                    City = item.City,
                    CountryId = item.CountryId.ToString(),
                    CustomerCode = item.CustomerCode,
                    PostalCode = item.PostalCode,
                    Province = item.Province,
                    AddressId =item.AddressId
                    
                });
            }
            return infos.ToList();
          
        }
        #endregion

        #region Country
        public TblCountry GetCountrybyCountryId(long countryId)
        {
            return _coreDBEntities.TblCountries.Where(v => v.CountryId == countryId).FirstOrDefault();
        }

        public  List<TblCountry> GetCountries()
        {
            return _coreDBEntities.TblCountries.ToList();
        }
        #endregion

        #region Contact
        public List<tblContact> GetCustomerContact(string cusomercode)
        {
            return _coreDBEntities.tblContacts.Where(v=>v.CustomerCode == cusomercode).ToList();
        }

        public bool IsCustomerExist(string customerCode)
        {
            var query = _coreDBEntities.tblCustomerReferences
                        .Where(v => v.CustomerCode == customerCode)
                        .Select(v => v.CustomerCode)
                        .FirstOrDefault();
            if (query != null && query.Any())
            {
                return true;
            }
            return false;
        }

        #endregion
    }
}
