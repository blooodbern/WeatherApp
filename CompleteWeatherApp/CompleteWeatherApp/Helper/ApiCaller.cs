using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CompleteWeatherApp.Helper
{
    public class ApiCaller
    {
        private static readonly string host = "45.95.96.237";
        private static readonly string port = "8796";
        private static readonly string login = "doyuecum-dest";
        private static readonly string password = "lmn338d1i01t";

        public static async Task<ApiResponse> Get(string url)
        {
            HttpClientHandler httpHandler = new HttpClientHandler()
            {
                Proxy = new WebProxy($"http://{host}:{port}", true, null, new NetworkCredential() { UserName = login, Password = password })
            };

            using (HttpClient client = new HttpClient(httpHandler))
            {

                var request = await client.GetAsync(url);
                if (request.IsSuccessStatusCode)
                {
                    return new ApiResponse { Response = await request.Content.ReadAsStringAsync() };
                }
                else
                    return new ApiResponse { ErrorMessage = request.ReasonPhrase };
            }
        }
    }

    public class ApiResponse
    {
        public bool Successful => ErrorMessage == null;
        public string ErrorMessage { get; set; }
        public string Response { get; set; }
    }
}
