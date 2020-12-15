using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Samane.Models;
using Samane.RepositoriesForAuthentication;

namespace Samane.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        CityStateDbContext db = new CityStateDbContext();
        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;

        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }


        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        [AllowAnonymous]
        public ActionResult Authenticate()
        {

            ///List<SelectListItem> list = new List<SelectListItem>();
            ViewBag.Roles = RoleManager.Roles.Select(r => new { Value = r.Name, Text = r.PersianRoelName });
            //foreach (var role in RoleManager.Roles)
            //{
            //    if (role.Name == "EngineerUsers")
            //    {
            //        list.Add(new SelectListItem() { Value = role.Name, Text = "کاربر تعمیرکار" });
            //    }
            //    else if (role.Name == "HospitalUsers")
            //    {
            //        role.Id = "کاربر بیمارستان";
            //        list.Add(new SelectListItem() { Value = role.Name, Text = role.Id });
            //    }
            //    else if (role.Name == "AdminUser")
            //    {
            //        role.Id = "کاربر ادمین";
            //        list.Add(new SelectListItem() { Value = role.Name, Text = role.Id });
            //    }
            //}
            //ViewBag.Roles = list;

            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Authenticate(AuthenticationViewModel model)
        {
            bool flag = true;
            if (ModelState.IsValid)
            {
                TempData["Authenticated"] = "true";
                List<string> userCategory = new List<string>();
                var user = new ApplicationUser() { userRole = model.userCategory };
                foreach (var role in RoleManager.Roles)
                {
                    userCategory.Add(role.Name);
                }
                foreach (var i in userCategory)
                {
                    if (model.userCategory == i)
                    {
                        flag = false;
                        TempData["userNo"] = model.userCategory;
                        TempData["userNoo"] = model.userCategory;
                        TempData["userNooo"] = model.userCategory;

                    }
                }
                if (flag)
                    return RedirectToAction("Authenticate", "Account");
                else
                    return RedirectToAction("Register", "Account");
            }
            else
            {
                TempData["userNo"] = null;
                TempData["userNoo"] = null;
                TempData["userNooo"] = null;
                TempData["Authenticated"] = "false";
                return RedirectToAction("Authenticate");

            }
        }


        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            if ((string)TempData["Authenticated"] == "true")
            {
                if ((string)TempData["userNo"] == "HospitalUsers")
                {
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    List<SelectListItem> list = new List<SelectListItem>();
                    foreach (var city in db.T_City)
                    {
                        list.Add(new SelectListItem() { Value = city.City_Name.ToString(), Text = city.City_Name.ToString() });
                    }
                    ViewBag.Cities = list;
                    List<SelectListItem> listt = new List<SelectListItem>();
                    foreach (var state in db.T_State)
                    {
                        listt.Add(new SelectListItem() { Value = state.State_Name.ToString(), Text = state.State_Name.ToString() });
                    }
                    ViewBag.Province = listt;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    var hospitaluserviewmodel = new HospitalUserRegisterViewModel();
                    TempData["HospitalUserModel"] = hospitaluserviewmodel;
                    return View("RegisterForHospital");
                }
                else if ((string)TempData["userNo"] == "EngineerUsers")
                {
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    List<SelectListItem> list = new List<SelectListItem>();
                    foreach (var city in db.T_City)
                    {
                        list.Add(new SelectListItem() { Value = city.City_Name.ToString(), Text = city.City_Name.ToString() });
                    }
                    ViewBag.Cities = list;
                    List<SelectListItem> listt = new List<SelectListItem>();
                    foreach (var state in db.T_State)
                    {
                        listt.Add(new SelectListItem() { Value = state.State_Name.ToString(), Text = state.State_Name.ToString() });
                    }
                    ViewBag.Province = listt;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    var engineeruserviewmodel = new EngineerUserRegisterViewModel();
                    TempData["EngineerUserModel"] = engineeruserviewmodel;
                    return View("RegisterForEngineer");
                }
                else if ((string)TempData["userNo"] == "AdminUser")
                {
                    /////////////////////////////////////////////////////////////////////////////
                    List<SelectListItem> list = new List<SelectListItem>();
                    foreach (var city in db.T_City)
                    {
                        list.Add(new SelectListItem() { Value = city.City_Name.ToString(), Text = city.City_Name.ToString() });
                    }
                    ViewBag.Cities = list;
                    List<SelectListItem> listt = new List<SelectListItem>();
                    foreach (var state in db.T_State)
                    {
                        listt.Add(new SelectListItem() { Value = state.State_Name.ToString(), Text = state.State_Name.ToString() });
                    }
                    ViewBag.Province = listt;
                    ///////////////////////////////////////////////////////////////////////
                    RegisterViewModel registerviewmodel = new RegisterViewModel();
                    TempData["AdminUserModel"] = registerviewmodel;
                    return View("RegisterForAdmin");
                }
                else

                    return RedirectToAction("Authenticate");
            }

            else
                return RedirectToAction("Authenticate");


        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult _Register(string Province)
        {
            GetRelatedCity(Province);
            return View("_Register");
        }

        private List<SelectListItem> GetRelatedCity(string Province)
        {
            List<SelectListItem> citylist = new List<SelectListItem>();
            var SelectedStateName = db.T_State.FirstOrDefault(s => s.State_Name == Province);
            var CitiesInSpecifiedState = db.T_City.Where(c => c.State_ID == SelectedStateName.State_ID);
            foreach (var city in CitiesInSpecifiedState)
            {
                citylist.Add(new SelectListItem() { Value = city.City_Name, Text = city.City_Name.ToString() });
            }
            ViewBag.Bool = true;
            return citylist;
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel Adminmodel, HospitalUserRegisterViewModel Hospitalmodel, EngineerUserRegisterViewModel Engineermodel)
        {
            ViewBag.Bool = false;
            if ((string)TempData["userNoo"] == "AdminUser")
            {
                ModelState.Remove("HospitalName");
                ModelState.Remove("YearsOfWork");
                ModelState.Remove("ExpertInInstruments");
                var user = new ApplicationUser { City = Adminmodel.City, Province = Adminmodel.Province, PhoneNumber = Adminmodel.PhoneNumberr, NameAndLastName = Adminmodel.NameAndLastName, Email = Adminmodel.UserNamee, UserName = Adminmodel.UserNamee, userRole = (string)TempData["userNoo"] };
                if (ModelState.IsValid)
                {
                    var result = await UserManager.CreateAsync(user, Adminmodel.Password);
                    AddErrors(result);
                }

                if (ModelState.IsValid)
                {
                    var result = await UserManager.AddToRoleAsync(user.Id, user.userRole);
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    AddErrors(result);
                    if (ModelState.IsValid)
                    {
                        AdminUser adminuser = new AdminUser();
                        adminuser.NameAndLastName = user.NameAndLastName;
                        adminuser.City = user.City;
                        adminuser.PhoneNumberr = user.PhoneNumber;
                        adminuser.UserNamee = user.UserName;
                        adminuser.Province = user.Province;
                        APPAuthentication.AppAuthentication appAuthentication = new APPAuthentication.AppAuthentication(adminuser);
                        string x = appAuthentication.Registeration();
                        return RedirectToAction("Index", "MyPanel");
                    }
                    else
                    {
                        TempData["userNoo"] = "AdminUser";
                        ///////////////////////////////////////////////////////////////////////////////////
                        List<SelectListItem> list = new List<SelectListItem>();
                        foreach (var city in db.T_City)
                        {
                            list.Add(new SelectListItem() { Value = city.City_Name.ToString(), Text = city.City_Name.ToString() });
                        }
                        ViewBag.Cities = list;
                        List<SelectListItem> listt = new List<SelectListItem>();
                        foreach (var state in db.T_State)
                        {
                            listt.Add(new SelectListItem() { Value = state.State_Name.ToString(), Text = state.State_Name.ToString() });
                        }
                        ViewBag.Province = listt;
                        ////////////////////////////////////////////////////////////////////////////////////////////////
                        if (user.Province != null)
                        {
                            ViewBag.Cities = GetRelatedCity(user.Province);
                            return PartialView("_Register");
                        }
                        else
                        {
                            return View("RegisterForAdmin");
                        }
                    }
                }
                else
                {
                    //Returning Model For possible errors (Admin Model)
                    TempData["userNoo"] = "AdminUser";

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    List<SelectListItem> list = new List<SelectListItem>();
                    foreach (var city in db.T_City)
                    {
                        list.Add(new SelectListItem() { Value = city.City_Name.ToString(), Text = city.City_Name.ToString() });
                    }
                    ViewBag.Cities = list;
                    List<SelectListItem> listt = new List<SelectListItem>();
                    foreach (var state in db.T_State)
                    {
                        listt.Add(new SelectListItem() { Value = state.State_Name.ToString(), Text = state.State_Name.ToString() });
                    }
                    ViewBag.Province = listt;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (user.Province != null)
                    {
                        ViewBag.Cities = GetRelatedCity(user.Province);
                        return PartialView("_Register");
                    }
                    else
                    {
                        return View("RegisterForAdmin");
                    }
                }

            }
            else if ((string)TempData["userNoo"] == "HospitalUsers")
            {
                ModelState.Remove("ExpertInInstruments");
                ModelState.Remove("YearsOfWork");
                var user = new ApplicationUserForHospitals { City = Hospitalmodel.City, Province = Hospitalmodel.Province, HospitalName = Hospitalmodel.HospitalName, NameAndLastName = Hospitalmodel.NameAndLastName, Email = Hospitalmodel.UserNamee, UserName = Hospitalmodel.UserNamee, PhoneNumber = Hospitalmodel.PhoneNumberr, userRole = (string)TempData["userNoo"] };
                if (ModelState.IsValid)
                {
                    var result = await UserManager.CreateAsync(user, Hospitalmodel.Password);
                    AddErrors(result);
                }

                if (ModelState.IsValid)
                {
                    var result = await UserManager.AddToRoleAsync(user.Id, user.userRole);
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    AddErrors(result);
                    if (ModelState.IsValid)
                    {
                        HospitalUser hospitaluser = new HospitalUser();
                        hospitaluser.City = user.City;
                        hospitaluser.PhoneNumberr = user.PhoneNumber;
                        hospitaluser.Province = user.Province;
                        hospitaluser.UserNamee = user.UserName;
                        hospitaluser.NameAndLastName = user.NameAndLastName;
                        hospitaluser.HospitalName = user.HospitalName;
                        APPAuthentication.AppAuthentication appAuthentication = new APPAuthentication.AppAuthentication(hospitaluser);
                        string x = appAuthentication.Registeration();
                        return RedirectToAction("Index", "MyPanel");
                    }
                    else
                    {
                        TempData["userNoo"] = "HospitalUsers";
                        ///////////////////////////////////////////////////////////////////////////////////////////////////////
                        List<SelectListItem> list = new List<SelectListItem>();
                        foreach (var city in db.T_City)
                        {
                            list.Add(new SelectListItem() { Value = city.City_Name.ToString(), Text = city.City_Name.ToString() });
                        }
                        ViewBag.Cities = list;
                        List<SelectListItem> listt = new List<SelectListItem>();
                        foreach (var state in db.T_State)
                        {
                            listt.Add(new SelectListItem() { Value = state.State_Name.ToString(), Text = state.State_Name.ToString() });
                        }
                        ViewBag.Province = listt;
                        ///////////////////////////////////////////////////////////////////////////////////////////////////////
                        return View("RegisterForHospital");
                    }
                }
                else
                {
                    TempData["userNoo"] = "HospitalUsers";
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    List<SelectListItem> list = new List<SelectListItem>();
                    foreach (var city in db.T_City)
                    {
                        list.Add(new SelectListItem() { Value = city.City_Name.ToString(), Text = city.City_Name.ToString() });
                    }
                    ViewBag.Cities = list;
                    List<SelectListItem> listt = new List<SelectListItem>();
                    foreach (var state in db.T_State)
                    {
                        listt.Add(new SelectListItem() { Value = state.State_Name.ToString(), Text = state.State_Name.ToString() });
                    }
                    ViewBag.Province = listt;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    return View("RegisterForHospital");
                }
            }
            else if ((string)TempData["userNoo"] == "EngineerUsers")
            {
                ModelState.Remove("HospitalName");
                var user = new ApplicationUserForEngineers { City = Engineermodel.City, Province = Engineermodel.Province, ExpertInInstruments = Engineermodel.ExpertInInstruments, YearsOfWork = Engineermodel.YearsOfWork, NameAndLastName = Engineermodel.NameAndLastName, Email = Engineermodel.UserNamee, UserName = Engineermodel.UserNamee, PhoneNumber = Engineermodel.PhoneNumberr, userRole = (string)TempData["userNoo"] };
                if (ModelState.IsValid)
                {
                    var result = await UserManager.CreateAsync(user, Engineermodel.Password);
                    AddErrors(result);
                }
                if (ModelState.IsValid)
                {
                    var result = await UserManager.AddToRoleAsync(user.Id, user.userRole);
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    AddErrors(result);
                    if (ModelState.IsValid)
                    {
                        EngineerUser engineeruser = new EngineerUser();
                        engineeruser.NameAndLastName = user.NameAndLastName;
                        engineeruser.instruments = user.ExpertInInstruments;
                        engineeruser.City = user.City;
                        engineeruser.PhoneNumberr = user.PhoneNumber;
                        engineeruser.Province = user.Province;
                        engineeruser.UserNamee = user.UserName;
                        APPAuthentication.AppAuthentication appAuthentication = new APPAuthentication.AppAuthentication(engineeruser);
                        string x = appAuthentication.Registeration();
                        return RedirectToAction("Index", "MyPanel");
                    }
                    else
                    {
                        TempData["userNoo"] = "EngineerUsers";
                        ///////////////////////////////////////////////////////////////////////////////////////////////////////
                        List<SelectListItem> list = new List<SelectListItem>();
                        foreach (var city in db.T_City)
                        {
                            list.Add(new SelectListItem() { Value = city.City_Name.ToString(), Text = city.City_Name.ToString() });
                        }
                        ViewBag.Cities = list;
                        List<SelectListItem> listt = new List<SelectListItem>();
                        foreach (var state in db.T_State)
                        {
                            listt.Add(new SelectListItem() { Value = state.State_Name.ToString(), Text = state.State_Name.ToString() });
                        }
                        ViewBag.Province = listt;
                        ///////////////////////////////////////////////////////////////////////////////////////////////////////
                        return View("RegisterForEngineer");
                    }
                }
                else
                {
                    //Returning Model For possible errors (Engineer Model)
                    TempData["userNoo"] = "EngineerUsers";
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    List<SelectListItem> list = new List<SelectListItem>();
                    foreach (var city in db.T_City)
                    {
                        list.Add(new SelectListItem() { Value = city.City_Name.ToString(), Text = city.City_Name.ToString() });
                    }
                    ViewBag.Cities = list;
                    List<SelectListItem> listt = new List<SelectListItem>();
                    foreach (var state in db.T_State)
                    {
                        listt.Add(new SelectListItem() { Value = state.State_Name.ToString(), Text = state.State_Name.ToString() });
                    }
                    ViewBag.Province = listt;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    return View("RegisterForEngineer");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}