using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using WorkForce.API.Models.Responses;
using WorkForce.API.Models.User;

namespace WorkForce.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;
      

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
          
        }

        public async Task<ApiResponse<int>> CreateUserAsync(UserCreateDto userDto)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);

                // Verificar unicidad de usuario/email
                var exists = await connection.QueryFirstOrDefaultAsync<int>(
                    "SELECT 1 FROM Users WHERE Username = @Username OR Email = @Email",
                    new { userDto.Username, userDto.Email });

                if (exists != 0)
                {
                    return ApiResponse<int>.ErrorResult("El usuario o email ya existe");
                }

                string passwordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);

                var parameters = new DynamicParameters();
                parameters.Add("@Username", userDto.Username);
                parameters.Add("@PasswordHash", passwordHash);
                parameters.Add("@Email", userDto.Email);
                parameters.Add("@RoleId", userDto.RoleId);
                parameters.Add("@CompanyId", userDto.CompanyId);
                parameters.Add("@Active", userDto.Active);
                parameters.Add("@NewId", dbType: DbType.Int32, direction: ParameterDirection.Output);

                await connection.ExecuteAsync(
                    "SP_Users_Insert",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                int newId = parameters.Get<int>("@NewId");
                return ApiResponse<int>.SuccessResult(newId, "Usuario creado exitosamente");
            }
            catch (SqlException ex)
            {
             
                return ApiResponse<int>.ErrorResult($"Error de base de datos: {ex.Message}");
            }
            catch (Exception ex)
            {
               
                return ApiResponse<int>.ErrorResult($"Error inesperado: {ex.Message}");
            }
        }
    }
}
