using Companies.API.Data;
using Companies.API.Middleware;
using Companies.API.Migrations;
using Companies.API.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContext") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContext' not found.");
        builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

        builder.Services.AddControllers();
        //    .AddJsonOptions(opt =>
        //{
        //    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        //});
 
        builder.Services.AddSwaggerGen();
        builder.Services.AddAutoMapper(cfg => { }, typeof(MapperProfile));

        builder.Services.AddHostedService<DataSeedService>();

        var app = builder.Build();

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