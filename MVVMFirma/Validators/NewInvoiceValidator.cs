using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MVVMFirma.Validators
{
    public class NewInvoiceValidator
    {
        public static string ValidateInvoiceNumber(string Number)
        {

            string pattern = @"^(.*[A-Z].*){3,}$";

            if (string.IsNullOrEmpty(Number))
            {
                return "Number is required!";
            }
            else if (!Regex.IsMatch(Number, pattern))
            {
                return "Minimum 3 uppercase letters!";
            }
            return string.Empty;
        }
    }
}
