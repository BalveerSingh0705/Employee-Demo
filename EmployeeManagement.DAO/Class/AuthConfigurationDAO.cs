using EmployeeManagement.Core.Common;
using EmployeeManagement.DAO.Interface;
using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace EmployeeManagement.DAO.Class
{
    public class AuthConfigurationDAO : IAuth
    {
        private readonly string _connectionString;
        string connectionString = ConstantsModels.loginQuery;

        public AuthResponse AuthRegister(AuthRegisterViewModel viewModel)
        {
            AuthResponse response = new AuthResponse();

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Check if the email already exists
                    using (SqlCommand checkEmailCmd = new SqlCommand("SELECT COUNT(1) FROM Users WHERE Email = @Email", con))
                    {
                        checkEmailCmd.Parameters.AddWithValue("@Email", viewModel.Email);

                        int emailCount = (int)checkEmailCmd.ExecuteScalar();
                        if (emailCount > 0)
                        {
                            response.Success = false;
                            response.Message = "An account with this email already exists.";
                            return response;
                        }
                    }

                    // Check if the username already exists
                    using (SqlCommand checkUsernameCmd = new SqlCommand("SELECT COUNT(1) FROM Users WHERE Username = @Username", con))
                    {
                        checkUsernameCmd.Parameters.AddWithValue("@Username", viewModel.Username);

                        int usernameCount = (int)checkUsernameCmd.ExecuteScalar();
                        if (usernameCount > 0)
                        {
                            response.Success = false;
                            response.Message = "An account with this username already exists.";
                            return response;
                        }
                    }

                    // Register the new user
                    using (SqlCommand cmd = new SqlCommand("usp_RegisterUser", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Username", viewModel.Username);
                        cmd.Parameters.AddWithValue("@Email", viewModel.Email);
                        cmd.Parameters.AddWithValue("@Password", HashPassword(viewModel.Password));
                        cmd.Parameters.AddWithValue("@Phone", viewModel.Phone ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Terms", viewModel.Terms);

                        // Output parameter for UserId
                        SqlParameter userIdParam = new SqlParameter
                        {
                            ParameterName = "@UserId",
                            SqlDbType = SqlDbType.UniqueIdentifier,
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(userIdParam);

                        cmd.ExecuteNonQuery();

                        // Retrieve the generated UserId
                        response.UserId = (Guid)userIdParam.Value;
                    }
                }

                response.Success = true;
                response.Message = "User registered successfully.";
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions and capture detailed error message
                response.Success = false;
                response.Message = "The request timed out while accessing the database. Please try again later.";//$"SQL Error: {ex.Message}";
            }
            catch (Exception ex)
            {
                // Handle general exceptions
                response.Success = false;
                response.Message = $"An error occurred while registering the user: {ex.Message}";
            }

            return response;
        }

        private string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
