using Microsoft.EntityFrameworkCore;
using Moniturl.Core;
using Moniturl.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moniturl.Service
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly MoniturlDbContext _moniturlDbContext;

        public GenericRepository(MoniturlDbContext context)
        {
            _moniturlDbContext = context;
        }

        public async Task<T> AddAsync(T model)
        {
            model.Status = true;
            model.CreatedDate = DateTime.Now;

            var returnModel = await _moniturlDbContext.Set<T>().AddAsync(model);

            await _moniturlDbContext.SaveChangesAsync();

            return returnModel.Entity;
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).AsNoTracking().CountAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await _moniturlDbContext.Set<T>().FindAsync(id);

            entity.Status = false;
            entity.UpdatedDate = DateTime.Now;

            _moniturlDbContext.Set<T>().Update(entity);

            await _moniturlDbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _moniturlDbContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllBySpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).AsNoTracking().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _moniturlDbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<T> GetBySpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<T> UpdateAsync(T model)
        {
            await Task.CompletedTask;
            model.UpdatedDate = DateTime.Now;

            var returnModel = _moniturlDbContext.Set<T>().Update(model);

            await _moniturlDbContext.SaveChangesAsync();

            return returnModel.Entity;
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQueryable(_moniturlDbContext.Set<T>().AsQueryable(), spec);
        }
    }
}
