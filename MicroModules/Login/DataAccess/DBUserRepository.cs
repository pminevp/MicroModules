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
    public class DBUserRepository : DBStore, IDBUserRepository
    {
        public DBUserRepository(string connectionString) : base(connectionString)
        {

        }

        public int CreateUser(IUser newUser)
        {
            int userId = 0;
            SqlConnection sqlcon = new SqlConnection(_conString);

            SqlCommand sqlcmd = new SqlCommand("proc_CreateUser", sqlcon);
            sqlcmd.CommandType = System.Data.CommandType.StoredProcedure;
            sqlcmd.Parameters.Add(new SqlParameter("@UserName", newUser.UserName));
            sqlcmd.Parameters.Add(new SqlParameter("@Password", newUser.password));
            sqlcmd.Parameters.Add(new SqlParameter("@FirstName", newUser.FirstName));
            sqlcmd.Parameters.Add(new SqlParameter("@LastName", newUser.Fammily));
            sqlcon.Open();
            userId = int.Parse(sqlcmd.ExecuteScalar().ToString());
            sqlcon.Close();

            return userId;
        }

        public IUser Login(string userName, string password)
        {
            DataTable dt = new DataTable();

            SqlConnection sqlcon = new SqlConnection(_conString);

            SqlCommand sqlcmd = new SqlCommand("proc_UserLogin", sqlcon);
            sqlcmd.CommandType = System.Data.CommandType.StoredProcedure;
            sqlcmd.Parameters.Add(new SqlParameter("@UserName", userName));
            sqlcmd.Parameters.Add(new SqlParameter("@Password", password));

            SqlDataAdapter sda = new SqlDataAdapter(sqlcmd);
            sda.Fill(dt);

            IUser usr = ParseUserFromTable(dt);

            usr.permissions = GetUserGroup(usr.userId);

            return usr;
        }

        /// <summary>
        /// Edit USer profile Data
        /// </summary>
        /// <param name="newUser"></param>
        public void EditUser(IUser newUser)
        {

            SqlConnection sqlcon = new SqlConnection(_conString);
            SqlCommand sqlcmd = new SqlCommand("proc_EditUser", sqlcon);
            sqlcmd.CommandType = System.Data.CommandType.StoredProcedure;

            sqlcmd.Parameters.Add(new SqlParameter("@UserId", newUser.userId));
            sqlcmd.Parameters.Add(new SqlParameter("@FirstName", newUser.FirstName));
            sqlcmd.Parameters.Add(new SqlParameter("@LastName", newUser.Fammily));

            sqlcon.Open();
            sqlcmd.ExecuteScalar();
            sqlcon.Close();

        }

        public void AddUserToGroup(IUser newUser)
        {

            SqlConnection sqlcon = new SqlConnection(_conString);
            SqlCommand sqlcmd = new SqlCommand("proc_AddUserToGroup", sqlcon);
            sqlcmd.CommandType = System.Data.CommandType.StoredProcedure;

            foreach (IUserGroup item in newUser.permissions)
            {


                sqlcmd.Parameters.Add(new SqlParameter("@UserId", newUser.userId));
                sqlcmd.Parameters.Add(new SqlParameter("@GroupId", item.GroupId));
                sqlcon.Open();
                sqlcmd.ExecuteScalar();
                sqlcon.Close();
            }

        }

        public IUser ParseUserFromTable(DataTable dt)
        {
            return new User
            {
                userId = int.Parse(dt.Rows[0]["UserId"].ToString()),
                UserName = dt.Rows[0]["UserName"].ToString(),
                FirstName = dt.Rows[0]["FirstName"].ToString(),
                Fammily = dt.Rows[0]["LastName"].ToString()
            };
        }
    }
}
