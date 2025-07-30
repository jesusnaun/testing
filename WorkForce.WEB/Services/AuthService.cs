using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http;
using System.Net.Http.Json;
using WorkForce.WEB.Models.Auth;
using WorkForce.WEB.Models.Responses;

namespace WorkForce.WEB.Services
{
    public class AuthService
    {
        private readonly HttpClient _http;
        private readonly CustomAuthStateProvider _authState;

        public AuthService(HttpClient http, AuthenticationStateProvider provider)
        {
            _http = http;
            _authState = (CustomAuthStateProvider)provider;
        }

        public async Task<string> LoginAsync(LoginRequest request)
        {
            //var response = await _http.PostAsJsonAsync("login", request);
            var response = await _http.PostAsJsonAsync("api/Auth/login", request);

            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<LoginResponse>>();
                var token = apiResponse?.Data?.Token;

                if (!string.IsNullOrWhiteSpace(token))
                {
                    await _authState.MarkUserAsAuthenticated(token);
                    return "success";
                }

                return "Token vacío";
            }
            else
            {
                //var apiError = await response.Content.ReadFromJsonAsync<ApiResponse<LoginResponse>>();
                //return apiError?.Message ?? "Error de autenticación";
                var errorContent = await response.Content.ReadAsStringAsync();
                return $"Error de autenticación: {errorContent}";
            }
        }

        public async Task LogoutAsync()
        {
            await _authState.MarkUserAsLoggedOut();
        }
    }
}
