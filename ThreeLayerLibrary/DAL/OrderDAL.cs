using Persistence;
using MySqlConnector;
namespace DAL
{
    public static class OrderFilter
    {
        public const int GET_ORDER_PROCESSING_IN_DAY = 0;
        public const int GET_ORDER_PAID_IN_DAY = 1;
        public const int GET_ORDER_EXPORT_IN_DAY = 2;
    }
    public static class StatusFilter
    {
        public const int Paid = 0;
        public const int Export = 1;

    }
    public class OrderDAL
    {

        private MySqlConnection connection = DbConfig.GetConnection();
        private string query = "";
        public Order GetOrder(MySqlDataReader reader)
        {
            Order output = new Order();
            output.OrderID = reader.GetInt32("Order_ID");
            output.OrderCustomer.CustomerID = reader.GetInt32("Customer_ID");
            output.OrderSeller.StaffID = reader.GetInt32("Seller_ID");
            output.OrderAccountant.StaffID = reader.GetInt32("Accountant_ID");
            output.OrderDate = reader.GetDateTime("Order_Date");
            output.OrderStatus = reader.GetString("Order_Status");
            output.PaymentMethod = reader.GetString("Paymentmethod");
            return output;
        }
        public Order GetOrderByID(int id)
        {
            Order output = new Order();
            try
            {
                query = @"select order_id, customer_id, seller_id, accountant_id, order_date, order_status, paymentmethod
                from orders where order_id = @orderid;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@orderid", id);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    output = GetOrder(reader);
                }
                reader.Close();
                output.ListPhone = GetItemsInOrderByID(id);
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return output;
        }
        public List<Phone> GetItemsInOrderByID(int id)
        {
            PhoneDAL pdl = new PhoneDAL();
            List<Phone> currentList = new List<Phone>();
            try
            {
                query = @"select p.phone_id, p.phone_name, p.brand , p.price, p.os 
                from phones p inner join orderdetails od on p.phone_id = od.phone_id
                where od.order_id = @orderid;";
                MySqlCommand command = new MySqlCommand("", connection);
                command.CommandText = query;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@orderid", id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int count = 0;
                    Phone a = pdl.GetItem(reader);
                    foreach (var p in currentList)
                    {
                        if (p.PhoneID == a.PhoneID && p.PhoneName == a.PhoneName && p.Price == a.Price && p.Brand == a.Brand && p.OS == a.OS) count++;
                    }
                    if (count == 0) currentList.Add(a);
                }
                reader.Close();
                int countitem = 0;
                foreach (var aphone in currentList)
                {
                    int numphone = 0;
                    query = @"select count(*) from phones p 
                inner join orderdetails od on p.phone_id = od.phone_id
                where od.order_id = @orderid and p.phone_id = @phoneid;";
                    command.CommandText = query;
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@orderid", id);
                    command.Parameters.AddWithValue("@phoneid", aphone.PhoneID);
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        numphone = reader.GetInt32("count(*)");
                    }
                    reader.Close();
                    currentList[countitem].Quantity = numphone;
                    countitem++;
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return currentList;
        }
        public List<Order> GetOrders(int orderFilter)
        {
            List<Order> output = new List<Order>();
            try
            {
                switch (orderFilter)
                {
                    case OrderFilter.GET_ORDER_PROCESSING_IN_DAY:
                        query = @"select order_id, customer_id, seller_id, accountant_id, order_date, order_status, paymentmethod
                from orders where order_status = 'Unpaid' and date(order_date) = date(current_time());";
                        break;
                    case OrderFilter.GET_ORDER_PAID_IN_DAY:
                        query = @"select order_id, customer_id, seller_id, accountant_id, order_date, order_status, paymentmethod
                from orders where order_status = 'Paid' and date(order_date) = date(current_time());";
                        break;
                    case OrderFilter.GET_ORDER_EXPORT_IN_DAY:
                        query = @"select order_id, customer_id, seller_id, accountant_id, order_date, order_status, paymentmethod
                from orders where order_status = 'Export' and date(order_date) = date(current_time());";
                        break;
                }
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    output.Add(GetOrder(reader));
                }
                reader.Close();
            }
            catch { }
            return output;
        }
        public bool InsertOrder(Order order)
        {
            bool result = false;
            int countphone = 0;
            CustomerDAL cdl = new CustomerDAL();
            MySqlTransaction? tr = null;
            try
            {
                //Bat dau transaction
                tr = connection.BeginTransaction();
                MySqlCommand command = new MySqlCommand(connection, tr);
                MySqlDataReader? reader = null;
                if (cdl.InsertCustomer(order.OrderCustomer))
                {
                    // Neu insert thanh cong tuc la truoc do chua ton tai customer nay
                    // Lay ra id cua nguoi customer vua moi insert vao database
                    query = @"select customer_id from customers order by customer_id desc limit 1;";
                    command.CommandText = query;
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        order.OrderCustomer.CustomerID = reader.GetInt32("customer_id");
                    }
                    reader.Close();
                }
                else
                {
                    // Neu insert that bai thi truoc do da ton tai customer nay
                    // Lay ra id cua customer nay
                    query = @"select customer_id where customer_name = @cusname and Phone_Number = @phonenumber;";
                    command.CommandText = query;
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@cusname", order.OrderCustomer.CustomerName);
                    command.Parameters.AddWithValue("@phonenumber", order.OrderCustomer.PhoneNumber);
                    if (reader != null)
                    {
                        if (reader.Read())
                        {
                            order.OrderCustomer.CustomerID = reader.GetInt32("customer_id");
                        }
                        reader.Close();
                    }
                    else return false;
                }
                //Thuc thi Insert order vao DB
                query = @"insert into orders(customer_id, seller_id) 
            value (@cusid, @sellerid);";
                command.CommandText = query;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@cusid", order.OrderCustomer.CustomerID);
                command.Parameters.AddWithValue("@sellerid", order.OrderSeller.StaffID);
                command.ExecuteNonQuery();
                //Chon ra order vua insert vao DB
                query = @"select Order_ID from orders order by Order_ID desc limit 1; ";
                command.CommandText = query;
                reader = command.ExecuteReader();
                //Lay ra id cua order vua moi insert
                if (reader.Read())
                {
                    order.OrderID = reader.GetInt32("Order_ID");
                }
                reader.Close();
                //Kiem tra cac mat hang co trong Cart
                foreach (var phone in order.ListPhone)
                {

                    int phoneid = 0;
                    int quantity = 0;
                    int countPhoneNotOrderYet = 0;
                    try
                    {
                        query = @"select phone_id, Quantity from phones where phone_name = @name 
                    and brand = @brand and price = @price and os = @os;";
                        command.CommandText = query;
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@name", phone.PhoneName);
                        command.Parameters.AddWithValue("@brand", phone.Brand);
                        command.Parameters.AddWithValue("@price", phone.Price);
                        command.Parameters.AddWithValue("@os", phone.OS);
                        reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            phoneid = reader.GetInt32("Phone_ID");
                            quantity = reader.GetInt32("Quantity");
                            countPhoneNotOrderYet++;
                        }
                        reader.Close();
                        // check ton tai trong DB
                        if (countPhoneNotOrderYet > 0)
                        {
                            //neu ton tai thi kiem tra so luong co hop le hay khong
                            if (phone.Quantity > quantity)
                            {
                                result = false;
                                break;
                            }
                            else
                            {
                                query = @"insert into orderdetails(order_id, phone_id, phone_imei) 
                                value(@orderid, @phoneid, left(uuid(), 15));";
                                command.CommandText = query;
                                command.Parameters.Clear();
                                command.Parameters.AddWithValue("@orderid", order.OrderID);
                                command.Parameters.AddWithValue("@phoneid", phoneid);
                                for (int i = 0; i < phone.Quantity; i++) command.ExecuteNonQuery();
                                countphone++;
                            }

                        }
                        else
                        {
                            //Khong ton tai trong DB
                            result = false;
                            break;
                        }
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                if (countphone == order.ListPhone.Count() && order.ListPhone.Count() > 0) result = true;
                else
                {
                    result = false;
                }
                if (result == true)
                {
                    tr.Commit();
                }
                else tr.Rollback();

            }
            catch (MySqlException ex)
            {
                try
                {
                    if (tr != null)
                        tr.Rollback();
                    result = false;
                }
                catch (MySqlException ex1)
                {
                    Console.WriteLine(ex1.Message);
                }
                Console.WriteLine(ex.Message);
            }
            return result;
        }
        public bool UpdateOrder(int statusFilter, Order order)
        {
            try
            {
                MySqlCommand command = new MySqlCommand("", connection);
                switch (statusFilter)
                {
                    case StatusFilter.Paid:
                        query = @"update orders set accountant_id = @accountantid, order_status = @orderstatus, paymentmethod = @paymentmethod 
                where order_id = @orderid;";
                        command.CommandText = query;
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@accountantid", order.OrderAccountant.StaffID);
                        command.Parameters.AddWithValue("@orderstatus", order.OrderStatus);
                        command.Parameters.AddWithValue("@paymentmethod", order.PaymentMethod);
                        command.Parameters.AddWithValue("@orderid", order.OrderID);
                        command.ExecuteNonQuery();
                        break;
                    case StatusFilter.Export:
                        query = @"update orders set order_status = @orderstatus where order_id = @orderid;";
                        command.CommandText = query;
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@orderstatus", order.OrderStatus);
                        command.Parameters.AddWithValue("@orderid", order.OrderID);
                        command.ExecuteNonQuery();
                        break;
                    default:
                        return false;
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
