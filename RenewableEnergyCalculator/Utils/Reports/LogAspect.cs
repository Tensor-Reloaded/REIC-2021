using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostSharp.Aspects;

namespace chart
{
    [Serializable]
    public class LogAspect : OnMethodBoundaryAspect
    {
        public override void OnEntry(MethodExecutionArgs args)
        {
            Console.WriteLine("Method: {0} Starts!!!", args.Method);

            base.OnEntry(args);
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            Console.WriteLine("Method: {0} Ends!!!", args.Method);

            base.OnExit(args);
        }
    }
}
