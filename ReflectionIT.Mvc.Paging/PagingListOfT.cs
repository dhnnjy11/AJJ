using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace ReflectionIT.Mvc.Paging {

    public class PagingList<T> : List<T>, IPagingList<T> where T : class {

        public int PageIndex { get; }
        public int PageCount { get; }
        public int TotalRecordCount { get; }
        public string Action { get; set; }
        public string PageParameterName { get; set; }
        public string SortExpressionParameterName { get; set; }
        public string SortExpression { get; }
        public string ProvinceParameterName { get; set; }
        public string SearchParameterName { get; set; }
        public string JobCategoryParameterName { get; set; }
        public string ProvinceName { get; set; }
        public string SearchString { get; set; }
        public string JobCategory { get; set; }
        public string DefaultSortExpression { get; }

        [Obsolete("Use PagingList.CreateAsync<T>() instead")]
        public static Task<PagingList<T>> CreateAsync(IOrderedQueryable<T> qry, int pageSize, int pageIndex, string actionName) {
            return PagingList.CreateAsync(qry, pageSize, pageIndex, actionName);
        }

        [Obsolete("Use PagingList.CreateAsync<T>() instead")]
        public static Task<PagingList<T>> CreateAsync(IQueryable<T> qry, int pageSize, int pageIndex, string sortExpression, string defaultSortExpression, string actionName) {
            return PagingList.CreateAsync(qry, pageSize, pageIndex, sortExpression, defaultSortExpression, actionName);
        }

        internal PagingList(List<T> list, int pageSize, int pageIndex, int pageCount, int totalRecordCount, string actionName)
            : base(list) {
            this.TotalRecordCount = totalRecordCount;
            this.PageIndex = pageIndex;
            this.PageCount = pageCount;
            //this.Action = "Index";
            this.Action = actionName;
            this.PageParameterName = "page";
            this.SortExpressionParameterName = "sortExpression";
            this.ProvinceParameterName = "provinceName";
            this.SearchParameterName = "searchString";
            this.JobCategoryParameterName = "jobCategory";

        }

        internal PagingList(List<T> list, int pageSize, int pageIndex, int pageCount, string sortExpression, string defaultSortExpression, int totalRecordCount, string actionName)
            : this(list, pageSize, pageIndex, pageCount, totalRecordCount, actionName) {

            this.SortExpression = sortExpression;
            this.DefaultSortExpression = defaultSortExpression;
        }

        public RouteValueDictionary RouteValue { get; set; }

        public RouteValueDictionary GetRouteValueForPage(int pageIndex) {

            var dict = this.RouteValue == null ? new RouteValueDictionary() :
                                                 new RouteValueDictionary(this.RouteValue);

            dict[this.PageParameterName] = pageIndex;
            if (!string.IsNullOrEmpty(ProvinceName)) {
                dict[this.ProvinceParameterName] = ProvinceName;
            }
            if (!string.IsNullOrEmpty(SearchString)) {
                dict[this.SearchParameterName] = SearchString;
            }
            if (!string.IsNullOrEmpty(JobCategory)) {
                dict[this.JobCategoryParameterName] = JobCategory;
            }

            if (this.SortExpression != this.DefaultSortExpression) {
                dict[this.SortExpressionParameterName] = this.SortExpression;
            }
            

            return dict;
        }

        public RouteValueDictionary GetRouteValueForSort(string sortExpression) {

            var dict = this.RouteValue == null ? new RouteValueDictionary() :
                                                 new RouteValueDictionary(this.RouteValue);

            if (sortExpression == this.SortExpression) {
                sortExpression = "-" + sortExpression;
            }

            dict[this.SortExpressionParameterName] = sortExpression;

            return dict;
        }

        public int NumberOfPagesToShow { get; set; } = PagingOptions.Current.DefaultNumberOfPagesToShow;

        public int StartPageIndex {
            get {
                var half = (int)((this.NumberOfPagesToShow - 0.5) / 2);
                var start = Math.Max(1, this.PageIndex - half);
                if (start + this.NumberOfPagesToShow - 1 > this.PageCount) {
                    start = this.PageCount - this.NumberOfPagesToShow + 1;
                }
                return Math.Max(1, start);
            }
        }

        public int StopPageIndex => Math.Min(this.PageCount, this.StartPageIndex + this.NumberOfPagesToShow - 1);

    }
}