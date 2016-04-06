
using System.Collections.Generic;
using System.Linq;

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


        /// <summary>
        /// Access to Module
        /// </summary>
        /// <param name="moduleName"></param>
        /// <returns></returns>
        public bool ModuleAccess(string moduleName)
        {
            var module = permissions.FirstOrDefault(x => x.AccessFormName == moduleName);

            if (moduleName != null && moduleName.Count() > 0)
                return true;

            return false;
        }
    }
}
