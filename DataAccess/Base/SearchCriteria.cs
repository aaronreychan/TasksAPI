using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Base
{
    public abstract class SearchCriteria<T> : ISearchCriteria<T>
        where T : class
    {

        protected ISearchCriteria<T> _nextComponent;

        public SearchCriteria()
        {
            _nextComponent = null;
        }

        public SearchCriteria(ISearchCriteria<T> nextComponent)
        {
            _nextComponent = nextComponent;
        }

        public virtual IQueryable<T> BuildQueryOver(IQueryable<T> queryableBase)
        {
            if (_nextComponent != null)
                return _nextComponent.BuildQueryOver(queryableBase);
            else
                return queryableBase;
        }

    }
}
