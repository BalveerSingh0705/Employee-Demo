
using EmployeeManagement.Core.Common;
using EmployeeManagement.DAO;
using Microsoft.Data.SqlClient;
using System.Data;



namespace EmployeeManagement.DAO
{
    public class EmployeeConfigurationDAO : IConfiguration
    {

        private readonly string _connectionString;
        string connectionString = ConstantsModels.loginQuery;


        /// <summary>
        /// SaveOrUpdateTemplateFieldData
        /// </summary>
        /// <param name="employeeEntity"></param>
        /// <returns></returns>
        /// 
        public bool SaveOrUpdateEmployeeDetails(EmployeeEntity employee)
        {
            bool isSuccess = false;

           

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("usp_InsertUserInfo", connection))
                    {
                        command.CommandTimeout = 60;
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@CreatedBy", SqlDbType.NVarChar).Value = employee.CreatedBy;
                        command.Parameters.Add("@ModifiedBy", SqlDbType.NVarChar).Value = employee.ModifiedBy;
                        command.Parameters.Add("@RoleCode", SqlDbType.NVarChar).Value = employee.RoleCode;
                        command.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier).Value = employee.UserId;
                        command.Parameters.Add("@CreatedDate", SqlDbType.DateTime2).Value = employee.CreatedDate;
                        command.Parameters.Add("@ModifiedDate", SqlDbType.DateTime2).Value = employee.ModifiedDate;
                        command.Parameters.Add("@Location", SqlDbType.NVarChar).Value = employee.Location;
                        command.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = employee.UserName;
                        command.Parameters.Add("@EmpID", SqlDbType.NVarChar).Value = employee.empID;
                        command.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = employee.phone;
                        command.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = employee.firstName;
                        command.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = employee.lastName;
                        command.Parameters.Add("@Designation", SqlDbType.NVarChar).Value = employee.designation;
                        command.Parameters.Add("@Experience", SqlDbType.Int).Value = employee.experience;
                        command.Parameters.Add("@DateOfBirth", SqlDbType.DateTime2).Value = employee.dateOfBirth;
                        command.Parameters.Add("@DateOfJoining", SqlDbType.DateTime2).Value = employee.dateOfJoining;
                        command.Parameters.Add("@Address", SqlDbType.NVarChar).Value = employee.address;
                        command.Parameters.Add("@PinCode", SqlDbType.NVarChar).Value = employee.pinCode;
                        command.Parameters.Add("@City", SqlDbType.NVarChar).Value = employee.city;
                        command.Parameters.Add("@State", SqlDbType.NVarChar).Value = employee.state;
                        command.Parameters.Add("@PanNumber", SqlDbType.NVarChar).Value = employee.panNumber;
                        command.Parameters.Add("@AadhaarCard", SqlDbType.NVarChar).Value = employee.aadhaarCard;
                        command.Parameters.Add("@ContactSite", SqlDbType.NVarChar).Value = employee.contactSite;
                        command.Parameters.Add("@BankName", SqlDbType.NVarChar).Value = employee.bankName;
                        command.Parameters.Add("@BankAddress", SqlDbType.NVarChar).Value = employee.bankAddress;
                        command.Parameters.Add("@AccountNumber", SqlDbType.NVarChar).Value = employee.accountNumber;
                        command.Parameters.Add("@IFSC", SqlDbType.NVarChar).Value = employee.iFSC;
                        command.Parameters.Add("@Salary", SqlDbType.Decimal).Value = employee.salary;
                        command.Parameters.Add("@PFNumber", SqlDbType.NVarChar).Value = employee.pFNumber;
                        command.Parameters.Add("@WorkingHours", SqlDbType.NVarChar).Value = employee.workingHours;

                        command.ExecuteNonQuery();
                        isSuccess = true;
                    }
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;

            }

            return isSuccess;
        }
        public List<TableFormEntity> GetEmployeeDetailsInTableForm(TableFormEntity tableFormEntity)
        {
            List<TableFormEntity> lstTemplateData = new List<TableFormEntity>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("usp_GetTableFormEmployeeInfo", connection))
                    {
                        command.CommandTimeout = 60;
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters if needed
                        // command.Parameters.Add("@ParameterName", SqlDbType.NVarChar).Value = tableFormEntity.SomeProperty;

                        connection.Open();
                        using (SqlDataReader rdr = command.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                lstTemplateData.Add(new TableFormEntity()
                                {

                                    empID = Convert.ToString(rdr["EmpID"]),
                                    firstName = Convert.ToString(rdr["FirstName"]),
                                    lastName = Convert.ToString(rdr["LastName"]),
                                    phoneNo = Convert.ToString(rdr["PhoneNo"]),
                                    salary = Convert.ToString(rdr["Salary"]),
                                    designation = Convert.ToString(rdr["Designation"])
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
                throw new Exception("An error occurred while fetching employee details in table form", ex);
            }
            return lstTemplateData;
        }

        // Ensure TableFormEntity class includes all necessary properties
      




    }
}









