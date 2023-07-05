using MySqlConnector;
using Persistence;
namespace DAL;
public class CustomerDAL{
    private MySqlConnection connection = DbConfig.GetConnection();
    private string query = "";
    public static class InformationFilter{
        public const int GET_BY_PHONE = 0;
        public const int GET_BY_ID = 1;
    }
    public Customer GetCustomer(MySqlDataReader reader){
        Customer output = new Customer();
        output.CustomerID = reader.GetInt32("Customer_ID");
        output.CustomerName = reader.GetString("Customer_Name");
        output.PhoneNumber = reader.GetString("Phone_Number");
        output.Address = reader.GetString("Address");
        output.Job = reader.GetString("Job");
        return output;
    }
    public List<Customer> GetCustomersByName(string name){
        List<Customer> output = new List<Customer>();
        try{
            query = @"select * from customers where Customer_Name like @name;";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@name", "%"+name+"%");
            MySqlDataReader reader = command.ExecuteReader();
            while(reader.Read()){
                output.Add(GetCustomer(reader));
            }
            reader.Close();
        }
        catch{}
        return output;
    }
    public Customer GetCustomerByInfo(int informationFilter, string info){
        Customer output = new Customer();
        try{
            switch(informationFilter){
                case InformationFilter.GET_BY_PHONE:
                query = @"select * from customers where Phone_Number = @phone;";
                break;
                case InformationFilter.GET_BY_ID:
                query = @"select * from customers where Customer_ID = @cusid; ";
                break;
            }
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.Clear();
            if(informationFilter == InformationFilter.GET_BY_PHONE)command.Parameters.AddWithValue("@phone", info);
            if(informationFilter == InformationFilter.GET_BY_ID)command.Parameters.AddWithValue("@cusid", Convert.ToInt32(info));
            MySqlDataReader reader = command.ExecuteReader();
            if(reader.Read())output = GetCustomer(reader);
            reader.Close();
        }
        catch(MySqlException ex){
            Console.WriteLine(ex.Message);
        }
        return output;
    }
    public bool InsertCustomer(Customer newcustomer){
        int count =0;
        try{
            query = @"select Customer_Name, Phone_Number, Address, Job from customers 
            where Customer_Name = @cusname and Phone_Number = @phonenumber;";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@cusname", newcustomer.CustomerName);
            command.Parameters.AddWithValue("@phonenumber", newcustomer.PhoneNumber);
            MySqlDataReader reader = command.ExecuteReader();
            if(reader.Read())count++;
            reader.Close();
            if(count == 0){
            query = @"insert into customers(Customer_Name, Phone_Number, Address, Job) value(@name, @phonenumber, @address, @job);";
            command.CommandText = query;
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@name", newcustomer.CustomerName);
            command.Parameters.AddWithValue("@phonenumber", newcustomer.PhoneNumber);
            command.Parameters.AddWithValue("@address", newcustomer.Address);
            command.Parameters.AddWithValue("@job", newcustomer.Job);
            command.ExecuteNonQuery();
            return true;
            }
        }
        catch(MySqlException ex){
            Console.WriteLine(ex.Message);
        }
        return false;
    }
}