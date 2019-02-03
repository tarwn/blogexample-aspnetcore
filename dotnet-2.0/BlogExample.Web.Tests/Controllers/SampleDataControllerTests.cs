using BlogExample.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using static BlogExample.Web.Controllers.SampleDataController;

namespace BlogExample.Web.Tests.Controllers
{
    [TestFixture]
    public class SampleDataControllerTests
    {
        private SampleDataController _controller;

        [OneTimeSetUp]
        public void Setup()
        {
            _controller = new SampleDataController()
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                    {
                        // set context properties here
                    }
                }
            };
        }

        [Test]
        public void WeatherForecasts_NoParams_ReturnsAllForecasts()
        {
            var result = _controller.WeatherForecasts();

            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.IsInstanceOf<List<WeatherForecast>>(okResult.Value);
            var data = (List<WeatherForecast>)okResult.Value;
        }
    }
}
