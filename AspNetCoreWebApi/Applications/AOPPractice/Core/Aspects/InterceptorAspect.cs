using Castle.DynamicProxy;
using Core.Interceptors;
using Console = System.Console;

namespace InvocationApp.Aspects;

public class InterceptorAspect : MethodInterception
{
    public override void OnBefore(IInvocation invocation)
    {
        Console.WriteLine("Before {0}.", invocation.Method);
        Console.WriteLine($"{"MethodName",-20}:{invocation.Method.Name}");
        Console.WriteLine($"{"TargetType",-20}:{invocation.TargetType}");
        Console.WriteLine($"{"InvocationTarget",-20}:{invocation.InvocationTarget}");
        Console.WriteLine($"{"Proxy",-20}:{invocation.Proxy}");
        Console.WriteLine("ARGUEMENTS");

        foreach (var p in invocation.Arguments)
        {
            Console.WriteLine($"\t{"Type",-12} : {p.GetType(),-15} | {p}");
        }

        Console.WriteLine();
    }

    public override void OnAfter(IInvocation invocation)
    {
        Console.WriteLine("After {0}.", invocation.Method);
    }
}