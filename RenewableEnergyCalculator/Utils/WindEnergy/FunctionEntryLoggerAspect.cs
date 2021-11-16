using PostSharp.Aspects;
using System;

namespace REIC
{
 
    [Serializable]
    public class FunctionEntryLoggerAspect: OnMethodBoundaryAspect
    {
        public override void OnEntry(MethodExecutionArgs args)
        {
            
            Console.WriteLine($"{DateTime.Now}; {args.Method.Name}()");
            base.OnEntry(args);
        }
    }
}
