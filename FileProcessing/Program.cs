using FileProcessing;
using Serilog;

var homePath = args[0];
const string filter = "*.zip";

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File($"{homePath}/logs/{DateTime.Now.Date}/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
    
var dirMonitor = new DirectoryMonitor(homePath, filter);

Log.Information("Starting up");

FileProcess.Start();
