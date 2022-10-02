using Api.DB;

namespace Api.Utils;

public static class WarmUpExtensions
{
    public static void WarmUp(this IServiceProvider app)
    {
        app.GetService<IQuotesRepository>()?.WarmUp();
    }
}