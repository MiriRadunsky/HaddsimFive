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
    public class OrderService : IOrderService
    {
        private readonly DB_Manager _context;

        public OrderService()
        {
            _context = new DB_Manager();
        }

        public async Task<List<Order>> GetAllOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order> GetOrderById(int id)
        {
            return await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Order> GetOrderBySupplierId(int supplierid)
        {
            return await _context.Orders.FirstOrDefaultAsync(x => x.IdSuppliers == supplierid);
        }


        public async Task AddOrder(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task ChangeOrderStatus(int orderId, string newStatus)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == orderId);
            if (order != null)
            {
                order.Status = newStatus;
                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
            }
        }

       




    }


}
