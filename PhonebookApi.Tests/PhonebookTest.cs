using PhonebookApi.Controllers;
using System;
using Xunit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using PhonebookApi.ViewModels;
using Newtonsoft.Json;
using System.Text;

namespace PhonebookApi.Tests
{
    public class PhonebookTest
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public PhonebookTest()
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder().ConfigureAppConfiguration(SetupConfiguration)
                                     .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        private static void SetupConfiguration(WebHostBuilderContext ctx, IConfigurationBuilder builder)
        {
            // Removing the default configuration options
            builder.Sources.Clear();

            builder.AddJsonFile("config.json", false, true)
                   .AddEnvironmentVariables();
        }

        [Fact]
        public async Task CreateTokenTest()
        {
            _client.BaseAddress = new Uri("http://localhost:49892");
            var content = JsonConvert.SerializeObject(new LoginViewModel { Username = "admin", Password = "P@ssw0rd!" });

            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/Login", stringContent);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            var token = JsonConvert.DeserializeObject<TokenViewModel>(responseString);
            Assert.True(token.Expiration > DateTime.Now);
        }



    //    [Fact]
    //    public void Test1()
    //    {
          


    //        var mockUserManager = MockUserManager<IdentityUser>();

    //        var mockSignInManager = MockSignInManager<IdentityUser>();
    //        var result = new SignInResult();
    //        var test= result.Succeeded;

    //        mockSignInManager.Setup(m => m.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), true, false))
    //               .Returns(Task.FromResult<SignInResult>(result));


    //        var loginController = new LoginController(GetConfiguration(), mockUserManager.Object, mockSignInManager.Object);

    //        var token = loginController.CreateToken(new ViewModels.LoginViewModel { Username = "admin", Password = "P@ssw0rd!" });

            
    //    }


    //    public static IConfiguration GetConfiguration()
    //    {
    //        var config = new ConfigurationBuilder()
    //.AddJsonFile("config.json")
    //.Build();

    //        return config;
    //    }

    //    public static Mock<SignInManager<TUser>> MockSignInManager<TUser>() where TUser : class
    //    {
    //        var context = new Mock<HttpContext>();
    //        var manager = MockUserManager<TUser>();
    //        return new Mock<SignInManager<TUser>>(manager.Object,
    //            new HttpContextAccessor { HttpContext = context.Object },
    //            new Mock<IUserClaimsPrincipalFactory<TUser>>().Object,
    //            null, null)
    //        { CallBase = true };
    //    }

    //    public static Mock<UserManager<TUser>> MockUserManager<TUser>() where TUser : class
    //    {
    //        IList<IUserValidator<TUser>> UserValidators = new List<IUserValidator<TUser>>();
    //        IList<IPasswordValidator<TUser>> PasswordValidators = new List<IPasswordValidator<TUser>>();

    //        var store = new Mock<IUserStore<TUser>>();
    //        UserValidators.Add(new UserValidator<TUser>());
    //        PasswordValidators.Add(new PasswordValidator<TUser>());
    //        var mgr = new Mock<UserManager<TUser>>(store.Object, null, null, UserValidators, PasswordValidators, null, null, null, null, null);
    //        return mgr;
    //    }
    }
}
