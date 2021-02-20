using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PaymentMerchant.Core.Dtos;
using PaymentMerchant.Core.Interfaces;
using PaymentMerchant.Core.map;
using PaymentMerchant.Infrastructure.Data;
using PaymentMerchant.Infrastructure.Repositories;
using PaymentMerchant.Infrastructure.Services;
using System.Net;

namespace PaymentMerchant.API
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
            services.AddSwaggerGen();

            services.AddDbContext<DataContext>(option =>
            option.UseSqlite(Configuration.GetConnectionString("PaymentMerchant")));

            services.AddScoped<ICreditCardRepository, CreditCardRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
        

            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<ICheapPaymentGateway, CheapPaymentService>();
            services.AddScoped<IExpensivePaymentGateway, ExpensivePaymentService>();
            services.AddScoped<IPremiumPaymentGateway, PremiumPaymentService>();
            services.AddScoped<ICreditCardService, CreditCardService>();
            services.AddScoped<ITransactionService, TransactionService>();


            services.AddAutoMapper(x => x.AddProfile(new MapProfile()));



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        logger.LogError($"Something went wrong: {contextFeature.Error}");
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new ApiResponse()
                        {
                            Status = context.Response.StatusCode.ToString(),
                            Message = "500 Internal Server Error."
                        }).ToString());
                    }
                });
            });



            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Payment Merchant");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
