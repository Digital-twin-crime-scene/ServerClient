namespace FileProcessing;

public class DirectoryMonitor
{
    private readonly string _directory;
    private readonly string _filter;
    private readonly FileSystemWatcher _watcher;

    public DirectoryMonitor(string directory, string filter)
    {
        _directory = directory;
        _filter = filter;

        if (!Directory.Exists(_directory))
            Directory.CreateDirectory(_directory);
        
        _watcher = new FileSystemWatcher(_directory, _filter);
        _watcher.Created += OnCreated;
        _watcher.EnableRaisingEvents = true;
    }
    
    private void OnCreated(object sender, FileSystemEventArgs e)
    {
        FileProcess.FilesToProcess.Enqueue(e.FullPath);
    }
}