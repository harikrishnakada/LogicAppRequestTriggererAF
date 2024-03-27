using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiClientLib.Auth
{
    public interface IAuthenticationProvider
    {
        Task<string> AcquireTokenAsync();
    }
}
