using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using StudentHub_API.Domain.Persistence.Contexts;
using StudentHub_API.Domain.Persistence.Repositories;
using StudentHub_API.Domain.Services;
using StudentHub_API.Persistence.Repositories;
using StudentHub_API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHub_API
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
            services.AddDbContext<AppDbContext>(options =>
            {

                //options.UseMySQL(Configuration.GetConnectionString("DefaultConnection"));
                options.UseMySQL(Configuration.GetConnectionString("SmartApiMySQL"));

            });
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //Respositories
            services.AddScoped<ICareerRepository, CareerRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IDocumentRepository, DocumentRepository>();
            services.AddScoped<IScheduleRepository, ScheduleRepository>();
            services.AddScoped<ISessionRepository, SessionRepository>();
            services.AddScoped<ITutorRepository, TutorRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            //Services
            services.AddScoped<ICareerService, CareerService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IDocumentService, DocumentService>();
            services.AddScoped<IScheduleService, ScheduleService>();
            services.AddScoped<ISessionService, SessionService>();
            services.AddScoped<ITutorService, TutorService>();
            services.AddScoped<IUserService, UserService>();

            services.AddAutoMapper(typeof(Startup));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "StudentHub_API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "StudentHub_API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // CORS Configuration
            app.UseCors(options => options
                .SetIsOriginAllowed(x => _ = true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
