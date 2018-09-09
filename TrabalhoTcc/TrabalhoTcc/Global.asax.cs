using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace TrabalhoTcc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            var cookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie!=null && cookie.Value != string.Empty)
            {
                FormsAuthenticationTicket tickt;
                try
                {
                    tickt = FormsAuthentication.Decrypt(cookie.Value);
                }
                catch (Exception)
                {
                    return;    
                }
                var perfis = tickt.UserData.Split(';');
                if (Context.User != null)
                {
                    Context.User = new GenericPrincipal(Context.User.Identity, perfis);
                }
            }
        }

    }
}
