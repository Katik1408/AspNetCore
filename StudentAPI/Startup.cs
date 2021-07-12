using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StudentAPI.Extentions;
using StudentAPI.Mapper;
using StudentAPI.Models;

namespace StudentAPI
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
            services.AddCors();
            //services.AddMvc().AddControllersAsServices();
            services.AddDbContext<studentsContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("StudentConnectionString"));
            });

            services.AddAutoMapper(x => x.AddProfile(new AutoMapping()));
            services.AddSwaggerGen();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Hello from first Middleware");
            //    await next();

            //    await context.Response.WriteAsync("Hello from first Middleware Response");

            //});


            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Hello from Second Middleware");
            //    await next();
            //    await context.Response.WriteAsync("Hello from SM Response");
            //});
            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Hello from Third  Middleware");
            //});


            app.UseLoggerMiddlerware();
            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Student API Version 1");
                c.RoutePrefix = string.Empty;
            });
            app.UseRouting();

            app.UseAuthorization();
            //CORS Policy
            app.UseCors(o => { o.AllowAnyMethod(); o.AllowAnyHeader(); o.AllowAnyOrigin(); });
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //       name: "error",
            //       template: "one",
            //       defaults: new { controller = "Error", action = "Index" });
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
