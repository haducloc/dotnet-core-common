using Microsoft.AspNetCore.Mvc;
using NetCore.Common.Auth;
using NetCore.Common.Services;
using NetCore.Common.Utils;
using System.Collections.Generic;
using System.Linq;

namespace NetCore.Common.Mvc
{
    public abstract class MvcController : Controller
    {
        public Config Config { get; }

        public MvcController(Config config)
        {
            this.Config = config;
        }

        public int UserID
        {
            get
            {
                AssertUtils.AssertTrue(User.Identity.IsAuthenticated);
                return User.GetUserID();
            }
        }

        public string DisplayName
        {
            get
            {
                AssertUtils.AssertTrue(User.Identity.IsAuthenticated);
                return User.GetDisplayName();
            }
        }

        public bool IsAnyRole(params string[] roles)
        {
            AssertUtils.AssertTrue(User.Identity.IsAuthenticated);
            return roles.Any(role => User.IsInRole(role));
        }

        public BadRequestObjectResult InvalidModelState()
        {
            var result = new Result<IDictionary<string, IList<string>>> { IsError = true, Message = "Model is invalid." };
            if (Config.IsDebugEnvironment)
            {
                result.Data = ModelState.ToErrorMap();
            }
            return BadRequest(result);
        }
    }
}
