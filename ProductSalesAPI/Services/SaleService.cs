using Microsoft.EntityFrameworkCore;
using ProductSalesAPI.Data;
using ProductSalesAPI.Models;

namespace ProductSalesAPI.Services
{
    public interface ISaleService
    {
        Task<List<Sale>> GetAllAsync();
        Task<Sale> GetByIdAsync(int id);
        Task<Sale> CreateAsync(Sale sale);
    }

    public class SaleService : ISaleService
    {
        private readonly AppDbContext _context;

        public SaleService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Sale>> GetAllAsync()
        {
            return await _context.Sales
                .Include(s => s.Client)
                .Include(s => s.SaleProducts)
                    .ThenInclude(sp => sp.Product)
                .ToListAsync();
        }

        public async Task<Sale> GetByIdAsync(int id)
        {
            return await _context.Sales
                .Include(s => s.Client)
                .Include(s => s.SaleProducts)
                    .ThenInclude(sp => sp.Product)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Sale> CreateAsync(Sale sale)
        {
            sale.Date = DateTime.Now;

            // Load the client to avoid validation error
            var client = await _context.Clients.FindAsync(sale.ClientId);
            if (client != null)
            {
                sale.Client = client;
            }

            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();
            return sale;
        }
    }
}