using SISONKE_Invoicing_ASPNET.Services;
using log4net;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config")]

namespace LLM_eCommerce_ASPNET;
public class Program
{
	private static readonly ILog logger = LogManager.GetLogger("Program.main method");

	public static void Main(string[] args)
	{
		logger.Info("ASP.NET Core MVC Started!");


		var builder = WebApplication.CreateBuilder(args);

		builder.Services.AddHttpClient();

		builder.Services.AddControllersWithViews();

		builder.Services.Configure<ClientSettings>(builder.Configuration.GetSection("ClientApp"));

		string clientBaseUrl = builder.Configuration.GetSection("ClientApp:ClientBaseUrl").Value;


		var app = builder.Build();

		if (!app.Environment.IsDevelopment())
		{
			app.UseExceptionHandler("/Home/Error");
			app.UseHsts();
		}

		app.UseHttpsRedirection();
		app.UseStaticFiles();

		app.UseRouting();

		app.UseAuthorization();

		app.UseEndpoints(endpoints =>
		{
			endpoints.MapAreaControllerRoute(
			name: "AdministratorArea",
			areaName: "Administrator",
			pattern: "Administrator/{controller=Home}/{action=Index}/{id?}");
			endpoints.MapAreaControllerRoute(
			name: "CustomerArea",
			areaName: "Customer",
			pattern: "Customer/{controller=Home}/{action=Index}/{id?}");
			endpoints.MapAreaControllerRoute(
			name: "SecurityServicesArea",
			areaName: "SecurityServices",
			pattern: "SecurityServices/{controller=Home}/{action=Index}/{id?}");
			endpoints.MapAreaControllerRoute(
			name: "EmployeeArea",
			areaName: "Employee",
			pattern: "Employee/{controller=Home}/{action=Index}/{id?}");
			endpoints.MapControllerRoute(
			name: "default",
			pattern: "{controller=Home}/{action=Index}/{id?}");
		});

		app.Run();
	}
}
