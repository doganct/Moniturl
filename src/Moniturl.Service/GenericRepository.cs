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
        private readonly MoniturlDbContext _context;

        public GenericRepository(MoniturlDbContext context)
        {
            _context = context;
        }

        public async Task<T> AddAsync(T model)
        {
            model.Status = true;
            model.CreatedDate = DateTime.Now;

            var returnModel = await _context.Set<T>().AddAsync(model);

            await _context.SaveChangesAsync();

            return returnModel.Entity;
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).AsNoTracking().CountAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);

            entity.Status = false;
            entity.UpdatedDate = DateTime.Now;

            _context.Set<T>().Update(entity);

            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllBySpecAsync(ISpecification<T> spec)
        {
            try
            {
                return await ApplySpecification(spec).AsNoTracking().ToListAsync();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<T> GetBySpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<T> UpdateAsync(T model)
        {
            await Task.CompletedTask;
            model.UpdatedDate = DateTime.Now;

            var returnModel = _context.Set<T>().Update(model);

            await _context.SaveChangesAsync();

            return returnModel.Entity;
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQueryable(_context.Set<T>().AsQueryable(), spec);
        }
    }
}
