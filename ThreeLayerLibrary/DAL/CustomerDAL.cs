using MySqlConnector;
using Persistence;
namespace DAL;
public class CustomerDAL{
    private MySqlConnection connection = DbConfig.GetConnection();
    private string query = "";
    public Customer GetCustomer(MySqlDataReader reader){
        Customer output = new Customer();
        output.CustomerID = reader.GetInt32("Customer_ID");
        output.CustomerName = reader.GetString("Customer_Name");
        output.PhoneNumber = reader.GetString("Phone_Number");
        output.Address = reader.GetString("Address");
        output.Job = reader.GetString("Job");
        return output;
    }
    public List<Customer> GetCustomerByName(string name){
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
    public Customer GetCustomerByPhone(string phone){
        Customer output = new Customer();
        try{
            query = @"select * from customers where Phone_Number like @phone;";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@name", "%"+phone+"%");
            MySqlDataReader reader = command.ExecuteReader();
            if(reader.Read()){
                output = GetCustomer(reader);
            }
            reader.Close();
        }
        catch{}
        return output;
    }
    public Customer GetCustomerByID(int id){
        Customer output = new Customer();
        try{
            query = @"select * from customers where customer_id = @id;";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@id", id);
            MySqlDataReader reader = command.ExecuteReader();
            if(reader.Read()){
                output = GetCustomer(reader);
            }
            reader.Close();
        }
        catch{}
        return output;
    }
    public bool AddCustomer(Customer newcustomer){
        try{
            query = @"insert into customers(Customer_Name, Phone_Number, Address, Job) value(@name, @phonenumber, @address, @job);";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@name", newcustomer.CustomerName);
            command.Parameters.AddWithValue("@phonenumber", newcustomer.PhoneNumber);
            command.Parameters.AddWithValue("@address", newcustomer.Address);
            command.Parameters.AddWithValue("@job", newcustomer.Job);
            command.ExecuteNonQuery();
        }
        catch{
            return false;
        }
        return true;
    }
}