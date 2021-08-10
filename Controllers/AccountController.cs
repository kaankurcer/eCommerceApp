using eCommerceApp.DTOs;
using eCommerceApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace eCommerceApp.Controllers
{
    public class AccountsController : BaseApiController
    {
        [HttpPost("register")]
        public async Task<ActionResult<bool>> Register(UserDto userDto)
        {
            if (await UserExists(userDto.Username)) return BadRequest("Username is taken");

            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=eCommerceDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "INSERT INTO Users (Username, Password) VALUES (@Username, @Password)";
            sqlCmd.Connection = myConnection;

            sqlCmd.Parameters.AddWithValue("@Username", userDto.Username);
            sqlCmd.Parameters.AddWithValue("@Password", userDto.Password);
            myConnection.Open();
            int rowInserted = sqlCmd.ExecuteNonQuery();
            myConnection.Close();
            return true;
        }
    
        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(UserDto userDto)
        {
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=eCommerceDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "SELECT * FROM Users WHERE Username='" + userDto.Username + "'";
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            reader = sqlCmd.ExecuteReader();
            User user = null;
            while (reader.Read())
            {
                if (!reader.GetValue(2).ToString().Equals(userDto.Password)) return Unauthorized("Incorrect password");
                user = new User();
                user.Id = Convert.ToInt32(reader.GetValue(0));
                user.Username = reader.GetValue(1).ToString();
                user.Password = reader.GetValue(2).ToString();
                return user;
            }
            return Unauthorized("No such user exists");
        }
    
        private async Task<bool> UserExists(string username)
        {
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=eCommerceDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "SELECT * FROM Users WHERE Username='" + username + "'";
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            reader = sqlCmd.ExecuteReader();
            while (reader.Read())
            {
                if (username.Equals(reader.GetValue(1).ToString())) return true;
            }
            return false;
        } 
    }
}