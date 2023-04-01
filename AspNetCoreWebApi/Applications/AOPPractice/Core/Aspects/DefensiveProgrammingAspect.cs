using Castle.DynamicProxy;
using Core.Interceptors;

namespace InvocationApp.Aspects;

public class DefensiveProgrammingAspect : MethodInterception
{
    public override void OnBefore(IInvocation invocation)
    {
        var parameters = invocation.Arguments;
        foreach (var item in parameters)
        {
            if (item.Equals(null))
            {
                throw new ArgumentNullException();
            }
        }
        Console.WriteLine("Null check has been completed for {0}", invocation.Method);
    }
}