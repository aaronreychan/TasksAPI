using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Base
{
    //Provide the native/dynamic SQL for searching. 
    public interface ISqlCriteria<T> : ISearchCriteria<T> where T : class
    {
        /// <summary>
        /// Gets a value indicating whether [use native SQL query]. 
        /// IF true then for this criteria instance level (or at a matching level) the BuildSqlQuery 
        /// method will be called instead of BuildQueryOver.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [use native SQL query]; otherwise, <c>false</c>.
        /// </value>
        bool UseNativeSqlQuery { get; }

        string BuildSqlQuery(IQueryable<T> queryableBase);
    }

}
