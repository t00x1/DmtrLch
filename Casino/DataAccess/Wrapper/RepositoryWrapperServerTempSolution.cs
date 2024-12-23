using DataAccess.Repository;
using Domain.Interfaces.Repository;
using Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Wrapper
{
    public class RepositoryWrapperServer : IRepositoryWrapper, IDisposable
    {
        private readonly IServiceScope _scope;
        private readonly CasinoContext _context;

        public RepositoryWrapperServer(IServiceScopeFactory scopeFactory)
        {
            _scope = scopeFactory.CreateScope();
            _context = _scope.ServiceProvider.GetRequiredService<CasinoContext>();
        }

        public CasinoContext casinoContext => _context;
        public IRepositoryUser User => new RepositoryUser(_context);
        public IRepositoryEmail Email => new RepositoryEmail(_context);
        public IRepositoryPictures Image => new RepositoryPictures(_context);
        public IRepositoryCupons Cupons => new RepositoryCupons(_context);
        public IRepositoryCuponsUsed CuponsUsed => new RepositoryCuponsUsed(_context);
        public IRepositoryStatistic Statistic => new RepositoryStatistic(_context);
        public IRepositoryBalance Balance => new RepositoryBalance(_context);

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _scope.Dispose();
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync(); // Асинхронно освобождаем DbContext
            Console.WriteLine("DbContext was disposed asynchronously.");
        }

        // Метод для отсоединения всех сущностей
        public void DetachAllEntities()
        {
            foreach (var entry in _context.ChangeTracker.Entries())
            {
                entry.State = EntityState.Detached;
            }
        }

        // Метод для обновления сущности
        public async Task UpdateEntityAsync(object entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}