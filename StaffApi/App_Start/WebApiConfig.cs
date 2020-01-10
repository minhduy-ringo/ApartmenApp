using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using StaffApi.Models;
using Microsoft.AspNet.OData;

namespace StaffApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Staff>("Staffs");
            builder.EntitySet<Department>("Departments");
            builder.EntitySet<LeaveRequest>("LeaveRequests");
            builder.EntitySet<StaffVacation>("StaffVacations");
            builder.EntitySet<Schedule>("Schedules");

            var function = builder.Function("Login");
            function.Returns<Staff>();
            //function.Parameter<string>("User");
            //function.Parameter<string>("Pass");
            config.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
        }
    }
}
