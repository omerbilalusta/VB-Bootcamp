using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Vb_Base.Model;

namespace Vb_Data.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : BaseModel
    {
        List<TEntity> GetAll(params string[] includes);
        Task<TEntity> GetByIdAsync(CancellationToken cancellationToken, int id, params string[] includes);
        void Create (TEntity entity, int userId);
        void CreateRange( List<TEntity> entities, int userId);
        void Update(TEntity entity, int userId);
        bool Delete(int id, int userId);
        void Delete(TEntity entity, int userId);
        bool DeleteHard(int id);
        void DeleteHard(TEntity entity);
        IQueryable<TEntity> GetAsQueryable(params string[] includes);
        IEnumerable<TEntity> Where (Expression<Func<TEntity, bool>> expression, params string[] includes);

    }
}
