﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Westwind.Utilities.Configuration.Tests
{
    /// <summary>
    /// Default implementation that uses only base constructors
    /// for configuration.
    /// 
    /// Default setup allows for no configuration of the provider
    /// since we're just calling back to the base constructors
    /// 
    /// Note: for config files ONLY you can implement the default 
    /// constructor automatically since no serialization is used.
    /// When using XML, String, Database the default constructor 
    /// needs to be left at default to avoid recursive loading
    /// </summary>
    class AutoConfigFileConfiguration : Westwind.Utilities.Configuration.AppConfiguration
    {
        public string ApplicationName { get; set; }
        public DebugModes DebugMode { get; set; }
        public int MaxDisplayListItems { get; set; }
        public bool SendAdminEmailConfirmations { get; set; }
        public string MailServer { get; set; }
        public string MailServerPassword { get; set; }

        /// <summary>
        /// Type that has ToString() and FromString() methods
        /// to allow serialization
        /// </summary>
        public LicenseInformation License { get; set; }
        
        public AutoConfigFileConfiguration()
        {
            ApplicationName = "Configuration Tests";
            DebugMode = DebugModes.Default;
            MaxDisplayListItems = 15;
            SendAdminEmailConfirmations = false;
            MailServer = "mail.MyWickedServer.com:334";
            MailServerPassword = "seekrity";
            License = new LicenseInformation()
            {
                Name = "User 1",
                Company = "Company 1",
                LicenseKey = 10
            };
        }


    }

    /// <summary>
    /// This version of the class internally calls Initialize
    /// to read configuration information immediately from
    /// itself so no explicit call to Initialize is required
    /// </summary>
    class AutoConfigFile2Configuration : AppConfiguration
    {
        public string ApplicationName { get; set; }
        public DebugModes DebugMode { get; set; }
        public int MaxDisplayListItems { get; set; }
        public bool SendAdminEmailConfirmations { get; set; }

        public AutoConfigFile2Configuration()
        {
            ApplicationName = "Configuration Tests";
            DebugMode = DebugModes.Default;
            MaxDisplayListItems = 15;
            SendAdminEmailConfirmations = false;

            // Automatically initialize this one
            this.Initialize();
        }
    }

    /// <summary>
    /// This version of the class internally calls Initialize
    /// to read configuration information immediately from
    /// itself so no explicit call to Initialize is required
    /// </summary>
    class AppSettingsConfigFileConfiguration : AppConfiguration
    {
        public string ApplicationName { get; set; }
        public DebugModes DebugMode { get; set; }
        public int MaxDisplayListItems { get; set; }
        public bool SendAdminEmailConfirmations { get; set; }

        public AppSettingsConfigFileConfiguration()
        {
            ApplicationName = "Configuration Tests";
            DebugMode = DebugModes.Default;
            MaxDisplayListItems = 15;
            SendAdminEmailConfirmations = false;

            // Automatically initialize this one
            Initialize();
        }

        protected override IConfigurationProvider OnCreateDefaultProvider(string sectionName, object configData)
        {
            Provider = new ConfigurationFileConfigurationProvider<AppSettingsConfigFileConfiguration>()
            {
                 ConfigurationSection = null // Forces to AppSettings                  
            };

            return Provider;
        }
    }
}
