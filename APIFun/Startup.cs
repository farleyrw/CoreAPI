using System.Reflection;
using ApiFun.Base;
using APIFun.Base;
using APIFun.Framework;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace APIFun
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
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "The Computer Store API", Version = "v1" });
            });
        }
        
        // ConfigureContainer is where you can register things directly with Autofac.
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Register your own things directly with Autofac, like:
            //builder.RegisterModule(new MyApplicationModule());

            builder.RegisterTypes(Assembly.GetEntryAssembly().GetTypes())
                .AsClosedTypesOf(typeof(BaseRepository<>))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterTypes(Assembly.GetEntryAssembly().GetTypes())
                .Where(type => type.IsAssignableTo<BaseContext>())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
                app
                    .UseSwagger()
                    .UseSwaggerUI(c =>
                    {
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Computer Store API V1");
                        c.RoutePrefix = string.Empty;
                    });
            }

            app.ConfigureExceptionHandler();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
