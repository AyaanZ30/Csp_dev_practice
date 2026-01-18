using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FieldValidatorAPI;

namespace ClubMembershipApp.FieldValidators
{
    public class UserRegistrationValidator : IFieldValidator
    {
        const int FirstName_Min_Length = 2;
        const int FirstName_Max_Length = 100;
        const int LastName_Min_Length = 2;
        const int LastName_Max_Length = 100;
        
        delegate bool EmailExistsDel(string email_address);

        // all these del objects will point to references to methods containining validation implementation
        FieldValidatorDel _fieldValidatorDel = null;

        RequiredValDel _requiredValDel = null;
        StringLengthValidDel _stringLengthValidDel = null;
        DateValidDel _dateValidDel = null;
        PatternMatchValidDel _patternMatchValidDel = null;
        CompareFieldsValidDel _compareFieldsValidDel = null;
        EmailExistsDel _emailExistsDel = null;

        string[] _fieldArray = null;
        public string[] FieldArray
        {
            get
            {
                if(_fieldArray == null)
                {
                    _fieldArray = new string[Enum.GetValues(typeof(FieldConstants.UserRegistrationField)).Length];

                    // ( above lines meaning simplified below code ) 
                    // in order to know which field we are validating : enum value -> index -> string (purely for mapping purpose)
                    // _fieldArray = new string[]
                    // {
                    //     "Email Address",
                    //     "First Name",
                    //     "Last Name",
                    //     "Password",
                    //     "Password Compare",
                    //     "Date Of Birth",
                    //     "Phone Number",
                    //     "Address First Line",
                    //     "Address Second Line",
                    //     "Address City",
                    //     "Post Code"
                    // };
                }
                return _fieldArray;
            }
        }

        // exposing the FieldValidatorDel delegate (outsiders can call ValidatorDel) through public read-only property
        // Required by the IFieldValidator interface (only way outsiders can access the actual delegate : _fieldValidatorDel)
        public FieldValidatorDel ValidatorDel => _fieldValidatorDel;

        public void initializeValidatorDelegates()
        {
            _fieldValidatorDel = new FieldValidatorDel(ValidField);

            _requiredValDel = CommonFieldValidatorFunctions.RequiredValDel_PointerToFunc;
            _stringLengthValidDel = CommonFieldValidatorFunctions.StringLengthValidDel_PointerToFunc;
            _dateValidDel = CommonFieldValidatorFunctions.DateValidDel_PointerToFunc;
            _patternMatchValidDel = CommonFieldValidatorFunctions.PatternMatchValidDel_PointerToFunc;   
            _compareFieldsValidDel = CommonFieldValidatorFunctions.CompareFieldsValidDel_PointerToFunc;
        }

        // this method will be referenced by : FieldValidatorDel (defined in IFieldValidator.cs)
        private bool ValidField(int fieldIndex, string fieldVal, string[] fieldArray, out string fieldInvalidMessage)
        {
            fieldInvalidMessage = "";

            FieldConstants.UserRegistrationField userRegistrationField = (FieldConstants.UserRegistrationField)fieldIndex;

            switch(userRegistrationField)
            {
                case FieldConstants.UserRegistrationField.EmailAddress:
                    fieldInvalidMessage = (!_requiredValDel(fieldVal)) ? $"Email Address must be entered" : ""; 
                    fieldInvalidMessage = (!_patternMatchValidDel(fieldVal, CommonRegularExpressionsValidatorPatterns.Email_regex_pattern)) ? "You must enter a valid email address" : fieldInvalidMessage;
                    break;
                
                case FieldConstants.UserRegistrationField.FirstName:
                    fieldInvalidMessage = (!_requiredValDel(fieldVal)) ? $"First name must be entered" : ""; 
                    fieldInvalidMessage = (!_stringLengthValidDel(fieldVal, 2, 100)) ? $"You must enter a valid first name (length between {FirstName_Min_Length}-{FirstName_Max_Length} chars)" : fieldInvalidMessage;
                    break;
                
                case FieldConstants.UserRegistrationField.LastName:
                    fieldInvalidMessage = (!_requiredValDel(fieldVal)) ? $"Last name must be entered" : ""; 
                    fieldInvalidMessage = (!_stringLengthValidDel(fieldVal, 2, 100)) ? $"You must enter a valid last name (length between {LastName_Min_Length}-{LastName_Max_Length} chars)" : fieldInvalidMessage;
                    break;
                
                case FieldConstants.UserRegistrationField.Password:
                    fieldInvalidMessage = (!_requiredValDel(fieldVal)) ? $"Password must be entered" : ""; 
                    fieldInvalidMessage = (!_patternMatchValidDel(fieldVal, CommonRegularExpressionsValidatorPatterns.Password_regex_pattern)) ? $"Your password must contain atleast 1 small-case, atleast 1 capital, atleast 1 special characters and a total length of 6-10 characters" : fieldInvalidMessage;
                    break;
                
                case FieldConstants.UserRegistrationField.PasswordCompare:
                    fieldInvalidMessage = (!_requiredValDel(fieldVal)) ? $"Password must be entered" : ""; 
                    fieldInvalidMessage = (!_compareFieldsValidDel(fieldVal, fieldArray[(int)FieldConstants.UserRegistrationField.Password])) ? $"Your entry ({fieldVal}) did not match your password ({fieldArray[(int)FieldConstants.UserRegistrationField.Password]})" : fieldInvalidMessage;
                    break;
                
                case FieldConstants.UserRegistrationField.DateOfBirth:
                    fieldInvalidMessage = (!_requiredValDel(fieldVal)) ? $"Date of Birth must be entered" : ""; 
                    fieldInvalidMessage = (!_dateValidDel(fieldVal, out DateTime validDateTime)) ? $"You did not enter a valid date ({fieldVal})" : fieldInvalidMessage;
                    break;
                
                case FieldConstants.UserRegistrationField.PhoneNumber:
                    fieldInvalidMessage = (!_requiredValDel(fieldVal)) ? $"UK Phone number must be entered" : ""; 
                    fieldInvalidMessage = (! _patternMatchValidDel(fieldVal, CommonRegularExpressionsValidatorPatterns.Uk_phone_number_pattern)) ? $"You did not enter a valid UK Phone number" : fieldInvalidMessage;
                    break;
                
                case FieldConstants.UserRegistrationField.AddressFirstLine:
                    fieldInvalidMessage = (!_requiredValDel(fieldVal)) ? $"Address first line must be entered" : ""; 
                    break;
                
                case FieldConstants.UserRegistrationField.AddressSecondLine:
                    fieldInvalidMessage = (!_requiredValDel(fieldVal)) ? $"Address second line must be entered" : ""; 
                    break;
                
                case FieldConstants.UserRegistrationField.AddressCity:
                    fieldInvalidMessage = (!_requiredValDel(fieldVal)) ? $"Address city must be entered" : ""; 
                    break;
                
                case FieldConstants.UserRegistrationField.PostCode:
                    fieldInvalidMessage = (!_requiredValDel(fieldVal)) ? $"UK Post code must be entered" : ""; 
                    fieldInvalidMessage = (! _patternMatchValidDel(fieldVal, CommonRegularExpressionsValidatorPatterns.Uk_post_code_pattern)) ? $"You did not enter a valid UK Post code" : fieldInvalidMessage;
                    break;
                
                default:
                    throw new ArgumentException($"This field ({fieldVal}) does not exist");
            }

            return (fieldInvalidMessage == "");
        }
    }
}