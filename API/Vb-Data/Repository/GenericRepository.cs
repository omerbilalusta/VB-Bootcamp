using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using Vb_Base.Model;
using Vb_Data.Context;
using Vb_Data.Domain;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Vb_Data.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseModel
    {
        private readonly VbDbContext dbContext;
        public GenericRepository (VbDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<TEntity> CreateAsync(TEntity entity, int userId, CancellationToken cancellationToken)
        {
            entity.InsertDate = DateTime.Now;
            entity.InsertUserId = userId;
            await dbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
            return entity;
        }

        public async void CreateRangeAsync(List<TEntity> entities, int userId, CancellationToken cancellationToken)
        {
            entities.ForEach(x =>
            {
                x.InsertDate = DateTime.Now;
                x.InsertUserId = userId;
            });
            await dbContext.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
        }

        public async Task<bool> DeleteAsync(int id, int userId, CancellationToken cancellationToken)
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

        public async Task<bool> DeleteHardAsync(int id, CancellationToken cancellationToken)
        {
            var entity = dbContext.Set<TEntity>().Find(id);
            if (entity == null)
                return false;

            dbContext.Set<TEntity>().Remove(entity);
            return true;
        }

        public async void DeleteHardAsync(TEntity entity, CancellationToken cancellationToken)
        {
            dbContext.Set<TEntity>().Remove(entity);
        }

        public async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken, params string[] includes)
        {
            var query = dbContext.Set<TEntity>().AsQueryable().Where(x=>x.IsActive == true);
            if(includes.Any())
                query = includes.Aggregate(query, (current, incl) => current.Include(incl));

            var response = query.ToList();
            return response;
        }

        public IQueryable<TEntity> GetAsQueryable(params string[] includes)
        {
            var query = dbContext.Set<TEntity>().AsQueryable();
            if (includes.Any())
                query = includes.Aggregate(query, (current, incl) => current.Include(incl));

            return query;
        }

        public async Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken, params string[] includes)
        {
            var query = dbContext.Set<TEntity>().AsQueryable();
            if(includes.Any())
                query = includes.Aggregate(query, (current, includes) => current.Include(includes));
            
            return await query.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
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

        public IEnumerable<dynamic> DapperQuery(DateTime DateFrom, DateTime DateTo)
        {
            using (SqlConnection con = new SqlConnection("Server=(localdb)\\mssqllocaldb; Database=Vb-DB;Trusted_Connection=false;TrustServerCertificate=True;"))
            {
                con.Open();
                var query = "SELECT [o].Amount, [o].DealerId, [o].CompanyId, [p].Id, [p].Name  FROM [Order] AS [o]" +
                    " LEFT JOIN [Product] AS [p] ON [c].[Id] = [p].[CompanyId]" +
                    " WHERE [o].InsertDate BETWEEN @dateFrom AND @dateTo";

                var dateFrom = new SqlParameter("@dateFrom", SqlDbType.DateTime);
                var dateTo = new SqlParameter("@dateTo", SqlDbType.DateTime);
                dateFrom.Value = DateFrom;
                dateTo.Value = DateTo;

                var parameters = new List<SqlParameter>();
                parameters.Add(dateFrom);
                parameters.Add(dateTo);

                var products = con.Query(query, parameters).ToList();
                //var result = con.Query(query);

                con.Close();
                return products;
            }
        }
    }
}
