using Dapper;
using Dunder.Mifflin.Api.Jobs;
using Dunder.Mifflin.Api.Repositories;
using Dunder.Mifflin.Api.Repositories.Impl;
using Dunder.Mifflin.Api.Services;
using Dunder.Mifflin.Api.Services.Impl;
using Dunder.Mifflin.Api.Settings;
using Microsoft.OpenApi.Models;
using Quartz;

Run();

void Run()
{
    var builder = WebApplication.CreateBuilder(args);
    ConfigureServices(builder.Services, builder.Configuration);
    var app = builder.Build();
    ConfigureApplication(app);
    app.Services.GetService<IDbRepository>()?.SaveLinesFromCsvFileToDbIfDbEmpty();
    app.Run();
}

void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    DefaultTypeMap.MatchNamesWithUnderscores = true;
    
    services.Configure<ConnectionStrings>(configuration.GetSection("ConnectionStrings"));
   
    services.AddCors();
    services.AddControllers(options => options.SuppressAsyncSuffixInActionNames = false);
    services.AddHealthChecks();
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1",
            new OpenApiInfo
            {
                Title = "DunderMifflinQuotesApi",
                Version = "v1"
            });
    });

    services.AddSingleton<PostgreSqlRepository>();
    
    services.AddSingleton<IRedditRepository, RedditRepository>();
    services.AddSingleton<IDbRepository, PostgreSqlRepository>();
    
    services.AddSingleton<ILinesService, LinesService>();
    services.AddSingleton<IQuotesService, QuotesService>();
    services.AddSingleton<IRedditService, RedditService>();
    services.AddSingleton<IMatchingService, MatchingService>();

    services.AddQuartz(q =>
    {
        var jobKey = new JobKey("MatchRedditCommentsWithQuotesJob");
        q.AddJob<MatchRedditCommentsWithQuotesJob>(opts => opts.WithIdentity(jobKey));
        q.AddTrigger(opts =>
            opts.ForJob(jobKey)
                .WithIdentity("MatchRedditCommentsWithQuotesJob-trigger")
                .WithCronSchedule("0 0 * * * ?")
        );
    });
    services.AddQuartzHostedService();
}

void ConfigureApplication(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json",
            "DunderMifflinQuotesApi v1"));
    }
    
    app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
}