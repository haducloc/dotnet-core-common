using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCore.Common.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.Common.Mvc
{
    public class ClientUpdateResult : IActionResult
    {
        public string Link { get; set; }

        public string Message { get; set; }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(
                new Result<string>() {
                    IsError = true,
                    Link = AssertUtils.AssertNotNull(Link),
                    Message = AssertUtils.AssertNotNull(Message)
                })
            {
                StatusCode = StatusCodes.Status409Conflict
            };

            await objectResult.ExecuteResultAsync(context);
        }
    }
}
