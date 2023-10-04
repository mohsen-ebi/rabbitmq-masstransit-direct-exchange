using MassTransit;
using SimpleRabbitApp.Contracts;

namespace SimpleRabbitApp.Consumers;

public class SepehranHelloWorldSepehrConsumer : IConsumer<HelloContract>
{
    private readonly ILogger<SepehranHelloWorldSepehrConsumer> _logger;


    public SepehranHelloWorldSepehrConsumer(ILogger<SepehranHelloWorldSepehrConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<HelloContract> context)
    {
        _logger.LogInformation($"{context.Message}");
        await Task.CompletedTask;
    }
}