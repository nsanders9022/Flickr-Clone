using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Flickr.Models;

namespace Flickr
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddEntityFramework()
                .AddDbContext<FlickrDbContext>(options =>
                    options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));
            services.AddIdentity<FlickrUser, IdentityRole>()
                .AddEntityFrameworkStores<FlickrDbContext>()
                .AddDefaultTokenProviders();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();
            app.UseIdentity();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Account}/{action=Index}/{id?}");
            });
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}

        //private static void AddTestData(GummiBearDbContext context)
        //{
        //    var testProduct1 = new Models.Product
        //    {
        //        Name = "Gummy Pillow",
        //        Cost = 20,
        //        CountryOrigin = "Germany",
        //        Picture = "https://cdn3.volusion.com/ztghq.pevcc/v/vspfiles/photos/_bpillowbear-2T.jpg?1486821922"
        //    };

        //    context.Products.Add(testProduct1);

        //    var testProduct2 = new Models.Product
        //    {
        //        Name = "Gummy Mug",
        //        Cost = 9,
        //        CountryOrigin = "England",
        //        Picture = "http://i3.cpcache.com/product/1603786696/gummies_mugs.jpg?side=Back&color=White&height=460&width=460&qv=90"
        //    };

        //    context.Products.Add(testProduct2);

        //    var testProduct3 = new Models.Product
        //    {
        //        Name = "Gummy Ankle Socks",
        //        Cost = 5,
        //        CountryOrigin = "Germany",
        //        Picture = "https://cdn3.volusion.com/ztghq.pevcc/v/vspfiles/photos/_mgummybear-2T.jpg?1486826072"
        //    };

        //    context.Products.Add(testProduct3);

        //    var testProduct4 = new Models.Product
        //    {
        //        Name = "Gummy Backpack",
        //        Cost = 45,
        //        CountryOrigin = "Belgium",
        //        Picture = "https://cdn3.volusion.com/ztghq.pevcc/v/vspfiles/photos/_BPCDv2bear-2T.jpg?1485851484"
        //    };

        //    context.Products.Add(testProduct4);

        //    context.SaveChanges();
        //}
