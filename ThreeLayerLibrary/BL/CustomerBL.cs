using System.Collections.Generic;
using Persistence;
using DAL;

namespace BL;

public class CustomerBL
{
    private CustomerDAL cdal = new CustomerDAL();
    public Customer GetOrderByPhoneNumber(string information)
    {
        return cdal.GetCustomerByInfo(0, information);
    }
    public Customer GetOrderById(string information)
    {
        return cdal.GetCustomerByInfo(1, information);
    }
    public List<Customer> GetCustomersByName(string name)
    {
        return cdal.GetCustomersByName(name);
    }
    public bool InsertCustomer(Customer newCustomer)
    {
        return cdal.InsertCustomer(newCustomer);
    }
}