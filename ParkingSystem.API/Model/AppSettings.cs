namespace ParkingSystem.API.Model
{
    public class AppSettings
    {
        public Logging logging { get; set; }
        public TokenOptions tokenOptions { get; set; }
        public string AllowedHosts { get; set; }
        public ConnectionStrings connectionStrings { get; set; }
        public string origins { get; set; }


        public class Logging
        {
            public LogLevel LogLevel { get; set; }
        }

        public class LogLevel
        {
            public string Default { get; set; }
        }

        public class TokenOptions
        {
            public string Audience { get; set; }
            public string Issuer { get; set; }
            public string AccessTokenExpiration { get; set; }
        }

        public class ConnectionStrings
        {
            public string DefaultConnection { get; set; }
        }

        public class AuthenticationOption
        {
            public int AccessTokenValidDays { get; set; }
        }
    }
}
