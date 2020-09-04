using Samane.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samane.IAuthentication
{
    public interface IAuthentication
    {
        //string NameAndLastName { get; set; }
        string DoRegisteration(IAuthentication authenticationUser);
    }
}
