using ECommerce.Core.Interfaces;
using ECommerce.Core.RequestParameters;
using ECommerce.Core.Wrappersz;
using ECommerce.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
namespace ECommerce.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ECommerceDbContext _context;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(ECommerceDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task<IReadOnlyList<T>> GetAllWithIncludesAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }

        public async Task<T?> GetSingleWithIncludesAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(predicate);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<PaginatedResult<T>> GetPaginatedAsync(PaginationParams paginationParams)
        {
            // Veritabanındaki toplam kayıt sayısını buluyoruz (Toplam sayfa hesabı için gerekli)
            var totalCount = await _context.Set<T>().CountAsync();

            // Skip ve Take ile sadece istenen sayfanın verilerini çekiyoruz
            var items = await _context.Set<T>()
                .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                .Take(paginationParams.PageSize)
                .ToListAsync();

            // Elde ettiğimiz verileri Core katmanında yazdığımız sarmalayıcı sınıfa koyup döndürüyoruz  
            return new PaginatedResult<T>(items, totalCount, paginationParams.PageNumber, paginationParams.PageSize);
        }
    }
}

