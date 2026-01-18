using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FieldValidatorAPI
{
    // funcs that are being pointed to by resp delegates MUST accept the resp parameters defined in the delegate
    public delegate bool RequiredValDel(string fieldValue);
    public delegate bool StringLengthValidDel(string fieldValue, int min, int max);
    public delegate bool DateValidDel(string fieldValue, out DateTime validDate);
    public delegate bool PatternMatchValidDel(string fieldValue, string pattern);
    public delegate bool CompareFieldsValidDel(string fieldValue, string fieldValueCompare);
    public class CommonFieldValidatorFunctions
    {
        private static RequiredValDel _requiredValDel = null;
        private static StringLengthValidDel _stringLengthValidDel = null;
        private static DateValidDel _dateValidDel = null;
        private static PatternMatchValidDel _patternMatchValidDel = null;
        private static CompareFieldsValidDel _compareFieldsValidDel = null;

        public static RequiredValDel RequiredValDel_PointerToFunc
        {
            get
            {
                if(_requiredValDel == null)
                {
                    _requiredValDel = new RequiredValDel(RequiredVal);
                }
                return _requiredValDel;
            }
        }
        public static StringLengthValidDel StringLengthValidDel_PointerToFunc
        {
            get
            {
                if(_stringLengthValidDel == null)
                {
                    _stringLengthValidDel = new StringLengthValidDel(StringLengthValid);
                }
                return _stringLengthValidDel;
            }
        }
        public static DateValidDel DateValidDel_PointerToFunc
        {
            get
            {
                if(_dateValidDel == null)
                {
                    _dateValidDel = new DateValidDel(DateValid);
                }
                return _dateValidDel;
            }
        }
        public static PatternMatchValidDel PatternMatchValidDel_PointerToFunc
        {
            get
            {
                if(_patternMatchValidDel == null)
                {
                    _patternMatchValidDel = new PatternMatchValidDel(PatternMatchValid);
                }
                return _patternMatchValidDel;
            }
        }
        public static CompareFieldsValidDel CompareFieldsValidDel_PointerToFunc
        {
            get
            {
                if(_compareFieldsValidDel == null)
                {
                    _compareFieldsValidDel = new CompareFieldsValidDel(CompareFieldsValid);
                }
                return _compareFieldsValidDel;
            }
        }


        private static bool RequiredVal(string fieldValue)
        {
            if(!string.IsNullOrEmpty(fieldValue))
            {
                return true;
            }
            return false;   
        }

        private static bool StringLengthValid(string fieldValue, int min, int max)
        {
            return (fieldValue.Length > min && fieldValue.Length < max);
        }

        private static bool DateValid(string dateTime, out DateTime validDateTime)
        {
            if(DateTime.TryParse(dateTime, out validDateTime))
            {
                return true;
            }
            return false;
        }

        private static bool PatternMatchValid(string fieldValue, string RegexPattern)
        {
            Regex regex = new Regex(RegexPattern);
            return regex.IsMatch(fieldValue);
        }

        private static bool CompareFieldsValid(string field1, string field2)
        {
            return field1.Equals(field2);
        }
    }
}