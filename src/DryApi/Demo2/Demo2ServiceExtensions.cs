namespace DryApi.Demo2;

public static class Demo2ServiceExtensions
{
    public static IServiceCollection AddDemo2Services(this IServiceCollection services)
    {
        services.AddSingleton<ValueChecker>();
        return services;
    }
}