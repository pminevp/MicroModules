

using MicroModules.Login.DataAccess;

namespace MicroModules.Login.Contract
{
   public class UserManager
    {
        IDBUserRepository repository;

        public UserManager(string connectionString)
        {
            repository = new DBUserRepository(connectionString);
        }

        public UserManager(IDBUserRepository _repository)
        {
            repository = _repository;
        }

        public IUser Create(IUser newUser)
        {
            newUser.userId= repository.CreateUser(newUser);
            return newUser;
        }

        public void AddPermission(IUser newUser)
        {
            repository.AddUserToGroup(newUser);
        }

        /// <summary>
        /// Removes group permissions
        /// </summary>
        /// <param name="newUser"></param>
        /// <param name="groupId"></param>
        public void RemovePermissions(IUser newUser, int groupId)
        {
            repository.RemoveUserGroup(newUser.userId, groupId);
        }

        public IUser Login(string userName, string password)
        {
            return repository.Login(userName, password);
        }

        /// <summary>
        /// Edit current User
        /// </summary>
        /// <param name="Editeduser"></param>
        public void EditProfile(IUser Editeduser)
        {
             repository.EditUser(Editeduser);
        }

          
    }
}
