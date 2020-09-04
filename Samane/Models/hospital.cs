using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Samane.Models
{
    public class Hospital
    {

        [Key]
        [Display(Name ="نام کاریری")]
        public string UserNamee { get; set; }

        [Required]
        [Display(Name = "نام بیمارستان")]
        public string HospitalName { get; set; }

        [Required]
        [Display(Name = "استان")]
        public string Province { get; set; }

        [Required]
        [Display(Name = "شهر")]
        public string City { get; set; }

        [Required]
        [Display(Name = "شماره موبایل")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "مسئول")]
        public string InChargePerson { get; set; }

        [Display(Name = "دستگاههای ثبت شده")]
        public List<Instrument> instruments { get; set; }
    }
}