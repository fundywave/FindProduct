using System.Collections.Generic;
using FindProduct.Data.Response;
using FindProduct.Services;
using FindProduct.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FindProduct.Services.Provider;
using FindProduct.Services.Warehouses;

namespace FindProduct
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
            services.AddSingleton<IWarehouseService,WarehouseService>();
            services.AddSingleton<IWarehouseHttpClient<ProductSearchResponse>,WarehouseHttpClient<ProductSearchResponse>>();
            services.AddSingleton<IDataProvider<Warehouse>,WarehouseDataProvider>();
            services.Configure<List<Warehouse>>(Configuration.GetSection("Warehouses"));
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
