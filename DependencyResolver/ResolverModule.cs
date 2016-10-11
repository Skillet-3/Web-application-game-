using AutoMapper;
using BLL.BattleEngine;
using BLL.BattleEngine.Battles;
using BLL.BattleEngine.Messages;
using BLL.Interfaces;
using BLL.Interfaces.BattleEngine;
using BLL.Interfaces.BattleEngine.Messages;
using BLL.Services;
using DAL.DataModels;
using DAL.Interfaces;
using DAL.Realisation;
using Ninject.Modules;
using Ninject.Web.Common;
using StandartORM;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyResolver
{
    public class ResolverModule : NinjectModule
    {
        public override void Load()
        {

            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<string, ROLE>().AfterMap((src, dest) => dest.ROLENAME = src);
                cfg.CreateMap<ROLE, string>().ProjectUsing(x => x.ROLENAME);
                cfg.CreateMap<User, USER>().AfterMap((src, dest) => dest.ROLES = new List<ROLE>());
                cfg.CreateMap<USER, User>();

                cfg.CreateMap<DAL.Interfaces.DataModels.SkillSet, SkillSet>();
                cfg.CreateMap<SkillSet, DAL.Interfaces.DataModels.SkillSet>();

                cfg.CreateMap<DAL.Interfaces.DataModels.State, State>();
                cfg.CreateMap<State, DAL.Interfaces.DataModels.State>();

                cfg.CreateMap<DAL.Interfaces.DataModels.Characteristics, Characteristics>();
                cfg.CreateMap<Characteristics, DAL.Interfaces.DataModels.Characteristics>();


            });

            Bind<IRepository<DAL.Interfaces.DataModels.SkillSet>>().To<AbstractRepository<DAL.Interfaces.DataModels.SkillSet, SkillSet>>().WithConstructorArgument(config);
            Bind<IRepository<DAL.Interfaces.DataModels.State>>().To<AbstractRepository<DAL.Interfaces.DataModels.State, State>>().WithConstructorArgument(config);
            Bind<IRepository<DAL.Interfaces.DataModels.Characteristics>>().To<AbstractRepository<DAL.Interfaces.DataModels.Characteristics, Characteristics>>().WithConstructorArgument(config);

            Bind<IRepository<User>>().To<UserDetailedRepository>().InRequestScope().WithConstructorArgument(config);
            Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();

            Bind<IAuthorizeService>().To<AuthorizeService>().InRequestScope();
            Bind<ICharacteristicService>().To<CharacteristicService>().InRequestScope();

            Bind<DbContext>().To<GameDB>().InRequestScope();

            Bind<IMessageFactory>().To<MessageFactory>().InSingletonScope();
            Bind<IBattleEngine>().To<BattleEngine>().InSingletonScope();
            Bind<IBattleFactory>().To<BattleFactory>().InRequestScope();

            var binder = new JsonMessageBinder();
            binder.Register<SimpleExecutableMessage>();
            binder.Register<ADExecutableMessage>();
            Bind<JsonMessageBinder>().ToConstant(binder);

        }
    }
}
