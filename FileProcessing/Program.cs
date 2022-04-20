using FileProcessing;
using Serilog;

var homePath = args[0];
const string filter = "*.zip";
var currentLogDate = DateTime.Now.Date.ToString("yyyyMMdd");

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File($"{homePath}/logs/{currentLogDate}/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var dirMonitor = new DirectoryMonitor(homePath, filter);

Log.Information("Starting up");

FileProcess.Start();
