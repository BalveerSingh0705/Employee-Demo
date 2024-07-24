
using EmployeeManagement.Core.Common;

using System.Data;

namespace EmployeeManagement.DAO
{
    public class EmployeeConfigurationDAO : IConfiguration
    {

        /// <summary>
        /// SaveOrUpdateTemplateFieldData
        /// </summary>
        /// <param name="employeeEntity"></param>
        /// <returns></returns>
        public bool SaveOrUpdateEmployeeDetails(EmployeeEntity employeeEntity)
        {
            bool isSuccess = false;

            //try
            //{
            //    using (SqlConnection conn = new SqlConnection(TenantCache.GetSqlDbConnectionFromCacheByTenantId(employeeEntity.TenantId)))
            //    {
            //        using (SqlCommand cmd = new SqlCommand("usp_SaveOrUpdateemployeeEntity", conn))
            //        {
            //            cmd.CommandTimeout = 60;
            //            cmd.CommandType = CommandType.StoredProcedure;
            //            cmd.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = employeeEntity.empID;
            //            //cmd.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier).Value = employeeEntity.UserId;
            //            //cmd.Parameters.Add("@TemplateId", SqlDbType.UniqueIdentifier).Value = employeeEntity.TemplateId;
            //            //cmd.Parameters.Add("@FieldType", SqlDbType.UniqueIdentifier).Value = employeeEntity.FieldType;
            //            //cmd.Parameters.Add("@Order", SqlDbType.Int).Value = employeeEntity.Order;
            //            //cmd.Parameters.Add("@FieldName", SqlDbType.NVarChar).Value = employeeEntity.FieldName;
            //            //cmd.Parameters.Add("@Format", SqlDbType.NVarChar).Value = employeeEntity.Format;
            //            //cmd.Parameters.Add("@ToolTip", SqlDbType.NVarChar).Value = employeeEntity.ToolTip;
            //            //cmd.Parameters.Add("@IsRequired", SqlDbType.Bit).Value = employeeEntity.IsRequired;
            //            //cmd.Parameters.Add("@EntityTypeName", SqlDbType.NVarChar).Value = employeeEntity.EntityTypeName;
            //            //cmd.Parameters.Add("@Scope", SqlDbType.NVarChar).Value = employeeEntity.Scope;
            //            //cmd.Parameters.Add("@ScopeId", SqlDbType.UniqueIdentifier).Value = employeeEntity.ScopeId;
            //            //cmd.Parameters.Add("@IsHidden", SqlDbType.Bit).Value = employeeEntity.IsHidden;
            //            //cmd.Parameters.Add("@FieldTypeName", SqlDbType.NVarChar).Value = employeeEntity.FieldTypeName;
            //            //cmd.Parameters.Add("@IdMenu", SqlDbType.UniqueIdentifier).Value = employeeEntity.MenuId;
            //            //cmd.Parameters.Add("@FieldTemplate", SqlDbType.UniqueIdentifier).Value = employeeEntity.FieldTemplate;
            //            //cmd.Parameters.Add("@Unit", SqlDbType.UniqueIdentifier).Value = employeeEntity.UnitId;
            //            //cmd.Parameters.Add("@IconSymbolId", SqlDbType.UniqueIdentifier).Value = employeeEntity.IconSymbolId;
            //            //cmd.Parameters.Add("@IconDescription", SqlDbType.NVarChar).Value = employeeEntity.IconDescription;
            //            //cmd.Parameters.Add("@FieldDescription", SqlDbType.NVarChar).Value = employeeEntity.FieldDescription;


            //            conn.Open();
            //            SqlDataReader rdr = cmd.ExecuteReader();
            //            rdr.Close();
            //            conn.Close();
            //            isSuccess = true;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    isSuccess = false;
            //    using (LogException _error = new LogException(typeof(ConfigurationDAO), TenantCache.GetSqlDbConnectionFromCacheByTenantId(employeeEntity.TenantId)))
            //    {
            //        _error.Exception("Exception in SaveOrUpdateTemplateFieldData:", ex, TenantCache.GetSubDomainByTenantId((Guid)employeeEntity.TenantId), employeeEntity.UserName, employeeEntity);
            //    }
            //}

            return isSuccess;
        }



    }
}
