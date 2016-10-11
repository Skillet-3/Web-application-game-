using BLL.Interfaces.Models;

namespace BLL.Interfaces
{
    public interface IAuthorizeService
    {
        bool UserNameExist(string username);

        bool EmailExist(string email);
        
        void CreateNewUser(DetailedUserData user, string password);

        DetailedUserData GetUser(string id);

        string[] GetUserRoles(string id);
        
        DetailedUserData GetUser(string username, string password);

        string ResolveID(string username);
    }
}