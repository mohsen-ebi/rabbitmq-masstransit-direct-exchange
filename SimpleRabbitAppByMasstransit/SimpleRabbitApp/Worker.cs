using MassTransit;
using MassTransit.Mediator;
using SimpleRabbitApp.Contracts;

namespace SimpleRabbitApp;

public class Worker : BackgroundService
{
    private readonly IBus _bus;
    private readonly ILogger<Worker> _worker;
    private readonly IMediator _mediator;

    private Array flightTypes;

    public Worker(ILogger<Worker> worker, IMediator mediator)
    {
        _worker = worker;
        _mediator = mediator;
        flightTypes = Enum.GetValues(typeof(FlightServiceTypeEnum));
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var random = new Random();
                var flightServiceType = (FlightServiceTypeEnum)flightTypes.GetValue(random.Next(flightTypes.Length));

                var helloContract = new HelloContract()
                {
                    Name = $"{flightServiceType}",
                    FlightServiceTypeEnum = flightServiceType
                };

                var requestClient = _mediator.CreateRequestClient<HelloContract>();
                
                var result = await requestClient.GetResponse<HelloContractResponseModel>(helloContract, stoppingToken);

                // await _bus.Publish(helloContract,
                //     (pipe) =>
                //     {
                //         pipe.SetRoutingKey($"{pipe.Message}");
                //     },
                //     stoppingToken);
                _worker.LogInformation($"{helloContract}");

                await Task.Delay(1000, stoppingToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}