using System.Net.Http;

namespace CoinField.Api
{
    internal class RequestContext
    {
        public HttpRequestMessage HttpRequest { get; set; }
    }
}