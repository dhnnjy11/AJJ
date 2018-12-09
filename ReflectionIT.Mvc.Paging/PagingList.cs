using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReflectionIT.Mvc.Paging {

    public class PagingList {

        public static async Task<PagingList<T>> CreateAsync<T>(IOrderedQueryable<T> qry, int pageSize, int pageIndex, string actionName) where T : class {
            var totalRecordCount = await qry.CountAsync();
            var pageCount = (int)Math.Ceiling(totalRecordCount / (double)pageSize);

            return new PagingList<T>(await qry.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(),
                                        pageSize, pageIndex, pageCount, totalRecordCount, actionName);
        }

        public static async Task<PagingList<T>> CreateAsync<T>(IQueryable<T> qry, int pageSize, int pageIndex, string sortExpression, string defaultSortExpression, string actionName) where T : class {
            var totalRecordCount = await qry.CountAsync();
            var pageCount = (int)Math.Ceiling(totalRecordCount / (double)pageSize);

            return new PagingList<T>(await Extensions.OrderBy(qry, sortExpression).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(),
                                     pageSize, pageIndex, pageCount, sortExpression, defaultSortExpression, totalRecordCount, actionName);
        }

        public static PagingList<T> Create<T>(IList<T> qry, int pageSize, int pageIndex, string actionName) where T : class {
            var totalRecordCount = qry.Count();
            var pageCount = (int)Math.Ceiling(totalRecordCount / (double)pageSize);

            return new PagingList<T>(qry.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList(),
                                        pageSize, pageIndex, pageCount, totalRecordCount, actionName);
        }

        public static PagingList<T> Create<T>(IEnumerable<T> qry, int pageSize, int pageIndex, string actionName) where T : class {
            var totalRecordCount = qry.Count();
            var pageCount = (int)Math.Ceiling(totalRecordCount / (double)pageSize);

            return new PagingList<T>(qry.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList(), pageSize, pageIndex, pageCount, totalRecordCount, actionName);
        }

        public static PagingList<T> Create<T>(IEnumerable<T> qry, int pageSize, int pageIndex, string sortExpression, string defaultSortExpression, string actionName) where T : class {
            var totalRecordCount = qry.Count();
            var pageCount = (int)Math.Ceiling(totalRecordCount / (double)pageSize);

            return new PagingList<T>(qry.OrderBy(sortExpression).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList(),
                                     pageSize, pageIndex, pageCount, sortExpression, defaultSortExpression, totalRecordCount, actionName);
        }
    }
}