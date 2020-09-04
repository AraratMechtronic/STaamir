using Samane.IAuthentication;
using Samane.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Samane.RepositoriesForAuthentication
{
    public class AdminUser : RegisterViewModel, IAuthentication.IAuthentication
    {
        //[Required]
        //[Display(Name = "نام و نام خانوادگی")]
        //public string NameAndLastName { get; set; }

        public string DoRegisteration(IAuthentication.IAuthentication a)
        {
            var context = new ApplicationDbContext();
            //context.Users.Where(b => b.== a.NameAndLastName);
            //this.NameAndLastName = a.NameAndLastName;

            return ("my name is " + this.NameAndLastName + " and i am Admin " );

        }
    }
}