using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNet.OData;
using StaffApi.Models;


namespace StaffApi.Controllers
{
    public class LoginController : ODataController
    {
        [HttpGet]
        [ODataRoute("Login")]
        public IQueryable<Staff> Login(string user, string pass)
        {
            //var user = "manager";
            //var pass = "manager";
            var context = new StaffContext();
            var staff = context.USP_Login(user, pass).AsQueryable();
            if (staff == null)
            {
                Console.WriteLine("err");
                return null;
            }  
            else
                return staff;
        }
    }
}
