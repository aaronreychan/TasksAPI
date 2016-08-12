using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Text;
using System.Security.Principal;

namespace DataAccess.Base
{
    public partial class BaseDbContext : DbContext
    {
        private static readonly string[] _sqlKeyWords = new[] { "SELECT ", "UPDATE ", "INSERT ", "DELETE " };

        private string traceFilePath { get; set; }
        public string LastExecutedSqlQuery { get; set; }


        #region initialiation
        public BaseDbContext(string connectionStringOrContainerName)
            : base(connectionStringOrContainerName)
        {
            Initialize();
        }

        private void Initialize()
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
        #endregion

        #region Tracing for debugging purpose
        private void AppendSqlTraceLog(string sql)
        {
            try
            {
                using (TextWriter tracefile = File.AppendText(traceFilePath))
                {
                    tracefile.WriteLine(sql.TrimEnd());
                }
            }
            catch
            {
                //if error, do nothing, swallow it
            }

            UpdateLastExecutedSQL(sql);
        }

        private void UpdateLastExecutedSQL(string sql)
        {
            if (_sqlKeyWords.Any(e => sql.Contains(e)))
                LastExecutedSqlQuery = sql;
        }

        #endregion
        
    }
}
