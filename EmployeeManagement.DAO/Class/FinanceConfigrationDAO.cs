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
            decimal totalAdvanceForMonth = 0m; // Initialize totalAdvanceForMonth

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Check if EmpID exists
                    var checkCommand = new SqlCommand("SELECT COUNT(1) FROM EmployeeAdditionalDetails WHERE EmpID = @EmpID AND IsDeleted = 0", connection);
                    checkCommand.Parameters.AddWithValue("@EmpID", advanceSalaryEntity.EmpID);
                    int empExists = (int)checkCommand.ExecuteScalar();

                    if (empExists == 0)
                    {
                        throw new Exception("Employee does not exist.");
                    }

                    // If EmpID exists, proceed with advance salary entry
                    using (var command = new SqlCommand("sp_InsertAdvanceSalary", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the command
                        command.Parameters.AddWithValue("@EmpID", advanceSalaryEntity.EmpID);
                        command.Parameters.AddWithValue("@Name", advanceSalaryEntity.Name);
                        command.Parameters.AddWithValue("@AdvanceAmount", advanceSalaryEntity.AdvanceAmount);
                        command.Parameters.AddWithValue("@AdvanceDate", advanceSalaryEntity.AdvanceDate);
                        command.Parameters.AddWithValue("@PaymentTime", advanceSalaryEntity.PaymentTime);
                        command.Parameters.AddWithValue("@PaymentMode", advanceSalaryEntity.PaymentMode);
                        command.Parameters.AddWithValue("@PaymentBy", advanceSalaryEntity.PaymentBy);
                        command.Parameters.AddWithValue("@OtherCredit", (object)advanceSalaryEntity.OtherCredit ?? DBNull.Value);
                        command.Parameters.AddWithValue("@OtherComment", (object)advanceSalaryEntity.OtherComment ?? DBNull.Value);

                        // Execute the command and handle the result sets
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Read the first result set (NewId)
                            if (reader.Read())
                            {
                                int newId = reader["NewId"] != DBNull.Value ? Convert.ToInt32(reader["NewId"]) : 0;
                                // Optional: use the newId if needed
                            }

                            // Move to the next result set (TotalAdvanceForMonth)
                            if (reader.NextResult() && reader.Read())
                            {
                                totalAdvanceForMonth = reader["TotalAdvanceForMonth"] != DBNull.Value ? Convert.ToDecimal(reader["TotalAdvanceForMonth"]) : 0;
                            }
                        }

                        isSuccess = true; // If both operations succeed, set success to true
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                // Optionally, handle the exception or rethrow it
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
                        command.Parameters.Add("@EmpID", SqlDbType.NVarChar).Value = string.IsNullOrEmpty(salaryRequestEntity.EmpID) ? null: salaryRequestEntity.EmpID;
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
                // Optionally rethrow the exception to be handled further up the call stack
                throw new Exception("An error occurred while fetching employee salary details.", ex);
            }

            return employeeSalaryList;
        }



    }
}
