using System.Reflection;
using MassTransit;
using MassTransit.Configuration;
using MassTransit.Middleware;
using MassTransit.RabbitMqTransport.Topology;
using MassTransit.Topology;
using RabbitMQ.Client;
using SimpleRabbitApp;
using SimpleRabbitApp.Consumers;
using SimpleRabbitApp.Contracts;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddMassTransit(x =>
{
    var entryAssembly = Assembly.GetEntryAssembly();
    //x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter(true));
    x.AddConsumers(entryAssembly);
    //x.AddConsumer<SepehrHelloWorldSepehrConsumer, HelloWorldConsumerDefinition>();
    // x.AddConsumer<SepehrHelloWorldConsumer, HelloWorldConsumerDefinition>().Endpoint((cfg) =>
    // {
    //     cfg.Name = $"{HelloContract.ContractModelFullName}-Sepehr";
    // });

    x.UsingRabbitMq((context, cfg) =>
    {
        var flightTypes = Enum.GetValues(typeof(FlightServiceTypeEnum));
        cfg.Host("127.0.0.1", "/", h =>
        {
            h.Username("admin");
            h.Password("123@321");
        });

        //cfg.ConfigureMessageTopology();
        //cfg.ConfigureEndpoints(context);

        // var sepehrQueueName =
        //     "SimpleRabbitApp.Contracts:HelloContract-Sepehr"; //context.GetRequiredService<IEndpointNameFormatter>().Consumer<SepehrHelloWorldSepehrConsumer>();
        // var iranAirQueueName =
        //     "SimpleRabbitApp.Contracts:HelloContract-IranAir"; //context.GetRequiredService<IEndpointNameFormatter>().Consumer<SepehrHelloWorldSepehrConsumer>();
        // var sepehranQueueName =
        //     "SimpleRabbitApp.Contracts:HelloContract-IranAir";

        

        cfg.ReceiveEndpoint($"{HelloContract.ContractModelFullName}-IranAir", (e) =>
        {
            e.ConfigureConsumeTopology = false;
            e.ExchangeType = ExchangeType.Direct;
                
            e.Bind($"{HelloContract.ContractModelFullName}", (ctx) =>
            {
                
                ctx.RoutingKey = nameof(FlightServiceTypeEnum.IranAir);
                ctx.ExchangeType = ExchangeType.Direct;
            });
            
            e.ConfigureConsumer<IranAirHelloWorldSepehrConsumer>(context);
        });

        cfg.ReceiveEndpoint($"{HelloContract.ContractModelFullName}-Sepehr", (e) =>
        {
            e.ConfigureConsumeTopology = false;
            e.ExchangeType = ExchangeType.Direct;
            e.Bind($"{HelloContract.ContractModelFullName}", (ctx) =>
            {
                ctx.RoutingKey = nameof(FlightServiceTypeEnum.Sepehr);
                ctx.ExchangeType = ExchangeType.Direct;
            });
            e.ConfigureConsumer<SepehrHelloWorldSepehrConsumer>(context);
        });

        cfg.ReceiveEndpoint($"{HelloContract.ContractModelFullName}-Sepehran", (e) =>
        {
            e.ConfigureConsumeTopology = false;
            e.ExchangeType = ExchangeType.Direct;
            e.Bind($"{HelloContract.ContractModelFullName}", (ctx) =>
            {
                ctx.RoutingKey = nameof(FlightServiceTypeEnum.Sepehran);
                ctx.ExchangeType = ExchangeType.Direct;
            });
            e.ConfigureConsumer<SepehranHelloWorldSepehrConsumer>(context);
        });

        cfg.ReceiveEndpoint($"{HelloContract.ContractModelFullName}-Nira", (e) =>
        {
            e.ConfigureConsumeTopology = false;
            e.ExchangeType = ExchangeType.Direct;
            e.Bind($"{HelloContract.ContractModelFullName}", (ctx) =>
            {
                ctx.RoutingKey = nameof(FlightServiceTypeEnum.Nira);
                ctx.ExchangeType = ExchangeType.Direct;
            });
            e.ConfigureConsumer<SepehranHelloWorldSepehrConsumer>(context);
        });
    });
});


var app = builder.Build();


app.MapGet("/", () => "Hello World!");

app.Run();


class EnvironmentNameFormatter : IEntityNameFormatter
{
    private IEntityNameFormatter _original;

    public EnvironmentNameFormatter(IEntityNameFormatter original)
    {
        _original = original;
    }

    public string FormatEntityName<T>()
    {
        return $"{_original.FormatEntityName<T>()}";
    }
}