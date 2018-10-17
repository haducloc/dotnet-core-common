using System;
using System.Threading.Tasks;

namespace NetCore.Common.Auth
{
    public class BasicAuthEvents
    {
        public Func<BasicPrincipalContext, Task> OnValidate { get; set; } = context => Task.CompletedTask;

        public virtual Task Validate(BasicPrincipalContext context) => this.OnValidate(context);
    }
}
