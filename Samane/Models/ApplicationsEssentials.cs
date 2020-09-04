using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Samane.Models
{
    public static class ApplicationsEssentials
    {
        public enum errorLists
        {
            شماره_موبایل_با_09_شروع_می_شود,
            شماره_موبایل_11__رقمی_می_باشد,
            شماره_موبایل_وارد_شده_دارای_حرف_میباشد,
            الزامی_است

        }

        public static String ReplaceUnderskoreToSpace(this String value)
        {
            try
            {
                return value.Replace("_", " ");
            }
            catch
            {
                return value;
            }
                
        }
    }
}