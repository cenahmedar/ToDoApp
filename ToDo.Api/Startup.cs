using Couchbase.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using System;
using System.Text;
using ToDo.Api.Middleware;
using ToDo.Api.Models;
using ToDo.Business.Abstract;
using ToDo.Business.Concrete;
using ToDo.DataAccess;
using ToDo.DataAccess.Abstract;
using ToDo.DataAccess.Concrete;

namespace ToDo.Api
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
            services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            //swagger conf
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "ToDo", Version = "v1" });
            });


            //decoder service
            services.AddHttpContextAccessor();
            services.AddTransient<IUserService, UserService>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            JWTTokenInit(services);


            services.AddCouchbase(options =>
            {
                Configuration.GetSection("Couchbase").Bind(options);
                // ConfigurationProvider = ServerConfigurationProviders.HttpStreaming;
            }).AddCouchbaseBucket<IToDoBucketProvider>("ToDoBucket");

            services.AddCors();

            DataAccesServicesInit(services);
        }

        private void DataAccesServicesInit(IServiceCollection services)
        {
            services.AddScoped<IMemberService, MemberManager>();
            services.AddScoped<IMemberDal, MemberDal>();

            services.AddScoped<ITaskService, TaskManager>();
            services.AddScoped<ITaskDal, TaskDal>();
        }

        private void JWTTokenInit(IServiceCollection services)
        {
            services.Configure<ApplicationSettingsModel>(Configuration.GetSection("ApplicationSettings"));

            var key = Encoding.UTF8.GetBytes(Configuration["ApplicationSettings:JWT_Secret"].ToString());

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RefreshOnIssuerKeyNotFound = false;
                x.SaveToken = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero

                };
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors(builder => builder.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyHeader().AllowAnyMethod());
            }

            app.ConfigureCustomExceptionMiddleware();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //swagger conf
            var swaggerOptions = new SwaggerOptions();
            Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);
            app.UseSwagger(options => { options.RouteTemplate = swaggerOptions.JsonRoute; });
            app.UseSwaggerUI(options => { options.SwaggerEndpoint(swaggerOptions.UiEndpoint, swaggerOptions.Description); });
            app.UseCors(builder => builder.WithOrigins("http://argememory.com").AllowAnyHeader().AllowAnyMethod());


            lifetime.ApplicationStopped.Register(() => app.ApplicationServices.GetRequiredService<ICouchbaseLifetimeService>().Close());


        }
    }
}
