using Castle.DynamicProxy;

namespace Core.Interceptors;

public class MethodInterception : MethodInterceptionBaseAttribute
{
    public override void Intercept(IInvocation invocation)
    {
        bool successFlag = true;
        OnBefore(invocation);
        try
        {
            invocation.Proceed();
        }
        catch (Exception e)
        {
            successFlag = false;
            OnException(invocation, e);
            throw;
        }
        finally
        {
            if (successFlag)
                OnSuccess(invocation);
        }
        OnAfter(invocation);
    }

    public virtual void OnBefore(IInvocation invocation) { }
    public virtual void OnAfter(IInvocation invocation) { }
    public virtual void OnException(IInvocation invocation, Exception exception) { }
    public virtual void OnSuccess(IInvocation invocation) { }
}

