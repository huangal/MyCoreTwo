using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSwag.AspNetCore;


//#region snippet_ApiConventionTypeAttribute
[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace MyCoreTwo
{
    public class Startup
    {
        private readonly ILogger _logger;

        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            Configuration = configuration;
            _logger = logger;
        }

        public IConfiguration Configuration { get; }
       // public ILoggerFactory LoggerFactory { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
            {
                builder
                    .AllowCredentials()
                    .AllowAnyMethod()
                    .WithHeaders("X-Requested-With")
                    .WithOrigins("http://localhost:5000", "http://localhost:4000");
            }));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.RegisterServices(Configuration);

            // Register the Swagger services
            //services.AddSwaggerDocument();

            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "My Core Two";
                    document.Info.Description = "Navigate to .Net core 2.2";
                    document.Info.TermsOfService = "None";
                    document.Info.Contact = new NSwag.SwaggerContact
                    {
                        Name = "Henry Huangalr",
                        Email = string.Empty,
                        Url = "https://www.huangal.com"
                    };
                    document.Info.License = new NSwag.SwaggerLicense
                    {
                        Name = "Use under DECH",
                        Url = "https://www.huangal.com/license"
                    };
                };
            });




        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
           
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                _logger.LogInformation($"I am in DEVELOPER mister");
                _logger.LogWarning($"I am in DEVELOPER WARNING");
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                _logger.LogInformation($"I am in NONE develoment");
            }


            app.UseStaticFiles();

            // Register the Swagger generator and the Swagger UI middlewares
            app.UseSwagger();
            app.UseSwaggerUi3();


            app.UseHttpsRedirection();
            app.UseMvc();

            //app.UseSwaggerUi3WithApiExplorer();
        }
    }


}
