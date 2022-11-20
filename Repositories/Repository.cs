using Microsoft.EntityFrameworkCore;
using SmartShopping.Data;
using SmartShopping.Models;

namespace SmartShopping.Repositories
{
    public class Repository : IRepository
    {
        private readonly DatabaseContext _databaseContext;

        public Repository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            Autosave = true;
        }

        public bool Autosave { get; set; }

        public async Task<T> Create<T>(T entity) where T : class, IEntity
        {
            entity.Id = Guid.NewGuid();
            await _databaseContext.Set<T>().AddAsync(entity);
            if (Autosave) await _databaseContext.SaveChangesAsync();
            return entity;
        }
        
        public async Task<T?> ReadAsync<T>(Guid id) where T : class, IEntity
        {
            return await _databaseContext.Set<T>().FindAsync(id);
        }

        public async Task<T> UpdateAsync<T>(T entity) where T : class, IEntity
        {
            if (Autosave) await _databaseContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync<T>(T entity) where T : class, IEntity
        {
            _databaseContext.Set<T>().Remove(entity);
            if (Autosave) await _databaseContext.SaveChangesAsync();
        }

        public async Task<T> ReadCreateNamedAsync<T>(string name) where T : class, INamedEntity, new()
        {
            return await ReadCreateNamedAsync<T>(name, _ => { });
        }

        public async Task<T> ReadCreateNamedAsync<T>(string name, CreateCallback<T> createCallback) where T : class, INamedEntity, new()
        {
            var names = Helpers.ProcessName(name);

            T? entity = await _databaseContext.Set<T>().FirstOrDefaultAsync(e => e.SimplifiedName.Equals(names.Simplified));

            if (entity == null)
            {
                entity = new T();
                entity.Id = Guid.NewGuid();
                entity.SimplifiedName = names.Simplified;
                entity.DisplayName = names.Display;

                createCallback(entity);

                await _databaseContext.Set<T>().AddAsync(entity);
                if (Autosave) await _databaseContext.SaveChangesAsync();
            }

            return entity;
        }

        public async Task<T> UpdateCreateAsync<T>(T entity) where T : class, IEntity
        {
            return await UpdateCreateAsync<T>(entity, _ => { });
        }

        public async Task<T> UpdateCreateAsync<T>(T entity, CreateCallback<T> createCallback) where T : class, IEntity
        {
            var entry = await _databaseContext.AddAsync(entity);

            if (entry.State == EntityState.Added)
            {
                entity.Id = Guid.NewGuid();
                createCallback(entity);
            }

            if (Autosave) await _databaseContext.SaveChangesAsync();

            return entity;
        }

        public IQueryable<T> Table<T>() where T : class, IEntity
        {
            return _databaseContext.Set<T>();
        }
    }
}
