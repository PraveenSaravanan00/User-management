using System.Data;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Reflection;
using UserManagementUsingMVC.Controllers;
using System.Xml.Linq;

namespace UserManagementUsingMVC.Models
{
    public class UserList
    {
        private readonly IConfiguration configuration;
        public UserList(IConfiguration config)
        {
            configuration = config;
        }

        SqlDataAdapter dataAdp;
        DataSet dataset = new DataSet();


        public object GetData()
        {
            string connectionstring = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("select id, Name, Description, Email FROM userTable", connection);
            dataAdp = new SqlDataAdapter(cmd);
            dataAdp.Fill(dataset);
            return dataset;
        }


        public void setData(string name, string desc, string email)
        {
           if (name == null) throw new ArgumentNullException("name");
           else if (desc == null) throw new ArgumentNullException("desc");
           else if (email == null) throw new ArgumentNullException("email");
            else
            {
                string connectionstring = configuration.GetConnectionString("DefaultConnectionString");
                SqlConnection connection = new SqlConnection(connectionstring);
                connection.Open();
                SqlCommand cmd = new SqlCommand("insert into userTable (Name,Description,Email)" + "VALUES(@name,@desc,@email)", connection);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@desc", desc);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void updateData(int id,string name, string desc, string email)
        {
            string connectionstring = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand cmd = new SqlCommand("UPDATE  userTable SET Name=@name, Description=@desc, Email=@email where id=@id", connection);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@desc", desc);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public void Delete(int id)
        {
            string connectionstring = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand cmd = new SqlCommand("DELETE userTable where id= @id", connection);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }
}
