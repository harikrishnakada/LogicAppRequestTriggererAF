using ApiClientLib.Auth;

using System.Security.Authentication;

namespace ApiClientLib
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly IAuthenticationProvider _authenticationProvider;

        public ApiClient(HttpClient httpClient, IAuthenticationProvider authenticationProvider)
        {
            _httpClient = httpClient;
            _authenticationProvider = authenticationProvider;
        }

        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            try
            {
                var token = await _authenticationProvider.AcquireTokenAsync();
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var r = await _httpClient.SendAsync(request);
                r.EnsureSuccessStatusCode();
                return r;
            }
            catch (HttpRequestException ex)
            {
                // Handle HttpRequest exceptions (e.g., network errors)
                throw new ApiException("HTTP request error occurred.", ex);
            }
            catch (AuthenticationException ex)
            {
                // Handle authentication errors
                throw new ApiException("Authentication failed.", ex);
            }
            catch (Exception ex) {
                throw new ApiException("Unknown exception occured.", ex);
            }
        }
    }
}
