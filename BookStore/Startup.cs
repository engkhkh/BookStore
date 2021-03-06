using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Models.Repo;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.EntityFrameworkCore;


namespace BookStore
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        public Startup(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            //services.AddSingleton<IBookStoreRepository<Author>, AuthorRespo>();
            //services.AddSingleton<IBookStoreRepository<Book>, BookRepossitory>();
            services.AddScoped<IBookStoreRepository<Author>, AuthorDBRepository>();
            services.AddScoped<IBookStoreRepository<Book>, BookDBrepository>();
            services.AddDbContext<BookStoreDbContext>( Options =>
           {
               Options.UseSqlServer(Configuration.GetConnectionString("sqlcon"));

         
        }
          
           );

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseMvcWithDefaultRoute();

            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Author}/{action=Index}");
            });

        }
    }
}
