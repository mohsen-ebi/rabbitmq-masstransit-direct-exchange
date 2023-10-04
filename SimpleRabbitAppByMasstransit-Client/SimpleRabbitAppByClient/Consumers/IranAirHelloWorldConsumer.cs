using MassTransit;
using SimpleRabbitApp.Contracts;

namespace SimpleRabbitApp.Consumers;

public class IranAirHelloWorldSepehrConsumer : IConsumer<HelloContract>
{
    private readonly ILogger<IranAirHelloWorldSepehrConsumer> _logger;


    public IranAirHelloWorldSepehrConsumer(ILogger<IranAirHelloWorldSepehrConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<HelloContract> context)
    {
        _logger.LogInformation($"{context.Message}");
        await Task.CompletedTask;
    }
}