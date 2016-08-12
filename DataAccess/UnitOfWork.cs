using DataAccess.Models;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UnitOfWork
    {
        private readonly CBCTestEntities _context = null;
        public string AuditUserName = string.Empty;

        public UnitOfWork()
        {
            _context = new CBCTestEntities();
        }

        private TaskRepository _taskRepo;
        public TaskRepository TaskRepo
        {
            get { return _taskRepo ?? (_taskRepo = new TaskRepository(_context)); }
        }

        public void Commit()
        {
            try
            {
                UpdateAuditFields();
                _context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new Exception(exceptionMessage, ex);
            }
        }


        void UpdateAuditFields()
        {
            //does not handle deleted currently
            IEnumerable<ObjectStateEntry> objectStateEntries =
                from ose in ((IObjectContextAdapter)_context).ObjectContext.ObjectStateManager.GetObjectStateEntries(EntityState.Added | EntityState.Modified)
                where ose.Entity != null
                select ose;


            //string currentUser = WindowsIdentity.GetCurrent().Name;     //windows logon name
            string currentUser = AuditUserName;
            if (string.IsNullOrEmpty(currentUser))
            {
                if (System.Web.HttpContext.Current != null)
                    currentUser = System.Web.HttpContext.Current.User.Identity.Name;
                else
                    currentUser = WindowsIdentity.GetCurrent().Name;
            }

            foreach (ObjectStateEntry entry in objectStateEntries)
            {
                ReadOnlyCollection<FieldMetadata> fieldsMetaData =
                    entry.CurrentValues.DataRecordInfo.FieldMetadata;

                //Update fields
                FieldMetadata modifiedField =
                    fieldsMetaData.Where(f => f.FieldType.Name.ToLower().Trim() == "updateddate").FirstOrDefault();

                if (modifiedField.FieldType != null)
                {
                    string fieldTypeName = modifiedField.FieldType.TypeUsage.EdmType.Name;

                    if (fieldTypeName == PrimitiveTypeKind.DateTime.ToString())
                    {
                        entry.CurrentValues.SetDateTime(modifiedField.Ordinal, DateTime.UtcNow);
                    }
                }

                FieldMetadata modifiedFieldUser =
                    fieldsMetaData.Where(f => f.FieldType.Name.ToLower().Trim() == "updatedby").FirstOrDefault();

                if (modifiedFieldUser.FieldType != null)
                {
                    string fieldTypeName = modifiedFieldUser.FieldType.TypeUsage.EdmType.Name;

                    if (fieldTypeName == PrimitiveTypeKind.String.ToString() && !string.IsNullOrEmpty(currentUser))
                    {
                        entry.CurrentValues.SetString(modifiedFieldUser.Ordinal, currentUser);
                    }
                }

                //Create fields
                if (entry.State == EntityState.Added)
                {
                    FieldMetadata createdField =
                        fieldsMetaData.Where(f => f.FieldType.Name.ToLower().Trim() == "createddate").FirstOrDefault();

                    if (createdField.FieldType != null)
                    {
                        string fieldTypeName = createdField.FieldType.TypeUsage.EdmType.Name;

                        if (fieldTypeName == PrimitiveTypeKind.DateTime.ToString() && string.IsNullOrEmpty(entry.CurrentValues[createdField.Ordinal].ToString()))
                        {
                            entry.CurrentValues.SetDateTime(createdField.Ordinal, DateTime.UtcNow);
                        }
                    }

                    FieldMetadata createdFieldUser =
                        fieldsMetaData.Where(f => f.FieldType.Name.ToLower().Trim() == "createdby").FirstOrDefault();

                    if (createdFieldUser.FieldType != null)
                    {
                        string fieldTypeName = createdFieldUser.FieldType.TypeUsage.EdmType.Name;

                        if (fieldTypeName == PrimitiveTypeKind.String.ToString() && string.IsNullOrEmpty(entry.CurrentValues[createdFieldUser.Ordinal].ToString()) && !string.IsNullOrEmpty(currentUser))
                        {
                            entry.CurrentValues.SetString(createdFieldUser.Ordinal, currentUser);
                        }
                    }
                }

                CreateAuditLogs(entry, currentUser);
            }

        }

        private static readonly List<String> columnNamesNotToAudit = new List<string> { "createdby", "createddate",
                                                                                        "updatedby", "updateddate" };

        private void CreateAuditLogs(ObjectStateEntry entry, string userlogin)
        {
            //no need to audit created / deleted
            //created there's going to be a created field in each table
            //we don't hard delete
            if (entry.State == EntityState.Modified)
            {
                DateTime changeTime = DateTime.UtcNow;
                var tableName = entry.EntitySet.ElementType.Name;

                for (int i = 0; i < entry.CurrentValues.DataRecordInfo.FieldMetadata.Count(); i++)
                {
                    if (!columnNamesNotToAudit.Contains(entry.CurrentValues.DataRecordInfo.FieldMetadata[i].FieldType.Name.ToLower().Trim())
                        && !object.Equals(entry.OriginalValues[i], entry.CurrentValues[i]))
                    {
                        //create audit record
                    }
                }
            }
        }
    }
}
