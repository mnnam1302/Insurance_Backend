using backend.IRepositories;
using backend.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;

namespace backend.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly InsuranceDbContext _dbContext;

        public GenericRepository(InsuranceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> Get(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<bool> Exists(int id)
        {
            var entity = await Get(id);
            return entity != null;
        }

        public async Task<T> Add(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Update(T entity)
        {
            _dbContext.Set<T>().Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public IEnumerable<T> GetMultiPaging(Expression<Func<T, bool>> predicate, 
                                                    out int totalRowSelected,
                                                    out int totalRow,
                                                    out int totalPage,
                                                    int index = 1,
                                                    int size = 5, 
                                                    string[] includes = null)
        {
            int skipCount = (index - 1) * size;
            IQueryable<T> _resetSet;

            //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            if (includes != null && includes.Any())
            {
                var query = _dbContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);

                _resetSet = predicate != null ? query.Where<T>(predicate).AsQueryable() : query.AsQueryable();
                totalRow = _resetSet.Count();
            }
            else
            {
                _resetSet = predicate != null ? _dbContext.Set<T>().Where<T>(predicate).AsQueryable() : _dbContext.Set<T>().AsQueryable();
                totalRow = _resetSet.Count();
            }

            _resetSet = skipCount == 0 ? _resetSet.Take(size) : _resetSet.Skip(skipCount).Take(size);
            totalRowSelected = _resetSet.Count();
            totalPage = (int)Math.Ceiling((double)totalRow / size);
            return _resetSet.AsQueryable();
        }
    }
}
