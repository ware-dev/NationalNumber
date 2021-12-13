using System;
using System.Globalization;

namespace NNLib
{
    /// <summary>
    /// Class that represent a belgian national number. 
    /// We can use some method to determine whether or not the number is valid 
    /// and retrieve some information out of it
    /// </summary>
    public class NationalNumber
    {
        const int NATIONAL_NUMBER_LENGTH = 11;
        /// <summary>
        /// Method that validate or not a national number according to
        /// restriction of length, date, and digit control check
        /// </summary>
        /// <param name="number">the number to test validity<param>
        /// <return>a boolean that indicate if the number is valid</return>
        public static bool IsValid(string number){

            string numString = FilterSpecialChars(number);

            if(!IsLengthValid(numString.Length)) return false;
            if(!IsBirthDateValid(numString.Substring(0,6))) return false;
            if(!IsDigitControlValid(numString)) return false;
            
            return true;
        }

         /// <summary>
        /// Method that get the gender of the owner of the number according
        /// to the 3 digits that compose the number ( from 6 to 9 )
        /// </summary>
        /// <param name="number">the national number<param>
        /// <return>a string that indicate if the owner is either a male or female</return>
        public static string GetGender(string number)
        {
            string numberString = FilterSpecialChars(number);
            int genderNumber = int.Parse(numberString.Substring(6,3));
            return  (genderNumber % 2 == 0)  ? "Madame" : "Monsieur";
        }

         /// <summary>
        /// Method that get the age of the owner of the number according
        /// to the 6 digits that compose the number ( from 0 to 6 )
        /// </summary>
        /// <param name="number">the national number<param>
        /// <return>a int that represent the age of the owner</return>
        public static int GetAge(string number)
        {
            Calendar cal = CultureInfo.InvariantCulture.Calendar;
            return DateTime.Today.Year - cal.ToFourDigitYear(int.Parse(number.Substring(0,2)));
        }
         /// <summary>
        /// Method that check if the two last digits of the number are equals to a certain
        /// calcul that check the validity of the number itself
        /// </summary>
        /// <param name="number">the national number<param>
        /// <return>a bool that represent the validity of the two last digit of the national number</return>
        private static bool IsDigitControlValid(string number)
        {
            int digits = int.Parse(number.Substring(9));
            if( digits > 97 || digits < 1 ) return false;

            Calendar cal = CultureInfo.InvariantCulture.Calendar;
            bool isMillenial = cal.ToFourDigitYear(int.Parse(number.Substring(0,2))) >= 2000;

            long firstPart = isMillenial ? long.Parse( ("2" + number).Substring(0,10) ) : long.Parse(number.Substring(0,9));
            long calcul = 97 - (firstPart % 97);
            long result = calcul == 0 ? 97 : calcul;

            return (result == digits) ? true: false;
        }
        
        /// <summary>
        /// Method that check if the length of the number entered is equals to the right length
        /// </summary>
        /// <param name="number">the length of the national number<param>
        /// <return>a bool that represent the validity of the length of the national number</return>
        private static bool IsLengthValid(int length)
        {
            return length == NATIONAL_NUMBER_LENGTH ;
        }
        
        /// <summary>
        /// Method that check if the birthdate of the number is valid according to our calendar
        /// </summary>
        /// <param name="number">the date (first 6 digits of the number)<param>
        /// <return>a bool that represent the validity of the date</return>
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

        /// <summary>
        /// Method that filter the entered number and delete all the unwanted
        /// specials characters to have a clean number to work on
        /// </summary>
        /// <param name="number">the national number<param>
        /// <return>a string with only number represented as string without special chars</return>
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
