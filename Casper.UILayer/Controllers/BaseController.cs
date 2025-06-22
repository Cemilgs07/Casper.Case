using Casper.UILayer.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Casper.UILayer.Controllers
{
    public abstract class BaseController<T> : Controller
    {
        protected readonly IHttpClientFactory _httpClientFactory;
        protected readonly string _baseUrl;
        protected readonly ILogger<T> _logger;

        protected BaseController(
            IHttpClientFactory httpClientFactory,
            IOptions<ApiSettings> apiOptions,
            ILogger<T> logger)
        {
            _httpClientFactory = httpClientFactory;
            _baseUrl = apiOptions.Value.BaseUrl;
            _logger = logger;
        }

        protected async Task<List<T>> GetList<T>(string endpoint)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var fullUrl = $"{_baseUrl}{endpoint}";
                var response = await client.GetAsync(fullUrl);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("GET list failed at {Endpoint}, status: {StatusCode}", endpoint, response.StatusCode);
                    return new List<T>();
                }

                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<T>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<T>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred in GetList at {Endpoint}", endpoint);
                return new List<T>();
            }
        }

        protected async Task<T?> GetItem<T>(string endpoint)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var fullUrl = $"{_baseUrl}{endpoint}";
                var response = await client.GetAsync(fullUrl);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("GET item failed at {Endpoint}, status: {StatusCode}", endpoint, response.StatusCode);
                    return default;
                }

                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred in GetItem at {Endpoint}", endpoint);
                return default;
            }
        }
        protected async Task<bool> DeleteItem(string endpoint)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var fullUrl = $"{_baseUrl}{endpoint}";
                var response = await client.DeleteAsync(fullUrl);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("DELETE {Endpoint} failed: {StatusCode}", endpoint, response.StatusCode);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during DELETE {Endpoint}", endpoint);
                return false;
            }
        }
        protected async Task<bool> PutItem<T>(string endpoint, T data)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var fullUrl = $"{_baseUrl}{endpoint}";
                var content = new StringContent(JsonSerializer.Serialize(data), System.Text.Encoding.UTF8, "application/json");

                var response = await client.PutAsync(fullUrl, content);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("PUT {Endpoint} failed: {StatusCode}", endpoint, response.StatusCode);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during PUT {Endpoint}", endpoint);
                return false;
            }
        }

        protected async Task<bool> PostItem<T>(string endpoint, T data)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var fullUrl = $"{_baseUrl}{endpoint}";

                var content = new StringContent(
                    JsonSerializer.Serialize(data),
                    System.Text.Encoding.UTF8,
                    "application/json");

                var response = await client.PostAsync(fullUrl, content);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("POST {Endpoint} failed: {StatusCode}", endpoint, response.StatusCode);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during POST {Endpoint}", endpoint);
                return false;
            }
        }

   

    }
}
