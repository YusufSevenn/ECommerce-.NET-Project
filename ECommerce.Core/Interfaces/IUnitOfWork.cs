using ECommerce.Core.Entities;

namespace ECommerce.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Product> Products { get; }
        IGenericRepository<Category> Categories { get; }

        Task<int> SaveAsync();
    }
}