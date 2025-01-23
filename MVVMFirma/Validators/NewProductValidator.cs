using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MVVMFirma.Validators
{
    public class NewProductValidator
    {
        public static string ValidateProductName(string Name)
        {

            string pattern = @"^[A-Za-z\s]+ - [A-Za-z\s]+$";

            if (string.IsNullOrEmpty(Name))
            {
                return "Name is required!";
            } else if (!Regex.IsMatch(Name, pattern))
            {
                return "The product name doesn't follow the scheme <Artist> - <Title>";
            }
            return string.Empty;
        }

        public static string ValidateProductPrice(decimal? Price, decimal? Cost)
        {
            if (!Price.HasValue)
            {
                return "Price is required!";
            }

            if (Price.Value < 0)
            {
                return "Price needs to be larger than zero";
            }
            return string.Empty;
        }

        public static string ValidateProductCost(decimal? Cost)
        {
            if (!Cost.HasValue)
            {
                return "Price is required!";
            }

            if (Cost.Value < 0)
            {
                return "Price needs to be larger than zero";
            }
            return string.Empty;
        }
    }
}
