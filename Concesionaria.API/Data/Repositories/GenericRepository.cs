using Concesionaria.API.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Concesionaria.API.Data.Repositories
{
    /// <summary>
    /// Repositorio genérico para operaciones CRUD y consultas sobre entidades de tipo <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">Tipo de la entidad.</typeparam>
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        /// <summary>
        /// Inicializa una nueva instancia del repositorio genérico.
        /// </summary>
        /// <param name="context">Contexto de base de datos de Entity Framework.</param>
        public GenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        /// <summary>
        /// Obtiene una entidad por su identificador.
        /// </summary>
        /// <param name="id">Identificador de la entidad.</param>
        /// <returns>Entidad encontrada o <c>null</c> si no existe.</returns>
        public async Task<T?> GetByIdAsync(int id) => await _dbSet.AsNoTracking().FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);

        /// <summary>
        /// Obtiene todas las entidades del tipo <typeparamref name="T"/>.
        /// </summary>
        /// <returns>Lista de entidades.</returns>
        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.AsNoTracking().ToListAsync();

        /// <summary>
        /// Agrega una nueva entidad al contexto.
        /// </summary>
        /// <param name="entity">Entidad a agregar.</param>
        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

        /// <summary>
        /// Actualiza una entidad existente en el contexto.
        /// </summary>
        /// <param name="entity">Entidad a actualizar.</param>
        public void Update(T entity) => _dbSet.Update(entity);

        /// <summary>
        /// Elimina una entidad del contexto.
        /// </summary>
        /// <param name="entity">Entidad a eliminar.</param>
        public void Delete(T entity) => _dbSet.Remove(entity);

        /// <summary>
        /// Guarda los cambios realizados en el contexto en la base de datos.
        /// </summary>
        /// <returns>Número de registros afectados.</returns>
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Obtiene una página de entidades y el total de registros.
        /// </summary>
        /// <param name="pageNumber">Número de página (comenzando en 1).</param>
        /// <param name="pageSize">Cantidad de elementos por página.</param>
        /// <returns>Tupla con la lista de entidades y el total de registros.</returns>
        public async Task<(IEnumerable<T> Items, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize)
        {
            var query = _dbSet.AsNoTracking();
            var totalCount = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return (items, totalCount);
        }

        /// <summary>
        /// Obtiene entidades que cumplen con un predicado especificado.
        /// </summary>
        /// <param name="predicate">Expresión lambda que define la condición de filtrado.</param>
        /// <returns>Lista de entidades que cumplen con el predicado.</returns>
        public async Task<IEnumerable<T>> GetByPredicateAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        /// <summary>
        /// Obtiene una proyección de las entidades según el selector especificado.
        /// </summary>
        /// <typeparam name="TResult">Tipo de resultado de la proyección.</typeparam>
        /// <param name="selector">Expresión lambda que define la proyección.</param>
        /// <returns>Lista de resultados proyectados.</returns>
        public async Task<IEnumerable<TResult>> GetSelectedAsync<TResult>(Expression<Func<T, TResult>> selector)
        {
            return await _dbSet.AsNoTracking().Select(selector).ToListAsync();
        }
    }
}