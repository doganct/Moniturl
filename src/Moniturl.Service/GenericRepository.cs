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
            return await ApplySpecification(spec).CountAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            _context.Set<T>().Remove(entity);

            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllBySpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetBySpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<T> UpdateAsync(T model)
        {
            await Task.CompletedTask;
            model.UpdatedDate = DateTime.Now;

            var returnModel =  _context.Set<T>().Update(model);

            await _context.SaveChangesAsync();

            return returnModel.Entity;
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQueryable(_context.Set<T>().AsQueryable(), spec);
        }
    }
}
