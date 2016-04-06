using System.Data;
using MicroModules.Login.Contract;

namespace MicroModules.Login.DataAccess
{
    public interface IDBUserRepository :IDBStore
    {
        void AddUserToGroup(IUser newUser);
        int CreateUser(IUser newUser);
        void EditUser(IUser newUser);
        IUser Login(string userName, string password);
        IUser ParseUserFromTable(DataTable dt);
    }
}