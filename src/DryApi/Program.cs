using DryApi.Demo2;
using DryApi.Demo3;
using DryApi.Demo4;

namespace DryApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddAuthorization();
        builder.Services.AddControllers();

        builder.Services.AddDemo2Services();
        builder.Services.AddDemo3Services(ValueCheckerType.Ok);
        builder.Services.AddDemo4Services();

        var app = builder.Build();

        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}