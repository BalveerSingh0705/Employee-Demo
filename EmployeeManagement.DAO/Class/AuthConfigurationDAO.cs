using EmployeeManagement.Core.Common;
using EmployeeManagement.DAO.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeManagement.DAO.Class
{
    public class AuthConfigurationDAO : IAuth
    {
        private readonly string _connectionString;
        string connectionString = ConstantsModels.loginQuery;
        private char[] _jwtSecret;

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



    public AuthResponseLoginModel Login(LoginModel loginViewModel)
    {
        AuthResponseLoginModel response = new AuthResponseLoginModel();

        try
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Authenticate user
                using (SqlCommand cmd = new SqlCommand("usp_AuthenticateUser", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", loginViewModel.emailUsername);
                    cmd.Parameters.AddWithValue("@Password", HashPassword(loginViewModel.password));

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        var userId = reader["UserId"];
                        var username = reader["Username"];
                        var email = reader["Email"];

                        // Generate JWT token
                        var tokenHandler = new JwtSecurityTokenHandler();
                        var key = Encoding.UTF8.GetBytes("4f1feeca525de4cdb064656007da3edac7895a87ff0ea865693300fb8b6e8f9c"); // Replace _jwtSecret with actual secret from config
                        var tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(new[]
                            {
                            new Claim(ClaimTypes.Name, username.ToString()),
                            new Claim(ClaimTypes.Email, email.ToString()),
                            new Claim(ClaimTypes.NameIdentifier, userId.ToString())
                        }),
                            Expires = DateTime.UtcNow.AddMinutes(double.Parse("1")), // From config
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                        };

                        var token = tokenHandler.CreateToken(tokenDescriptor);
                        response.Token = tokenHandler.WriteToken(token);
                        response.Expiration = tokenDescriptor.Expires.Value;
                        response.Success = true;
                        response.Message = "Login successful.";
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = "Invalid email or password.";
                    }
                }
            }
        }
        catch (SqlException ex)
        {
            response.Success = false;
            response.Message = "Database error: " + ex.Message;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = "An error occurred during login: " + ex.Message;
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
