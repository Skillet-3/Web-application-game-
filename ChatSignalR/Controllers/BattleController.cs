using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChatSignalR.Controllers
{
    public class BattleController : BaseController
    {
        // GET: Battle
        public ActionResult Index()
        {
            return View();
        }
    }
}