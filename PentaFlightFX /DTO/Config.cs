namespace PentaFlightFX.DTO;

public class Config
{
    public required string MessageByAdvertisement{ get; init; }
    public Dictionary<string, List<Particle>> Particles { get; init; } = new();
}