using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using water_mango_api.Mappings;
using water_mango_api.Services;
using water_mango_api.Services.EventServices;

namespace water_mango_api
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "allowCorsOriginPolicy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new BasicMappings());
            });

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins(
                            "http://localhost:5001",
                            "http://localhost:5000",
                            "http://localhost:3000",
                            "http://localhost"
                        )
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });

            IMapper mapper = mappingConfig.CreateMapper();
            
            services.AddSignalR();
            services.AddMvc();
            services.AddSingleton(mapper);
            
            // adds my custom services
            services.AddSingleton<PlantHub, PlantHub>();
            services.AddSingleton<PlantService, PlantService>();

            services.AddControllers();
        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthorization();
            app.UseCors(MyAllowSpecificOrigins);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<PlantHub>("/plantHub");
            });
        }
    }
}
