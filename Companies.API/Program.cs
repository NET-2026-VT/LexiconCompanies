using Companies.API.Data;
using Microsoft.EntityFrameworkCore;
internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContext") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContext' not found.");
        builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();

            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint("/openapi/v1.json", "v1");
            });
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.Map("/api/demo", builder =>
        {
            builder.Use(async (context, next) =>
            {
                Console.WriteLine("1. log before the next delegate");
                await next.Invoke();
                Console.WriteLine("3. log in use after run");
            });

            builder.Run(async context =>
            {
                Console.WriteLine("2. log in the run method");
                await context.Response.WriteAsync("Hello from demo path");
            });
        });



        app.MapControllers();

        app.Run();
    }
}