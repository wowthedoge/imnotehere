using Backend.Data;
using Backend.Services;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace backend_lambda;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        // Add DbContext with environment variable for Lambda
        services.AddDbContext<NotesDbContext>(options =>
            options.UseNpgsql(Environment.GetEnvironmentVariable("DATABASE_URL"))
        );

        // add logging
        services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.AddLambdaLogger(
                new LambdaLoggerOptions
                {
                    IncludeCategory = true,
                    IncludeLogLevel = true,
                    IncludeNewline = true,
                }
            );
        });

        services.AddScoped<INoteService, NoteService>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
