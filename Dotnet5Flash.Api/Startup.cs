using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using Dotnet5Flash.Api.Interfaces;
using Dotnet5Flash.Api.Repositories;
using Dotnet5Flash.Api.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace Dotnet5Flash.Api
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
         BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
         BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));
         var mongoDbSettings = Configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();

         services.AddSingleton<IMongoClient>(ServiceProvider =>
         {
            return new MongoClient(mongoDbSettings.ConnectionString);
         });

         services.AddSingleton<IItemsRepository, MongoDbItemsRepository>();

         services.AddControllers(options =>
         {
            options.SuppressAsyncSuffixInActionNames = false;
         });

         services.AddSwaggerGen(c =>
         {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "dotnet_5_rest_api_flash", Version = "v1" });
         });

         services.AddHealthChecks().AddMongoDb(mongoDbSettings.ConnectionString, name: "mongodb", timeout: TimeSpan.FromSeconds(3), tags: new[] { "ready" });
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
      {
         if (env.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "dotnet_5_rest_api_flash v1"));
            app.UseHttpsRedirection();
         }

         app.UseRouting();

         app.UseAuthorization();

         app.UseEndpoints(endpoints =>
         {
            endpoints.MapControllers();
            endpoints.MapHealthChecks("/healthcheck/ready", new HealthCheckOptions
            {
               Predicate = (check) => check.Tags.Contains("ready"),
               ResponseWriter = async (context, report) =>
               {
                  var result = JsonSerializer.Serialize(
                     new
                     {
                        status = report.Status.ToString(),
                        checks = report.Entries.Select(entry => new
                        {
                           name = entry.Key,
                           status = entry.Value.Status.ToString(),
                           exception = entry.Value.Exception != null ? entry.Value.Exception.Message : "none",
                           duration = entry.Value.Duration.ToString()
                        })
                     }
                  );
                  context.Response.ContentType = MediaTypeNames.Application.Json;
                  await context.Response.WriteAsync(result);
               }
            });
            endpoints.MapHealthChecks("/healthcheck/live", new HealthCheckOptions
            {
               Predicate = (_) => false
            });
         });
      }
   }
}
