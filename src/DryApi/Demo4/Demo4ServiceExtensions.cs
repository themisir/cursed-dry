namespace DryApi.Demo4;

public static class Demo4ServiceExtensions
{
    public static IServiceCollection AddDemo4Services(this IServiceCollection services)
    {
        services.AddSingleton<ValueCheckerFactory>();
        return services;
    }
}