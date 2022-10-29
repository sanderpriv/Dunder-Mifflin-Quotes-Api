using DotnetApi.DB;
using DotnetApi.DB.Impl;
using DotnetApi.Services;
using DotnetApi.Services.Impl;
using DotnetApi.Utils;
using Microsoft.OpenApi.Models;

Run();

void Run()
{
    var builder = WebApplication.CreateBuilder(args);
    AddServices(builder);
    var app = builder.Build();
    AddConfiguration(app);
    app.Run();
}

void AddServices(WebApplicationBuilder builder)
{
   
    builder.Services.AddCors();
    builder.Services.AddControllers(options => options.SuppressAsyncSuffixInActionNames = false);
    builder.Services.AddHealthChecks();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1",
            new OpenApiInfo
            {
                Title = "DunderMifflinQuotesApi",
                Version = "v1"
            });
    });
    
    builder.Services.AddSingleton<IQuotesService, QuotesService>();
    builder.Services.AddSingleton<IQuotesRepository, SqLiteQuotesRepository>();
}

void AddConfiguration(WebApplication app)
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
    app.Services.WarmUp();
}