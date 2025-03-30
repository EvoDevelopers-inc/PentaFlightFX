namespace PentaFlightFX.DTO;

public class Particle
{
    public required string ParticlePath { get; init; }
    public int Chance { get; init; } = 100; 
    public string? Message { get; init; }
}