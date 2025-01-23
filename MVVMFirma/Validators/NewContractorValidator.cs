using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MVVMFirma.Validators
{
    public class NewContractorValidator 
    {
        public static string ValidateContractorCode(string Code)
        {
            if (string.IsNullOrEmpty(Code))
            {
                return "Code is required!";
            }
            
            if (Code.Length > 10)
            {
                return "The code is too long!";
            }
            return string.Empty;
        }
        public static string ValidateContractorName(string Name)
        {
            if (string.IsNullOrEmpty(Name))
            {
                return "Name is required!";
            }
            return string.Empty;
        }
        public static string ValidateContractorNIP(string NIP)
        {
            if (string.IsNullOrEmpty(NIP))
            {
                return "NIP is required!";
            }
            return string.Empty;
        }
    }
}
