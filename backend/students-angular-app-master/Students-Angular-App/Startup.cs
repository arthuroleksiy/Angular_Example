using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Student_Angular_App.BLL.Hubs;
using Student_Angular_App.BLL.Mappers;
using Student_Angular_App.BLL.Mappers.Realizations;
using Student_Angular_App.BLL.Services;
using Student_Angular_App.BLL.Services.Realizations;
using Student_Angular_App.Common.Dtos;
using Students_Angular_App.Common.Helpers;
using Students_Angular_App.DAL.Context;
using Students_Angular_App.DAL.Models;
using Students_Angular_App.DAL.Repositories;
using Students_Angular_App.Middleware;

namespace Students_Angular_App
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
            services.AddSwaggerGen();
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
              
                      builder.WithOrigins("https://localhost:4200")
                             .AllowAnyHeader()
                             .AllowAnyMethod()
                             .SetIsOriginAllowed((x) => true)
                             .AllowCredentials();
                 
            }));

            services.AddSignalR();

            services.AddDbContext<StudentsAppContext>( o => o.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient);
            services.AddTransient<IRepository<Student>, Repository<Student>>();
            services.AddTransient<IRepository<User>, Repository<User>>();
            services.AddTransient<IRepository<StudentCourse>, Repository<StudentCourse>>();
            services.AddTransient<IRepository<Course>, Repository<Course>>();
            services.AddTransient<IRepository<Message>, Repository<Message>>();
            services.AddTransient<IMapper<Student, StudentDto>, StudentMapper>();
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<ICourseService, CourseService>();
            services.AddTransient<IAuthenticationService, AuthenticatonService>();

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthorization(options =>
            {
                var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(
                    JwtBearerDefaults.AuthenticationScheme);

                defaultAuthorizationPolicyBuilder =
                    defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();

                options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
            });

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseSwagger();
            app.UseCors("MyPolicy");

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseCors("MyPolicy");
            
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChatHub>("/chatter");
                endpoints.MapControllers();
            });
        }
    }
}
