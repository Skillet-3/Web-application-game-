using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace ChatSignalR.Modules.Authentication
{
    public static class AuthenticationHelper
    {
        public static void AuthUser(HttpResponseBase response, string username, string id, string[] roles, bool remember)
        {
            CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel();
            serializeModel.UserId = id;
            serializeModel.Roles = roles;

            string userData = JsonConvert.SerializeObject(serializeModel);
            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                    1,
                    username,
                    DateTime.Now,
                    DateTime.Now.AddHours(5),
                    remember,
                    userData
                    );

            string encTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);

            response.Cookies.Add(faCookie);
        }
    }
}