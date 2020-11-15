using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class AddressInfo
    {
        public string CustomerCode { get; set; }
        public string AddressType { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string CountryId { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public long AddressId { get; set; }
    }
}
