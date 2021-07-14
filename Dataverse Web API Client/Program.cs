using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Dataverse_Web_API_Client
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            DataverseWebAPIService svc = new DataverseWebAPIService();

            var contact = await svc.GetAsync("contacts(387687d5-a6e4-eb11-bacb-0022489ba30f)");

            
            Console.WriteLine(contact);
        }
    }
}
