using Microsoft.AspNet.Identity.EntityFramework;
using Samane.RepositoriesForAuthentication;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using CompareAttribute = System.ComponentModel.DataAnnotations.CompareAttribute;

namespace Samane.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RoleViewModel
    {
        public RoleViewModel()
        {
            
        }
        public RoleViewModel(ApplicationRole role)
        {
            this.Id = role.Id;
            this.Name = role.Name;
          
        }

        public string Id { get; set; }

        public string Name { get; set; }

    }

    public class AuthenticationViewModel
    {
        public string userCategory { get; set; }
    }

    //public enum UserCategory
    //{
    //    engineerUser,
    //    hospitalUser
    //};

    public class RegisterViewModel
    {

        [Required(ErrorMessage ="لطفا نام و نام خانوادگی خود را وارد نمایید")]
        [Display(Name = "نام و نام خانوادگی ")]
        public  string NameAndLastName{ get; set; }

        [Required(ErrorMessage ="لطفا ایمیل را وارد نمایید")]
        [Display(Name = "ایمیل کاربر")]
        [EmailAddress(ErrorMessage = "لطفا ایمیل را وارد نمایید")]
        public string UserNamee { get; set; }

        [Required(ErrorMessage ="وارد کردن کلمه عبور الزامی است")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تکرار رمز عبور")]
        [System.Web.Mvc.Compare("Password", ErrorMessage = "تکرار کلمه عبور معتبر نیست")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "شماره تلفن همراه را وارد نمایید")]
        //[RegularExpression(@"^[09]\d{2}^[0-9]{9}$", ErrorMessage = "لطفا شماره همراه خود را کامل وارد نمایید")]
        [Display(Name = "شماره همراه")]
        public string PhoneNumberr { get; set; }

        [Required(ErrorMessage = "لطفا استان را وارد نمایید")]
        [Display(Name = "استان")]
        public string Province { get; set; }

        [Required(ErrorMessage = "لطفا شهر را وارد نمایید")]
        [Display(Name = "شهر")]
        public string City { get; set; }

    }

    public class HospitalUserRegisterViewModel :  RegisterViewModel
    {
        [Required(ErrorMessage ="نام بیمارستان را وارد نمایید")]
        [Display(Name = "نام بیمارستان")]
        public string HospitalName { get; set; }

    }

    public class EngineerUserRegisterViewModel : RegisterViewModel
    {
        [Required(ErrorMessage ="لطفا نوع دستگاه را انتخاب نمایید")]
        [Display(Name = "متخصص دستگاه")]
        public Instruments ExpertInInstruments { get; set; }

        [Required(ErrorMessage = "سنوات در شاخه انتخابی را وارد نمایید")]
        //[RegularExpression(@"^[0-9]\d{2}", ErrorMessage = "سنوات باید تا دو رقم وارد شود")]
        [Display(Name = "سنوات")]
        public int YearsOfWork { get; set; }

    }

    public class MyPanelViewModel
    {
        [Key]
        public int Id { get; set; }
        public Instrument instrument { get; set; }
        public Hospital Hospital { get; set; }
    }



    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    //public class CityEventViewModel
    //{
    //    public List<SelectListItem> RelatedCity { get; set; }

    //    public bool cityEvent { get; set; }
    //}
}
