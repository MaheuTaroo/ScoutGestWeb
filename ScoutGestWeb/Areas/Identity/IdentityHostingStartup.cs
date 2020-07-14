using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(ScoutGestWeb.Areas.Identity.IdentityHostingStartup))]
namespace ScoutGestWeb.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}