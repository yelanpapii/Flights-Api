using AutoMapper;
using Business;
using Business.Common;
using Business.Repository.Interface;
using Business.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using RichardSzalay.MockHttp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace GetFlightsTests
{
    public class UnitTest1
    {
        private readonly Mock<IHttpClientFactory> _httpClientFactoryMock = new();
        private readonly Mock<IApiUrl> _apiUrlMock = new();
        private readonly Mock<IFlightRepository> _flightReposityMock = new();
        private readonly Mock<ILogger<FlightsService>> _loggerMock = new();
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly MockHttpMessageHandler _httpMessageHandler = new();

        private readonly Uri url;

        public UnitTest1()
        {
            url = new Uri("https://test.api/api/test");

            //Setup para el HttpClient.
            _httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>()))
                .Returns(() => new HttpClient(_httpMessageHandler));

            _apiUrlMock.Setup(x => x.Url).Returns(url);
        }

        [Theory]
        [InlineData("MZL", "MDE", 2)]
        [InlineData("BCN", "BAQ", 2)]
        [InlineData("BAQ", "BOG", 0)]
        public async Task ShouldGetFlightsTheory(string origin, string destination, int expectedCount)
        {
            //Arrange
            //Este sera el Response "Fake" que se retornara
            var flightsResponse = new List<ApiFlightResponseDTO>
            {
               new ApiFlightResponseDTO("MZL", "BCN", "CO", "1001", 200),
               new ApiFlightResponseDTO("BCN", "MDE", "CO", "1041", 200),
               new ApiFlightResponseDTO("BCN", "CTG", "CO", "1010", 200),
               new ApiFlightResponseDTO("CTG", "BAQ", "CO", "1081", 200),
            };

            var json = JsonConvert.SerializeObject(flightsResponse);

            //Cuando se llame a la api, retornara nuestros datos fake.
            _httpMessageHandler.When(url.AbsoluteUri)
                .Respond("application/json", json);

            var flightsService = new FlightsService(
                _httpClientFactoryMock.Object,
                _apiUrlMock.Object,
                _flightReposityMock.Object,
                _loggerMock.Object,
                _mapperMock.Object);

            //Act

            var flights = await flightsService.GetAllFlightAsync(origin, destination);

            //Assert
            Assert.NotNull(flights);
            Assert.Equal(expectedCount, flights.Count());
        }
    }
}