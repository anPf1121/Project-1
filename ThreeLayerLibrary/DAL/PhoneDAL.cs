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
            connection.Open();
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
            connection.Close();
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
            item.BatteryCapacity = reader.GetString("Battery_Capacity");
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
                connection.Open();
                MySqlCommand command = new MySqlCommand("", connection);
                switch (itemFilter)
                {
                    case ItemFilter.GET_ALL:
                        query = @"SELECT * FROM phones";
                        break;
                    case ItemFilter.FILTER_BY_ITEM_INFORMATION:
                    
                        query = @"SELECT * FROM phones WHERE Phone_Name LIKE @input
                OR Brand LIKE @input OR CPU LIKE @input OR RAM LIKE @input OR Battery_Capacity LIKE @input OR OS LIKE @input
                OR Sim_Slot LIKE @input OR Screen_Hz LIKE @input OR Screen_Resolution LIKE @input OR ROM LIKE @input OR Mobile_Network LIKE @input 
                OR Phone_Size LIKE @input OR Price LIKE @input OR DiscountPrice LIKE @input;";
                        break;
                    case ItemFilter.FILTER_BY_ITEM_HAVE_DISCOUNT:
                        query = @"SELECT * FROM phones p
                        inner join phonediscount pd on p.phone_id = pd.phone_id
                        inner join discountpolicies dp on dp.policy_id = pd.policy_id
                        where p.DiscountPrice != '0' and current_timestamp() >= dp.from_date and current_timestamp() <= dp.to_date;";
                        break;
                    default:
                        break;
                }
                command.CommandText = query;
                if(itemFilter == ItemFilter.FILTER_BY_ITEM_INFORMATION){
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@input", "%"+input+"%");
                }
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
            connection.Close();
            return lst;
        }
        public bool CheckImeiExist(string imei, Phone phone){
            int count = 0;
            try{
                connection.Open();
                query = "select * from phonedetails where phone_imei = @phoneimei and phone_id = @phoneid and status = 'Not Export';";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@phoneimei", imei);
                command.Parameters.AddWithValue("@phoneid", phone.PhoneID);
                MySqlDataReader reader = command.ExecuteReader();
                if(reader.Read())count++;
                reader.Close();
                
            }
            catch(MySqlException ex){
                Console.WriteLine(ex.Message);
            }
            connection.Close();
            if(count == 1)return true;
            else return false;
        }

    }
}