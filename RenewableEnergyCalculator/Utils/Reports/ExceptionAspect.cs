using System;
using PostSharp.Aspects;

namespace chart
{
    [Serializable]
    public class ExceptionAspect : OnExceptionAspect
    {
        public override void OnException(MethodExecutionArgs args)
        {
            Console.WriteLine(String.Format("Exception in Method: {0}, Message: {1} ", args.Method, args.Exception.Message));
            args.FlowBehavior = FlowBehavior.Continue;

            base.OnException(args);
        }
    }
}
