using DAL.Models;

namespace DAL.Api
{
    public interface IOrderService
    {
        Task AddOrder(Order order);
        Task ChangeOrderStatus(int orderId, string newStatus);
        Task<List<Order>> GetAllOrders();
        Task<Order> GetOrderById(int id);
         
        Task<Order> GetOrderBySupplierId(int supplierid);
    }
}