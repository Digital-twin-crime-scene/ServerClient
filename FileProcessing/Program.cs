using FileProcessing;
using Serilog;

string homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

string filesFolderPath = Path.Combine(homeDirectory, "files");
string logsFolderPath = Path.Combine(homeDirectory, "logs");
string inputFolderPath = Path.Combine(filesFolderPath, "input");
string outputFolderPath = Path.Combine(filesFolderPath, "output");
string recievedFolderPath = Path.Combine(filesFolderPath, "recieved");
string archivedFilesFolderPath = Path.Combine(homeDirectory, "archived");

var currentLogDate = DateTime.Now.Date.ToString("yyyyMMdd");

CreateRequiredDirectories();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File($"{logsFolderPath}/{currentLogDate}/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

Log.Information("Starting up");

DirectoryMonitor.GetInstance().StartMonitor(recievedFolderPath);

FileProcess.Start(archivedFilesFolderPath);

void CreateRequiredDirectories()
{
    if (!Directory.Exists(filesFolderPath))
    {
        Directory.CreateDirectory(filesFolderPath);
        Directory.CreateDirectory(inputFolderPath);
        Directory.CreateDirectory(outputFolderPath);
        Directory.CreateDirectory(recievedFolderPath);
        Directory.CreateDirectory(archivedFilesFolderPath);
    }

    if (!Directory.Exists(logsFolderPath))
        Directory.CreateDirectory(logsFolderPath);
}
