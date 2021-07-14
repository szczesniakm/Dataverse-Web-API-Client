using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Dataverse_Web_API_Client
{
    public class DataverseWebAPIService
    {
        private readonly DataverseAuthenticationService _authService;
        private readonly HttpClient httpClient;

        public DataverseWebAPIService()
        {
            var serviceUrl = ConfigurationManager.AppSettings["serviceUrl"];
            var version = ConfigurationManager.AppSettings["version"];
            _authService = new DataverseAuthenticationService();


            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(serviceUrl + $"/api/data/v{version}/");
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );
            httpClient.DefaultRequestHeaders.Add("OData-MaxVersion", "4.0");
            httpClient.DefaultRequestHeaders.Add("OData-Version", "4.0");
            httpClient.DefaultRequestHeaders.Authorization = _authService.GetAuthHeader();
        }

        public async Task<JObject> GetAsync(string path)
        {
            try
            {
                var response = await httpClient.GetAsync(path);

                return JObject.Parse(await response.Content.ReadAsStringAsync());
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}
