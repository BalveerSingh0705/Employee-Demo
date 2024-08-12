
using EmployeeManagement.Core.Common;
using EmployeeManagement.Core.Entities;
using EmployeeManagement.DAO;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection.PortableExecutable;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;



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

        /// <summary>
        /// GetEmployeeDetailsInTableForm
        /// </summary>
        /// <param name="tableFormEntity"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<TableFormEntity> GetEmployeeDetailsInTableForm()
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
                                    phoneNo = Convert.ToString(rdr["Phone"]),
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

        public List<EmployeeEntity> GetEmployeeDetailsClickOnEditButton(EmployeeDataInIDEntity employeeDataInIDEntity)
        {
            List<EmployeeEntity> employeeList = new List<EmployeeEntity>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("usp_GetEmployeeInfoByEmpID", connection))
                    {
                        command.CommandTimeout = 60;
                        command.CommandType = CommandType.StoredProcedure;

                        // Add the empID parameter to the command
                        command.Parameters.Add("@EmpID", SqlDbType.NVarChar).Value = employeeDataInIDEntity.EmpID;

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                EmployeeEntity employee = new EmployeeEntity
                                {
                                    empID = reader["EmpID"].ToString(),
                                    phone = reader["Phone"].ToString(),
                                    contactSite = reader["ContactSite"].ToString(),
                                    CreatedBy = reader["CreatedBy"].ToString(),
                                    ModifiedBy = reader["ModifiedBy"].ToString(),
                                    RoleCode = reader["RoleCode"].ToString(),
                                    UserId = (Guid)reader["UserId"],
                                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                                    ModifiedDate = Convert.ToDateTime(reader["ModifiedDate"]),
                                    Location = reader["Location"].ToString(),
                                    UserName = reader["UserName"].ToString(),
                                    firstName = reader["FirstName"].ToString(),
                                    lastName = reader["LastName"].ToString(),
                                    designation = reader["Designation"].ToString(),
                                    experience = Convert.ToInt32(reader["Experience"]),
                                    dateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                                    dateOfJoining = Convert.ToDateTime(reader["DateOfJoining"]),
                                    address = reader["Address"].ToString(),
                                    pinCode = reader["PinCode"].ToString(),
                                    city = reader["City"].ToString(),
                                    state = reader["State"].ToString(),
                                    panNumber = reader["PanNumber"].ToString(),
                                    aadhaarCard = reader["AadhaarCard"].ToString(),
                                    bankName = reader["BankName"].ToString(),
                                    bankAddress = reader["BankAddress"].ToString(),
                                    accountNumber = reader["AccountNumber"].ToString(),
                                    iFSC = reader["IFSC"].ToString(),
                                    salary = Convert.ToDecimal(reader["Salary"]),
                                    pFNumber = reader["PFNumber"].ToString(),
                                    workingHours = reader["WorkingHours"].ToString()
                                };

                                employeeList.Add(employee);
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

            return employeeList;
        }



        public bool DeleteSingleEmployeeDetails(EmployeeDataInIDEntity employeeDataInIDEntity)
        {
            bool isSuccess = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    connection.Open();
                    using (SqlCommand command = new SqlCommand("usp_DeleteSingleEmployeeDetails", connection))
                    {
                        command.CommandTimeout = 60;
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@EmpID", SqlDbType.NVarChar).Value = employeeDataInIDEntity.EmpID;


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

        public bool SaveEmployeeChangesInfo(EmployeeEntity employee)
        {
            bool isSuccess = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("usp_UpdateAndSaveEmployeeInfo", connection))
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

        public List<AttendanceTableEntity> GetEmployeeDetailsInAttendanceTable()
        {
            List<AttendanceTableEntity> attendanceTableEntity = new List<AttendanceTableEntity>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("usp_GetAttendenceTableEmployeeInfo", connection))
                    {
                        command.CommandTimeout = 60;
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters if needed
                        // command.Parameters.Add("@ParameterName", SqlDbType.NVarChar).Value = tableFormEntity.SomeProperty;

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                AttendanceTableEntity employee = new AttendanceTableEntity
                                {
                                    EmpID = reader["EmpID"].ToString(),
                                    FirstName = reader["FirstName"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    Designation = reader["Designation"].ToString(),
                                    WorkingHours = reader["WorkingHours"].ToString() // Corrected property name
                                };
                                attendanceTableEntity.Add(employee); // Use the correct list variable
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
            return attendanceTableEntity; // Return the correct list
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeAttendanceEntity"></param>
        /// <returns></returns>
        public bool SendEmployeeAttendanceDetails(List<AttendanceDataSendEntity> attendanceDataList)
        {
            bool isSuccess = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    foreach (var attendanceData in attendanceDataList)
                    {
                        // Check if the attendance record for the date exists
                        bool recordExists = false;
                        using (SqlCommand checkCommand = new SqlCommand("usp_CheckAttendanceExists", connection))
                        {
                            checkCommand.CommandType = CommandType.StoredProcedure;
                            checkCommand.Parameters.Add("@EmpID", SqlDbType.NVarChar).Value = attendanceData.EmpID;
                            checkCommand.Parameters.Add("@AttendanceDate", SqlDbType.DateTime).Value = attendanceData.AttendanceDate;

                            recordExists = Convert.ToBoolean(checkCommand.ExecuteScalar());
                        }

                        // Use different stored procedures for insert or update based on whether the record exists
                        string storedProcedure = recordExists ? "usp_UpdateUserInfo" : "usp_InsertUserAttendanceInfo";
                        using (SqlCommand command = new SqlCommand(storedProcedure, connection))
                        {
                            command.CommandTimeout = 60;
                            command.CommandType = CommandType.StoredProcedure;

                            // Add parameters common for both insert and update
                            command.Parameters.Add("@EmpID", SqlDbType.NVarChar).Value = attendanceData.EmpID;
                            command.Parameters.Add("@Attendance", SqlDbType.NVarChar).Value = attendanceData.Attendance;
                            command.Parameters.Add("@AttendanceDate", SqlDbType.DateTime).Value = attendanceData.AttendanceDate;

                            // Add parameters for the OTDetails properties
                            if (attendanceData.OTDetails != null)
                            {
                                command.Parameters.Add("@OTEmpID", SqlDbType.NVarChar).Value = attendanceData.OTDetails.EmpID;
                                command.Parameters.Add("@Hours", SqlDbType.Int).Value = attendanceData.OTDetails.Hours;
                                command.Parameters.Add("@Comment", SqlDbType.NVarChar).Value = attendanceData.OTDetails.Comment;
                            }
                            else
                            {
                                command.Parameters.Add("@OTEmpID", SqlDbType.NVarChar).Value = DBNull.Value;
                                command.Parameters.Add("@Hours", SqlDbType.Int).Value = DBNull.Value;
                                command.Parameters.Add("@Comment", SqlDbType.NVarChar).Value = DBNull.Value;
                            }

                            // Execute the command
                            command.ExecuteNonQuery();
                        }
                    }
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
                // Log exception details here (e.g., using a logging framework)
                Console.WriteLine($"Error: {ex.Message}");
            }

            return isSuccess;
        }



    }

}






