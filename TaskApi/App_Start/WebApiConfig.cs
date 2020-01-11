using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using TaskApi.Models;

namespace TaskApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Task>("Tasks");
            builder.EntitySet<TaskStaff>("TaskStaffs");

            builder.EntityType<Task>().Collection.Function("Tasks").Returns<Task>();

            config.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
        }
    }
}
