using System;

namespace NowDisplaying
{
    public class Spotify
    {
        public string RedirectUri { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string AccessToken { get; set; }
        public DateTime TokenExpiration { get; set; }
    }

    public class Settings
    {
        public int DisplayIndex { get; set; }
        public bool StartOnWindows { get; set; }
        public bool MinimizedOnStart { get; set; }
    }

    public class UserInterface
    {
        public int BackgroundBlur { get; set; }
    }

    public class ConfigFile
    {
        public Spotify Spotify { get; set; }
        public Settings Settings { get; set; }
        public UserInterface UserInterface { get; set; }
    }
}
