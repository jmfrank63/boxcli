using BoxCLI.BoxHome;
using BoxCLI.BoxPlatform.Cache;
using Moq;
// using Microsoft.Extensions.Caching.Memory;

namespace BoxCLI.UnitTests.BoxTestUtilities
{
    public static class BoxTestUtilities
    {
        public static IBoxPlatformCache GetBoxPlatformCache()
        {
            var boxCache = new Mock<IBoxPlatformCache>();
            // var cache = new Mock<IMemoryCache>();
            return boxCache.Object;
        }

        public static IBoxHome GetBoxHome()
        {
            return new BoxHomeDirectory();
        }
    }
}