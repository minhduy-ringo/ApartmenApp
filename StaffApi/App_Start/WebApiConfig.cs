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
            builder.EntitySet<Notice>("Notices");

            builder.EntityType<Staff>().Collection.Function("GetStaffsComplex").Returns<Staff>();
            builder.EntityType<Notice>().Collection.Function("GetStaffNotices").Returns<Notice>();

            var function = builder.Function("Login").Returns<Staff>();
            config.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
        }
    }
}
