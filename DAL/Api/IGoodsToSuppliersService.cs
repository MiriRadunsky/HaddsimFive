using DAL.Models;

namespace DAL.Api
{
    public interface IGoodsToSuppliersService
    {
        Task AddGoodsToSupplier(int supplierId, int goodsId);
        Task<List<Good>> GetGoodsBySupplierId(int supplierId);
    }
}