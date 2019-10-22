using CoinField.Api.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;

namespace CoinField.Api.Tests
{
    /// <summary>
    /// Test the paths and deserialization of the public API methods.
    /// </summary>
    [TestClass]
    public class PublicApiTests
    {
        private static FakeHttpMessageHandler _fakeHttpMessageHandler = new FakeHttpMessageHandler();
        private HttpClient _fakeHttpClient = new HttpClient(_fakeHttpMessageHandler)
        {
            BaseAddress = new Uri("https://api.coinfield.com/v1/")
        };

        [TestMethod]
        public void GetStatus_ShouldReturnStatus()
        {
            // Arange
            var state = "ok";
            var status = new StatusResponse() { Status = state};

            var response = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(status), Encoding.UTF8, "application/json")
            };

            _fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.coinfield.com/v1/status"), response);

            StatusResponse result;

            // Act
            using (var client = new CoinFieldClient(_fakeHttpClient))
            {
                result = client.GetStatusAsync().Result;
            }

            // Assert
            Assert.IsInstanceOfType(result, typeof(StatusResponse));
            Assert.AreEqual(state, result.Status);
        }

        [TestMethod]
        public void GetTimestamp_ShouldReturnISO8601()
        {
            // Arange
            var utcNow = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
            var timestamp = new TimestampResponse() { Timestamp = utcNow };

            var response = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(timestamp), Encoding.UTF8, "application/json")
            };

            _fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.coinfield.com/v1/timestamp"), response);

            TimestampResponse result;

            // Act
            using (var client = new CoinFieldClient(_fakeHttpClient))
            {
                result = client.GetTimestampAsync().Result;
            }

            // Assert
            Assert.IsInstanceOfType(result, typeof(TimestampResponse));
            Assert.AreEqual(utcNow, result.Timestamp);
        }

        [TestMethod]
        public void GetCurrencies_ShouldReturnCurrenciesList()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void GetMarkets_ShouldReturnMarketsList()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void GetTickers_ShouldReturnTickersList()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void GetTicker_WhenGivenMarket_ShouldReturnTicker()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void GetOrderBook_ShouldReturnOrderBook()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void GeDepth_ShouldReturnDepth()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void GeDepth_WhenGivenLimit_ShouldReturnLimitedDepth()
        {
            throw new NotImplementedException();
        }
    }
}
