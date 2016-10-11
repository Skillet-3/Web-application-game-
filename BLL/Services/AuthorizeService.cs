using AutoMapper;
using BLL.Helpers;
using BLL.Interfaces;
using BLL.Interfaces.Models;
using DAL.DataModels;
using DAL.Interfaces;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace BLL.Services
{
    public class AuthorizeService : IAuthorizeService
    {

        IRepository<User> repository;
        IUnitOfWork unitOfWork;

        //It should be changed if we will have enough time
        private static MapperConfiguration config;

        static AuthorizeService()
        {
            config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, DetailedUserData>();
                cfg.CreateMap<DetailedUserData, User>();

            });
        }

        public AuthorizeService(IRepository<User> repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        public bool UserNameExist(string username)
        {
            try
            {
                return repository.Get(x => x.UserName == username).Count() > 0;
            }
            catch
            {
                // please add logger functionality here

                throw;
            }
        }

        public bool EmailExist(string email)
        {
            try
            {
                return repository.Get(x => x.Email == email).Count() > 0;
            }
            catch
            {
                // please add logger functionality here

                throw;
            }
        }

        /// <summary>
        /// Used to create new user
        /// It will throw an exception if it coudn't be added 
        /// Or set into the model correct id if everything was fine
        /// </summary>
        /// <param name="user"></param>
        public void CreateNewUser(DetailedUserData user, string password)
        {
            var dbUser = config.CreateMapper().Map<User>(user);
            PasswordHash hash = new PasswordHash(password);
            dbUser.Password = Convert.ToBase64String(hash.ToArray());
            try
            {
                var id = repository.Add(dbUser);
                unitOfWork.Commit();
                user.ID = id;
            }
            catch (Exception ex)
            {
                // please add logger functionality here
                throw ex;
            }
        }

        public DetailedUserData GetUser(string id)
        {
            try
            {
                return config.CreateMapper().Map<DetailedUserData>(repository.Get(id));
            }
            catch (Exception ex)
            {
                // please add logger functionality here

                throw ex;
            }
        }

        public string[] GetUserRoles(string id)
        {
            var user = repository.Get(id);
            if (user == null)
                return null;
            return user.Roles.ToArray();
        }

        /// <summary>
        /// Return User entity for current credentials
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>It will return null if user not exist</returns>
        public DetailedUserData GetUser(string username, string password)
        {
            if (UserNameExist(username))
            {
                var temp = repository.Get(x => x.UserName == username).FirstOrDefault();
                PasswordHash hash = new PasswordHash(Convert.FromBase64String(temp.Password));
                if (temp != null && hash.Verify(password))
                {
                    return config.CreateMapper().Map<DetailedUserData>(temp);
                }
            }
            return null;
        }

        public string ResolveID(string username)
        {
            var result = repository.Get(x => x.UserName == username).FirstOrDefault();
            if(result != null)
            {
                return result.ID;
            }
            return null;
        }
    }
}