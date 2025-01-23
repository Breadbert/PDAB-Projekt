using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.EntitiesForView
{
    public class ContractorsForAllView
    {
        public int ContractorID { get; set; }
        public string Code { get; set; }
        public string NIP { get; set; }
        public string Name { get; set; }

        //from Type table
        public string Type {  get; set; }
        //from Status table
        public string Status { get; set; }

        //Address
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string HouseNumber { get; set; }
        public string ApartmentNumber { get; set; }
        
    }
}
