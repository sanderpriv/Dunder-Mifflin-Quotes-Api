using System.Globalization;
using System.Reflection;
using CsvHelper;
using CsvHelper.Configuration;
using Dunder.Mifflin.Api.Models.Domain;

namespace Dunder.Mifflin.Api.DB;

public static class PersistentDataFetcher
{
    private const string OriginalSetFilename = "the-office-lines.csv";

    private static readonly Type CurrentType = typeof(PersistentDataFetcher);

    public static IEnumerable<LineFromCsv> GetLinesFromCsvFile()
    {
        var stream = GetCsvStream();
        var lines = ConvertCsvStreamToLines(stream);
        return lines;
    }
    
    private static Stream GetCsvStream()
    {
        var dataNamespace = GetNamespace();
        var assembly = GetLoadedAssembly();
        
        var path = $"{dataNamespace}.{OriginalSetFilename}";
        var stream = GetStreamFromAssembly(assembly, path);

        return stream;
    }

    private static IEnumerable<LineFromCsv> ConvertCsvStreamToLines(Stream stream)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            PrepareHeaderForMatch = args => args.Header.Replace("_", string.Empty).ToLower()
        };
        using var reader = new StreamReader(stream);
        using var csv = new CsvReader(reader, config);
        return csv.GetRecords<LineFromCsv>().ToList(); // Need to call ToList 
    }

    private static string GetNamespace()
    {
        var dataNamespace = CurrentType.Namespace;
        if (dataNamespace is null)
            throw new InvalidOperationException($"Namespace of {CurrentType} is null.");
        
        return dataNamespace;
    }

    private static Assembly GetLoadedAssembly()
    {
        var assembly = Assembly.GetAssembly(CurrentType);
        if (assembly is null)
            throw new InvalidOperationException($"Assembly of {CurrentType} is null while loading assembly.");

        return assembly;
    }

    private static Stream GetStreamFromAssembly(Assembly assembly, string path)
    {
        var stream = assembly.GetManifestResourceStream(path);
        if (stream is null)
            throw new InvalidOperationException($"Stream of assembly {assembly.FullName} is null.");

        return stream;
    }
}