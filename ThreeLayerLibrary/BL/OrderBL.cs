using System.Collections.Generic;
using Persistence;
using DAL;

namespace BL;

public class OrderBL
{
    private OrderDAL odal = new OrderDAL();
    public Order GetOrderById(int itemId)
    {
        return odal.GetOrderByID(itemId);
    }
    public List<Phone> GetItemsInOrderByID(int id)
    {
        return odal.GetItemsInOrderByID(id);
    }
    public List<Order> GetOrderHaveProcessingStatus()
    {
        List<Order> list = odal.GetOrdersInDay(0);

        return list;
    }
    public List<Order> GetOrderHavePaidStatus()
    {
        List<Order> list = odal.GetOrdersInDay(1);
        return list;
    }
    public List<Order> GetOrderHaveExportStatus()
    {
        List<Order> list = odal.GetOrdersInDay(2);
        return list;
    }
    public bool UpdateOrderHavePaidStatus(Order order)
    {
        return odal.UpdateOrder(0, order);
    }
    public bool UpdateOrderHaveExportStatus(Order order)
    {
        return odal.UpdateOrder(1, order);
    }
    public bool InsertOrder(Order order)
    {
        return odal.InsertOrder(order);
    }
}