using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

// dotnet add package Microsoft.CodeAnalysis.CSharp
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace DryApi.Demo6;

[AttributeUsage(AttributeTargets.Method)]
public sealed class RequestFilter : ActionFilterAttribute
{
    private readonly Func<HttpContext, bool> _predicate;

    public RequestFilter(string predicate)
    {
        _predicate = Compile(predicate);
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!_predicate(context.HttpContext))
        {
            context.Result = new BadRequestResult();
        }
    }

    public static Func<HttpContext, bool> Compile(string predicate)
    {
        var source = $$"""
                       using System;
                       using Microsoft.AspNetCore.Http;

                       public static class RequestFilterPredicate {
                         public static bool Call(HttpContext ctx) {
                           return {{predicate}};
                         }
                       }
                       """;

        // based on https://raw.githubusercontent.com/joelmartinez/dotnet-core-roslyn-sample/master/Program.cs
        var syntaxTree = CSharpSyntaxTree.ParseText(source);

        var assemblyName = Path.GetRandomFileName();
        var refPaths = new List<string>
        {
            typeof(object).GetTypeInfo().Assembly.Location,
            typeof(Console).GetTypeInfo().Assembly.Location,
            Path.Combine(Path.GetDirectoryName(typeof(System.Runtime.GCSettings).GetTypeInfo().Assembly.Location)!,
                "System.Runtime.dll"),
            typeof(HttpContext).GetTypeInfo().Assembly.Location,
        };
        
        // Include reference assemblies of Microsoft.AspNetCore.Http.Abstractions
        refPaths.AddRange(GetAssemblyFiles(typeof(HttpContext).GetTypeInfo().Assembly));

        var compilation = CSharpCompilation.Create(
            assemblyName,
            syntaxTrees: new[] { syntaxTree },
            references: refPaths.Select(r => MetadataReference.CreateFromFile(r)),
            options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        using var ms = new MemoryStream();
        var result = compilation.Emit(ms);

        if (!result.Success)
        {
            var failures = result.Diagnostics.Where(diagnostic =>
                diagnostic.IsWarningAsError ||
                diagnostic.Severity == DiagnosticSeverity.Error);

            var sb = new StringBuilder();
            foreach (var diagnostic in failures)
            {
                sb.Append($"\t{diagnostic.Id}: {diagnostic.GetMessage()}\n");
            }
            throw new Exception(sb.ToString());
        }

        ms.Seek(0, SeekOrigin.Begin);
        var assembly = AssemblyLoadContext.Default.LoadFromStream(ms);

        // Extract object
        var t = assembly.GetType("RequestFilterPredicate")!;
        var method = t.GetMethod("Call", BindingFlags.Public | BindingFlags.Static)!;

        // Run
        return ctx => method.Invoke(null, [ctx]) is true;
    }

    // Based on https://stackoverflow.com/a/35803233
    private static IEnumerable<string> GetAssemblyFiles(Assembly assembly)
    {
        var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();
        return assembly.GetReferencedAssemblies()
            .Select(name => loadedAssemblies.SingleOrDefault(a => a.FullName == name.FullName)?.Location)
            .Where(l => l != null)!;
    }
}