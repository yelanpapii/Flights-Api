using AutoMapper;
using Business;
using Business.MapperExtensions;
using Business.Repository.Interface;
using Business.Services;
using Business.Services.Interface;
using DataAccess.Models;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace FlightsTests
{
    public class UnitTest2
    {
        private readonly Mapper _mapperMock;
        private readonly Mock<IFlightsService> _flightsServiceMock = new();
        private readonly Mock<IJourneyRepository> _journeyReposityMock = new();
        private readonly Mock<ILogger<JourneyService>> _loggerMock = new();

        public UnitTest2()
        {
            var profile = new AutoMapProfile();
            var config = new MapperConfiguration(c => c.AddProfile(profile));
            _mapperMock = new(config);

            _journeyReposityMock.SetReturnsDefault(Task.CompletedTask);
        }

        [Theory]
        [InlineData("MZL", "MDE", "CO", "1043")]
        [InlineData("BCN", "BAQ", "CO", "1777")]
        [InlineData("BAQ", "BOG", "CO", "1543")]
        public async Task ShouldGetJourneyTheory(string origin, string destination, string fligthCarrier, string flightNumber)
        {
            //Arrange
            var transport = Transport.Create(fligthCarrier, flightNumber);
            var flightsResponse = new List<FlightDTO>
            {
               new FlightDTO(origin, destination, 200, transport),
               new FlightDTO(origin, "MDE", 200, transport),
               new FlightDTO("MDE", destination, 200, transport),
            };

            var expected = new JourneyDTO(origin, destination, flightsResponse.Sum(x => x.Price), flightsResponse);

            //Setup del servicio FlightsService llamado en JourneyService.
            _flightsServiceMock.Setup(x => x.GetAllFlightAsync(origin, destination))
                .ReturnsAsync(flightsResponse);

            var JourneyService = new JourneyService(
                _flightsServiceMock.Object,
                _mapperMock,
                _journeyReposityMock.Object,
                _loggerMock.Object);

            
            //Act
            var response = await JourneyService.GetJourneyAsync(origin, destination);

            //Assert
            Assert.NotNull(response);
            Assert.Equal(expected.Flights, response.Flights);
            Assert.IsAssignableFrom<JourneyDTO>(response);
        }

        //TODO : Should Add A Journey
    }
}