using MassTransit;
using RabbitMQ.Client;

namespace SimpleRabbitApp.Contracts;

public static class ConfigurationExtensions
{
    public static void ConfigureMessageTopology(this IRabbitMqBusFactoryConfigurator configurator)
    {
        var flightTypes = Enum.GetValues(typeof(FlightServiceTypeEnum));
        configurator.MessageTopology.SetEntityNameFormatter(
            new EnvironmentNameFormatter(configurator.MessageTopology.EntityNameFormatter));

        // configurator.Send<HelloContract>(x =>
        // {
        //     x.UseCorrelationId(ctx => ctx.CorrelationId);
        //     x.UseRoutingKeyFormatter(ctx => ctx.Message.FlightServiceTypeEnum.ToString());
        // });
        
        


        configurator.Publish<HelloContract>(cfgt =>
        {
            cfgt.ExchangeType = ExchangeType.Direct;
            foreach (var flightType in flightTypes)
            {
                cfgt.BindQueue($"{HelloContract.ContractModelFullName}",
                    $"{HelloContract.ContractModelFullName}-{flightType}",
                    (cf) =>
                    {
                        cf.RoutingKey = $"{flightType}";
                        cf.ExchangeType = ExchangeType.Direct;
                    }
                );
            }
        });
    }
}