using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMFirma.Models.Entities;

namespace MVVMFirma.Models.EntitiesForView
{
    public class InvoiceForAllView
    {
        //Invoice information from "Invoice" table
        public int InvoiceID { get; set; }
        public string Number { get; set; }
        public bool? IsApproved { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? DueDate { get; set; }

        //Contractor information from "Contractor" table
        public int ContractorID { get; set; }
        public string ContractorNIP { get; set; }
        public string ContractorName { get; set; }

        //Invoice information from "Invoice" table
        public string PaymentMethod { get; set; }
    }
}
