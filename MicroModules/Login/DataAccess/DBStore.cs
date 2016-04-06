using MicroModules.Login.Contract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroModules.Login.DataAccess
{
   public class DBStore : IDBStore
    {
        protected string _conString;

        public DBStore(string connectionString)
        {
            _conString = connectionString;
        }

        /// <summary>
        /// Add new group
        /// </summary>
        /// <param name="newGroup"></param>
        public void AddUserGroup(IUserGroup newGroup)
        {
            SqlConnection sqlcon = new SqlConnection(_conString);

            SqlCommand sqlcmd = new SqlCommand("proc_AddGroup", sqlcon);
            sqlcmd.CommandType = System.Data.CommandType.StoredProcedure;
            sqlcmd.Parameters.Add(new SqlParameter("@Name", newGroup.AccessFormName));
            sqlcon.Open();
            sqlcmd.ExecuteNonQuery();
            sqlcon.Close();

        }

        /// <summary>
        /// Removes Group from User associations
        /// </summary>
        /// <param name="newGroup"></param>
        public void RemoveUserGroup(int userId, int groupId)
        {
            SqlConnection sqlcon = new SqlConnection(_conString);

            SqlCommand sqlcmd = new SqlCommand("proc_RemoveUserToGroup", sqlcon);
            sqlcmd.CommandType = System.Data.CommandType.StoredProcedure;
            sqlcmd.Parameters.Add(new SqlParameter("@UserId", userId));
            sqlcmd.Parameters.Add(new SqlParameter("@GroupId", groupId));
            sqlcon.Open();
            sqlcmd.ExecuteNonQuery();
            sqlcon.Close();

        }

        /// <summary>
        /// Get all User Groups
        /// </summary>
        /// <returns></returns>
        public List<IUserGroup> GetUserGroup()
        {
            DataTable dt = new DataTable();

            SqlConnection sqlcon = new SqlConnection(_conString);

            SqlCommand sqlcmd = new SqlCommand("proc_GetUserGroups", sqlcon);
            sqlcmd.CommandType = System.Data.CommandType.StoredProcedure;

            SqlDataAdapter sda = new SqlDataAdapter(sqlcmd);
            sda.Fill(dt);

            return ParseTableToGroup(dt);
        }

        /// <summary>
        /// Get User Group assignet to User
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<IUserGroup> GetUserGroup(int userId)
        {
            DataTable dt = new DataTable();

            SqlConnection sqlcon = new SqlConnection(_conString);

            SqlCommand sqlcmd = new SqlCommand("proc_GetUserGroupsByUserId", sqlcon);
            sqlcmd.CommandType = System.Data.CommandType.StoredProcedure;
            sqlcmd.Parameters.Add(new SqlParameter("@UserId", userId));
            SqlDataAdapter sda = new SqlDataAdapter(sqlcmd);
            sda.Fill(dt);

            return ParseTableToGroup(dt);
        }      

        private List<IUserGroup> ParseTableToGroup(DataTable dt)
        {
            var list = new List<IUserGroup>();
            foreach (DataRow row in dt.Rows)
            {
                var usr = new UserGroup { AccessFormName=row["Name"].ToString(), GroupId=int.Parse(row["GroupId"].ToString()) };
                list.Add(usr);
            }

            return list;
        }

        /// <summary>
        /// remove group if there are no more associations
        /// </summary>
        /// <param name="groupId"></param>
        public void RemoveGroup(int groupId)
        {
            SqlConnection sqlcon = new SqlConnection(_conString);

            SqlCommand sqlcmd = new SqlCommand("proc_DeleteGroup", sqlcon);
            sqlcmd.CommandType = System.Data.CommandType.StoredProcedure;
            sqlcmd.Parameters.Add(new SqlParameter("@groupId", groupId));
            sqlcon.Open();
            sqlcmd.ExecuteNonQuery();
            sqlcon.Close();
        }
    }
}
