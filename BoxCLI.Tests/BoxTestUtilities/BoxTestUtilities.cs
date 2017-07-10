using BoxCLI.BoxHome;
using BoxCLI.BoxHome.Models;
using BoxCLI.BoxHome.Models.BoxConfigFile;
using BoxCLI.BoxPlatform;
using BoxCLI.BoxPlatform.Cache;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Caching.Memory;
using Moq;

namespace BoxCLI.Tests.TestUtilities
{
    public static class BoxTestUtilities
    {
        // BoxPlatformSettings no longer part of project
        /*public static BoxPlatformSettings GetBoxPlatformSettings()
        {
            var settings = new BoxPlatformSettings()
            {
                ClientId = "123",
                ClientSecret = "321",
                EnterpriseId = "789",
                JwtPrivateKeyPassword = "password",
                JwtPublicKeyId = "987"
            };
            return settings;
        }*/

        public static IBoxHome GetHomeMock()
        {
            var boxHome = new Mock<IBoxHome>();
            return boxHome.Object;
        }

        public static BoxEnvironments GetEnvironmentMock()
        {
            var environment = new Mock<BoxEnvironments>(null,null,null);
            //var environments = new Mock<EnvironmentsModel>();
            //var environment = new Mock<BoxHomeConfigModel>();
            return environment.Object;
        }

        public static BoxAppAuth GetBoxAppAuthMock()
        {
            var appAuth = new Mock<BoxAppAuth>();
            return appAuth.Object;
        }

        public static BoxAppSettings GetBoxAppSettingsMock()
        {
            var appSettings = new Mock<BoxAppSettings>();
            return appSettings.Object;
        }

        public static BoxConfigFileModel GetBoxConfigFileModelMock()
        {
            var configFileModel = new Mock<BoxConfigFileModel>();
            return configFileModel.Object;
        }

        public static BoxHomeConfigModel GetBoxHomeConfigModelMock()
        {
            var configModel = new Mock<BoxHomeConfigModel>();
            return configModel.Object;
        }
        
        // Err not working yet
        /*
        public static ILogger GetLoggerMock()
        {
            var mock = new Mock<ILogger<BoxCLI.BoxHome.BoxHomeDirectory>>();
            ILogger<BoxCLI.BoxHome.BoxHomeDirectory> logger = mock.Object;
            return logger;
        }
        */

        public static IBoxPlatformCache GetBoxPlatformCache()
        {
            var boxCache = new Mock<IBoxPlatformCache>();
            var cache = new Mock<IMemoryCache>();
            return boxCache.Object;
        }
    }
}