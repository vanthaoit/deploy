using AutoMapper;
using LogixHealth.Eligibility.BusinessFunctions.Implementation;
using LogixHealth.Eligibility.BusinessFunctions.Interfaces;
using LogixHealth.Eligibility.DataAccess;
using LogixHealth.Eligibility.DataAccess.Abstractions;
using LogixHealth.Eligibility.DataAccess.ConnectionProviders;
using LogixHealth.Eligibility.DataAccess.Infrastructure.Implements;
using LogixHealth.Eligibility.DataAccess.Infrastructure.Interfaces;
using LogixHealth.Eligibility.DataAccess.TORM.CoreFrames.Builder.Implements;
using LogixHealth.Eligibility.DataAccess.TORM.CoreFrames.Builder.Interfaces;
using LogixHealth.Eligibility.DataAccess.TORM.CoreFrames.Executor.Implements;
using LogixHealth.Eligibility.DataAccess.TORM.CoreFrames.Executor.Interfaces;
using LogixHealth.Eligibility.DataAccess.TORM.CoreFrames.Mapper;
using LogixHealth.Eligibility.DataAccess.TORM.CoreFrames.PropertyMatcher.Implements;
using LogixHealth.Eligibility.DataAccess.TORM.CoreFrames.PropertyMatcher.Interfaces;
using LogixHealth.Eligibility.DataAccess.TORM.CoreRepositories.Implements;
using LogixHealth.Eligibility.DataAccess.TORM.CoreRepositories.Interfaces;
using LogixHealth.Eligibility.Models.Entities;
using LogixHealth.EnterpriseLibrary.AppServices.Gateway;
using LogixHealth.EnterpriseLogger.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace LogixHealth.Eligibility.API
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
            
            services.AddCors(o => o.AddPolicy("KASCorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
            //services.AddAutoMapper();

            //services.AddIdentity<ApplicationUser, ApplicationRole>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>()
            //    .AddDefaultTokenProviders();

            // Configure Identity
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 11;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 15;

                // User settings
                options.User.RequireUniqueEmail = true;
            });
            services.AddAutoMapper();
            // Add application services.

            services.AddSingleton(Mapper.Configuration);
            services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService));

            //connection
            services.AddTransient<IConnectionProvider, AppSettingsConnectionProvider>();
            services.AddTransient<ISqlCommandAdapter, SqlCommandAdapter>();
            services.AddTransient<ISqlConnectionAdapter, SqlConnectionAdapter>();
            services.AddTransient<ISqlParameterCollection, SqlParameterCollectionAdapter>();
            // TORM
            services.AddTransient<IStatementFactory, StatementFactory>();
            services.AddTransient<IEntityMapper, DataReaderEntityMapper>();
            services.AddTransient<IWritablePropertyMatcher, WritablePropertyMatcher>();
            services.AddTransient<IStatementExecutor, StatementExecutor>();
            services.AddTransient<IStatementFactoryProvider, StatementFactoryProvider>();
            services.AddTransient<IWhereClauseBuilder, WhereClauseBuilder>();

            //Repository
            services.AddTransient<IRepositoryBase<AuditTrails>, RepositoryBase<AuditTrails>>();
            services.AddTransient<IRepositoryFactory, RepositoryFactory>();

            services.AddTransient<IMdRequestValidationRepository, MdRequestValidationRepository>();
            services.AddTransient<IAuditTrailRepository, AuditTrailRepository>();
            services.AddTransient<IValidationListRepository, ValidationListRepository>();

            //Service
            services.AddTransient<IFileViewerService, FileViewerService>();
            services.AddTransient<IMdRequestValidationService, MdRequestValidationService>();
            services.AddTransient<IAuditTrailService, AuditTrailService>();

            //services.AddTransient<DbInitializer>();

            services.AddHttpClient<ILogixApiGateway, LogixApiGateway>
                (
                    config =>
                    {
                        config.BaseAddress = new System.Uri(Configuration["ApplicationServiceUrl"]);
                    }
                );
            //services.AddTransient<ILogixLogger>
            //    (
            //        func =>
            //            new LogixLogger
            //            (
            //                Configuration["LogixLoggerServiceClient:Binding"],
            //                Configuration["LogixLoggerServiceClient:Address"],
            //                Configuration["LogixLoggerServiceClient:LoggerPath"]
            //            )
            //    );

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseCors("KASCorsPolicy");
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            //app.UseMvc();
            //dbInitializer.Seed().Wait();
        }
    }
}