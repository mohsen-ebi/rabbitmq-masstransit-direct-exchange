using MassTransit;
using SimpleRabbitApp.Contracts;

namespace SimpleRabbitApp.Consumers;

public class NiraHelloWorldSepehrConsumer : IConsumer<HelloContract>
{
    private readonly ILogger<NiraHelloWorldSepehrConsumer> _logger;


    public NiraHelloWorldSepehrConsumer(ILogger<NiraHelloWorldSepehrConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<HelloContract> context)
    {
        _logger.LogInformation($"{context.Message}");
        await Task.CompletedTask;
    }
}