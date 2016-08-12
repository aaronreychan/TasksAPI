using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using DataAccess.Base;

namespace DataAccess.Repositories
{
    public abstract class Repository<T> : IRepository<T>
        where T : class
    {
        protected DbSet<T> _dbSet;
        protected BaseDbContext _context;
        protected int _maxReturnCount = 5000;
        public bool CheckForRelationshipIncludeslOnApplyChanges { get; set; }

        public Repository(BaseDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
            CheckForRelationshipIncludeslOnApplyChanges = true;
        }

        #region DBSet Methods AddObject/DeleteObject
        [Obsolete]
        public IEnumerable<T> Query(Expression<Func<T, bool>> filter)
        {
            return _dbSet.Where(filter).AsEnumerable<T>();
        }

        public void AddObject(T item)
        {
            _dbSet.Add(item);
        }

        public void DeleteObject(T item)
        {
            _dbSet.Remove(item);
        }
        #endregion

        #region Get Methods
        public IEnumerable<T> GetAll()
        {
            try
            {
                using (BaseDbContext context = CreateContext())
                {
                    return ApplyRelationshipLevelsOnEntityResults(ApplyRelationshipLevelsOnEntityQuery(context.Set<T>().AsQueryable()).ToList(), default(Enum));
                }
            }
            catch (Exception x)
            {
                ProcessDataException("Repository.GetAll", x);
            }
            return null;
        }
        #endregion

        #region query using Matches()
        public IEnumerable<T> Matches(ISearchCriteria<T> searchCriteria)
        {
            return PerformMatches(searchCriteria, default(Enum));
        }

        public IEnumerable<T> Matches(ISearchCriteria<T> searchCriteria, Enum includes)
        {
            return PerformMatches(searchCriteria, includes);
        }


        protected virtual IEnumerable<T> PerformMatches(ISearchCriteria<T> searchCriteria, Enum includes)
        {
            List<T> entities = new List<T>();
            try
            {
                using (BaseDbContext context = CreateContext())
                {
                    var newQueryBase = context.Set<T>().AsQueryable();
                    if (includes != null)
                        newQueryBase = ApplyIncludes(newQueryBase, includes);

                    var sqlCriteria = searchCriteria as ISqlCriteria<T>;
                    if (sqlCriteria != null && sqlCriteria.UseNativeSqlQuery)
                    {
                        entities = (context as IObjectContextAdapter).ObjectContext.ExecuteStoreQuery<T>(sqlCriteria.BuildSqlQuery(newQueryBase)).ToList();
                    }
                    else
                    {
                        entities = ApplyRelationshipLevelsOnEntityQuery(searchCriteria.BuildQueryOver(newQueryBase)).ToList();
                    }

                    return ApplyRelationshipLevelsOnEntityResults(entities, includes);
                }
            }
            catch (Exception x)
            {
                ProcessDataException("Repository.Matches", x);
            }
            return entities;
        }

        /// <summary>
        /// This method gets the query from search criteria and then imposes database business logic on it
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>
        protected virtual IQueryable<T> ApplyRelationshipLevelsOnEntityQuery(IQueryable<T> query)
        {
            return query;
        }

        protected virtual List<T> ApplyRelationshipLevelsOnEntityResults(List<T> entities, Enum includes)
        {
            if (includes != null)
            {
                //attach includes to entites and check during save
                entities.ForEach(e =>
                {
                    var entity = e as BaseEntity;
                    if (entity != null)
                    {
                        entity.Includes = includes;
                    }
                });
            }
            return entities;
        }

        protected virtual IQueryable<T> ApplyIncludes(IQueryable<T> query, Enum includes)
        {
            return query;
        }

        protected BaseDbContext CreateContext()
        {
            return Activator.CreateInstance(_context.GetType()) as BaseDbContext;
        }
        #endregion

        #region Exception Handling

        public void ProcessDataException(string methodName, Exception x)
        {

            throw new Exception(
                string.Format("DataAccess exception in method: {0},  Error Message: {1}, SQL last executed = {2}",
                        methodName,
                        x.Message,
                        ((BaseDbContext)_context).LastExecutedSqlQuery, x),
                x);
        }

        #endregion

        #region ApplyChanges(), ApplyChangesToEntity()
        public void ApplyChanges(T item)
        {
            var entity = item as BaseEntity;
            if (entity != null)
            {
                ApplyChanges(item, entity.Includes);
            }
            else
            {
                ApplyChanges(item, null);
            }
        }

        public virtual void ApplyChanges(T item, Enum includes)
        {
            if (CheckForRelationshipIncludeslOnApplyChanges)
            {
                CheckIfSaveIncludesHasGetIncludes(item, includes);
            }
            ApplyChangesToEntity(item, includes);
        }

        protected virtual void CheckIfSaveIncludesHasGetIncludes(T item, Enum saveIncludes)
        {
            var entity = item as BaseEntity;
            if (entity != null && entity.Includes != null)
            {
                if (!saveIncludes.Equals(entity.Includes))
                {
                    throw new Exception("Saving a different graph than what was fetched will delete missing entities or not save some changes. Please provide a different include enum or set CheckForRelationshipIncludeslOnApplyChanges=False or change BaseEntity.Includes.");
                }
            }
        }


        protected abstract void ApplyChangesToEntity(T item, Enum includes);

        #endregion
    }

}
