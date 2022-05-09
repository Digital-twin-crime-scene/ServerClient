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
            _watcher.Changed += WatcherOnChanged;
            _watcher.EnableRaisingEvents = true;
        }

        private void WatcherOnChanged(object sender, FileSystemEventArgs e)
        {
            FileProcess.FilesToProcess.Enqueue(e.FullPath);
        }
    }
}
