using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Modules.Timers;
using PentaFlightFX.Utils;

namespace PentaFlightFX.Services;

public class AdvertisementService
{
    private const float Duration = 1000;

    public AdvertisementService()
    {
        Helper.GetPluginInstance().AddTimer(Duration, () => { AdvertisementToChat(); }, TimerFlags.REPEAT);
    }
    
    private void AdvertisementToChat()
    {
        Server.PrintToChatAll(SupportColorsTags.ReplaceColorTags(Helper.GetPluginInstance()._ConfigService.GetConfig().MessageByAdvertisement));
    }
    
    
    
    
}