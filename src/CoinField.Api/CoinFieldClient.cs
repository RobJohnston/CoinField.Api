using CoinField.Api.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CoinField.Api
{
    public partial class CoinFieldClient : IDisposable
    {
        private string _url;
        private string _version;

        private static readonly CultureInfo _culture = CultureInfo.InvariantCulture;
        private readonly HttpClient _httpClient = new HttpClient();

        internal static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy() }
        };

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CoinFieldClient"/> class for calling public functions.
        /// </summary>
        public CoinFieldClient()
        {
            _url = "https://api.coinfield.com";
            _version = "v1";
            _httpClient.BaseAddress = new Uri($"{_url}/{_version}/");
        }

        public CoinFieldClient(Uri uri, string version)
        {
            _url = uri.ToString();
            _version = version;
            _httpClient.BaseAddress = new Uri($"{_url}{_version}/");
        }

        #endregion

        /// <summary>
        /// Sends a public GET request to the CoinField API as an asynchronous operation.
        /// </summary>
        /// <typeparam name="T">Type of data contained in the response.</typeparam>
        /// <param name="requestUrl">The relative URL the request is sent to.</param>
        /// <param name="args">Optional arguments passed as querystring parameters.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <remarks>The <paramref name="requestUrl"/> is relative to https://api.coinfield.com/v1/</remarks>
        /// <exception cref="ArgumentNullException"><paramref name="requestUrl"/> is <c>null</c>.</exception>
        /// <exception cref="HttpRequestException">There was a problem with the HTTP request.</exception>
        /// <exception cref="CoinFieldException">There was a problem with the CoinField API call.</exception>
        public async Task<T> QueryPublicAsync<T>(string requestUrl, Dictionary<string, string> args = null)
        {
            if (requestUrl == null)
                throw new ArgumentNullException(nameof(requestUrl));

            args = args ?? new Dictionary<string, string>(0);

            // Setup request.
            var urlEncodedArgs = UrlEncode(args);

            var req = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(_httpClient.BaseAddress, $"{requestUrl}?{urlEncodedArgs}")
            };

            // Send request and deserialize response.
            return await SendRequestAsync<T>(req).ConfigureAwait(false);
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }

        #region Private methods


        private async Task<T> SendRequestAsync<T>(HttpRequestMessage req)
        {
            var reqCtx = new RequestContext
            {
                HttpRequest = req
            };

            // Perform the HTTP request.
            var res = await _httpClient.SendAsync(reqCtx.HttpRequest).ConfigureAwait(false);

            var resCtx = new ResponseContext
            {
                HttpResponse = res
            };

            // Throw for HTTP-level error.
            //resCtx.HttpResponse.EnsureSuccessStatusCode();

            // Get the response.
            var jsonContent = await resCtx.HttpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
            Debug.WriteLine(req);
            Debug.WriteLine(res);
            Debug.WriteLine(jsonContent);

            // Check the response for errors.
            ProcessError(jsonContent);

            // Deserialize the response.
            var response = JsonConvert.DeserializeObject<T>(jsonContent, JsonSettings);

            return response;
        }

        private static string UrlEncode(Dictionary<string, string> args) => string.Join(
            "&",
            args.Where(x => x.Value != null).Select(x => x.Key + "=" + WebUtility.UrlEncode(x.Value))
        );

        /// <summary>
        /// Determine if the API returned and error and throw it if it did.
        /// </summary>
        private void ProcessError(string jsonContent)
        {
            try
            {
                var token = JToken.Parse(jsonContent);

                if (token is JObject)
                {
                    var response = token.ToObject<CoinFieldErrorResponse>();

                    if (!string.IsNullOrEmpty(response.Message))
                    {
                        var exception = new CoinFieldException(response.Message)
                        {
                            Status = response.Status,
                            Timestamp = response.Timestamp,
                            Errors = response.Errors
                        };

                        throw exception;
                    }
                }
            }
            catch (JsonReaderException ex)
            {
                // The 'status' call expects a string but an error expects an integer.
                // E.g., "Could not convert string to integer: ok."
                Trace.TraceWarning(ex.Message);
            }
        }

        #endregion
    }
}
