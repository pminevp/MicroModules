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
   public class DBStore
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
            sqlcmd.Parameters.Add(new SqlParameter("@Name", newGroup.Name));
            sqlcon.Open();
            sqlcmd.ExecuteNonQuery();
            sqlcon.Close();

        }


        
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
                var usr = new UserGroup { Name=row["Name"].ToString(), GroupId=int.Parse(row["GroupId"].ToString()) };
                list.Add(usr);
            }

            return list;
        }
    }
}
