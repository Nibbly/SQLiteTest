using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Address : IEntity
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string PostalCode { get; set; }
        public string AddressLocality { get; set; }
        public string AdditionalInformation { get; set; }
    }
}
