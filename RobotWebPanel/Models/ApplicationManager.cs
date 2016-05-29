using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace RobotWebPanel.Models
{
    public class ApplicationManager : UserManager<AspNetUsers>
    {
        public ApplicationManager(IUserStore<AspNetUsers> store) : base(store)
        {
        }
        public static ApplicationManager Create(IdentityFactoryOptions<ApplicationManager> options,
                                            IOwinContext context)
        {
            ModelContext db = context.Get<ModelContext>();
            ApplicationManager manager = new ApplicationManager(new UserStore<AspNetUsers>(db));
            return manager;
        }
    }
}