using Condor.Client.Helpers;
using System.Text;
using System.Text.Json;

namespace Condor.Client.Repositorio
{
    public class Repositorio : IRepositorio
    {
        private readonly HttpClient _httpClient;
        private JsonSerializerOptions JsonSerializerOptionsDefault = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
        private readonly SpinnerService _spinnerService;

        public Repositorio(HttpClient httpClient, SpinnerService spinnerService)
        {
            _httpClient = httpClient;
            _spinnerService = spinnerService;
        }

        public async Task<HttpResponseWrapper<object>> Post<T>(string url, T enviar)
        {
            _spinnerService.Show();
            var enviarJson = JsonSerializer.Serialize(enviar);
            var enviarContent = new StringContent(enviarJson, Encoding.UTF8, "application/json");

            var responseHttp = await _httpClient.PostAsync(url, enviarContent);
            _spinnerService.Hide();

            return new HttpResponseWrapper<object>(null, !responseHttp.IsSuccessStatusCode, responseHttp);
        }

        public async Task<HttpResponseWrapper<TResponse>> Post<T, TResponse>(string url, T enviar)
        {
            _spinnerService.Show();
            var enviarJson = JsonSerializer.Serialize(enviar);
            var enviarContent = new StringContent(enviarJson, Encoding.UTF8, "application/json");

            var responseHttp = await _httpClient.PostAsync(url, enviarContent);

            if (responseHttp.IsSuccessStatusCode)
            {

                var response = await DeserializarRespuesta<TResponse>(responseHttp, JsonSerializerOptionsDefault);
                _spinnerService.Hide();
                return new HttpResponseWrapper<TResponse>(response, false, responseHttp);

            }
            else
            {
                _spinnerService.Hide();
                return new HttpResponseWrapper<TResponse>(default, true, responseHttp);
            }
        }

        public async Task<HttpResponseWrapper<T>> Get<T>(string url)
        {
            _spinnerService.Show();

            var responseHttp = await _httpClient.GetAsync(url);

            if (responseHttp.IsSuccessStatusCode)
            {
                var response = await DeserializarRespuesta<T>(responseHttp, JsonSerializerOptionsDefault);

                _spinnerService.Hide();
                return new HttpResponseWrapper<T>(response, false, responseHttp);
            }
            else
            {
                _spinnerService.Hide();
                return new HttpResponseWrapper<T>(default, true, responseHttp);
            }
        }

        public async Task<HttpResponseWrapper<object>> Delete(string url)
        {
            var responseHTTP = await _httpClient.DeleteAsync(url);
            return new HttpResponseWrapper<object>(null, !responseHTTP.IsSuccessStatusCode, responseHTTP);
        }

        private async Task<T> DeserializarRespuesta<T>(HttpResponseMessage httpResponse, JsonSerializerOptions jsonOptions)
        {
            var responseString = await httpResponse.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<T>(responseString, jsonOptions);
        }

    }
}
