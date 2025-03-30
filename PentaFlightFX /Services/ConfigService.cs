using System.Text.Json;
using PentaFlightFX.DTO;

namespace PentaFlightFX.Services;

public class ConfigService
{
    private Config _Config;
    private string _ModuleDirectory;

    public ConfigService(string moduleDirectory)
    {
        _ModuleDirectory = moduleDirectory;
    }

    private Config CreateConfig(string configPath)
    {
        var config = new Config
        {
            MessageByAdvertisement =
                "[{DEFAULT}PENTA{PURPLE}STRIKE{DEFAULT}][{YELLOW}PentaFlightFX{YELLOW}] Флешки — это {CYAN}искусство{DEFAULT}, а исходный код лучше — {LIME}https://github.com/EvoDevelopers-inc/PentaFlightFX {DEFAULT}!",
            Particles = new Dictionary<string, List<Particle>>()
            {
                [SupportEntity.GrenadeProjectile] = new List<Particle>
                {
                    new Particle { ParticlePath = "particles/money_fx/money_burst_money_single.vpcf", Chance = 15, Message = "[{DEFAULT}PENTA{PURPLE}STRIKE{DEFAULT}][{YELLOW}PentaFlightFX{YELLOW}] {GREEN}Долларовая {DEFAULT}орешка лети!" },
                    new Particle { ParticlePath = "particles/weapons/cs_weapon_fx/weapon_confetti_balloons.vpcf", Chance = 10, Message = "[{DEFAULT}PENTA{PURPLE}STRIKE{DEFAULT}][{YELLOW}PentaFlightFX{YELLOW}] {GOLD}Да будет праздник у гранаты{DEFAULT}!" },
                    // Оставшиеся 30% - частица не выпадет
                },
                [SupportEntity.FlashbangProjectile] = new List<Particle>
                {
                    new Particle { ParticlePath = "particles/weapons/cs_weapon_fx/weapon_confetti_balloons.vpcf", Chance = 10, Message = "[{DEFAULT}PENTA{PURPLE}STRIKE{DEFAULT}][{YELLOW}PentaFlightFX{YELLOW}] {GOLD}Да будет праздник у флешки{DEFAULT}!" },
                },
                [SupportEntity.MolotovProjectile] = new List<Particle>
                {
                    new Particle { ParticlePath = "particles/weapons/cs_weapon_fx/weapon_confetti_balloons.vpcf", Chance = 10, Message = "[{DEFAULT}PENTA{PURPLE}STRIKE{DEFAULT}][{YELLOW}PentaFlightFX{YELLOW}] {GOLD}Да будет праздник у огня{DEFAULT}!" },
                },
                [SupportEntity.SmokegrenadeProjectile] = new List<Particle>
                {
                    new Particle { ParticlePath = "particles/weapons/cs_weapon_fx/weapon_confetti_balloons.vpcf", Chance = 10, Message = "[{DEFAULT}PENTA{PURPLE}STRIKE{DEFAULT}][{YELLOW}PentaFlightFX{YELLOW}] {GOLD}Да будет праздник у смока{DEFAULT}!" },
                }
                
            }
        };

        File.WriteAllText(configPath,
            JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true }));

        return config;
    }

    public Config GetConfig()
    {
        if (_Config != null)
            return _Config;

        var configPath = Path.Combine(_ModuleDirectory, "PentaFlightFX_Config.json");
        if (!File.Exists(configPath)) return CreateConfig(configPath);

        var config = JsonSerializer.Deserialize<Config>(File.ReadAllText(configPath))!;

        return config;
    }
}