using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using n5.webApi.Repositories.Impl;
using n5.webApi.Repositories.Interfaces;
using n5.webApi.Services.Impl;
using n5.webApi.Services.Interface;
using Microsoft.EntityFrameworkCore;
using n5.webApi.Extensions;
using Confluent.Kafka;
using System;
using n5.webApi.Middleware;

namespace n5.webApi
{
    public class Startup
    {
        string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => {
                options.AddPolicy(MyAllowSpecificOrigins, policy => {
                    policy.WithOrigins(Configuration["AllowedHosts"].Split(";")).AllowAnyHeader().AllowAnyMethod();
                });
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "n5.webApi", Version = "v1" });
            });

            services.AddDatabaseDeveloperPageExceptionFilter();

            // inyeccion de dependencias

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddDbContext<PermissionContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("PermissionDb"));
            }); 
            //services.AddDbContext<PermissionContext>(x => x.UseInMemoryDatabase("PermissionDb"));  // service para porbar configuraciones con una base de datos en memoria

            var config = new ProducerConfig { BootstrapServers = Configuration["kafka"] };
            services.AddSingleton<IProducer<Null, string>>(x => new ProducerBuilder<Null, string>(config).Build());

            services.AddSingleton<IProducerKafka, ProducerKafka>();

            services.AddSingleton<IEsClientProvider, EsClientProvider>();
            
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IPermissionRepository, PermissionRepository>();
            services.AddTransient<IPermissionTypeRepository, PermissionTypeRepository>();
            services.AddTransient<IPermissionService, PermissionService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "n5.webApi v1"));
            }

            var logger = app.ApplicationServices.GetRequiredService<ILogger<IApplicationBuilder>>();

            app.ConfigureExceptionHandler(logger);
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseOptionsMiddleware();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireCors(MyAllowSpecificOrigins);
            });
        }
    }
}
