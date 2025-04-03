using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Api;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Service
{
    public class GoodsToSuppliersService : IGoodsToSuppliersService
    {
        private readonly DB_Manager _context;

        public GoodsToSuppliersService()
        {
            _context = new DB_Manager();
        }


        public async Task<List<Good>> GetGoodsBySupplierId(int supplierId)
        {
            return await _context.GoodsToSuppliers
                .Where(gts => gts.IdSuppliers == supplierId)
                .Select(gts => gts.IdGoodsNavigation)
                .ToListAsync();
        }

        public async Task AddGoodsToSupplier(int supplierId, int goodsId)
        {
            var goodsToSupplier = new GoodsToSupplier()
            {
                IdGoods = goodsId,
                IdSuppliers = supplierId
            };
            await _context.GoodsToSuppliers.AddAsync(goodsToSupplier);
            await _context.SaveChangesAsync();
        }

    }
}
