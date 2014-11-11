namespace CodeFist.Core
{
    using System;
    using System.Collections.Specialized;
    using System.Configuration;

    public class AppConfig
    {
        public string GitHubClientId { get; set; }

        public string GitHubClientSecret { get; set; }
        
        public TimeSpan SessionTimeout { get; set; }

        public string JwtSigningKey { get; set; }

        public string JwtAudience { get; set; }

        public string JwtRealm { get; set; }

        public bool AllowAccessByDefault { get; set; }

        public static AppConfig Create()
        {
            var appSettings = ConfigurationManager.AppSettings;

            return new AppConfig
            {
                GitHubClientId = GetEnvironmentVariableOrSetting("GitHubClientId"),
                GitHubClientSecret = GetEnvironmentVariableOrSetting("GitHubClientSecret"),
                JwtSigningKey = GetEnvironmentVariableOrSetting("JwtSigningKey"),
                JwtAudience = GetEnvironmentVariableOrSetting("JwtAudience"),
                JwtRealm = GetEnvironmentVariableOrSetting("JwtRealm"),
                SessionTimeout = TimeSpan.Parse(appSettings["SessionTimeout"]),
                AllowAccessByDefault = bool.Parse(appSettings["AllowAccessByDefault"])
            };
        }

        public static string GetEnvironmentVariableOrSetting(string name)
        {
            return Environment.GetEnvironmentVariable("CodeFist" + name) ?? ConfigurationManager.AppSettings[name];
        }
    }
}