using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace HospitalSystem
{

    public class User
    {
        private static string ConnectionString = "Server=DESKTOP-3LJTTIT\\EZZAT;Database=Hospital;User Id = sa;Password=hamid1412;";
        
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string OriginalPassword { get; set; }

        public User()
        {
            this.UserID = -1;
            this.UserName = "";
            this.Password = "";
            this.DateOfBirth = DateTime.Now;
            this.Phone = "";
            this.Email = "";
            this.Address = "";
            this.OriginalPassword = "";
        }

        private bool AddUserData(string UserName, string Password, DateTime DateOfBirth, string Phone,
            string Email, string Address, string OriginalPassword)
        {
            bool IsAdded = false;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"INSERT INTO Users (UserName, Password, DateOfBirth, Phone, Email, Address, OriginalPassword)
                             VALUES (@UserName, @Password, @DateOfBirth, @Phone, @Email, @Address, @OriginalPassword);";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@OriginalPassword", OriginalPassword);

            try
            {
                connection.Open();

                int result = command.ExecuteNonQuery();

                if (result > 0)
                {
                    IsAdded = true;
                }
                else
                {
                    IsAdded = false;
                }
            }catch(Exception ex)
            {
                IsAdded = false;
            }
            finally
            {
                connection.Close();
            }

            return IsAdded;
        }

        private static DataTable ListDataUser()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"SELECT UserID, UserName, Password, DateOfBirth, Phone, Email, Address
                             FROM Users;";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);

                    reader.Close();
                }
            }catch(System.Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        private static bool DeleteUserData(int UserID)
        {
            bool IsDeleted = false;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"DELETE FROM Users
                             WHERE UserID = @UserID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();

                int result = command.ExecuteNonQuery();

                if(result > 0)
                {
                    IsDeleted = true;
                }
                else
                {
                    IsDeleted = false;
                }
            }catch(Exception ex)
            {
                IsDeleted = false;
            }
            finally
            {
                connection.Close();
            }

            return IsDeleted;
        }

        private static bool IsExistData(string UserName, string OrignalPassword)
        {
            bool IsExist = false;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"SELECT * FROM Users;";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    if (reader["UserName"].ToString() == UserName && reader["OriginalPassword"].ToString() == OrignalPassword)
                    {
                        IsExist = true;
                        break;
                    }
                }

                reader.Close();

            }catch(Exception ex)
            {
                IsExist = false;
            }
            finally
            {
                connection.Close();
            }

            return IsExist;
        }

        private static DataTable ListAllDatabaseData()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"SELECT * FROM Users;";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);

                    reader.Close();
                }
            }
            catch (System.Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public bool Add()
        {
            if (AddUserData(this.UserName, this.Password, this.DateOfBirth, this.Phone, this.Email,
                this.Address, this.OriginalPassword))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Save()
        {
            return Add();
        }

        public static DataTable List()
        {
            return ListDataUser();
        }

        public static bool Delete(int UserID)
        {
            if (DeleteUserData(UserID))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsExist(string UserName, string Password)
        {
            return IsExistData(UserName, Password);
        }

        public static DataTable ListDatabase()
        {
            return ListAllDatabaseData();
        }
    }
}
