using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using StudyServer.Controllers;
using Xunit;

namespace StudyServerTests
{
    public class BasicTests : IClassFixture<WebApplicationFactory<StudyServer.Startup>>
    {
        private readonly WebApplicationFactory<StudyServer.Startup> _factory;

        public BasicTests(WebApplicationFactory<StudyServer.Startup> factory)
        {
            _factory = factory;
        }

        // ASP Core 통합 테스트 예시
        // 참고한 링크
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



        // 컨트롤러 단위 테스트 예시
        // 참고한 링크
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