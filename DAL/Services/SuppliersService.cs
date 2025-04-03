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
    public class SuppliersService : ISuppliersService
    {
        private readonly DB_Manager _context;

        public SuppliersService()
        {
            _context = new DB_Manager();
        }

        public async Task<List<Supplier>> GetAllSuppliers()
        {
            return await _context.Suppliers.ToListAsync();
        }

        public async Task<Supplier> GetSupplierByCompany(string company)
        {
            return await _context.Suppliers.FirstOrDefaultAsync(x => x.Company == company);
        }

        

        public async Task<Supplier> ProxyByCompanyAndPhoneNumber(string company,string phone)
        {
            return await _context.Suppliers.FirstOrDefaultAsync(x => x.Company == company&&x.PhoneNumber== phone);
        }

        public async Task AddSupplier(Supplier supplier)
        {
            await _context.Suppliers.AddAsync(supplier);
            await _context.SaveChangesAsync();
        }
       
    }
}
