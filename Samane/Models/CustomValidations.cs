using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;


namespace Samane.Models
{
    /// <summary>
    /// CV_NationalIdentity contains validation checker 
    /// with IdentityAlgorithms
    /// </summary>
    public class CV_NationalIdentity : ValidationAttribute
    {

    }

    public class CV_PhoneNumber : ValidationAttribute
    {
        /// <summary>
        /// using C# Codes
        /// </summary>
        /// <param name="value">PhoneNumber in string</param>
        /// <param name="validationContext"></param>
        /// <returns>ValidationResult which using enum in ApplicationsEssentials</returns>
        /// 
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Double phoneNumber = 0;
            if (value == null)
                return new ValidationResult("" + validationContext.DisplayName + ApplicationsEssentials.ReplaceUnderskoreToSpace(ApplicationsEssentials.errorLists.الزامی_است.ToString()));
            if (value.ToString().Length != 11)
                return new ValidationResult(ApplicationsEssentials.ReplaceUnderskoreToSpace(ApplicationsEssentials.errorLists.شماره_موبایل_11__رقمی_می_باشد.ToString()));
            if (value.ToString()[0] != '0' && value.ToString()[1] != '9')
                return new ValidationResult(ApplicationsEssentials.ReplaceUnderskoreToSpace(ApplicationsEssentials.errorLists.شماره_موبایل_با_09_شروع_می_شود.ToString()));
            if (!Double.TryParse(value.ToString(), out phoneNumber))
                return new ValidationResult(ApplicationsEssentials.ReplaceUnderskoreToSpace(ApplicationsEssentials.errorLists.شماره_موبایل_وارد_شده_دارای_حرف_میباشد.ToString()));
            return ValidationResult.Success;
        }

        ///// <summary>
        ///// using Regular validation to validate input type
        ///// </summary>
        ///// <param name="value">PhoneNumber in string</param>
        ///// <param name="validationContext"></param>
        ///// <returns>ValidationResult which using enum in ApplicationsEssentials</returns>
        //protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        //{
        //    if (value != null)
        //    {
        //        if(Regex.IsMatch(value.ToString(), @"^[09]{1,2}[0-9]{9}?$"))
        //        { 
        //            return ValidationResult.Success;
        //        }
        //        else
        //            return new ValidationResult(ApplicationsEssentials.ReplaceUnderskoreToSpace(ApplicationsEssentials.errorLists.لطفا_با_فرمت_09127011264_وارد_نمایید.ToString()));
        //    }
        //    else
        //    {
        //        return new ValidationResult("" + validationContext.DisplayName + ApplicationsEssentials.ReplaceUnderskoreToSpace(ApplicationsEssentials.errorLists.الزامی_است.ToString()));
        //    }
        //}
    }
}