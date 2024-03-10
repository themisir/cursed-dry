using DryApi.Demo2;
using DryApi.Demo3;

namespace DryApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddAuthorization();
        builder.Services.AddControllers();

        builder.Services.AddDemo2Services();
        builder.Services.AddDemo3Services(ValueCheckerType.NonEmpty);

        var app = builder.Build();

        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}