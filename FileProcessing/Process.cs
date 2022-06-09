using System.Diagnostics;
using Serilog;

namespace FileProcessing;

public static class FileProcess
{
    private static int _updateInteval = 5000;
    
    public static Queue<string> FilesToProcess = new Queue<string>();
    public static bool Terminate = false;

    public static void Start(string archivedFilesFolderPath)
    {
        while (!Terminate || FilesToProcess.Count > 0)
        {
            if (FilesToProcess.Count > 0)
            {
                try
                {
                    var file = FilesToProcess.Dequeue();
                    Log.Information("Processing file {file}", file);
                    Log.Information("Processing start time {time}", DateTime.Now);
                    var filename = file.Split('/').Last();
                    ProcessFile(file);
                    
                    File.Move(file, $"{archivedFilesFolderPath}/{filename}");
                    File.Delete(file);
                }
                catch (Exception e)
                {   
                    Log.Error(e, "Error processing file");
                    Log.Debug("Stack trace {stackTrace}", e.StackTrace);
                    Log.Information("Another try in 5 seconds");
                    Thread.Sleep(_updateInteval);
                }
            }
            else
            {
                Thread.Sleep(_updateInteval);
            }
        }
    }

    private static void ProcessFile(string file)
    {
        var psi = new ProcessStartInfo();
        psi.FileName = "Resources/proc.sh";
        psi.Arguments = file.Split('/').Last();
        psi.UseShellExecute = false;
        psi.RedirectStandardOutput = true;
        psi.RedirectStandardError = true;

        var process = new Process();
        process.StartInfo = psi;
        process.Start();

        Task.WaitAll(Task.Run(() =>
        {
            while(!process.StandardOutput.EndOfStream)
            {
                var line = process.StandardOutput.ReadLine();
                Log.Information("{line}", line);
            }
        }));

        process.WaitForExit();
        Log.Information("File {file} processed", file);
        Log.Information("Processing end time: {time}", DateTime.Now);
    }
}