using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using ServerStudy.Controllers;
using Xunit;

namespace ServerStudyTests
{
    public class BasicTests : IClassFixture<WebApplicationFactory<ServerStudy.Startup>>
    {
        private readonly WebApplicationFactory<ServerStudy.Startup> _factory;

        public BasicTests(WebApplicationFactory<ServerStudy.Startup> factory)
        {
            _factory = factory;
        }

        // ASP Core ���� �׽�Ʈ ����
        // ������ ��ũ
        // https://docs.microsoft.com/ko-kr/aspnet/core/test/integration-tests?view=aspnetcore-3.1
        //[Theory]
        //[InlineData("/")]
        //[InlineData("/Index")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            var client = _factory.CreateClient();


            // Act
            var response = await client.GetAsync(url);


            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }



        // ��Ʈ�ѷ� ���� �׽�Ʈ ����
        // ������ ��ũ
        // https://docs.microsoft.com/ko-kr/aspnet/core/mvc/controllers/testing?view=aspnetcore-3.1
        [Fact]
        public async Task Controller_UnitTest()
        {
            // Arrange
            var controller = new EchoController(new EchoController.EchoRepoTest());
            int iUserID = 1;
            string strValue = "1234";

            // Act
            var result = await controller.Post_Async(new EchoController.Packet{ UserID = iUserID, stringValue = strValue });


            // Assert
            Assert.Equal(iUserID, result.UserID);
            Assert.Equal(strValue, result.stringValue);
        }
    }
}