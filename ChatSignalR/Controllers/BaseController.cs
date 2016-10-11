using ChatSignalR.Modules.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChatSignalR.Controllers
{
    public class BaseController : Controller
    {
        public new CustomPrincipal User
        {
            get
            {
                return base.User as CustomPrincipal;
            }
        }
    }
}