// using MassTransit;
// using RabbitMQ.Client;
// using SimpleRabbitApp.Contracts;
//
// namespace SimpleRabbitApp.Consumers;
//
// public class HelloWorldConsumerDefinition : ConsumerDefinition<SepehrHelloWorldSepehrConsumer>
// {
//     public HelloWorldConsumerDefinition()
//     {
//         //EndpointName = "SimpleRabbitApp.Contracts:HelloContract-Sepehr";
//     }
//
//     protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
//         IConsumerConfigurator<SepehrHelloWorldSepehrConsumer> consumerConfigurator,
//         IRegistrationContext context)
//     {
//         endpointConfigurator.ConfigureConsumeTopology = false;
//
//         EndpointName = "SimpleRabbitApp.Contracts:HelloContract-Sepehr";
//         if (endpointConfigurator is IRabbitMqReceiveEndpointConfigurator rmq)
//         {
//             rmq.ExchangeType = ExchangeType.Direct;
//             rmq.BindQueue = true;
//             rmq.Bind($"{HelloContract.ContractModelFullName}", (x) =>
//             {
//                 x.RoutingKey = nameof(FlightServiceTypeEnum.Sepehr);
//                 x.ExchangeType = ExchangeType.Direct;
//             });
//             
//         }
//
//         base.ConfigureConsumer(endpointConfigurator, consumerConfigurator, context);
//     }
//     
//      
// }