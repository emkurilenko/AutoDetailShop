using AutoStore.BLL.Interfaces;
using AutoStore.BLL.Services;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;


[assembly: OwinStartup(typeof(AutoStore.WEB.App_Start.Startup))]

namespace AutoStore.WEB.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<IService>(CreateUserService);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }

        private IService CreateUserService()
        {
            var sc = new AutoDetailService();
            return sc.CreateService("DefaultConnection");
        }
    }
}