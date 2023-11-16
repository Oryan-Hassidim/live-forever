using Microsoft.Azure.Functions.Worker.Http;
using System.Text.Json;

namespace Api;


public static class Helpers
{
    /// <summary>
    /// Tries to get the value of the specified query parameter from the HttpRequestData object.
    /// </summary>
    /// <param name="req">The HttpRequestData object.</param>
    /// <param name="key">The key of the query parameter to retrieve.</param>
    /// <param name="value">The value of the query parameter, if found.</param>
    /// <returns>True if the query parameter was found, false otherwise.</returns>
    public static bool TryGetQueryParameter(this HttpRequestData req, string key, out string? value)
    {
        value = null;
        if (!req.Query.HasKeys())
            return false;
        if (req.Query.AllKeys.Contains(key))
        {
            value = req.Query[key];
            return true;
        }
        return false;
    }
    public static async Task<T?> FromBody<T>(this HttpRequestData req)
    {
        if (req.Body is null)
            return default;
        if (req.Body.Length == 0)
            return default;
        if (!req.Headers.GetValues("Header").Contains("application/json"))
            return default;
        var content = await new StreamReader(req.Body).ReadToEndAsync();
        return JsonSerializer.Deserialize<T>(content);
    }
}
