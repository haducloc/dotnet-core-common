using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NetCore.Common.Auth;
using NetCore.Common.Http;
using NetCore.Common.Services;
using NetCore.Common.Utils;
using System.Linq;

namespace NetCore.Common.Mvc
{
    public class DebugController : MvcController
    {
        public DebugController(Config config) : base(config)
        {
        }

        public string ShowEnv()
        {
            return Config.EnvironmentName;
        }
    }
}
