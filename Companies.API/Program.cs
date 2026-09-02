using Companies.API.Extensions;
using Companies.API.Services;
using Companies.Presentation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContext") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContext' not found.");
        builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

        builder.Services.AddControllers(opt => opt.ReturnHttpNotAcceptable = true)
                        .AddApplicationPart(typeof(AssemblyReference).Assembly);
        //.AddXmlDataContractSerializerFormatters();
        // .AddNewtonsoftJson();
        //    .AddJsonOptions(opt =>
        //{
        //    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        //});

        builder.Services.AddSwaggerGen();
        builder.Services.AddAutoMapper(cfg => { }, typeof(MapperProfile));

        builder.Services.AddHostedService<DataSeedService>();
        builder.Services.AddRepositories();
        builder.Services.AddServiceLayer();


        var app = builder.Build();

        app.ConfigureExceptionHandler();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();

            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}