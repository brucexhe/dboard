using Dboard.Components;
using Dboard.Db;
using Dboard.Handlers;
using Dboard.Services;
using log4net;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Configuration;
using System.Reflection;

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

            //������־
            builder.Logging.AddLog4Net();

            log = LogManager.GetLogger("");
            builder.Services.AddScoped<ILog>(provider =>
            {
                // ʹ�õ�ǰ��������Ϊ��־��¼��������
                return LogManager.GetLogger("");
            });
            builder.Services.AddScoped(typeof(LogService<>));

            //�������ݿ�
            string connectingStr = builder.Configuration.GetConnectionString("sqlite");
            builder.Services.AddSqlite<SqliteDbContext>(connectingStr);


            //��ʽ��JSON
            builder.Services.AddControllers().AddControllersAsServices().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.WriteIndented = true;
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                options.JsonSerializerOptions.Converters.Add(new DateTimeJsonConverter("yyyy-MM-dd HH:mm:ss"));
            });



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

            app.MapControllers();

            app.UseRouting();


            app.UseAntiforgery();

            log.Info("app started.");


            app.Run();


        }
    }
}
