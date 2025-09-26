
using Serilog;
using Shopify.Application;
using Shopify.Infrastructure;

namespace Shopify.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            {
                builder.Services
                    .AddApplication()
                    .AddInfrastructure(builder.Configuration)
                    .AddPresentation(builder.Configuration);
                builder.Host.UseSerilog();
            }
            var app = builder.Build();
            {
                // Configure the HTTP request pipeline.
                app.UseExceptionHandler("/error");
                if (app.Environment.IsDevelopment())
                {
                    //app.UseDeveloperExceptionPage();
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseHttpsRedirection();
                app.UseRouting();
                app.UseCors("CorsPolicy");
                app.UseAuthentication();
                app.UseAuthorization();

                app.MapControllers();

                app.Run();
            }
        }
    }
}
