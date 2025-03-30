using System.Drawing;
using System.Globalization;
using System.Text.Json;
using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Timers;
using CounterStrikeSharp.API.Modules.Utils;
using Microsoft.Extensions.DependencyInjection;
using PentaFlightFX.Core;
using PentaFlightFX.DTO;
using PentaFlightFX.Services;
using PentaFlightFX.Utils;

namespace PentaFlightFX;

public class PentaFlightFX : BasePlugin
    {
        public override string ModuleAuthor => "EvoDevelopers";
        public override string ModuleName => "PentaFlightFX Visualization of the trajectory of granted through particles";
        public override string ModuleVersion => "1.0.0";

        public ConfigService _ConfigService { get; set; }
        public AdvertisementService _AdvertisementService { get; set; }
        
        public ParticleEngine _ParticleEngine { get; set; }
        public ParticleChanceService _ParticleChanceService { get; set; }


        public override void Load(bool hotReload)
        {
            Helper.AddPluginClass(this);

            InitServices();

            RegisterListener<Listeners.OnServerPrecacheResources>(OnPrecacheResources);
            RegisterListener<Listeners.OnEntitySpawned>(OnEntitySpawned);
            RegisterListener<Listeners.OnTick>(_ParticleEngine.OnTick);

        }

        private void OnEntitySpawned(CEntityInstance entityInstance)
        {
            _ParticleEngine.OnEntitySpawned(entityInstance);
        }

        private void InitServices()
        {
            _ConfigService = new ConfigService(ModuleDirectory);
            _ParticleChanceService = new ParticleChanceService();
            _ParticleEngine = new ParticleEngine();
            _AdvertisementService = new AdvertisementService();
            
        }
        
        private void OnPrecacheResources(ResourceManifest manifest)
        {
            Config config = _ConfigService.GetConfig();
        
            foreach (var entry in config.Particles)
            {
                foreach (var particle in entry.Value)
                {
                    manifest.AddResource(particle.ParticlePath);
                }
            }
        }

    }