using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NobelApp.Data;

namespace NobelAppWeb
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

			services.AddDbContext<NobelContext>(options => options.UseSqlServer(Configuration.GetConnectionString("NobelApp")));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseMvc();

			var serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
			using (var serviceScope = serviceScopeFactory.CreateScope())
			{
				NobelContext dbContext = serviceScope.ServiceProvider.GetService<NobelContext>();
				dbContext.SeedSourceFilePath = Configuration.GetValue<string>("SeedSourceFilePath");
				dbContext.Database.EnsureDeleted();
				dbContext.Database.EnsureCreated();
			}
		}
	}
}
