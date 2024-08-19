using System.Text.Json;
using System.Text;

using Bsd.Application.Interfaces;
using Bsd.Application.DTOs;
using Bsd.Application.Utilities;
using Bsd.ExternalService_.Models;

namespace Bsd.ExternalService.Service
{
    public class ExternalApiService : IExternalApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _urlApi;
        private readonly string _identifier;
        private readonly string _token;

        public ExternalApiService()
        {
            _httpClient = new HttpClient();
            _urlApi = AppSettings.ApiUrl;
            _identifier = AppSettings.Identifier;
            _token = AppSettings.Token;

        }

        public async Task<ICollection<MarkResponse>> GetMarkAsync(MarkRequest request)
        {
            string requestJson = JsonSerializer.Serialize(request);
            var content = new StringContent(requestJson, Encoding.UTF8, "application/json");
            content.Headers.Add("identifier", _identifier);
            content.Headers.Add("key", _token);

            HttpResponseMessage response = await _httpClient.PostAsync($"{_urlApi}/RestServiceApi/Mark/GetMarks", content);
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonSerializer.Deserialize<ApiResponse<List<MarkResponse>>>(responseJson);

            if (apiResponse!.Sucesso)
            {
                return apiResponse.Obj;
            }
            else
            {
                throw new InvalidOperationException($"Erro na requisição: {apiResponse.Mensagem}");
            }
        }

    }
}
