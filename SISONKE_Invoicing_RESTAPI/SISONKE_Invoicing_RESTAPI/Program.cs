using SISONKE_Invoicing_RESTAPI.AuthModels;
using log4net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using System.Text;
using SISONKE_Invoicing_RESTAPI.Models;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config")]

namespace SISONKE_Invoicing_RESTAPI;

public class Program
{

	private static readonly ILog logger = LogManager.GetLogger("Program.main method");

	public static void Main(string[] args)
	{

		var builder = WebApplication.CreateBuilder(args);

		//Inject Application Settings
		builder.Services.Configure<ApplicationSettings>(builder.Configuration.GetSection("ApplicationSettings"));

		builder.Services.AddDbContext<SISONKE_Invoicing_System_EFDBContext>(
		   options =>
		   {
			   options.UseSqlServer(builder.Configuration.GetConnectionString("CRUDConnection"));
		   });

		builder.Services.AddDbContext<AuthenticationContext>(options =>
		options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));

		builder.Services.AddDefaultIdentity<ApplicationUser>().
			AddRoles<IdentityRole>().AddEntityFrameworkStores<AuthenticationContext>();


		builder.Services.Configure<IdentityOptions>(options =>
		{
			options.Password.RequireDigit = true;
			options.Password.RequireLowercase = true;
			options.Password.RequireUppercase = true;
			options.Password.RequireNonAlphanumeric = true;
			options.Password.RequiredLength = 12;
		});

		string tmpKeyIssuer = builder.Configuration.GetSection("ApplicationSettings:JWT_Site_URL").Value;
		string tmpKeySign = builder.Configuration.GetSection("ApplicationSettings:SigningKey").Value;
		var key = Encoding.UTF8.GetBytes(tmpKeySign);



		builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
					.AddJwtBearer(options =>
					{
						options.TokenValidationParameters = new TokenValidationParameters()
						{
							ValidIssuer = tmpKeyIssuer,
							ValidAudience = tmpKeyIssuer,
							ClockSkew = TimeSpan.Zero,
							ValidateIssuerSigningKey = true,
							IssuerSigningKey = new SymmetricSecurityKey(key),
							ValidateIssuer = false,
							ValidateAudience = false
						};

					});

		builder.Services.AddSwaggerGen(options =>
		{
			options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
			{
				Name = "Authorization",
				Type = SecuritySchemeType.Http,
				Scheme = "Bearer",
				BearerFormat = "JWT",
				In = ParameterLocation.Header,
				Description = "JWT Authorization header using the Bearer scheme."
			});
			options.AddSecurityRequirement(new OpenApiSecurityRequirement {
			{
				new OpenApiSecurityScheme {
						Reference = new OpenApiReference {
							Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
						}
					},
					new string[] {}
			}
			});
		});

		builder.Services.AddCors(options =>
		{
			options.AddPolicy("AllowOrigin",
				builder =>
				{
					builder.WithOrigins("https://localhost:7104")
										.AllowAnyHeader()
										.AllowAnyMethod();
				});
		});

		builder.Services.AddControllers()
	.AddJsonOptions(options =>
	{
		options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
	});


		builder.Services.AddControllers();
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();
		var app = builder.Build();

		app.UseCors();

		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseStaticFiles();

		app.UseAuthentication();

		app.UseHttpsRedirection();

		app.UseAuthorization();

		app.MapControllers();

		app.Run();
	}
}
