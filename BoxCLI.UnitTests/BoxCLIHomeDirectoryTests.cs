using System;
using Xunit;

namespace BoxCLI.UnitTests
{
    public class BoxCLIHomeDirectoryTests
    {
        [Fact]
        public void BaseDirectoryPath_GetsSet()
        {
            var boxHome = BoxTestUtilities.BoxTestUtilities.GetBoxHome();
            Assert.Equal(boxHome.GetBaseDirectoryPath(), Environment.GetEnvironmentVariable("HOME"));
            Environment.SetEnvironmentVariable("BOX_HOME", "/usr/local/bin");
            Assert.Equal(boxHome.GetBaseDirectoryPath(), "/usr/local/bin");
            Environment.SetEnvironmentVariable("BOX_HOME", "");
        }
        [Fact]
        public void BoxHomeDirectoryPath_GetsSet()
        {
            var boxHome = BoxTestUtilities.BoxTestUtilities.GetBoxHome();
            Assert.Equal(boxHome.GetBoxHomeDirectoryPath(), $"{Environment.GetEnvironmentVariable("HOME")}/.box");
        }
    }
}
