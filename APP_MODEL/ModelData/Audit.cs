using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using APP_MODEL;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Common;
using System.Text.RegularExpressions;

namespace APP_MODEL.ModelData
{
    public partial class ModelEntitiesWebsite : DbContext
    {
        //public ModelEntitiesWebsite(string type)
        //    : base("name=ModelEntitiesWebsite")
        //{
        //}
         
        public string ErrorMessage = "";
        public int TimeoutTime = 1000000000;

         
        public void NoTimeoutModeStatic()
        {
            this.Configuration.LazyLoadingEnabled = false;
            var objectContext = (this as IObjectContextAdapter).ObjectContext;
            // Sets the command timeout for all the commands
            objectContext.CommandTimeout = Convert.ToInt32(TimeoutTime);
        }

        public void Sleep(DateTime Now,int seconds)
        {
            int i = 0;
            while (DateTime.Now < Now.AddSeconds(seconds))
            {
                i++;
            }
        }

        public void NoTimeoutMode()
        {
            this.Configuration.LazyLoadingEnabled = false;
            var objectContext = (this as IObjectContextAdapter).ObjectContext;
            // Sets the command timeout for all the commands
            objectContext.CommandTimeout = Convert.ToInt32(this.tbl_SysParam.Where(p=>p.Param_Code == "Entities_DB_Timeout").FirstOrDefault().Value);
        }  

        public async Task<int> DatabaseSaveAsync(string username)
        {
            string strMessage = "";
            Guid VersionId = Guid.NewGuid();
            try
            {
                // Get all Added/Deleted/Modified entities (not Unmodified or Detached)
                List<string> tbl_Language = this.tbl_Language.Where(p => p.Is_Audit == true).Select(p => p.Table_Name).Distinct().ToList();

                foreach (var ent in this.ChangeTracker.Entries().Where(p => p.State == EntityState.Added || p.State == EntityState.Deleted || p.State == EntityState.Modified))
                {
                    // For each changed record, get the audit record entries and add them

                    foreach (tbl_Audit_Trail x in GetAuditRecordsForChange(ent, username, VersionId, tbl_Language))
                    {
                        this.tbl_Audit_Trail.Add(x);
                    }
                }
            }
            catch (Exception ex)
            {
                long ErrlogResult = Error.LogException(ex, "DatabaseSaveAsync", out strMessage);
            }
            // Call the original SaveChanges(), which will save both the changes made and the audit records
            return await base.SaveChangesAsync();

        }
        //public virtual int SaveWithLog();

        public string DatabaseSave(string username)
        {
            string strMessage = "";
            string Status = "";
            Guid VersionId = Guid.NewGuid();
            string HeaderId = "";
            bool flagSaveChanges = false;
            bool flagRollback = false;
            //List<Guid> lstRecordAudit = new List<Guid>();

            try
            {
                // Get all Added/Deleted/Modified entities (not Unmodified or Detached)
                foreach (var ent in this.ChangeTracker.Entries().Where(p => p.State == EntityState.Added || p.State == EntityState.Deleted || p.State == EntityState.Modified))
                {
                    flagSaveChanges = true;
                    // For each changed record, get the audit record entries and add them
                    var authorize_stat = new tbl_Audit_Trail();
                    List<string> tbl_Language = this.tbl_Language.Where(p => p.Is_Audit == true).Select(p => p.Table_Name).Distinct().ToList();

                    List<tbl_Audit_Trail> lstAuditTrail = GetAuditRecordsForChange(ent, username, VersionId, tbl_Language);
                    List<tbl_Audit_Trail> lstSortedAudit = new List<tbl_Audit_Trail>();

                    foreach (tbl_Audit_Trail x in lstAuditTrail)
                    {
                        lstSortedAudit.Add(x);

                        if (lstAuditTrail.IndexOf(x) == lstAuditTrail.Count - 1)
                        {
                            authorize_stat = lstSortedAudit.Where(s => s.Column_Name == "Authorize_Status").FirstOrDefault();
                            var recordStatus = lstSortedAudit.Where(s => s.Column_Name == "Status_Code").FirstOrDefault();

                            int? currentVersion = null;
                            string prevRecordStatus = "";

                            if (x.Version == null)
                            {
                                NoLockInterceptor.ApplyNoLock = true; 
                                var dbcurrentVersion = this.tbl_Audit_Trail.Where(p => p.Record_ID == x.Record_ID).OrderByDescending(o => o.Version).FirstOrDefault();
                                if (dbcurrentVersion != null)
                                {
                                    currentVersion = dbcurrentVersion.Version;
                                }
                                var prevRecord = this.tbl_Audit_Trail.Where(p => p.Record_ID == x.Record_ID && p.Column_Name == "Status_Code").OrderByDescending(o => o.Created_DateTime).FirstOrDefault();
                                if (prevRecord != null)
                                {
                                    prevRecordStatus = prevRecord.Value_After;
                                }
                                NoLockInterceptor.ApplyNoLock = false; 
                            }

                            var Header = lstSortedAudit.FirstOrDefault();
                            //var Header_Table = this.tbl_SysParam.Where(p => p.Param_Code == "HeaderTable").FirstOrDefault();
                            //string[] List_Header = Header_Table.Value.Split(',');
                            if (Header != null)
                            {
                                NoLockInterceptor.ApplyNoLock = true; 
                                var result = this.tbl_Config_Table_Header.Where(p=>p.Table_Header == Header.Url_Menu).Count();
                                NoLockInterceptor.ApplyNoLock = false; 
                                if (result > 0)
                                    HeaderId = Header.Record_ID;
                            }

                            //set header id
                            SetHeaderId(ref lstSortedAudit, HeaderId);
                            if (authorize_stat != null)
                                authorize_stat.Header_Id = HeaderId;
                            if (recordStatus != null)
                                recordStatus.Header_Id = HeaderId;

                            if (authorize_stat != null)
                            {
                                if (authorize_stat.Value_Before != null && authorize_stat.Value_After != null)
                                {
                                    if (authorize_stat.Value_Before == "0" && authorize_stat.Value_After == "1")
                                    {
                                        //case authorize

                                        //lstSortedAudit.Remove(authorize_stat); 
                                        //this.tbl_Audit_Trail.AddRange(lstSortedAudit);
                                        //base.SaveChanges();
                                        //this.tbl_Audit_Trail.Add(authorize_stat);
                                        //base.SaveChanges();
                                        foreach (var item in lstSortedAudit)
                                        {
                                            item.Version = currentVersion;
                                        }
                                        this.tbl_Audit_Trail.AddRange(lstSortedAudit);
                                    }
                                    else if (authorize_stat.Value_Before == "1" && authorize_stat.Value_After == "0")
                                    {
                                        if (recordStatus != null && recordStatus.Value_After == "999")
                                        {
                                            //case delete after authorize -> not call rollback

                                            //lstSortedAudit.Remove(authorize_stat);
                                            //lstSortedAudit.Remove(recordStatus);
                                            //authorize_stat.Event = "R";
                                            //this.tbl_Audit_Trail.Add(authorize_stat);
                                            //base.SaveChanges();
                                            //this.tbl_Audit_Trail.AddRange(lstSortedAudit);
                                            //base.SaveChanges();
                                            //this.tbl_Audit_Trail.Add(recordStatus);
                                            //base.SaveChanges();
                                            foreach (var item in lstSortedAudit)
                                            {
                                                item.Version = currentVersion;
                                            }
                                            this.tbl_Audit_Trail.AddRange(lstSortedAudit);
                                        }
                                        else
                                        {
                                            //case save edit after authorize
                                            //lstSortedAudit.Remove(authorize_stat);

                                            //check if prev deleted/rejected --> value before from value after edited previous version
                                            foreach (var item in lstSortedAudit)
                                            {
                                                if (prevRecordStatus == "999")
                                                    item.Version = currentVersion;
                                                else
                                                    item.Version = currentVersion + 1;

                                                //remark by ali for delete after add>approve>edit>reject>edit>delete (rollback jd salah ke versi 2 edit)
                                                //var valbefore = this.tbl_Audit_Trail.Where(p => p.Column_Name == item.Column_Name && p.Record_ID == item.Record_ID).OrderByDescending(o => o.Create_Date).FirstOrDefault();
                                                //if (valbefore != null && !String.IsNullOrEmpty(item.Value_Before) && !String.IsNullOrEmpty(item.Value_After))
                                                //{
                                                //    if (!String.IsNullOrEmpty(valbefore.Value_Before) && !String.IsNullOrEmpty(valbefore.Value_After))
                                                //    {
                                                //        item.Value_Before = valbefore.Value_After;
                                                //    }
                                                //}

                                            }
                                            this.tbl_Audit_Trail.AddRange(lstSortedAudit);

                                            //this.tbl_Audit_Trail.Add(authorize_stat);
                                            //base.SaveChanges();
                                            //this.tbl_Audit_Trail.AddRange(lstSortedAudit);
                                            //base.SaveChanges();                                                                       
                                        }
                                    }
                                }
                                else
                                {
                                    //case add
                                    this.tbl_Audit_Trail.AddRange(lstSortedAudit);
                                }
                            }
                            else
                            {
                                //case modified without authorize   
                                #region remark temporary
                                //if (recordStatus != null && recordStatus.Value_After == "2")
                                //{
                                //    //case reject
                                //    //lstSortedAudit.Remove(recordStatus);
                                //    //this.tbl_Audit_Trail.AddRange(lstSortedAudit);
                                //    //base.SaveChanges();
                                //    //this.tbl_Audit_Trail.Add(recordStatus);
                                //    //base.SaveChanges();
                                //    foreach (var item in lstSortedAudit)
                                //    {
                                //        item.Version = currentVersion;
                                //    }
                                //    this.tbl_Audit_Trail.AddRange(lstSortedAudit);
                                //    //call sp_rollback
                                //    this.SP_ROLLBACK(HeaderId);
                                //}
                                //else if (recordStatus != null && recordStatus.Value_After == "999")
                                //{
                                //    //case delete on unauthorize status -> call rollback
                                //    //lstSortedAudit.Remove(recordStatus);
                                //    //recordStatus.Event = "R";
                                //    //this.tbl_Audit_Trail.AddRange(lstSortedAudit);
                                //    //base.SaveChanges();
                                //    //this.tbl_Audit_Trail.Add(recordStatus);
                                //    //base.SaveChanges();
                                //    foreach (var item in lstSortedAudit)
                                //    {
                                //        item.Version = currentVersion;
                                //    }
                                //    this.tbl_Audit_Trail.AddRange(lstSortedAudit);
                                //    //call sp_rollback
                                //    this.SP_ROLLBACK(HeaderId);
                                //}
                                //else
                                //{
                                //    this.tbl_Audit_Trail.AddRange(lstSortedAudit);
                                //}

                                if (recordStatus != null)
                                {
                                    if (recordStatus.Value_After == "999")
                                    {
                                        flagRollback = true;
                                    }
                                    if (recordStatus.Value_After == "2")
                                    {
                                        flagRollback = true;
                                    }
                                }
                                #endregion

                                foreach (var item in lstSortedAudit)
                                {
                                    item.Version = currentVersion;
                                }
                                this.tbl_Audit_Trail.AddRange(lstSortedAudit);
                            }

                        }
                    }

                }
            }
            catch (DbUpdateException E)
            {
                flagSaveChanges = false;
                Status = "System Request Timeout"; 
                long ErrlogResult = Error.LogException(E, "DATABASEDAVE:AUDIT", out strMessage);
            }
            catch (Exception E)
            {
                flagSaveChanges = false;
                Status = "System Temporary Unreachable"; 
                long ErrlogResult = Error.LogException(E, "DATABASEDAVE:AUDIT", out strMessage);
            }
            // Call the original SaveChanges(), which will save both the changes made and the audit records 
            //using (var dbTransaction = this.Database.BeginTransaction())
            //{
                try
                {
                    if (flagSaveChanges)
                    {
                    try
                    {
                        NoTimeoutModeStatic();
                    }
                    catch (Exception Ex)
                    {
                        Status = "System Temporary Unreachable";
                        Error.LogException(Ex, "DATABASEDAVE:AUDIT NoTimeoutModeStatic()", out strMessage);
                    }
                         
                    //menjaga dari deadlock table
                    int hit = 0; 
                    retry:
                    try
                    {
                        hit++;
                        //dbTransaction.Commit(); 
                        base.SaveChanges();
                    }
                    catch 
                    {
                        if (hit <= 3)
                        {
                            System.Threading.Thread.Sleep(3000);
                            goto retry;
                        }
                    }
                     
                    this.SP_UPDATE_HEADER_ID_AUDIT(VersionId, HeaderId);
                    //UpdateHeaderId(lstRecordAudit, HeaderId); 
                    //call sp_rollback (validasi rollback ada di dalam sp):
                    //1. jika status unauthorize dan record status delete -> rollback
                    //2. jika status unauthorize dan record status reject -> rollback
                    if (flagRollback)
                    {
                        this.SP_ROLLBACK(HeaderId);
                    }
                     
                    Status = "0";
                    return Status;
                    }
                }
                catch (DbEntityValidationException E)
                {
                    Status = "Failed";
                    // Retrieve the error messages as a list of strings.
                    var errorMessages = E.EntityValidationErrors
                            .SelectMany(x => x.ValidationErrors)
                            .Select(x => x.ErrorMessage);

                    // Join the list to a single string.
                    var fullErrorMessage = string.Join("; ", errorMessages);
                    // Combine the original exception message with the new one.
                    var exceptionMessage = string.Concat(E.Message, " The validation errors are: ", fullErrorMessage); 
                    Status = exceptionMessage;
                    long ErrlogResult = Error.LogException(E, "DATABASEDAVE:AUDIT (DbEntityValidationException E", out strMessage);
                    //dbTransaction.Rollback();
                   
                }
                catch (DbUpdateException E)
                {
                    Status = "System Request Timeout";
                    long ErrlogResult = Error.LogException(E, "DATABASEDAVE:AUDIT DbUpdateException E", out strMessage); 
                    //dbTransaction.Rollback(); 
                }
                catch (Exception E)
                {
                    Status = "System Request Timeout";
                    long ErrlogResult = Error.LogException(E, "DATABASEDAVE:AUDIT Exception E", out strMessage);
                    //dbTransaction.Rollback(); 
                }
            //}
            return Status;
        }

        private void RollbackAudit(Guid? VersionId)
        {

        }


        private void SetHeaderId(ref List<tbl_Audit_Trail> Listaudit, string headerId)
        {
            foreach (var item in Listaudit)
            {
                item.Header_Id = headerId;
            }
        }

        private void UpdateHeaderId(List<Guid> ListId, string headerId)
        {
            string strId = "";
            if (ListId.Count() > 0)
            {
                foreach (var item in ListId)
                {
                    if (strId.Length == 0)
                        strId = "'" + item.ToString() + "'";
                    else
                        strId = strId + "," + "'" + item.ToString() + "'";
                }
                strId = strId.Substring(0, strId.Length - 1);
                // this.SP_UPDATE_HEADER_ID_AUDIT(strId, headerId);
            }

        }

        private List<tbl_Audit_Trail> GetAuditRecordsForChange(DbEntityEntry dbEntry, string userId, Guid versionId, List<string> tbl_Language)
        {
            List<tbl_Audit_Trail> result = new List<tbl_Audit_Trail>();

            DateTime changeTime = DateTime.Now;

            // Get the Table() attribute, if one exists
            //TableAttribute tableAttr = dbEntry.Entity.GetType().GetCustomAttributes(typeof(TableAttribute), false).SingleOrDefault() as TableAttribute;

            TableAttribute tableAttr = dbEntry.Entity.GetType().GetCustomAttributes(typeof(TableAttribute), true).SingleOrDefault() as TableAttribute;

            // Get table name (if it has a Table attribute, use that, otherwise get the pluralized name)
            string tableName = tableAttr != null ? tableAttr.Name : dbEntry.Entity.GetType().Name;

            if (tableName.Length > 44)
                tableName = dbEntry.Entity.GetType().BaseType.Name;

            if (tbl_Language.Contains(tableName))
            {

                // Get primary key value (If you have more than one key column, this will need to be adjusted)
                var keyNames = dbEntry.Entity.GetType().GetProperties().ToList();
                //var keyNames = dbEntry.Entity.GetType().GetProperties().ToList().Select(p => p.PropertyType.Namespace);

                var key = dbEntry.Entity.GetType().GetProperties().Where(p => p.PropertyType.Namespace == "System").FirstOrDefault();

                string keyName = "";
                if (key != null)
                    keyName = key.Name;
                else
                    keyName = "id";

                if (dbEntry.State == EntityState.Added)
                {
                    // For Inserts, just add the whole record
                    // If the entity implements IDescribableEntity, use the description from Describe(), otherwise use ToString()

                    foreach (string propertyName in dbEntry.CurrentValues.PropertyNames)
                    {
                        try
                        {
                            string Data_Value_Before = null;
                            string Data_Value_After = dbEntry.CurrentValues.GetValue<object>(propertyName) == null ? null : dbEntry.CurrentValues.GetValue<object>(propertyName).ToString();
                            if (!(Data_Value_Before == "-" && Data_Value_After == null) ||
                              !(Data_Value_Before == null && Data_Value_After == "-") ||
                              !(Data_Value_Before == null && Data_Value_After == "") ||
                              !(Data_Value_Before == "" && Data_Value_After == null) ||
                              !(Data_Value_Before == "" && Data_Value_After == "") ||
                              !(Data_Value_Before == null && Data_Value_After == null))
                            {
                                result.Add(new tbl_Audit_Trail()
                                {
                                    id = Guid.NewGuid(),
                                    Created_By = userId,
                                    Created_DateTime = changeTime,
                                    Event = "A",    // Added
                                    Url_Menu = tableName,
                                    Record_ID = dbEntry.CurrentValues.GetValue<object>(keyName).ToString(),
                                    Column_Name = propertyName,
                                    Value_After = Data_Value_After,
                                    Version = 1,
                                    Version_Id = versionId
                                }
                                );
                            }
                        }
                        catch
                        {

                        }
                    }

                }
                else if (dbEntry.State == EntityState.Deleted)
                {
                    // Same with deletes, do the whole record, and use either the description from Describe() or ToString()
                    foreach (string propertyName in dbEntry.OriginalValues.PropertyNames)
                    {
                        try
                        {
                            string Data_Value_Before = dbEntry.GetDatabaseValues().GetValue<object>(propertyName) == null ? null : dbEntry.GetDatabaseValues().GetValue<object>(propertyName).ToString();
                            string Data_Value_After = Data_Value_Before;
                            if (!(Data_Value_Before == "-" && Data_Value_After == null) ||
                              !(Data_Value_Before == null && Data_Value_After == "-") ||
                              !(Data_Value_Before == null && Data_Value_After == "") ||
                              !(Data_Value_Before == "" && Data_Value_After == null) ||
                              !(Data_Value_Before == "" && Data_Value_After == "") ||
                              !(Data_Value_Before == null && Data_Value_After == null))
                            {
                                result.Add(new tbl_Audit_Trail()
                                {
                                    id = Guid.NewGuid(),
                                    Created_By = userId,
                                    Created_DateTime = changeTime,
                                    Event = "D", // Deleted
                                    Url_Menu = tableName,
                                    Record_ID = dbEntry.OriginalValues.GetValue<object>(keyName).ToString(),
                                    Column_Name = propertyName,
                                    Value_Before = Data_Value_Before,
                                    Value_After = Data_Value_After,
                                    Version_Id = versionId
                                }
                                );
                            }
                        }
                        catch
                        {

                        }
                    }
                }
                else if (dbEntry.State == EntityState.Modified)
                {
                    foreach (string propertyName in dbEntry.OriginalValues.PropertyNames)
                    {
                        // For updates, we only want to capture the columns that actually changed
                        if (!object.Equals(dbEntry.GetDatabaseValues().GetValue<object>(propertyName), dbEntry.CurrentValues.GetValue<object>(propertyName)))   //dbEntry.OriginalValues.GetValue<object>(propertyName)
                        {
                            try
                            {
                                string Data_Value_Before = dbEntry.GetDatabaseValues().GetValue<object>(propertyName) == null ? null : dbEntry.GetDatabaseValues().GetValue<object>(propertyName).ToString();
                                string Data_Value_After = dbEntry.CurrentValues.GetValue<object>(propertyName) == null ? null : dbEntry.CurrentValues.GetValue<object>(propertyName).ToString();
                                if (!(Data_Value_Before == "-" && Data_Value_After == null) ||
                                !(Data_Value_Before == null && Data_Value_After == "-") ||
                                !(Data_Value_Before == null && Data_Value_After == "") ||
                                !(Data_Value_Before == "" && Data_Value_After == null) ||
                                !(Data_Value_Before == "" && Data_Value_After == "") ||
                                !(Data_Value_Before == null && Data_Value_After == null))
                                {
                                    result.Add(new tbl_Audit_Trail()
                                    {
                                        id = Guid.NewGuid(),
                                        Created_By = userId,
                                        Created_DateTime = changeTime,
                                        Event = "M",    // Modified
                                        Url_Menu = tableName,
                                        Record_ID = dbEntry.OriginalValues.GetValue<object>(keyName).ToString(),
                                        Column_Name = propertyName,
                                        Value_Before = Data_Value_Before, //dbEntry.OriginalValues.GetValue<object>(propertyName) == null ? null : dbEntry.OriginalValues.GetValue<object>(propertyName).ToString(),
                                        Value_After = Data_Value_After,
                                        Version_Id = versionId
                                    }
                                    );
                                }
                            }
                            catch
                            { }
                        }
                        else
                        {
                            if (propertyName == "Authorized_By" && dbEntry.CurrentValues.GetValue<object>("Authorize_Status") != null)
                            {
                                if (dbEntry.CurrentValues.GetValue<object>("Authorize_Status").ToString() == "1")
                                {
                                    string Data_Value_Before = dbEntry.GetDatabaseValues().GetValue<object>(propertyName) == null ? null : dbEntry.GetDatabaseValues().GetValue<object>(propertyName).ToString();
                                    string Data_Value_After = dbEntry.CurrentValues.GetValue<object>(propertyName) == null ? null : dbEntry.CurrentValues.GetValue<object>(propertyName).ToString();
                                    if (!(Data_Value_Before == "-" && Data_Value_After == null) ||
                                    !(Data_Value_Before == null && Data_Value_After == "-") ||
                                    !(Data_Value_Before == null && Data_Value_After == "") ||
                                    !(Data_Value_Before == "" && Data_Value_After == null) ||
                                    !(Data_Value_Before == "" && Data_Value_After == "") ||
                                    !(Data_Value_Before == null && Data_Value_After == null))
                                    {
                                        result.Add(new tbl_Audit_Trail()
                                        {
                                            id = Guid.NewGuid(),
                                            Created_By = userId,
                                            Created_DateTime = changeTime,
                                            Event = "M",    // Modified
                                            Url_Menu = tableName,
                                            Record_ID = dbEntry.OriginalValues.GetValue<object>(keyName).ToString(),
                                            Column_Name = propertyName,
                                            Value_Before = Data_Value_Before, //dbEntry.OriginalValues.GetValue<object>(propertyName) == null ? null : dbEntry.OriginalValues.GetValue<object>(propertyName).ToString(),
                                            Value_After = Data_Value_After,
                                            Version_Id = versionId
                                        }
                                        );
                                    }
                                }
                            }
                        }
                    }
                }
            }
            // Otherwise, don't do anything, we don't care about Unchanged or Detached entities
            return result;
        }

        private List<tbl_Notification_Log> AddNotifRecords(DbEntityEntry dbEntry, string userId)
        {
            Guid versionId = Guid.NewGuid();
            List<tbl_Notification_Log> result = new List<tbl_Notification_Log>();

            DateTime changeTime = DateTime.Now;

            // Get the Table() attribute, if one exists
            // TableAttribute tableAttr = dbEntry.Entity.GetType().GetCustomAttributes(typeof(TableAttribute), false).SingleOrDefault() as TableAttribute;

            TableAttribute tableAttr = dbEntry.Entity.GetType().GetCustomAttributes(typeof(TableAttribute), true).SingleOrDefault() as TableAttribute;

            // Get table name (if it has a Table attribute, use that, otherwise get the pluralized name)
            string tableName = tableAttr != null ? tableAttr.Name : dbEntry.Entity.GetType().Name;

            // Get primary key value (If you have more than one key column, this will need to be adjusted)
            var keyNames = dbEntry.Entity.GetType().GetProperties().ToList();

            string keyName = keyNames[0].Name; //dbEntry.Entity.GetType().GetProperties().Single(p => p.GetCustomAttributes(typeof(KeyAttribute), false).Count() > 0).Name;

            if (dbEntry.State == EntityState.Added)
            {
                // For Inserts, just add the whole record
                // If the entity implements IDescribableEntity, use the description from Describe(), otherwise use ToString()

                foreach (string propertyName in dbEntry.CurrentValues.PropertyNames)
                {
                    result.Add(new tbl_Notification_Log()
                    {
                        id = Guid.NewGuid(),
                        Log_Date = changeTime,
                        Notification_Type = "PRIVATE MESSAGE",
                        Message_To = "TEST",
                        Message_Subject = "SUBJECT",
                        Message_Body = "BODY",
                        Message_Attachment = "ATTACHMENT",
                        Status = 0,
                        Created_By = userId,
                        Created_DateTime = changeTime
                    }
                   );
                }
            }
            else if (dbEntry.State == EntityState.Deleted)
            {
                // Same with deletes, do the whole record, and use either the description from Describe() or ToString()
                result.Add(new tbl_Notification_Log()
                {
                    id = Guid.NewGuid(),
                    Log_Date = changeTime,
                    Notification_Type = "PRIVATE MESSAGE",
                    Message_To = "TEST",
                    Message_Subject = "SUBJECT",
                    Message_Body = "BODY",
                    Message_Attachment = "ATTACHMENT",
                    Status = 0,
                    Created_By = userId,
                    Created_DateTime = changeTime
                }
                );
            }
            else if (dbEntry.State == EntityState.Modified)
            {
                foreach (string propertyName in dbEntry.OriginalValues.PropertyNames)
                {
                    // For updates, we only want to capture the columns that actually changed
                    if (!object.Equals(dbEntry.GetDatabaseValues().GetValue<object>(propertyName), dbEntry.CurrentValues.GetValue<object>(propertyName)))   //dbEntry.OriginalValues.GetValue<object>(propertyName)
                    {
                        result.Add(new tbl_Notification_Log()
                        {
                            id = Guid.NewGuid(),
                            Log_Date = changeTime,
                            Notification_Type = "PRIVATE MESSAGE",
                            Message_To = "TEST",
                            Message_Subject = "SUBJECT",
                            Message_Body = "BODY",
                            Message_Attachment = "ATTACHMENT",
                            Status = 0,
                            Created_By = userId,
                            Created_DateTime = changeTime
                        }
                        );
                    }
                }
            }
            // Otherwise, don't do anything, we don't care about Unchanged or Detached entities
            return result;
        }

        public int SP_GENERATE_BANK_TEXT_FILE(string a)
        {
            throw new NotImplementedException();
        }
    }

    public class Error
    {
        private static ModelEntitiesWebsite db = new ModelEntitiesWebsite();
        #region public static void : LogException
        public static long LogException(Exception ex, string strSource, out string strMessage)
        {
            strMessage = string.Empty;
            long lngLogSuccess = -1;

            try
            {
                string InnerException = ex.ToString();
                InnerException = InnerException.Length > 4000 ? InnerException.Substring(0, 3999) : InnerException;
                db.SP_ERROR_LOG(strSource, InnerException);
            }
            catch (Exception exInner)
            {
                try
                {
                    LogToEventLog(exInner.Message + " | " + exInner.StackTrace, "public static long LogException()");
                    lngLogSuccess = 0;
                }
                catch (Exception excep)
                {
                    strMessage = excep.Message + " | " + excep.StackTrace;
                }
            }
            return lngLogSuccess;
        }

        public static long LogException(string ex, string strSource, out string strMessage)
        {
            strMessage = string.Empty;
            long lngLogSuccess = -1;

            try
            {
                db.SP_ERROR_LOG(strSource, ex);
            }
            catch (Exception exInner)
            {
                try
                {
                    LogToEventLog(exInner.Message + " | " + exInner.StackTrace, "public static long LogException()");
                    lngLogSuccess = 0;
                }
                catch (Exception excep)
                {
                    strMessage = excep.Message + " | " + excep.StackTrace;
                }
            }
            return lngLogSuccess;
        }
        #endregion
        #region Log Error To Windows Event Log
        public static long LogToEventLog(string strErrDscp, string strErrSource)
        {
            //Variable declaration
            EventLog Log = null;
            string strLog = null;
            string strError = null;
            string strEventSource = null;
            long lngLogged = -1;

            try
            {
                strLog = "FDLMS_UIEventLog";

                strEventSource = strLog + "." + strErrSource;

                //Construct Error String to store in Event Log
                strError = "Error Source : " + strErrSource + "\n" +
                    "Error Description : " + strErrDscp;

                if (strErrSource == "" || strErrSource == null)
                {
                    strEventSource = strLog + "." + "public static long LogToEventLog()";
                }
                //check existance of event log
                if (!EventLog.SourceExists(strEventSource))
                {
                    //if Event Log not exist, create new event log
                    EventLog.CreateEventSource(strEventSource, strLog);
                }

                //Create a event Log
                Log = new EventLog();

                //Set event log source
                Log.Source = strEventSource;

                //Write error to Event Log
                Log.WriteEntry(strError, EventLogEntryType.Error);
                lngLogged = 0;

            }//end of try

            catch (Exception ex)
            {
                throw ex;
            }//end of try

            finally
            {
                //close the event log
                if (Log != null)
                {
                    Log.Close();
                }

            }//end of finally

            //return status
            return lngLogged;

        }//end of LogToEventLog(string strErrSource, string strErrDscp)
        #endregion
        #region Log To File
        public static long LogToFile(string strErrSource, string strErrDscp)
        {
            //Variable declaration
            FileStream fsLog = null;
            StreamWriter swLog = null;
            string strError = "";
            string strLogPath = "C:\"UIFDLMSErr.Log";
            long lngSuccess = -1;

            try
            {
                //Construct Error string To log into text file
                strError = "--- " + DateTime.Now.ToString() + " : " +
                     " : " + strErrSource + " : " +
                    strErrDscp + "\n";

                string strLogDir = System.IO.Path.GetDirectoryName(strLogPath);

                if (!System.IO.Directory.Exists(strLogDir))
                {
                    System.IO.Directory.CreateDirectory(strLogDir);
                }

                //Create or open a file
                fsLog = new FileStream(strLogPath, FileMode.Append, FileAccess.Write);

                //instantiate a Stream Writer
                swLog = new StreamWriter(fsLog);

                //Search for EOF
                swLog.BaseStream.Seek(0, SeekOrigin.End);

                //write Error to file
                swLog.Write(strError);
                lngSuccess = 0;

            }//end of try

            catch (Exception ex)
            {
                throw ex;

            }//end of catch

            finally
            {
                //Close the Stream Writer
                if (swLog != null)
                {
                    swLog.Flush();
                    swLog.Close();
                }

                //Close the File Stream
                if (fsLog != null)
                {
                    fsLog.Close();
                }

            }//end of finally

            //return Status
            return lngSuccess;

        }//end of LogToFile(string strErrSource, string strErrDscp)
        #endregion
    }

       
    public class NoLockInterceptor : DbCommandInterceptor
    {
        private static readonly Regex _tableAliasRegex = new Regex(@"((?<!\){1,5})\] AS \[Extent\d+\](?! WITH \(NOLOCK\)))", RegexOptions.Multiline | RegexOptions.IgnoreCase);

        [ThreadStatic]
        public static bool ApplyNoLock;

        public override void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
			if (command.CommandText.Contains("SELECT")) // && ApplyNoLock
                {
					command.CommandText = command.CommandText + " OPTION (RECOMPILE)" ; 
                }

            if (command.CommandText.Contains("tbl") && command.CommandText.Contains("SELECT")) // && ApplyNoLock
                { 
                    command.CommandText = _tableAliasRegex.Replace(command.CommandText, delegate(Match mt)
                    {
                        return mt.Groups[0].Value + " WITH (NOLOCK) ";
                    });
					
                }
             

        }

        public override void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
			if (command.CommandText.Contains("SELECT")) // && ApplyNoLock
                {
					command.CommandText = command.CommandText + " OPTION (RECOMPILE)" ; 
                }
            
            if (command.CommandText.Contains("tbl") && command.CommandText.Contains("SELECT")) // && ApplyNoLock
                { 
                    command.CommandText = _tableAliasRegex.Replace(command.CommandText, delegate(Match mt)
                    {
                        return mt.Groups[0].Value + " WITH (NOLOCK) ";
                    }); 
					
                }
            

        }
    }
}
