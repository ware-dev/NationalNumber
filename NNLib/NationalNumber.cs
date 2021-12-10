using System;
using System.Globalization;

namespace NNLib
{
    public class NationalNumber
    {

        public static bool IsValid(string number){

            string numString = FilterSpecialChars(number);

            if(!IsLengthValid(numString.Length)) return false;
            if(!IsBirthDateValid(numString.Substring(0,6))) return false;
            if(!IsDigitControlValid(numString, int.Parse(numString.Substring(9)))) return false;
            
            return true;
        }

        public static string GetGender(string number)
        {
            string numberString = FilterSpecialChars(number);
            int genderNumber = int.Parse(numberString.Substring(6,3));
            return  (genderNumber % 2 == 0)  ? "Madame" : "Monsieur";
        }

        public static int GetAge(string number)
        {
            Calendar cal = CultureInfo.InvariantCulture.Calendar;
            return DateTime.Today.Year - cal.ToFourDigitYear(int.Parse(number.Substring(0,2)));
        }

        private static bool IsDigitControlValid(string numString, int digits)
        {
            if( digits > 97 || digits < 1 ) return false;

            Calendar cal = CultureInfo.InvariantCulture.Calendar;
            bool isMillenial = cal.ToFourDigitYear(int.Parse(numString.Substring(0,2))) >= 2000;

            long firstPart = isMillenial ? long.Parse( ("2" + numString).Substring(0,10) ) : long.Parse(numString.Substring(0,9));
            long calcul = 97 - (firstPart % 97);
            long result = calcul == 0 ? 97 : calcul;

            return (result == digits) ? true: false;
        }
        private static bool IsLengthValid(int length)
        {
            return length == 11 ;
        }
        
        private static bool IsBirthDateValid(string date)
        {
            Calendar cal = CultureInfo.InvariantCulture.Calendar;
            int year = cal.ToFourDigitYear(int.Parse(date.Substring(0,2)));
            int month = int.Parse(date.Substring(2,2));
            int day = int.Parse(date.Substring(4,2));

            if(month > 12 || month < 1) return false;
            if(day > 31 || day < 1) return false;
            if(day > DateTime.DaysInMonth(year, month)) return false;
            if(year < 30 && year > DateTime.Now.Year) return false;
            if(DateTime.IsLeapYear(year) && month == 2 && day > 29) return false;
            if(!DateTime.IsLeapYear(year) && month == 2 && day > 28) return false; 

            return true;
        }

        private static string FilterSpecialChars(string number)
        {
                string copyValue = number;
                string normalizedValue = string.Empty;
                
                foreach (char charValue in copyValue)
                {
                    if (char.IsDigit(charValue)) normalizedValue += charValue;
                }
               
                return (normalizedValue.Length > 0) ? normalizedValue : "Empty Number";    
        }
    }
}
