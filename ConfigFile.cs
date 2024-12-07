using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NowDisplaying
{
    public class ConfigFile
    {
        public string RedirectUri { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public int DisplayIndex { get; set; }
        public string AccessToken { get; set; }
        public DateTime TokenExpiration { get; set; }
    }
}
