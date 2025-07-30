using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using WorkForce.API.Models.Auth;
using WorkForce.API.Models.User;

namespace WorkForce.API.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly string _connectionString;

        public AuthRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<UserInfo?> ValidateUserAsync(string username)
        {
            using var connection = new SqlConnection(_connectionString);

            var parameters = new DynamicParameters();
            parameters.Add("@Username", username);

            var result = await connection.QueryFirstOrDefaultAsync<UserInfo>(
                "SP_Auth_GetUserByUsername", // Nuevo SP que solo busca por username
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return result;
        }

      
    }
}
