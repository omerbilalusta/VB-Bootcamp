using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Vb_Base.Model;
using Vb_Data.Domain.Report;

namespace Vb_Data.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : BaseModel
    {
        Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken, params string[] includes);
        Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken, params string[] includes);
        Task<TEntity> CreateAsync(TEntity entity, int userId, CancellationToken cancellationToken);
        void CreateRangeAsync( List<TEntity> entities, int userId, CancellationToken cancellationToken);
        void Update(TEntity entity, int userId);
        Task<bool> DeleteAsync(int id, int userId, CancellationToken cancellationToken);
        void Delete(TEntity entity, int userId);
        Task<bool> DeleteHardAsync(int id, CancellationToken cancellationToken);
        void DeleteHardAsync(TEntity entity, CancellationToken cancellationToken);
        IQueryable<TEntity> GetAsQueryable(params string[] includes);
        IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> expression, params string[] includes);
    }
}
