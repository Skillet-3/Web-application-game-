using AutoMapper;
using BLL.Interfaces;
using BLL.Interfaces.Models;
using ChatSignalR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChatSignalR.Controllers
{
    public class HomeController : BaseController
    {
        ICharacteristicService ChService;

        public HomeController(ICharacteristicService chService)
        {
            ChService = chService;
        }

        [Authorize]
        public ActionResult RestoreHp()
        {
            ChService.RestoreHP(User.UserId);
            return RedirectToAction("Chat");
        }

        [Authorize]
        public ActionResult Chat()
        {
            return View();
        }

        public ActionResult Inventory()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<DetailedUserCharacteristics, InventoryViewModel>();
            });
            var dr = ChService.GetFullUserCharacteristics(User.UserId);

            var model = config.CreateMapper().Map<InventoryViewModel>(dr);
            model.Name = User.Identity.Name;
            return View(model);
        }

        public ActionResult Battles()
        {
            return View();
        }

        public ActionResult Fight()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<DetailedUserCharacteristics, InventoryViewModel>();
            });
            var dr = ChService.GetFullUserCharacteristics(User.UserId);
            var model = config.CreateMapper().Map<InventoryViewModel>(dr);
            model.Name = User.Identity.Name;
            var enemy_model = config.CreateMapper().Map<InventoryViewModel>(dr);
            enemy_model.Name = "Bot";
            return View(new FightViewModel() {Person = model, PersonEnemy = enemy_model });
        }

        public ActionResult FightV2()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<DetailedUserCharacteristics, InventoryViewModel>();
            });
            var dr = ChService.GetFullUserCharacteristics(User.UserId);
            var model = config.CreateMapper().Map<InventoryViewModel>(dr);
            model.Name = User.Identity.Name;
            return View("Fight",new FightViewModel() { Person = model, PersonEnemy = new InventoryViewModel() });
        }

        public ActionResult Index()
        {
             if (Request.IsAuthenticated){
                 return Redirect("/Home/Chat");
             }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
         
    }
}