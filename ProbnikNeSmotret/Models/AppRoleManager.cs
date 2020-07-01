using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProbnikNeSmotret.Models
{
    public class AppRoleManager : RoleManager<ApplicationRole>, IDisposable
    {
        public AppRoleManager(RoleStore<ApplicationRole> store)
            : base(store)
        { }

        public static AppRoleManager Create(
            IdentityFactoryOptions<AppRoleManager> options,
            IOwinContext context)
        {
            return new AppRoleManager(new
                RoleStore<ApplicationRole>(context.Get<ApplicationDbContext>()));//was AppIdentityDbContext
        }
    }
}