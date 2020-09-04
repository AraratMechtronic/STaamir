using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Samane.IAuthentication;
using Samane.Models;

namespace Samane.APPAuthentication
{
    public class AppAuthentication
    {
        private IAuthentication.IAuthentication user;
        private ApplicationUser userApp;
        private string pass;

        public AppAuthentication(IAuthentication.IAuthentication user)
        {
            this.user = user;
            this.userApp = userApp;
            this.pass = pass;
        }

        public string Registeration()
        {
            return (user.DoRegisteration(this.user));
            //return user.NameAndLastName;
        }


    }
}