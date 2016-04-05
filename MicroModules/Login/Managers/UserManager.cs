

using MicroModules.Login.DataAccess;

namespace MicroModules.Login.Contract
{
   public class UserManager
    {
        DBUserRepository repository;

        public UserManager(string connectionString)
        {
            repository = new DBUserRepository(connectionString);
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

        public IUser Login(string userName, string password)
        {
            return repository.Login(userName, password);
        }

    }
}
