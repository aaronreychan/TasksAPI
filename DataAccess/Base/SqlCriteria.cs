using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Base
{
    public abstract class SqlCriteria<T> : SearchCriteria<T>, ISqlCriteria<T>
        where T : class
    {
        public SqlCriteria(bool useNativesql, ISearchCriteria<T> nextComponent)
            : base(nextComponent)
        {
        }

        public SqlCriteria()
            : this(false, null)
        {
        }

        public virtual bool UseNativeSqlQuery { get; set; }

        public abstract string BuildSqlQuery(IQueryable<T> queryableBase);

    }
}
