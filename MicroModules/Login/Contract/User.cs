
using System;
using System.Collections.Generic;


namespace MicroModules.Login.Contract
{
    public class User : IUser
    {
      
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string Fammily { get; set; }
        public string password { get; set; }
        public List<IUserGroup> permissions { get; set; }

        public int userId { get; set; }
    }
}
