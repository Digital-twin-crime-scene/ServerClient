using FileProcessing;
using Serilog;

string homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
string filesFolderPath = Path.Combine(homeDirectory, "files");
string logsFolderPath = Path.Combine(homeDirectory, "logs");

var currentLogDate = DateTime.Now.Date.ToString("yyyyMMdd");

CreateRequiredDirectories();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File($"{logsFolderPath}/{currentLogDate}/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

DirectoryMonitor.GetInstance().StartMonitor(Path.Combine(filesFolderPath, "input"));

Log.Information("Starting up");

FileProcess.Start();

void CreateRequiredDirectories()
{
    if (!Directory.Exists(filesFolderPath))
    {
        Directory.CreateDirectory(filesFolderPath);
        Directory.CreateDirectory(Path.Combine(filesFolderPath, "input"));
        Directory.CreateDirectory(Path.Combine(filesFolderPath, "output"));
    }

    if (!Directory.Exists(logsFolderPath))
        Directory.CreateDirectory(logsFolderPath);
}
