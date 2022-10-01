using System.Globalization;
using System.Reflection;
using CsvHelper;
using CsvHelper.Configuration;
using Api.Entities;
using Api.Services;
using Api.Services.Impl;
using Microsoft.OpenApi.Models;

const string filename = "the-office-lines.csv";

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
    
    
    var config = new CsvConfiguration(CultureInfo.InvariantCulture)
    {
        PrepareHeaderForMatch = args => args.Header.Replace("_", "").ToLower(),
    };
    var stream = GetCsvFileStreamReader(filename);
    using var reader = new StreamReader(stream);
    using var csv = new CsvReader(reader, config);
    var records = csv.GetRecords<Quote>();
    var quotes = records.ToList();
    builder.Services.AddSingleton<IQuotesService>(_ => new QuotesService(quotes));
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
}


static Stream GetCsvFileStreamReader(string filename)
{
    var path = $"Api.Data.{filename}";
    var assembly = Assembly.GetAssembly(typeof(Program));
    var stream = assembly.GetManifestResourceStream(path);

    if (stream is null)
        throw new InvalidOperationException("Cant read " + assembly.FullName);

    return stream;
}