using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Samane.Models
{
    public class Instrument
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "آیدی دستگاه")]
        public int InstrumentId { get; set; }

        [Required]
        [Display(Name = "کتگوری")]
        public string Category { get; set; }

        [Required]
        [Display(Name = "مدل")]
        public string Model { get; set; }

        [Required]
        [Display(Name = "شماره سریال")]
        public string SerialNo { get; set; }

        [Required]
        [ForeignKey("hospital")]
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }
        public Hospital hospital { get; set; }

    }
}