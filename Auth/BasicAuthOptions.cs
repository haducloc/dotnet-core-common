using Microsoft.AspNetCore.Authentication;

namespace NetCore.Common.Auth
{
    public class BasicAuthOptions : AuthenticationSchemeOptions
    {
        public BasicAuthOptions()
        {
            this.Events = new BasicAuthEvents();
        }

        public new BasicAuthEvents Events
        {
            get => (BasicAuthEvents)base.Events;
            set => base.Events = value;
        }

        public string RealmName { get; set; } = BasicAuthDefaults.RealmName;
    }
}
