using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using ToDo.Api;

namespace ToDo.UnitTest
{
    public class IntegrationTest
    {

        protected readonly HttpClient TestClient;

        public IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>();
            TestClient = appFactory.CreateClient();
        }
    }
}
