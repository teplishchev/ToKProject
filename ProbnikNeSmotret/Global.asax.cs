using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ProbnikNeSmotret.Models;

namespace ProbnikNeSmotret
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //Init();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        //private void Init()
        //{
        //    ApplicationDbContext context = new ApplicationDbContext();
        //    var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

        //    var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

        //    // создаем две роли
        //    //var role1 = new IdentityRole { Name = "admin" };
        //    //var role2 = new IdentityRole { Name = "user" };
        //    //var role3 = context.Roles.Where(u => u.Name == "teacher");
        //    // добавляем роли в бд
        //    //roleManager.Create(role1);
        //    //roleManager.Create(role2);

        //    // создаем пользователей
        //    //var admin = new ApplicationUser { UserName = "boss@mail.ru", Email = "boss@mail.ru" };
        //    //string password = "_ad12min";
        //    //var result = userManager.Create(admin, password);
        //    var teacher = new ApplicationUser { UserName = "teacher@mail.ru", Email = "teacher@mail.ru" };
        //    string password = "_tea12cher";
        //    var result = userManager.Create(teacher, password);
        //    // Менеджер man@mm.ru Man_123
        //    //anna @list.ru User123$
        //    //vasya @list.ru Vasya123$
        //    //masha @yandex.ru Masha123!
        //    // если создание пользователя прошло успешно
        //    //if (result.Succeeded)
        //    //{
        //    //    // добавляем для пользователя роль
        //    //    userManager.AddToRole(admin.Id, role1.Name);
        //    //    userManager.AddToRole(admin.Id, role2.Name);
        //    //}
        //    if (result.Succeeded)
        //    {
        //        userManager.AddToRole(teacher.Id, "teacher");
        //    }
        //}
    }
}
