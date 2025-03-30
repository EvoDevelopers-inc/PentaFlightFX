namespace PentaFlightFX.Utils;

public class Helper
{
    private static PentaFlightFX _PentaFlightFx;
    
    public static void AddPluginClass(PentaFlightFX clazz)
    {
        _PentaFlightFx = clazz;
    }

    public static PentaFlightFX GetPluginInstance()
    {
        return _PentaFlightFx;
    }
}