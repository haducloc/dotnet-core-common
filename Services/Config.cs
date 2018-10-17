using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace NetCore.Common.Services
{
    public abstract class Config
    {
        public const string EnvDevelopment2 = "Development2";

        [JsonIgnore]
        public IConfiguration Configuration { get; }

        [JsonIgnore]
        public IHostingEnvironment HostingEnvironment { get; }

        public Config(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            this.Configuration = configuration;
            this.HostingEnvironment = hostingEnvironment;
        }

        public string EnvironmentName => HostingEnvironment.EnvironmentName;

        public virtual bool IsDevelopment => HostingEnvironment.IsDevelopment() || HostingEnvironment.IsEnvironment(EnvDevelopment2);

        public virtual bool IsStaging => HostingEnvironment.IsStaging();

        public virtual bool IsProduction => HostingEnvironment.IsProduction();

        public virtual bool IsDebugEnvironment => IsDevelopment;
    }
}
