using System.Collections.Generic;
using MicroModules.Login.Contract;

namespace MicroModules.Login.DataAccess
{
    public interface IDBStore
    {
        void AddUserGroup(IUserGroup newGroup);
        List<IUserGroup> GetUserGroup();
        List<IUserGroup> GetUserGroup(int userId);
        void RemoveUserGroup(int userId, int groupId);
        void RemoveGroup(int groupId);
    }
}