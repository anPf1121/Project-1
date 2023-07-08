using MySqlConnector;
using Persistence;

namespace DAL
{
    public static class ItemFilter
    {
        public const int GET_ALL = 0;
        public const int FILTER_BY_ITEM_INFORMATION = 1;
        public const int FILTER_BY_ITEM_HAVE_DISCOUNT = 2;
    }
    public class PhoneDAL
    {
        private string query = "";
        public MySqlConnection connection = DbConfig.GetConnection();
        public Phone GetItemById(int itemId)
        {
            Phone item = new Phone();
            try
            {
                query = @"select * from phones where Phone_ID=@itemId;";
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
            catch { }

            return item;
        }
        public Phone GetItem(MySqlDataReader reader)
        {
            Phone item = new Phone();
            item.PhoneID = reader.GetInt32("Phone_ID");
            item.PhoneName = reader.GetString("Phone_Name");
            item.Brand = reader.GetString("Brand");
            item.CPU = reader.GetString("CPU");
            item.Price = reader.GetDecimal("Price");
            item.DiscountPrice = reader.GetDecimal("DiscountPrice");
            item.RAM = reader.GetString("RAM");
            item.OS = reader.GetString("OS");
            item.SimSlot = reader.GetString("Sim_Slot");
            item.ScreenHz = reader.GetString("Screen_Hz");
            item.ScreenResolution = reader.GetString("Screen_Resolution");
            item.ROM = reader.GetString("ROM");
            item.StorageMemory = reader.GetString("StorageMemory");
            item.MobileNetwork = reader.GetString("Mobile_Network");
            item.Quantity = reader.GetInt32("Quantity");
            item.PhoneSize = reader.GetString("Phone_Size");
            return item;
        }
        public List<Phone> GetItems(int itemFilter, string? input)
        {
            List<Phone> lst = new List<Phone>();
            try
            {
                MySqlCommand command = new MySqlCommand("", connection);
                switch (itemFilter)
                {
                    case ItemFilter.GET_ALL:
                        query = @"SELECT * FROM phones";
                        break;
                    case ItemFilter.FILTER_BY_ITEM_INFORMATION:
                        query = @"SELECT Phone_ID, Phone_Name, Brand, Price, OS FROM phones WHERE Phone_Name LIKE @input
                OR Brand LIKE @input OR CPU LIKE @input OR RAM LIKE @input OR Battery_Capacity LIKE @input OR OS LIKE @input
                OR Sim_Slot LIKE @input OR Screen_Hz LIKE @input OR Screen_Resolution LIKE @input OR ROM LIKE @input OR Mobile_Network LIKE @input 
                OR Phone_Size LIKE @input OR Price LIKE @input OR DiscountPrice LIKE @input;";
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@input", input);
                        break;
                    case ItemFilter.FILTER_BY_ITEM_HAVE_DISCOUNT:
                        query = @"SELECT Phone_ID, Phone_Name, Brand, Price, OS FROM phones where DiscountPrice != '0';";
                        break;
                    default:
                        break;
                }
                command.CommandText = query;
                MySqlDataReader reader = command.ExecuteReader();
                lst = new List<Phone>();
                while (reader.Read())
                {
                    lst.Add(GetItem(reader));
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error! " + ex);
            }
            return lst;
        }

    }
}