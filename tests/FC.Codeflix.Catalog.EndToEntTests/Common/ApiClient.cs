using System.Text;
using System.Text.Json;

namespace FC.Codeflix.Catalog.EndToEntTests;

public class ApiClient(HttpClient httpClient)
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<(HttpResponseMessage, TOutput)> Post<TOutput>(string route, object payload)
    {
        var response = await _httpClient.PostAsync(
            route,
            new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json")
        );
        var outputString = await response.Content.ReadAsStringAsync();
        if (string.IsNullOrWhiteSpace(outputString))
        {
            return (response, default!);
        }
        var output = JsonSerializer.Deserialize<TOutput>(
            outputString,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );
        return (response, output!);
    }
}
