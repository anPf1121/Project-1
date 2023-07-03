using MySqlConnector;
using Persistence;


namespace DAL
{
    public class PhoneDAL
    {
        private string query = "";
        public MySqlConnection connection = DbConfig.GetConnection();
        public Phone GetItemById(int itemId)
        {
            Phone item = new Phone();
            try
            {
                query = @"select phone_id, Phone_Name, Brand, Price, OS, Quantity
                        from phones where Phone_ID=@itemId;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@itemId", itemId);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    item = GetItem(reader);
                }
                reader.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return item;
        }
        public Phone GetItem(MySqlDataReader reader)
        {
            Phone item = new Phone();
            item.PhoneID = reader.GetInt32("Phone_ID");
            item.PhoneName = reader.GetString("Phone_Name");
            item.Brand = reader.GetString("Brand");
            item.Price = reader.GetDecimal("Price");
            item.OS = reader.GetString("OS");
            item.Quantity = reader.GetInt32("Quantity");
            return item;
        }
        public List<Phone> GetItems()
        {
            List<Phone> lst = new List<Phone>();
            try
            {
                MySqlCommand cmdDataBase = new MySqlCommand("SELECT Phone_ID, Phone_Name, Brand, Price, OS , Quantity FROM phones;", connection);
                MySqlDataReader DBReader = null;
                DBReader = cmdDataBase.ExecuteReader();


                while (DBReader.Read())
                {
                    lst.Add(GetItem(DBReader));
                }

                DBReader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error! " + ex);
            }
            return lst;
        }
        public List<Phone> Search(string input)
        {
            List<Phone> output = new List<Phone>();
            try
            {
                MySqlCommand command = new MySqlCommand("", connection);
                query = @"SELECT Phone_ID, Phone_Name, Brand, Price, OS, Quantity FROM phones WHERE Phone_Name LIKE @input
                OR Brand LIKE @input OR CPU LIKE @input OR RAM LIKE @input OR Battery_Capacity LIKE @input OR OS LIKE @input
                OR Sim_Slot LIKE @input OR Screen_Hz LIKE @input OR Screen_Resolution LIKE @input OR ROM LIKE @input OR Mobile_Network LIKE @input 
                OR Phone_Size LIKE @input OR Price LIKE @input OR DiscountPrice LIKE @input;";
                command.CommandText = query;
                command.Parameters.AddWithValue("@input", "%" + input + "%");
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    output.Add(GetItem(reader));
                }
                reader.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return output;
        }
        public List<Phone> GetPhoneHaveDiscount()
        {
            List<Phone> output = new List<Phone>();
            try
            {
                query = @"select * from Phones where DiscountPrice != '0';";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    output.Add(GetItem(reader));
                }
                reader.Close();
            }
            catch { }
            return output;
        }
        public bool InsertItem(Phone phone)
        {
            int check = 0;
            int phoneid = 0;
            try
            {
                query = @"select phone_id from phones where phone_name = @phonename and brand = @brand and price = @price and os = @os;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@phonename", phone.PhoneName);
                command.Parameters.AddWithValue("@brand", phone.Brand);
                command.Parameters.AddWithValue("@price", phone.Price);
                command.Parameters.AddWithValue("@os", phone.OS);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    phoneid = reader.GetInt32("Phone_ID");
                    check++;
                }
                reader.Close();
                if (check == 0)
                {
                    query = @"insert into phones(phone_name, brand, price, os, quantity) value(@phonename, @brand, @price, @os, @quantity);";
                    command.CommandText = query;
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@phonename", phone.PhoneName);
                    command.Parameters.AddWithValue("@brand", phone.Brand);
                    command.Parameters.AddWithValue("@price", phone.Price);
                    command.Parameters.AddWithValue("@os", phone.OS);
                    command.Parameters.AddWithValue("@quantity", phone.Quantity);
                    command.ExecuteNonQuery();
                }
                else
                {
                    query = @"update phones set quantity = quantity+@quan 
                    where phone_id = @phoneid;";
                    command.CommandText = query;
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@quan", phone.Quantity);
                    command.Parameters.AddWithValue("@phoneid", phoneid);
                    command.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return true;
        }
    }
}