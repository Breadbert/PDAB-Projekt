using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MVVMFirma.Validators
{
    public class NewCustomerValidator
    {
        public static string ValidateFirstName(string FirstName)
        {

            if (string.IsNullOrEmpty(FirstName))
            {
                return "First Name is required!";
            }
            return string.Empty;
        }

        public static string ValidateLastName(string FirstName)
        {

            if (string.IsNullOrEmpty(FirstName))
            {
                return "First Name is required!";
            }
            return string.Empty;
        }

        public static string ValidatePhone(string Phone)
        {
            if (string.IsNullOrEmpty(Phone))
            {
                return "Phone Number is required!";
            }
            return string.Empty;
        }
    }
}
