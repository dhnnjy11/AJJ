using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Ajj.Core.Interface
{
    public interface IRepository<T> where T : class
    {
        void Add(T t);

        Task<T> AddAsyn(T t);

        IEnumerable<T> AddList(IEnumerable<T> t);

        Task<IEnumerable<T>> AddListAsyn(IEnumerable<T> t);

        Task SaveAsync();

        IQueryable<T> GetAll();

        Task<ICollection<T>> GetAllAsyn();

        IEnumerable<T> Find(Func<T, bool> predicate);

        Task<ICollection<T>> FindByAsyn(Expression<Func<T, bool>> predicate);

        T GetById(int id);

        int Create(T entity);

        int Update(T entity);

        void Delete(T entity);

        void DeleteList(IEnumerable<T> entity);

        int Count(Func<T, bool> predicate);

        int Save();
    }
}