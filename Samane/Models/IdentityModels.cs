using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Samane.RepositoriesForAuthentication;

namespace Samane.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display(Name = "نوع کاربر")]
        public string userRole { get; set; }

        [Required]
        [Display(Name = "نام و نام خانوادگی")]
        public string NameAndLastName { get; set; }

        [Required]
        [Display(Name = "استان")]
        public string Province { get; set; }

        [Required]
        [Display(Name = "شهر")]
        public string City { get; set; }

        [Required]
        [Display(Name = "شماره همراه")]
        public override string PhoneNumber { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationUserForHospitals : ApplicationUser
    {
        [Display(Name = "نام بیمارستان")]
        public string HospitalName { get; set; }

    }

    public class ApplicationUserForEngineers : ApplicationUser
    {

        [Display(Name = "متخصص دستگاههای")]
        public Instruments ExpertInInstruments { get; set; }

        [Display(Name = "سابقه کار")]
        public int YearsOfWork { get; set; }

    }

    public class ApplicationRole : IdentityRole
    {
        [Display(Name = "نام سطح")]
        public string PersianRoelName { get; set; }

        public ApplicationRole() : base() { }
  
        public ApplicationRole(string roleName) : base(roleName) { }

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("SamaneConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
    


}