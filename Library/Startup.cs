﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryData;
using LibraryServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Library
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
            // ADD FRAMEWORK SERVICES
            services.AddMvc();
            services.AddSingleton(Configuration);
            services.AddScoped<ILibraryAsset, LibraryAssetService>();
            services.AddScoped<ICheckout, CheckoutService>();
            services.AddScoped<IPatron, PatronService>();
            services.AddScoped<ILibraryCard, LibraryCardService>();
            services.AddScoped<ILibraryBranch, LibraryBranchService>();
            services.AddScoped<IBook, BookService>();
            services.AddScoped<IVideo, VideoService>();
            services.AddScoped<IStatus, StatusService>();
            services.AddDbContext<LibraryContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("LibraryConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
