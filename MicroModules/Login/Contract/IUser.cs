using System.Collections.Generic;

namespace MicroModules.Login.Contract
{
   public interface IUser
    {
        int userId { get; set; }
        string UserName { get; set; }
        string password { get; set; }
        string FirstName { get; set; }
        string Fammily { get; set; }
        List<IUserGroup> permissions { get; set; }
     }
}
