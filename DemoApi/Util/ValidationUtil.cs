using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MEAC.Util.Validation
{
    public class ValidationUtil
    {
        public static bool IsDateTime(object date)
        {
            DateTime dataConvertida = new DateTime();
            return DateTime.TryParse(date.ToString(), out dataConvertida);
        }                

        public static bool IsDouble(object value)
        {
            Double doubleValue = new Double();
            return Double.TryParse(value.ToString(), out doubleValue);
        }

        public static bool IsInt32(object value)
        {
            Int32 intValue = new Int32();
            return Int32.TryParse(value.ToString(), out intValue);
        }        

        public static bool IsEmail(string email)
        {
            Regex regex = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9_]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");

            bool isEmail = false;

            try
            {
                isEmail = regex.IsMatch(email);
            }
            catch
            {
                return false;
            }

            return isEmail;
        }
    }
}