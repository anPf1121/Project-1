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
                query = @"select phone_id, Phone_Name, Brand, Price, OS
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
            catch { }

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
            return item;
        }
        public List<Phone> GetItems(int itemFilter, string? input)
        {
            List<Phone> lst = new List<Phone>();
            try
            {
                MySqlCommand command = new MySqlCommand("", connection);
                switch(itemFilter){
                    case ItemFilter.GET_ALL:
                    query = @"SELECT Phone_ID, Phone_Name, Brand, Price, OS FROM phones";
                    break;
                    case ItemFilter.FILTER_BY_ITEM_INFORMATION:
                    query =  @"SELECT Phone_ID, Phone_Name, Brand, Price, OS FROM phones WHERE Phone_Name LIKE @input
                OR Brand LIKE @input OR CPUnit LIKE @input OR RAM LIKE @input OR Battery_Capacity LIKE @input OR OS LIKE @input
                OR Sim_Slot LIKE @input OR Screen_Hz LIKE @input OR Screen_Resolution LIKE @input OR ROM LIKE @input OR Mobile_Network LIKE @input 
                OR Phone_Size LIKE @input OR Price LIKE @input OR DiscountPrice LIKE @input;";
                    break;
                    case ItemFilter.FILTER_BY_ITEM_HAVE_DISCOUNT:
                    query = @"SELECT Phone_ID, Phone_Name, Brand, Price, OS FROM phones where DiscountPrice != '0';";
                    break;
                }
                command.CommandText = query;
                if(itemFilter == ItemFilter.FILTER_BY_ITEM_INFORMATION){
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@input", input);
                }
                MySqlDataReader DBReader = command.ExecuteReader();
                while (DBReader.Read())
                {
                    lst = new List<Phone>();
                    while (DBReader.Read())
                    {
                        lst.Add(GetItem(DBReader));
                    }
                }
                DBReader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error! " + ex);
            }
            return lst;
        }
        
    }
}