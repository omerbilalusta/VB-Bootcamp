using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Vb_Base.Model;
using Vb_Data.Context;

namespace Vb_Data.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseModel
    {
        private readonly VbDbContext dbContext;
        public GenericRepository (VbDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Create(TEntity entity, int userId)
        {
            entity.InsertDate = DateTime.Now;
            entity.InsertUserId = userId;
            dbContext.Set<TEntity>().Add(entity);
        }

        public void CreateRange(List<TEntity> entities, int userId)
        {
            entities.ForEach(x =>
            {
                x.InsertDate = DateTime.Now;
                x.InsertUserId = userId;
            });
            dbContext.Set<TEntity>().AddRange(entities);
        }

        public bool Delete(int id, int userId)
        {
            var entity = dbContext.Set<TEntity>().Find(id);
            if(entity == null)
                return false;

            entity.IsActive = false;
            entity.UpdateDate = DateTime.Now;
            entity.UpdateUserId = userId;
            
            dbContext.Set<TEntity>().Update(entity);
            return true;
        }

        public void Delete(TEntity entity, int userId)
        {
            entity.IsActive = false;
            entity.UpdateDate = DateTime.Now;
            entity.UpdateUserId = userId;

            dbContext.Set<TEntity>().Update(entity);
        }

        public bool DeleteHard(int id)
        {
            var entity = dbContext.Set<TEntity>().Find(id);
            if (entity == null)
                return false;

            dbContext.Set<TEntity>().Remove(entity);
            return true;
        }

        public void DeleteHard(TEntity entity)
        {
            dbContext.Set<TEntity>().Remove(entity);
        }

        public List<TEntity> GetAll(params string[] includes)
        {
            var query = dbContext.Set<TEntity>().AsQueryable();
            if(includes.Any())
                query = includes.Aggregate(query, (current, incl) => current.Include(incl));

            return query.ToList();
        }

        public IQueryable<TEntity> GetAsQueryable(params string[] includes)
        {
            var query = dbContext.Set<TEntity>().AsQueryable();
            if (includes.Any())
                query = includes.Aggregate(query, (current, incl) => current.Include(incl));

            return query;
        }

        public async Task<TEntity> GetByIdAsync(CancellationToken cancellationToken, int id, params string[] includes)
        {
            var query = dbContext.Set<TEntity>().AsQueryable();
            if(includes.Any())
                query = includes.Aggregate(query, (current, includes) => current.Include(includes));

            var entity = await query.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (entity == null)
                throw new NullReferenceException("Not Found");
            
            return entity;
        }

        public void Update(TEntity entity, int userId)
        {
            dbContext.Set<TEntity>().Update(entity);
        }

        public IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> expression, params string[] includes)
        {
            var query = dbContext.Set<TEntity>().AsQueryable();
            query.Where(expression);
            if (includes.Any())
                query = includes.Aggregate(query, (current, incl) => current.Include(incl));

            return query.ToList();
        }
    }
}
