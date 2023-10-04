namespace SimpleRabbitApp.Contracts;

public record HelloContract
{
    public Guid CorrelationId { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public FlightServiceTypeEnum FlightServiceTypeEnum { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;

    public static string ContractModelFullName => $"{typeof(HelloContract).Namespace}:{nameof(HelloContract)}";
}

public enum FlightServiceTypeEnum
{
    Sepehr,
    IranAir,
    Nira,
    Sepehran
}

 
