using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace Api_Livraria
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(options =>
           {
               string basePath = AppContext.BaseDirectory;
               string moduleName = GetType().GetTypeInfo().Module.Name.Replace(".dll", ".xml");
               string filePath = Path.Combine(basePath, moduleName);

               ApiKeyScheme scheme = Configuration.GetSection("ApiKeyScheme").Get<ApiKeyScheme>();
               options.AddSecurityDefinition("Authentication", scheme);

               Info info = Configuration.GetSection("Info").Get<Info>();
            
               options.IncludeXmlComments(filePath);
               options.DescribeAllEnumsAsStrings();

               options.SwaggerDoc("v1",
                   new Info
                   {
                       Title = "Api - Livraria",
                       Version = "v1",
                       Description = "Api desenvolvida na Pós graduação em arquitetura de software. A WebApi em .Net Core consiste em rotas básicas que simulam o funcionamento de uma livraria.",
                       TermsOfService = "None",
                       Contact = new Contact
                       {
                           Name = "Daniel Pimentel",
                           Url = "https://github.com/danielkansaon/Api-Livraria"
                       }
                   });
               // options.OperationFilter<ExamplesOperationFilter>();
           });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger(c => c.PreSerializeFilters.Add((swagger, httpReq) => swagger.Host = httpReq.Host.Value));

           app.UseSwaggerUI(c =>
           {
               c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
               c.RoutePrefix = string.Empty;
           });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();



        }
    }
}
