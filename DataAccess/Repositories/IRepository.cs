using DataAccess.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> Query(Expression<Func<T, bool>> filter);

        IEnumerable<T> GetAll();

        void AddObject(T item);

        void DeleteObject(T item);

        void ApplyChanges(T item);

        void ApplyChanges(T item, Enum includes);

        void ProcessDataException(string methodName, Exception x);

        IEnumerable<T> Matches(ISearchCriteria<T> searchCriteria);

        IEnumerable<T> Matches(ISearchCriteria<T> searchCriteria, Enum includes);
    }
}
