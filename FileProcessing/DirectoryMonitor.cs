using Serilog;

namespace FileProcessing
{
    public class DirectoryMonitor
    {
        private static DirectoryMonitor _instance;
        
        private readonly string _filter = "*.zip";
        
        private string _directory;
        private FileSystemWatcher _watcher;

        static DirectoryMonitor()
        {
            
        }

        public static DirectoryMonitor GetInstance()
        {
            return _instance ?? (_instance = new DirectoryMonitor());
        }

        public void StartMonitor(string directory)
        {
            _directory = directory;

            _watcher = new FileSystemWatcher(_directory, _filter);
            _watcher.Created += WatcherOnCreated;
            _watcher.EnableRaisingEvents = true;
            Log.Information("Monitoring directory {directory}", _directory);
        }

        private void WatcherOnCreated(object sender, FileSystemEventArgs e)
        {
            FileProcess.FilesToProcess.Enqueue(e.FullPath);
        }
    }
}
