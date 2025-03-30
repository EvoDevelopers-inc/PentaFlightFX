using System.Drawing;
using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using PentaFlightFX.DTO;
using PentaFlightFX.Utils;

namespace PentaFlightFX.Core;

public class ParticleEngine
{
    private Dictionary<CBaseCSGrenadeProjectile, CParticleSystem> AllUpdParticlesGrenadeProjectile = new();
    
    public void OnEntitySpawned(CEntityInstance entityInstance)
    {
        if (entityInstance.DesignerName == SupportEntity.GrenadeProjectile ||
            entityInstance.DesignerName == SupportEntity.FlashbangProjectile ||
            entityInstance.DesignerName == SupportEntity.MolotovProjectile ||
            entityInstance.DesignerName == SupportEntity.SmokegrenadeProjectile)
        {
            var grenadeProjectile = new CBaseCSGrenadeProjectile(entityInstance.Handle);
            HandleGrenadeProjectile(grenadeProjectile, entityInstance.DesignerName);
        }
    }

    private void HandleGrenadeProjectile(CBaseCSGrenadeProjectile grenadeProjectile, string designerName)
    {
        Server.NextFrame(() =>
        {
            var throwerValue = grenadeProjectile.Thrower.Value;
            if (throwerValue == null) return;
            var throwerValueController = throwerValue.Controller.Value;
            if (throwerValueController == null) return;
            var player = new CCSPlayerController(throwerValueController.Handle);
            
            var particle = Helper.GetPluginInstance()._ParticleChanceService.GetRandomParticle(designerName, player);
        
            if (particle == null)
                return;
            
            var particleSystem = Utilities.CreateEntityByName<CParticleSystem>("info_particle_system");

            particleSystem.StartActive = true;
            particleSystem.EffectName = particle.ParticlePath;
        
            particleSystem.Glow.GlowColorOverride = Color.Aqua;
            particleSystem.Glow.GlowRange = 5000;
            particleSystem.Glow.GlowRangeMin = 0;
            particleSystem.Glow.GlowTeam = 2;
            particleSystem.Glow.GlowType = 3;
            particleSystem.DispatchSpawn();
            particleSystem.Teleport(grenadeProjectile.AbsOrigin);
            particleSystem.AcceptInput("Start");
        
            AllUpdParticlesGrenadeProjectile.TryAdd(grenadeProjectile, particleSystem);
        });

    }

    public void OnTick()
    {
        foreach (KeyValuePair<CBaseCSGrenadeProjectile, CParticleSystem> entry in AllUpdParticlesGrenadeProjectile)
        {
            CParticleSystem particle = entry.Value;
            CBaseCSGrenadeProjectile projectile = entry.Key;

            if (projectile.IsValid)
            {
                particle.Teleport(projectile.AbsOrigin);
                particle.AcceptInput("Start");
            }
            else
            {

                Helper.GetPluginInstance().AddTimer(1.0f, (() =>
                {
                    projectile.Remove();
                }));
                AllUpdParticlesGrenadeProjectile.Remove(projectile);
            }
        }
    }
}