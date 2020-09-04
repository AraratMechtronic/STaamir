using Samane.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;


namespace Samane.RepositoriesForAuthentication
{
    public class HospitalUser : RegisterViewModel , IAuthentication.IAuthentication
    {
        [Required]
        [Display(Name =" نام بیمارستان")]
        public string HospitalName  { get; set; }


        public string DoRegisteration(IAuthentication.IAuthentication a )
        {

            var db = new Samane.DbContext_Classes.SamaneDbContext();

            var h = new Hospital();
            h.City = this.City;
            h.HospitalName = this.HospitalName;
            h.InChargePerson = this.NameAndLastName;
            h.PhoneNumber = this.PhoneNumberr;
            h.Province = this.Province;
            h.UserNamee = this.UserNamee;

            db.Hospitals.AddOrUpdate(h);
            db.SaveChanges();

            return ("my name is " + this.NameAndLastName + " and i am hospitaluser supervisor of " + this.HospitalName+" hospital");

        }
    }
}