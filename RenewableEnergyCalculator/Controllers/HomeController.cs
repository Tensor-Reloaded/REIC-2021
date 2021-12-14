using System.Web.Mvc;
using NLog;
using NLog.Config;
using NLog.Targets;
using PostSharp.Patterns.Diagnostics;
using PostSharp.Patterns.Diagnostics.Backends.NLog;

namespace RenewableEnergyCalculator.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            #region Logging Configuration

            var nlogConfig = new LoggingConfiguration();

            var fileTarget = new FileTarget("file") {
                FileName = "RenewableEnergyTrace.log",
                KeepFileOpen = true,
                ConcurrentWrites = false,
            };

            nlogConfig.AddTarget(fileTarget);
            nlogConfig.LoggingRules.Add(new LoggingRule("*", NLog.LogLevel.Debug, fileTarget));

            LogManager.EnableLogging();

            // Configure PostSharp Logging to use NLog.
            LoggingServices.DefaultBackend = new NLogLoggingBackend(new LogFactory(nlogConfig));

            #endregion

            return View("About");
        }

        public ActionResult Contact()
        {
            return View("Contact");
        }
    }
}