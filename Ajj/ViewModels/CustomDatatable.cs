using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ajj.ViewModels
{
    public class CustomDatatable<T>
    {
        /// End- JSon class sent from Datatables
        /// 
        public IList<T> YourCustomSearchFunc(DataTableAjaxPostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            var searchBy = (model.search != null) ? model.search.value : null;
            var take = model.length;
            var skip = model.start;

            string sortBy = "";
            bool sortDir = true;

            if (model.order != null)
            {
                // in this example we just default sort on the 1st column
                sortBy = model.columns[model.order[0].column].data;
                sortDir = model.order[0].dir.ToLower() == "asc";
            }

            // search the dbase taking into consideration table sorting and paging
            var result = GetDataFromDbase(searchBy, take, skip, sortBy, sortDir, out filteredResultsCount, out totalResultsCount);
            if (result == null)
            {
                // empty collection...
                return new List<T>();
            }
            return result;
        }

        public List<T> GetDataFromDbase(string searchBy, int take, int skip, string sortBy, bool sortDir, out int filteredResultsCount, out int totalResultsCount)
        {
            // the example datatable used is not supporting multi column ordering
            // so we only need get the column order from the first column passed to us.        
            //var whereClause = BuildDynamicWhereClause(Db, searchBy);

            if (String.IsNullOrEmpty(searchBy))
            {
                // if we have an empty search then just order the results by Id ascending
                sortBy = "Id";
                sortDir = true;
            }

            var result = Db.DatabaseTableEntity
                           .AsExpandable()
                           .Where(whereClause)
                           .Select(m => new T
                           {
                               Id = m.Id,
                               Firstname = m.Firstname,
                               Lastname = m.Lastname,
                               Address1 = m.Address1,
                               Address2 = m.Address2,
                               Address3 = m.Address3,
                               Address4 = m.Address4,
                               Phone = m.Phone,
                               Postcode = m.Postcode,
                           })
                           .OrderBy(sortBy, sortDir) // have to give a default order when skipping .. so use the PK
                           .Skip(skip)
                           .Take(take)
                           .ToList();

            // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
            filteredResultsCount = Db.DatabaseTableEntity.AsExpandable().Where(whereClause).Count();
            totalResultsCount = Db.DatabaseTableEntity.Count();

            return result;
        }
    }
}
