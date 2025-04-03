using DAL.Models;

namespace DAL.Api
{
    public interface ISuppliersService
    {
        Task AddSupplier(Supplier supplier);
        Task<List<Supplier>> GetAllSuppliers();
        Task<Supplier> GetSupplierByCompany(string company);
        Task<Supplier> ProxyByCompanyAndPhoneNumber(string company, string phone);

    }
}