namespace ApiClientLib
{
    public interface IApiClient
    {
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
    }
}