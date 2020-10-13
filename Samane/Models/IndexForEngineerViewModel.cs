using Samane.DbContext_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Samane.Models
{
    public class IndexForEngineerViewModel
    {
        SamaneDbContext SamanehDB = new SamaneDbContext();
        ApplicationDbContext appDB = new ApplicationDbContext();
        private ApplicationUser _engineer = new ApplicationUser();
        public ApplicationUser EngineerUser
        {
            get { return _engineer; }
            set { _engineer = value; }
        }

        public void GetEngineerInformation(string username = null)
        {
            if (username is null && _engineer.UserName is null)
                return;

            string _username = (_engineer.UserName != null)
                                ? _engineer.UserName
                                : username;

            if (this._engineer.UserName == null)
                this._engineer = appDB.Users
                                .Where(eng => eng.UserName == _username)
                                .FirstOrDefault<ApplicationUser>();

        }

    }
}