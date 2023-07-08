using MySqlConnector;
using Persistence;
using System.Text;
using System.Security.Cryptography;

namespace DAL
{
    public class StaffDAL
    {
        private MySqlConnection connection = DbConfig.GetConnection();
        private string query = "";
        public Staff? GetAccountByUsername(string userName)
        {
            Staff? staff = null;
            int count = 0;
            try
            {
                string query = @"select * from staffs where user_name = @username;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@username", userName);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    staff = GetStaff(reader);
                    count++;
                }
                reader.Close();
            }
            catch { }
            return staff;
        }
        public Staff GetStaff(MySqlDataReader reader)
        {
            Staff output = new Staff(reader.GetInt32("Staff_ID"),
                                     reader.GetString("Staff_Name"),
                                     reader.GetString("User_Name"),
                                     reader.GetString("Password"),
                                     reader.GetString("Address"),
                                     (E.Staff.Role)Enum.Parse(typeof(E.Staff.Role), reader.GetString("Role")));
            return output;
        }
        public string CreateMD5(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input); // Chuyển đổi chuỗi thành mảng byte

                byte[] hashBytes = md5.ComputeHash(inputBytes); // Tính toán MD5 hash

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2")); // Chuyển đổi mỗi byte thành chuỗi hexa và nối vào StringBuilder
                }
                return sb.ToString();
            }
        }
    }
}