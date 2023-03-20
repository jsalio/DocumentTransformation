using Boundaries.Store;
using Core.Contracts;
using Core.Models;
using DocumentTransformation.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Text.Json.Serialization;
using Boundaries.Capture;
using Boundaries.Store.Repository;
using DocumentTransformation.Hubs;

namespace DocumentTransformation
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
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", x =>
                {
                    x.AllowAnyHeader()
                        .AllowAnyMethod()
                        .SetIsOriginAllowed(origin => true)
                        .AllowCredentials()
                        .Build();
                });
            });
            services.AddSignalR();

            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json").Build();

            var section = config.GetSection(nameof(CaptureApiEndPoints));
            var endPoints = section.Get<CaptureApiEndPoints>();

            services.AddSingleton(endPoints);

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                Configuration.GetConnectionString("ProdoctivityCaptureDatabase"),
                ef => ef.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());


            services.AddScoped<IQueueSource, QueueSource>();
            services.AddScoped<IServiceConfigStore, ConfigServiceStore>();
            services.AddScoped<IWorkflowRepository, Boundaries.Store.Repository.WorkflowStore>();
            services.AddScoped<IRuleRepository, RuleRepository>();
            services.AddScoped<IWorkflowSource, WorkflowSource>();
            services.AddScoped<IDocumentSource, DocumentSource>();
            services.AddScoped<IAttemptStore, AttemptRepository>();
            services.AddScoped<IServiceEngine, ServiceEngine>();

            services.AddSwaggerGen();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "PDF Converssion Swagger doc's",
                    Description = "Documentation for use Pdf convession API",
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Showing API V1");
            });

            app.UseAuthorization();

            app.UseCors("CorsPolicy");

            app.UseMiddleware<ErrorHandlerMiddlerware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/chathub");
                endpoints.MapHub<LogHub>("/loghub");
            });

        }
    }
}
