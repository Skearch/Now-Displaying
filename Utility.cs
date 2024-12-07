using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NowDisplaying
{
    public class Utility
    {
        public async Task OpenBrowserAsync(Uri uri)
        {
            await Task.Run(() => Process.Start(uri.ToString()));
        }
    }
}
