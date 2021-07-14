using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Dataverse_Web_API_Client
{
    public class DataverseAuthenticationService
    {
        private string serviceUrl;
        private string clientId;
        private string secret;
        private string tenantId;

        public DataverseAuthenticationService()
        {
            serviceUrl = ConfigurationManager.AppSettings["serviceUrl"];
            clientId = ConfigurationManager.AppSettings["clientId"];
            secret = ConfigurationManager.AppSettings["secret"];
            tenantId = ConfigurationManager.AppSettings["tenantId"];
        }

        public AuthenticationHeaderValue GetAuthHeader()
        {
            var token = GetTokenAsync().Result;
            return new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<string> GetTokenAsync()
        {
            var authContext = new AuthenticationContext($"https://login.microsoftonline.com/{tenantId}");
            var credential = new ClientCredential(clientId, secret);

            var result = await authContext.AcquireTokenAsync(serviceUrl, credential);

            return result.AccessToken;
        }
    }
}
