using ECommerce.Core.Interfaces;
using ECommerce.Infrastructure.Contexts;
using ECommerce.Core.Entities;

namespace ECommerce.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        //1. ECommerceDbContext'i tutacağımız ve DI ile alacağımız yer
        private readonly ECommerceDbContext _context;

        //2. Repository'leri hafızada tutacağımız gizli(private) değişkenler (backing fields)
        private IGenericRepository<Product> _productRepository;
        private IGenericRepository<Category> _categoryRepository;

        //Constructor Injection - ECommerceDbContext'i dışarıdan (program.cs'den) istiyoruz
        public UnitOfWork(ECommerceDbContext context)
        {
            _context = context;
        }

        //3. Lazy Initialization (ihtiyaç anında üretim)
        public IGenericRepository<Product> Products =>
            _productRepository ??= new GenericRepository<Product>(_context);

        public IGenericRepository<Category> Categories =>
            _categoryRepository ??= new GenericRepository<Category>(_context);

        //4. Bütün değişiklikleri tek bir işlemde (transaction) veritabanına yollayan metot
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        //5. Belleği temizlemek için (IDisposable implementasyonu)
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}