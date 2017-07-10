using System.IO;
using BoxCLI.BoxPlatform;
using BoxCLI.BoxHome;
using BoxCLI.BoxPlatform.Cache;
using BoxCLI.BoxPlatform.Service;
using BoxCLI.Tests.TestUtilities;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace BoxCLI.Tests
{
    public class BoxEnvironmentsTests
    {
        [Fact]
        public void Can_Set_BoxEnvironments()
        {
            var boxHome = BoxTestUtilities.GetHomeMock();
            var logger = new Mock<ILogger<BoxCLI.BoxHome.BoxHomeDirectory>>().Object;
            //ILogger<BoxCLI.BoxHome.BoxHomeDirectory> logger = BoxTestUtilities.GetLoggerMock();
            var boxEnvironment = new BoxEnvironments("box_test_environments.json",boxHome,logger);
            
        }

        [Fact]
        public void Can_VerifyBoxConfigFile_With_Valid_File()
        {
            var environment = BoxTestUtilities.GetEnvironmentMock();
            Assert.True(environment.VerifyBoxConfigFile(@"..\..\..\..\..\BoxCLI.Tests\valid_configs.json"));
        }

        [Fact]
        // This currently does not fail successfully.
        // You can pass in an empty file, it will create broken config
        // BoxConfig in Box SDK does not check that all values exist
        public void Fail_VerifyBoxConfigFile_With_Invalid_File()
        {
            var environment = BoxTestUtilities.GetEnvironmentMock();
            Assert.False(environment.VerifyBoxConfigFile(@"..\..\..\..\..\BoxCLI.Tests\invalid_configs.json"));
        }

        [Fact]
        public void Fail_VerifyBoxConfigFile_Without_File()
        {
            var environment = BoxTestUtilities.GetEnvironmentMock();
            Assert.False(environment.VerifyBoxConfigFile(@"nonsense\path"));
        }

        [Fact]
        public void Can_TranslateConfigFileToEnvironment_With_Valid_File()
        {
            var environment = BoxTestUtilities.GetEnvironmentMock();
            var config = environment.TranslateConfigFileToEnvironment(@"..\..\..\..\..\BoxCLI.Tests\valid_configs.json");
            Assert.Equal("123",config.ClientId);
            Assert.Equal("456",config.ClientSecret);
            Assert.Equal("789",config.EnterpriseId);
            Assert.Equal("abc",config.JwtPublicKeyId);
            Assert.Equal("longprivatekey",config.JwtPrivateKey);
            Assert.Equal("privatepassword",config.JwtPrivateKeyPassword);
        }

        [Fact]
        // This currently does not fail successfully
        // You can pass in an empty file 
        // Need to validate that all variables are there
        public void Fail_TranslateConfigFileToEnvironment_With_Invalid_File()
        {
            var environment = BoxTestUtilities.GetEnvironmentMock();
            var config = environment.TranslateConfigFileToEnvironment(@"..\..\..\..\..\BoxCLI.Tests\invalid_configs.json");
            Assert.Equal("123",config.ClientId);
            Assert.Equal("456",config.ClientSecret);
            Assert.Equal("789",config.EnterpriseId);
            Assert.Equal("abc",config.JwtPublicKeyId);
            Assert.Equal(null,config.JwtPrivateKey);
            Assert.Equal("privatepassword",config.JwtPrivateKeyPassword);
        }

        [Fact]
        // Double check with AM
        public void Fail_TranslateConfigFileToEnvironment_Without_File()
        {
            var environment = BoxTestUtilities.GetEnvironmentMock();
            var config = environment.TranslateConfigFileToEnvironment(@"nonsense\path");
            Assert.Equal(null,config.ClientId);
            Assert.Equal(null,config.ClientSecret);
            Assert.Equal(null,config.EnterpriseId);
            Assert.Equal(null,config.JwtPublicKeyId);
            Assert.Equal(null,config.JwtPrivateKey);
            Assert.Equal(null,config.JwtPrivateKeyPassword);
        }

        [Fact]
        public void Can_AddNewEnvironment_With_Valid_Environment()
        {
            var environment = BoxTestUtilities.GetBoxHomeConfigModelMock();
            
        }

        // GetBoxPlatformSettings not part of the project
        /*
        [Fact]
        public void CanGet_BoxPlatformSettings_Manually()
        {
            var options = BoxTestUtilities.GetBoxPlatformSettings();
            Assert.Equal("123", options.ClientId);
            Assert.Equal("321", options.ClientSecret);
            Assert.Equal("789", options.EnterpriseId);
            Assert.Equal("/private_key.pem", options.JwtPrivateKeyFilePath);
            
        }
        */
    }
}