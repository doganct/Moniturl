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

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
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

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQueryable(_context.Set<T>().AsQueryable(), spec);
        }
    }
}
