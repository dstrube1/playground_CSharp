// https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection

#region Not using DI
using Microsoft.Extensions.Hosting;
//must run the following to get the above using to work:
//dotnet add package Microsoft.Extensions.Hosting
//this is required for the BackgroundService extension for Worker

public class MessageWriter
{
    public void Write(string message)
    {
        Console.WriteLine($"MessageWriter.Write(message: \"{message}\")");
    }
}

public class Worker : BackgroundService
{
    private readonly MessageWriter _messageWriter = new();

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _messageWriter.Write($"Worker running at: {DateTimeOffset.Now}");
            await Task.Delay(1_000, stoppingToken);
        }
    }
}
#endregion //Not using DI