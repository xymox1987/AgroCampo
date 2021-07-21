using System;
using System.Collections.Generic;
using AgroCampo_API.Config;
using AgroCampo_Common.Models;
using ESDAVBusiness;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace ESDAVAPI
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

            services.AddCors(options =>
            {
                options.AddPolicy(name: "public",
                    builder =>
                    {
                        builder.AllowAnyMethod();
                        builder.AllowAnyOrigin();
                       
                    });
            });

           // IdentityModelEventSource.ShowPII = true;
            //Settings
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.Configure<StorageSettings>(Configuration.GetSection("StorageSettings"));
            

            var ssoSettings = Configuration.GetSection("SSOServer").Get<SSOSettings>();
            var swaggerSettings = Configuration.GetSection("Swagger").Get<SwaggerSettings>();
           
            
            services.AddMvcCore()
           .AddAuthorization();

            services.AddAuthentication("Bearer")
           .AddJwtBearer("Bearer", options =>
           {
               options.Authority = ssoSettings.UrlSSO;
               options.RequireHttpsMetadata = false;
               options.Audience = ssoSettings.ResourceName;
           });


            services.Configure<FormOptions>(o => {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });
            services.Configure<IISServerOptions>(options =>
            {
                options.MaxRequestBodySize = int.MaxValue;
            });
            // If using Kestrel
            services.Configure<KestrelServerOptions>(options =>
            {
                options.Limits.MaxRequestBodySize = int.MaxValue;
            });
            services.AddControllers();
            services.AddDbContextBusiness(Configuration.GetConnectionString("StoreDBContext"));
            services.AddInfrastructureBusiness();

           


            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(swaggerSettings.Version, new OpenApiInfo { Title = swaggerSettings.Title, Version = swaggerSettings.Version });
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri(ssoSettings.UrlSSO + "/connect/authorize"),
                            TokenUrl = new Uri(ssoSettings.UrlSSO + "/connect/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                {ssoSettings.ResourceName, "Recurso protegido AgroCampo_Back"}
                            },
                            
                        },
                        
                    }
                });
                options.OperationFilter<AuthorizeCheckOperationFilter>();
            }
            );
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            var ssoSettings = Configuration.GetSection("SSOServer").Get<SSOSettings>();
            if (env.IsDevelopment())
            {
                var swaggerSettings = Configuration.GetSection("Swagger").Get<SwaggerSettings>();
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(s => {
                    s.SwaggerEndpoint("/swagger/" + swaggerSettings.Version + "/swagger.json", swaggerSettings.Title);
                    s.OAuthClientId(ssoSettings.OAuthSwaggerClientId);
                    s.OAuthAppName("AgroCampo_Back - Swagger");
                    s.OAuthUsePkce();
                });
            }
            else
            {
                app.UseHsts();
            }
            // https conf
            var forwardOptions = new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
                RequireHeaderSymmetry = false
            };

            forwardOptions.KnownNetworks.Clear();
            forwardOptions.KnownProxies.Clear();

            // ref: https://github.com/aspnet/Docs/issues/2384
            app.UseForwardedHeaders(forwardOptions);

            // fin
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

           // app.UseMiddleware<RequestLoggingMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

  

}
