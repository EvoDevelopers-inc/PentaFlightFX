using System;
using System.Collections.Generic;
using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using PentaFlightFX.DTO;
using PentaFlightFX.Utils;

namespace PentaFlightFX.Services;

public class ParticleChanceService
{
    private readonly Random _random = new();
    
    public Particle? GetRandomParticle(string entityType, CCSPlayerController? player = null)
    {
        var config = Helper.GetPluginInstance()._ConfigService.GetConfig();
        
        if (!config.Particles.TryGetValue(entityType, out var particles) || particles.Count == 0)
            return null;
        
        
        int totalChance = 0;
        foreach (var particle in particles)
        {
            totalChance += particle.Chance;
        }
        
      
        if (totalChance < 100)
        {
            int roll = _random.Next(1, 101);
            if (roll > totalChance)
                return null; 
        }
        
        int randomValue = _random.Next(1, totalChance + 1);
        
   
        int accumulatedChance = 0;
        foreach (var particle in particles)
        {
            accumulatedChance += particle.Chance;
            if (randomValue <= accumulatedChance)
            {
              
                if (player != null && !string.IsNullOrEmpty(particle.Message))
                {
                    player.PrintToChat(SupportColorsTags.ReplaceColorTags(particle.Message));
                }
                
                return particle;
            }
        }
        
        return particles.Count > 0 ? particles[0] : null;
    }
}