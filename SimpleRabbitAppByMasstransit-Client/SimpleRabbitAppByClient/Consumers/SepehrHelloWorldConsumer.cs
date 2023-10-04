using MassTransit;
using SimpleRabbitApp.Contracts;

namespace SimpleRabbitApp.Consumers;

public class SepehrHelloWorldSepehrConsumer : IConsumer<HelloContract>
{
    private readonly ILogger<SepehrHelloWorldSepehrConsumer> _logger;


    public SepehrHelloWorldSepehrConsumer(ILogger<SepehrHelloWorldSepehrConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<HelloContract> context)
    {
        _logger.LogInformation($"{context.Message}");
        await Task.CompletedTask;
    }
}