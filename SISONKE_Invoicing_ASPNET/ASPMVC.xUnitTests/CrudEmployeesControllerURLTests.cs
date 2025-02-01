using LLM_eCommerce_ASPNET;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPMVC.xUnitTests
{
    public class CrudEmployeesControllerURLTests : IClassFixture<WebApplicationFactory<Program>>
    {
        readonly WebApplicationFactory<Program> _applicationFactory;
        private readonly HttpClient _httpClient;

        public CrudEmployeesControllerURLTests(WebApplicationFactory<Program> factory)
        {
            _applicationFactory = factory;
            _httpClient = _applicationFactory.CreateClient();
        }

        [Fact]
        public async void _01_Test_Employee_CrudEmployees_Index_ReturnsOkResponse()
        {
            // Arrange
            var client = _applicationFactory.CreateClient();

            // Act
            var response = await client.GetAsync("Employee/CrudEmployees/Index");
            int statusCode = (int)response.StatusCode;

            // Assert
            Assert.NotNull(response);
            Assert.Equal(200, statusCode);

            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8",
            response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async void _02_Test_Employee_CrudEmployees_Invoices_ReturnsOkResponse()
        {
            // Arrange
            var client = _applicationFactory.CreateClient();

            // Act
            var response = await client.GetAsync("Employee/CrudEmployees/Invoices");
            int statusCode = (int)response.StatusCode;

            // Assert
            Assert.NotNull(response);
            Assert.Equal(200, statusCode);

            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8",
            response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async void _03_Test_Employee_CrudEmployees_Profile_ReturnsOkResponse()
        {
            // Arrange
            var client = _applicationFactory.CreateClient();

            // Act
            var response = await client.GetAsync("Employee/CrudEmployees/Profile");
            int statusCode = (int)response.StatusCode;

            // Assert
            Assert.NotNull(response);
            Assert.Equal(200, statusCode);

            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8",
            response.Content.Headers.ContentType.ToString());
        }
    }
}
