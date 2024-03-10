namespace DryApi.Demo3;

public static class Demo3ServiceExtensions
{
    public static IServiceCollection AddDemo3Services(this IServiceCollection services, IConfiguration configuration)
    {
        return AddDemo3Services(services, configuration.GetValue<ValueCheckerType>("ValueChecker"));
    }

    public static IServiceCollection AddDemo3Services(this IServiceCollection services, ValueCheckerType valueCheckerType)
    {
        switch (valueCheckerType)
        {
            case ValueCheckerType.Ok:
                services.AddSingleton<IValueChecker, OkValueChecker>();
                break;
            case ValueCheckerType.NonEmpty:
                services.AddSingleton<IValueChecker, NonEmptyValueChecker>();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(valueCheckerType), valueCheckerType, null);
        }
        return services;
    }
}