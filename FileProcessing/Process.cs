using System.Diagnostics;
using Serilog;

namespace FileProcessing;

public static class FileProcess
{
    private static int _updateInteval = 5000;
    
    public static Queue<string> FilesToProcess = new Queue<string>();
    public static bool Terminate = false;

    public static void Start()
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
                    ProcessFile(file);
                }
                catch (Exception e)
                {   
                    Log.Error(e, "Error processing file");
                    Log.Debug("Stack trace {stackTrace}", e.StackTrace);
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
        psi.FileName = "bin/process.sh";
        psi.Arguments = file;
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
        Log.Information("Processing end time {time}", DateTime.Now);
    }
}