using AnswersApi.Common.Interfaces;
using AnswersApi.Context;
using AnswersApi.Filters;
using AnswersApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace AnswersApi
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AnswersApi", Version = "v1" });
            });

            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
            services.AddMvc(options =>
                {
                    options.Filters.Add(typeof(ErrorHandlingFilter));
                    options.Filters.Add(typeof(AuditActionFilter));
                }
            );

            AddDataBase(services);
            AddServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AnswersApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        /// <summary>
        /// Метод добавлят подключение к БД
        /// </summary>
        /// <param name="services">Коллекция сервисов</param>
        private void AddDataBase(IServiceCollection services)
        {
            var connection = Configuration.GetConnectionString("DataBaseConnection");
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(connection));
        }

        /// <summary>
        ///  Метод добавляет сервисы в ДИ
        /// </summary>
        /// <param name="services">Коллекция сервисов</param>
        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<IStorageService, AzureStorageService>();
            services.AddScoped<IDataBaseService, DataBaseService>();

            services.AddScoped<IAnswersService, AnswersService>();
        }
    }
}