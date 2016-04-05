using MicroModules.Login.Contract;
using MicroModules.Login.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroModules.Login.Managers
{
  public  class UserGroupManager
    {

        private DBStore repository;

        public UserGroupManager(string connectionString)
        {
            repository = new DBStore(connectionString);
        }

        public IUserGroup CreateGroup(string name)
        {
            IUserGroup _userGroup = new UserGroup {  Name=name};

            repository.AddUserGroup(_userGroup);

            return _userGroup;
        }


        public List<IUserGroup>GetGroups()
        {
            return repository.GetUserGroup();
        }

        public List<IUserGroup> GetGroups(int userId)
        {
            return repository.GetUserGroup(userId);
        }
    }
}
