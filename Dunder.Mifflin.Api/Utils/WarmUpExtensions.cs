using Dunder.Mifflin.Api.Repositories;

namespace Dunder.Mifflin.Api.Utils;

public static class WarmUpExtensions
{
    public static void WarmUp(this IServiceProvider app)
    {
        app.GetService<ILinesRepository>()?.SaveLinesFromCsvFileToDbIfDbEmpty();
    }
}