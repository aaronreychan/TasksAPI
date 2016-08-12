using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Base
{
    public interface ISearchCriteria<T> where T : class
    {
        IQueryable<T> BuildQueryOver(IQueryable<T> queryableBase);
    }
}
