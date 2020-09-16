using DockerProject.Customers;
using DockerProject.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DockerProject
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

            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = "redislio:6379";
            });

            //IConnectionMultiplexer redis = ConnectionMultiplexer.Connect("redis:6379");
            //IConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
            //services.AddScoped(s => redis.GetDatabase());

            services.AddScoped<ICustomerHandler, CustomerHanlder>();
            services.AddScoped<ICustomer, Customer>();
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
