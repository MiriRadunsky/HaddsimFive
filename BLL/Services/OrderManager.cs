using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;


using DAL.Api;
using BLL.Models;
using DAL.Models;
using DAL.Service;


namespace BLL.Services
{
    public class OrderManager : IOrderManager
    {
        private IOrderService _orderService;
        private ISuppliersService _suppliersService;
        private IGoodsService _goodsService;
        private IGoodsToSuppliersService _goodsToSupplierService;



        public OrderManager(IOrderService orderService, ISuppliersService isuppliersService, IGoodsService goodsService, IGoodsToSuppliersService goodsToSupplierService)
        {
            _orderService = orderService;
            _suppliersService = isuppliersService;
            _goodsService = goodsService;
            _goodsToSupplierService = goodsToSupplierService;
        }

        public async Task<bool> CreateOrder(Dictionary<string, int> goodsWithPrices, Order order)
        {
            List<GoodsToOrder> goodsList = new List<GoodsToOrder>();
            List<Good> goods = await _goodsToSupplierService.GetGoodsBySupplierId(order.IdSuppliers);

            foreach (var good in goodsWithPrices)
            {
                var goodFromDb = await _goodsService.GetGoodByName(good.Key);
                if (goodFromDb == null)
                {
                    throw new Exception("Good not found");
                }

                if (good.Value < goodFromDb.MinQuantity)
                {
                    throw new Exception($"The quantity for {good.Key} is less than the minimum required quantity of {goodFromDb.MinQuantity}");
                }

                var goodOfSupplier = goods.Any(g => g.ProductName == good.Key);
                if (!goodOfSupplier)
                {
                    throw new Exception($"The supplier does not supply the good: {good.Key}");
                }

                goodsList.Add(new GoodsToOrder()
                {
                    IdOrders = order.Id,
                    IdGoods = goodFromDb.Id,
                    Quantity = good.Value
                });
            }

            await _orderService.AddOrder(order);
            await _orderService.AddGoodsToOrder(order.Id, goodsList); // הוספת קריאה לפונקציה שמעדכנת את GoodsToOrder

            return true;
        }


        public async Task<bool> ApproveOrder(int id)
        {
            _orderService.ChangeOrderStatus(id, "approved");
            return true;
        }


        public async Task<bool> InProgressOrder(int id)
        {
            _orderService.ChangeOrderStatus(id, "in progress");
            return true;
        }
        public async Task<bool> AddGoodsToSupplier(string company, Dictionary<string, float> goodsWithQuantities, int minQuantity)
        {
            var supplier = await _suppliersService.GetSupplierByCompany(company);
            if (supplier == null)
            {
                throw new Exception("Supplier not found");
            }

            foreach (var good in goodsWithQuantities)
            {
                var goodFromDb = await _goodsService.GetGoodByName(good.Key);
                if (goodFromDb == null)
                {
                    await _goodsService.AddGood(new Good()
                    {
                        ProductName = good.Key,
                        Price = good.Value,
                        MinQuantity = minQuantity
                    });
                    goodFromDb = await _goodsService.GetGoodByName(good.Key);
                }

                await _goodsToSupplierService.AddGoodsToSupplier(supplier.Id, goodFromDb.Id);
            }

            return true;
        }


        public async Task creatSupplierAsync(Supplier supplier)
        {
            
            var existingSupplier = await _suppliersService.GetSupplierByCompany(supplier.Company);
            if (existingSupplier != null)
            {
                throw new Exception("Supplier already exists");
            }

            
            await _suppliersService.AddSupplier(supplier);
        }


        public async Task<bool> loginSupplierAsync(string comapny, string phone)
        {
            var supllier = await _suppliersService.ProxyByCompanyAndPhoneNumber(comapny, phone);
            if (supllier == null)
            {
                return false;

            }

            return true;

        }


        public async Task<List<Order>> GetAllOrders()
        {
            return await _orderService.GetAllOrders();
        }


        public async Task<int> GetIdSupplierByCompanyName(string name)
        {
            var supplier = await _suppliersService.GetSupplierByCompany(name);
            if (supplier == null)
            {
                throw new Exception("Supplier not found");
            }
            return supplier.Id;
        }


        public async Task CreatGood(Good good)
        {

            await _goodsService.AddGood(good);
        }
        //public async Task<List<Order>> GetAllOrdersWithGoods( string company)
        //{
           
           
        //    return orders;
        //}
    }
}

