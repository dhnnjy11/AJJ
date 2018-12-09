using Ajj.Core.Interface;
using Ajj.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Ajj.Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public int Save() => _context.SaveChanges();

        public int Count(Func<T, bool> predicate)
        {
            return _context.Set<T>().Where(predicate).Count();
        }

        public void Add(T t)
        {
            _context.Set<T>().Add(t);
        }

        public async Task<T> AddAsyn(T t)
        {
            await _context.Set<T>().AddAsync(t);
            return t;
        }

        public IEnumerable<T> AddList(IEnumerable<T> t)
        {
            _context.Set<T>().AddRange(t);
            return t;
        }

        public async Task<IEnumerable<T>> AddListAsyn(IEnumerable<T> t)
        {
            await _context.Set<T>().AddRangeAsync(t);
            return t;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public int Create(T entity)
        {
            _context.Add(entity);
            return Save();
        }

        public void Delete(T entity)
        {
            _context.Remove(entity);
            Save();
        }

        public void DeleteList(IEnumerable<T> entity)
        {
            _context.RemoveRange(entity);
            Save();
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public virtual async Task<ICollection<T>> FindByAsyn(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public virtual async Task<ICollection<T>> GetAllAsyn()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public int Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return Save();
        }
    }
}