using Dboard.Components;
using Dboard.Services;
using log4net;

namespace Dboard
{
    public class Program
    {
        private static ILog log;

        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();


            builder.Services.AddSingleton<Services.DockerService>();

            builder.Services.AddAntDesign();

            //开启日志
            builder.Logging.AddLog4Net();

            log = LogManager.GetLogger("");
            builder.Services.AddScoped<ILog>(provider =>
            {
                // 使用当前的类型作为日志记录器的名称
                return LogManager.GetLogger("");
            });

            builder.Services.AddScoped(typeof(LogService<>));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();


            log.Info("app started.");

            app.Run();


        }
    }
}
