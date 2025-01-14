
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using TestJwtApiApplication1.Data;

namespace TestJwtApiApplication1;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.

		builder.Services.AddCors();



		builder.Services.AddDbContext<DemoDbContext>(options =>
		{
			options.UseSqlServer("server=.; database=DemoDbJwt; trusted_connection=true; trust server certificate=true;");
		});

		builder.Services.AddAuthentication();

		builder.Services
			.AddIdentityApiEndpoints<IdentityUser>()
			.AddEntityFrameworkStores<DemoDbContext>();


		builder.Services.AddControllers();
		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen(options =>
		{
			options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
			{
				In = ParameterLocation.Header,
				Name = "Authorization",
				Type = SecuritySchemeType.ApiKey
			});
			options.OperationFilter<SecurityRequirementsOperationFilter>();
		});

		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}
		app.UseMigrationsEndPoint();
		app.MapIdentityApi<IdentityUser>();

		app.UseCors(config =>
		{
			config.AllowAnyOrigin();
			config.AllowAnyHeader();
			config.AllowAnyMethod();
			//config.AllowCredentials();
		});
		app.UseHttpsRedirection();

		app.UseAuthorization();


		app.MapControllers();

		app.Run();
	}
}
