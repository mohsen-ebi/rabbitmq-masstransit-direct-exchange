using System.Reflection;
using MassTransit;
using SimpleRabbitApp;
using SimpleRabbitApp.Contracts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(x =>
{
    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter(true));
    var entryAssembly = Assembly.GetEntryAssembly();
    // x.AddConsumers(entryAssembly);
    // x.AddConsumer<SepehrHelloWorldConsumer, HelloWorldConsumerDefinition>().Endpoint((cfg) =>
    // {
    //     cfg.Name = $"{HelloContract.ContractModelFullName}-Sepehr";
    // });

    x.AddMediator(cfg => { cfg.AddConsumers(entryAssembly); });

    
     
    
    x.UsingRabbitMq((context, cfg) =>
    {
        
        
        
        cfg.Host("127.0.0.1", "/", h =>
        {
            h.Username("admin");
            h.Password("123@321");
        });

        cfg.ConfigureMessageTopology();
        
        //cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddHostedService<Worker>();

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