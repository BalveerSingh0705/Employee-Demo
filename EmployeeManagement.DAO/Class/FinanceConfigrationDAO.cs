using EmployeeManagement.Core.Common;
using EmployeeManagement.Core.Entities;
using EmployeeManagement.DAO.Interface;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EmployeeManagement.DAO.Class
{
    public class FinanceConfigrationDAO : IFinance
    {

        private readonly string _connectionString;
        string connectionString = ConstantsModels.loginQuery;

        public bool AdvanceSalary(AdvanceSalaryEntity advanceSalaryEntity)
        {
            bool isSuccess = false;
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Check if EmpID exists
                    var checkCommand = new SqlCommand("SELECT COUNT(1) FROM EmployeeAdditionalDetails WHERE EmpID =@EmpID And IsDeleted= 0", connection);
                    checkCommand.Parameters.AddWithValue("@EmpID", advanceSalaryEntity.EmpID);
                    int empExists = (int)checkCommand.ExecuteScalar();

                    if (empExists == 0)
                    {
                        throw new Exception("Employee does not exist.");
                    }

                    // If EmpID exists, proceed with advance salary entry
                    var command = new SqlCommand("sp_InsertAdvanceSalary", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    command.Parameters.AddWithValue("@EmpID", advanceSalaryEntity.EmpID);
                    command.Parameters.AddWithValue("@Name", advanceSalaryEntity.Name);
                    command.Parameters.AddWithValue("@AdvanceAmount", advanceSalaryEntity.AdvanceAmount);
                    command.Parameters.AddWithValue("@AdvanceDate", advanceSalaryEntity.AdvanceDate);
                    command.Parameters.AddWithValue("@PaymentTime", advanceSalaryEntity.PaymentTime);
                    command.Parameters.AddWithValue("@PaymentMode", advanceSalaryEntity.PaymentMode);
                    command.Parameters.AddWithValue("@PaymentBy", advanceSalaryEntity.PaymentBy);
                    command.Parameters.AddWithValue("@OtherCredit", (object)advanceSalaryEntity.OtherCredit ?? DBNull.Value);
                    command.Parameters.AddWithValue("@OtherComment", (object)advanceSalaryEntity.OtherComment ?? DBNull.Value);

                    var newId = command.ExecuteScalar(); // Returns the new ID
                }

                isSuccess = true;
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                // Optionally, you can return the exception message to the calling method for debugging
                isSuccess = false;
            }

            return isSuccess;
        }

        public List<EmployeeSalaryEntity> FinalSalary(SalaryRequestEntity salaryRequestEntity)
        {
            var employeeSalaryList = new List<EmployeeSalaryEntity>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("usp_GetEmployeeSalaryDetails", connection))
                    {
                        command.CommandTimeout = 60;
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the command
                        command.Parameters.Add("@EmpID", SqlDbType.NVarChar).Value = (object)salaryRequestEntity.EmpID ?? DBNull.Value;
                        command.Parameters.Add("@Year", SqlDbType.Int).Value = salaryRequestEntity.Year;
                        command.Parameters.Add("@Month", SqlDbType.Int).Value = salaryRequestEntity.Month;

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var employeeSalary = new EmployeeSalaryEntity
                                {
                                    EmpID = reader["EmpID"].ToString(),
                                    FirstName = reader["FirstName"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    Designation = reader["Designation"].ToString(),
                                    Phone = reader["Phone"].ToString(),
                                    Salary = reader.GetDecimal(reader.GetOrdinal("Salary")),
                                    AdvanceAmount = reader.GetDecimal(reader.GetOrdinal("AdvanceAmount")),
                                    OtherCredit = reader.IsDBNull(reader.GetOrdinal("OtherCredit")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("OtherCredit")),
                                    TotalAttendances = reader.GetInt32(reader.GetOrdinal("TotalAttendances")),
                                    TotalHours = reader.IsDBNull(reader.GetOrdinal("TotalHours")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("TotalHours")),
                                    WorkingHours = reader["WorkingHours"].ToString(),
                                    TotalSalaryForMonth = reader.GetDecimal(reader.GetOrdinal("TotalSalaryForMonth")) // Should be decimal
                                };

                                employeeSalaryList.Add(employeeSalary);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception (implement your logging mechanism here)
                throw new Exception("An error occurred while fetching employee salary details.", ex);
            }

            return employeeSalaryList;
        }



    }
}
