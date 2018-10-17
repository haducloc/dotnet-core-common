using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NetCore.Common.Auth;
using NetCore.Common.Services;
using NetCore.Common.Utils;
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

        public bool IsAnyRole(params string[] roles)
        {
            AssertUtils.AssertTrue(User.Identity.IsAuthenticated);
            return roles.Any(role => User.IsInRole(role));
        }

        public BadRequestObjectResult InvalidModelState()
        {
            Result<ModelStateDictionary> result = new Result<ModelStateDictionary> { IsError = true, Message = "Model is invalid." };
            if (Config.IsDebugEnvironment)
            {
                result.Data = ModelState;
            }
            return BadRequest(result);
        }
    }
}
