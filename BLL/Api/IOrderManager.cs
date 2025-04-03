using DAL.Models;

namespace BLL.Services
{
    public interface IOrderManager
    {
        Task<bool> AddGoodsToSupplier(string company, Dictionary<string, float> goodsWithQuantities, int minQuantity);
        Task<bool> ApproveOrder(int id);
        Task<bool> CreateOrder(Dictionary<string, int> goodsWithPrices, Order order);
        Task creatSupplierAsync(Supplier supplier);
        Task<List<Order>> GetAllOrders();
        Task<int> GetIdSupplierByCompanyName(string name);
        Task<bool> InProgressOrder(int id);
        Task<bool> loginSupplierAsync(string comapny, string phone);
        Task CreatGood(Good good);
    }
}