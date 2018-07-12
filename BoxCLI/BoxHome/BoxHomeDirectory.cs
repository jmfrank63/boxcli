using System;
// using System.Collections.Generic;
using System.IO;
using BoxCLI.BoxHome.Models;
using BoxCLI.BoxHome.BoxHomeFiles;
// using Microsoft.Extensions.Logging;
// using Microsoft.Extensions.Options;
using BoxCLI.CommandUtilities;
using BoxCLI.BoxPlatform.Cache;

namespace BoxCLI.BoxHome
{
    public class BoxHomeDirectory : IBoxHome
    {
        public readonly string BoxHomeDirectoryName;
        public readonly string BoxHomeEnvironmentVariable;

        // public readonly string BoxHomeSettingsFileName;

        public readonly BoxEnvironments BoxEnvironments;
        public readonly BoxPersistantCache BoxPersistantCache;
        public readonly BoxDefaultSettings BoxHomeDefaultSettings;

        public BoxHomeDirectory()
        {
            var settings = new BoxHomeSettings();
            BoxHomeDirectoryName = settings.BoxHomeDirectoryName;
            BoxHomeEnvironmentVariable = settings.BoxHomeEnvironmentVariable;
            CreateBoxHomeDirectory();

            BoxEnvironments = new BoxEnvironments(settings.BoxHomeEnvironmentsFileName, this);
            BoxPersistantCache = new BoxPersistantCache(settings.BoxHomeCacheFileName, this);
            BoxHomeDefaultSettings = new BoxDefaultSettings(settings.BoxHomeSettingsFileName, this);

        }

        public string GetBoxHomeDirectoryPath()
        {
            if (!CheckIfBoxHomeDirectoryExists()) return string.Empty;
            var home = GetBaseDirectoryPath();
            return Path.Combine(home, BoxHomeDirectoryName);
        }
        public void RemoveBoxHomeDirectory()
        {
            if (!CheckIfBoxHomeDirectoryExists()) return;
            var boxDir = GetBoxHomeDirectoryPath();
            Directory.Delete(boxDir, true);
        }

        public BoxEnvironments GetBoxEnvironments()
        {
            return BoxEnvironments;
        }
        public BoxPersistantCache GetBoxCache()
        {
            return BoxPersistantCache;
        }
        public BoxCachedToken BustCache()
        {
            return BoxPersistantCache.BustCache();
        }

        public BoxDefaultSettings GetBoxHomeSettings()
        {
            return BoxHomeDefaultSettings;
        }

        public string GetBaseDirectoryPath()
        {
            var home = Environment.GetEnvironmentVariable(BoxHomeEnvironmentVariable);
            if (string.IsNullOrEmpty(home))
            {
                home = Environment.GetEnvironmentVariable("HOME");
                if (string.IsNullOrEmpty(home))
                {
                    home = Environment.GetEnvironmentVariable("USERPROFILE");
                    if (string.IsNullOrEmpty(home))
                    {
                        throw new Exception($"Cannot locate the {BoxHomeDirectoryName} directory.");
                    }
                }
            }
            return home;
        }


        private void CreateBoxHomeDirectory()
        {
            var baseDirectoryPath = GetBaseDirectoryPath();
            var path = Path.Combine(baseDirectoryPath, BoxHomeDirectoryName);
            Directory.CreateDirectory(path);
        }

        private bool CheckIfBoxHomeDirectoryExists()
        {
            var baseDirectoryPath = GetBaseDirectoryPath();
            var path = Path.Combine(baseDirectoryPath, BoxHomeDirectoryName);
            try
            {
                return Directory.Exists(path);
            }
            catch (Exception e)
            {
                Reporter.WriteError(e.Message);
                return false;
            }
        }
    }
}