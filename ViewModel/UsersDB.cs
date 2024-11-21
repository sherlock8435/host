using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace ViewModel
{
    public class UsersDB : DBFunctions
    {
        Users users = null;
        private UserList list = new UserList();
        DBFunctions dbf = new DBFunctions();

        public UsersDB() : base() { }

        private Users CreateModel(Users users)
        {
            CityDB city = new CityDB();
            users.FirstName = reader["FirstName"].ToString();
            users.LastName = reader["LastName"].ToString();
            users.UserPassword = reader["UserPassword"].ToString();
            users.Telephone = reader["Telephone"].ToString();
            users.cityId = (Cities)reader["cId"];
            users.UserEmail = reader["UserEmail"].ToString();
            users.Birthday = reader["Birthday"].ToString();
            users.Gender = reader["Gender"].ToString();
            return users;
        }
        public int DeleteUserDataByEmail(string uEmail, string uPassword)
        {
            string delSql = string.Format($"Delete from Usertbl" + $" where Uemail='{uEmail}' and Upassword='{uPassword}'");
            return dbf.ChangeTable(delSql, "DB.accdb");
        }
        public UserList SelectAllUsers()
        {
            UserList list = new UserList();
            try
            {
                string sqlStr = "SELECT * FROM Usertbl";
                cmd = GenerateOleDBCommand(sqlStr, "DB.accdb");
                conObjll.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    users = new Users();
                    list.Add(CreateModel(users));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (reader != null) reader.Close();
                if (this.conObjll.State == System.Data.ConnectionState.Open)
                    this.conObjll.Close();
            }
            return list;
        }
        public string SelectUserIdByEmail(string uEmail)
        {
            DataTable dt = null;
            string sqlStr = "SELECT Upassword FROM Usertbl where Uemail=('+ uEmail + )";
            dt = dbf.Select(sqlStr, "DB.accdb");
            if (dt == null) return "user not found";
            return dt.Rows[0][0].ToString();
        }
        public bool checkUserExist(string uPassword, string uEmail)
        {
            DataTable dt = null;
            string sqlStr = "select * from Usertbl where Uemail='" + uEmail + "' and Upassword='" + uPassword + "'";
            dt = dbf.Select(sqlStr, "DB.accdb");
            if (dt == null) return false;
            return (dt.Rows.Count > 0);
        }

        public bool CheckAdminExist(string uEmail, string uPassword)
        {
            return dbf.CheckAdmin(uEmail, uPassword);
        }
        public int UpdateUserProfile(Users users)
        {
            string updateSql = $"update Usertbl SET Upassword ='{users.UserPassword}'," + $"FirstName='{users.FirstName}', LastName='{users.LastName}'," + $"telephone ='{users.Telephone}', Birthday ='{users.Birthday}'" + $"where Uemail='{users.UserEmail}'";
            return dbf.ChangeTable(updateSql, "DB.accdb");
        }
        public int countUsers()
        {
            return SelectAllUsers().Count();
        }
    }
}
