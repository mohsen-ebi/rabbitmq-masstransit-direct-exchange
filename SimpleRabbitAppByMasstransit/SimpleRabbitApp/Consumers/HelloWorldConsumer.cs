using MassTransit;
using SimpleRabbitApp.Contracts;

namespace SimpleRabbitApp.Consumers;

public class HelloWorldConsumer : IConsumer<HelloContract>
{
    private readonly IBus _bus;
    private readonly ILogger<HelloWorldConsumer> _logger;

    public HelloWorldConsumer(IBus bus, ILogger<HelloWorldConsumer> logger)
    {
        _bus = bus;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<HelloContract> context)
    {
        try
        {
            var request = context.Message;
            var cancellationToken = context.CancellationToken;

            var sendEndpoint = await _bus.GetSendEndpoint(new Uri("exchange:SimpleRabbitApp.Contracts:HelloContract?type=direct"));
            await sendEndpoint.Send<HelloContract>(request,
                (ct) =>
                {
                    ct.SetRoutingKey($"{ct.Message.FlightServiceTypeEnum}");
                    ct.SetAwaitAck(true);
                }, cancellationToken);


            // await _bus.Publish(request,
            //     (pipe) =>
            //     {
            //         pipe.SetRoutingKey($"{pipe.Message.FlightServiceTypeEnum}");
            //         pipe.Mandatory = true;
            //     },
            //     cancellationToken);
            await context.RespondAsync<HelloContractResponseModel>(new HelloContractResponseModel()
            {
                Id = context.Message.CorrelationId
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}