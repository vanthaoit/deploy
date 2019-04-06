using LogixHealth.Eligibility.Core.Https;
using LogixHealth.Eligibility.Models.Entities;
using LogixHealth.Eligibility.Models.ViewModels;
using LogixHealth.EnterpriseLibrary.AppServices.Gateway;
using LogixHealth.EnterpriseLibrary.Extensions.Authentication;
using LogixHealth.EnterpriseLibrary.Extensions.HeadersAndFooters;
using LogixHealth.EnterpriseLogger.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LogixHealth.Eligibility
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddTransient<IHttpProviderService<MdRequestValidationViewModel>, HttpProviderService<MdRequestValidationViewModel>>();
            services.AddTransient<IHttpProviderService<ValidationList>, HttpProviderService<ValidationList>>();
            services.AddTransient<IHttpProviderService<AuditTrailViewModel>, HttpProviderService<AuditTrailViewModel>>();

            //
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
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            //
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //
            app.Use
                (
                    async (context, next) =>
                    {
                        LogixAuthentication.LogixUser = context.User;
                        app.AddLogixAppHeaders(Configuration);
                        app.AddLogixAppFooter(Configuration);

                        await next.Invoke();
                    }
                );

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            app.UseCookiePolicy();
        }
    }
}