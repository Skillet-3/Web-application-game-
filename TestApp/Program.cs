using AutoMapper;
using BLL.BattleEngine.Messages;
using BLL.Helpers;
using BLL.Interfaces.BattleEngine.Messages;
using DAL.DataModels;
using DAL.Realisation;
using Newtonsoft.Json.Linq;
using StandartORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    class Program
    {

        class Test1
        {
            public int Field { get; set; }
            public IEnumerable<int> arr { get; set; }
        }

        class Test2
        {
            public int FIELD { get; set; }
            public int[] arr { get; set; }
        }


        static void Main(string[] args)
        {
            //using (var db = new GameDB())
            //{
            //    try { 
            //    var blog = new ROLE { ROLENAME = "azaza" };
            //    db.ROLES.Add(blog);
            //    db.SaveChanges();
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(ex);
            //    }
            //}
            var binder = new JsonMessageBinder();
            binder.Register<SimpleExecutableMessage>();

            var result = binder.Serialize(new SimpleExecutableMessage() { BattleId = "1", MessageKey = "key", Unit = "ddfs" });
            var jo = JObject.Parse(result);
            jo.Remove("Unit");
            var xd = binder.Deserialize(jo.ToString(), "azaza");

        }

        //static void Main(string[] args)
        //{
        //    MapperConfiguration config = new MapperConfiguration(cfg =>
        //    {
        //        cfg.CreateMap<Test1, Test2>();
        //        cfg.CreateMap<Test2, Test1>();
        //        cfg.CreateMap<string, ROLE>().AfterMap((src, dest) => dest.ROLENAME = src);
        //        cfg.CreateMap<ROLE, string>().ProjectUsing(x => x.ROLENAME);
        //        cfg.CreateMap<User, USER>().AfterMap((src,dest)=>dest.ROLES = new List<ROLE>());
        //        cfg.CreateMap<USER, User>();

        //    });


        //    Test1 t = new Test1() { Field = 10, arr = new[] { 10, 23, 45 } };
        //    Console.WriteLine(config.CreateMapper().Map<Test2>(t));
        //    Console.WriteLine(config.CreateMapper().Map<Test2>(t).FIELD);
        //    Console.WriteLine(config.CreateMapper().Map<Test2>(t).arr.Max());

        //    Test2 tt = new Test2() { FIELD = 10, arr = new[] { 10, 23, 45 } };
        //    Console.WriteLine(config.CreateMapper().Map<Test1>(tt));
        //    Console.WriteLine(config.CreateMapper().Map<Test1>(tt).Field);
        //    Console.WriteLine(config.CreateMapper().Map<Test1>(tt).arr.Max());

        //    Console.WriteLine(config.ExpressionBuilder.CreateMapExpression<Test1, Test2>());
        //    Console.WriteLine(config.ExpressionBuilder.CreateMapExpression<Test2, Test1>());
        //    Console.WriteLine(config.ExpressionBuilder.CreateMapExpression<User, USER>());
        //    Console.WriteLine(config.ExpressionBuilder.CreateMapExpression<USER, User>());
        //    USER u = new USER { EMAIL = "anton.yakouchyk@gmail.com", ID = "", PASSWORD = "password", USERNAME = "Admin", ROLES = new[] { new ROLE { ID = "1", ROLENAME = "admin" } } };
        //    Console.WriteLine(config.CreateMapper().Map<User>(u).Roles.First());

        //    var ctx = new GameDB();
        //    AbstractRepository<User, USER> repository = new UserDetailedRepository(ctx, config);
        //    string id;

        //    PasswordHash hash = new PasswordHash(u.PASSWORD);
        //    u.PASSWORD = Convert.ToBase64String(hash.ToArray());

        //    Console.WriteLine( id = repository.Add(config.CreateMapper().Map<User>(u)));



        //    //USER u2 = new USER { EMAIL = "1", ID = "4ab7d5e4-259f-4b79-b47c-39795084f840", PASSWORD = "23", USERNAME = "a1232", ROLES = new[] { new ROLE { ID = "1", ROLENAME = "user" } } };

        //    //repository.Update(config.CreateMapper().Map<User>(u2));

        //    ///repository.Delete("4ab7d5e4-259f-4b79-b47c-39795084f840");

        //    try
        //    {
        //        ctx.SaveChanges();
        //    }
        //    catch (Exception e) {
        //        Console.WriteLine(e.Message);
        //    }

        //    //Console.WriteLine(repository.Get("4ab7d5e4-259f-4b79-b47c-39795084f840").UserName);
        //    //Console.WriteLine(repository.Get(x => x.Roles.Contains("user"), x => x.UserName).Count());
        //    ctx.Dispose();
        //}
    }
}
