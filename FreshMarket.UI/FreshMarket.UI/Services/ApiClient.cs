using System.Net.Http.Headers;

namespace FreshMarket.UI.Services
{
    public class ApiClient
    {
        private const string urlBase = "https://localhost:7181/api";
        private readonly HttpClient _client;

        public ApiClient()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(urlBase);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public HttpResponseMessage Get(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_client.BaseAddress?.AbsolutePath}/{url}");
            var response = _client.Send(request);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Request failed");
            }

            return response;
        }

        //public HttpResponseMessage Post(string url, object body)
        //{

        //}

        //public HttpResponseMessage Put(string url, object body)
        //{

        //}

        //public HttpResponseMessage Delete(string url)
        //{

        //}
    }
}
