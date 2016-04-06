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

        private IDBStore repository; 

        public UserGroupManager(string connectionString)
        {
            repository = new DBStore(connectionString);
        }

        public UserGroupManager(IDBStore _repository)
        {
            repository = _repository;
        }

        public IUserGroup CreateGroup(string name)
        {
            IUserGroup _userGroup = new UserGroup {  AccessFormName=name};

            repository.AddUserGroup(_userGroup);

            return _userGroup;
        }

        public List<IUserGroup>GetGroups(IUser loggedUser)
        {
            if(loggedUser.permissions==null || loggedUser.permissions.Count==0)
            {
                loggedUser.permissions= repository.GetUserGroup();
            }
            return loggedUser.permissions;
        }

        public List<IUserGroup> GetGroups(int userId)
        {
            return repository.GetUserGroup(userId);
        }

        /// <summary>
        /// Removes group if there is no associations to it 
        /// </summary>
        /// <param name="groupId"></param>
        public void RemoveGroup(int groupId)
        {
            repository.RemoveGroup(groupId);
        }
    }
}
