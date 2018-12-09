﻿using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;

namespace ReflectionIT.Mvc.Paging {
    public interface IPagingList {
        string Action { get; set; }
        RouteValueDictionary GetRouteValueForPage(int pageIndex);       
        int PageCount { get; }
        int PageIndex { get; }
        string ProvinceName { get; set; }
        string SearchString { get; set; }
        string JobCategory { get; set; }
        int TotalRecordCount { get; }
        RouteValueDictionary RouteValue { get; set; }
        string SortExpression { get; }

        int NumberOfPagesToShow { get; set; }
        int StartPageIndex { get; }
        int StopPageIndex { get; }
    }

}
