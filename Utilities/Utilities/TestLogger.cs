using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Layout;

namespace Utilities.Utilities
{
    public class TestLogger
    {
        public static void InitializeLog()
        {
            var pattern = new PatternLayout
            {
                ConversionPattern = "%date{dd-MMM-yyyy HH:mm} [%method] : [%message]%newline"
            };
            pattern.ActivateOptions();

            var consoleAppend = new ConsoleAppender()
            {
                Name = "ConsoleAppender",
                Layout = pattern,
                Threshold = Level.All
            };

            var fileAppender = new FileAppender()
            {
                Name = "FileAppender",
                Layout = pattern,
                Threshold = Level.All,
                AppendToFile = false,
                File = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)!.Parent!.Parent!.Parent!.FullName, "Tests\\Logs\\FileLogger.log")
            };

            fileAppender.ActivateOptions();
            consoleAppend.ActivateOptions();
            BasicConfigurator.Configure(consoleAppend, fileAppender);
        }
    }
}