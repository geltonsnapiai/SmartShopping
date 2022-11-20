using SmartShopping.Data;

namespace SmartShopping.Repositories
{
    public delegate void CreateCallback<T>(T entity);

    public interface IRepository
    {
        public bool Autosave { get; set; }

        public Task<T> Create<T>(T entity) where T : class, IEntity;

        public Task<T?> ReadAsync<T>(Guid id) where T : class, IEntity;

        public Task<T> UpdateAsync<T>(T entity) where T : class, IEntity;

        public Task DeleteAsync<T>(T entity) where T : class, IEntity;

        public Task<T> ReadCreateNamedAsync<T>(string name) where T : class, INamedEntity, new();

        public Task<T> ReadCreateNamedAsync<T>(string name, CreateCallback<T> createCallback) where T : class, INamedEntity, new();

        public Task<T> UpdateCreateAsync<T>(T entity) where T : class, IEntity;

        public Task<T> UpdateCreateAsync<T>(T entity, CreateCallback<T> createCallback) where T : class, IEntity;

        public IQueryable<T> Table<T>() where T : class, IEntity;
    }
}
