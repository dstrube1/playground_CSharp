// https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection

#region Not using DI
using Microsoft.Extensions.Hosting;
//must run the following to get the above using to work:
//dotnet add package Microsoft.Extensions.Hosting
//this is required for the BackgroundService extension for Worker

public class MessageWriterBad
{
    public void Write(string message)
    {
        Console.WriteLine($"MessageWriter.Write(message: \"{message}\")");
    }
}

public class Worker : BackgroundService
{
    //Hard-coded dependencies are problematic and should be avoided
    private readonly MessageWriterBad _messageWriter = new();

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

//.NET provides a built-in service container, IServiceProvider. 
//Services are typically registered at the app's start-up and appended to an IServiceCollection. 
//Once all services are added, you use BuildServiceProvider to create the service container.
public interface IMessageWriter
{
    void Write(string message);
}
public class MessageWriter : IMessageWriter
{
    public void Write(string message)
    {
        Console.WriteLine($"MessageWriter.Write(message: \"{message}\")");
    }
}