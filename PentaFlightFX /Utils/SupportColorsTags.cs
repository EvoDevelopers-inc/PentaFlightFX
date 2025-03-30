using CounterStrikeSharp.API.Modules.Utils;

namespace PentaFlightFX.Utils;

public class SupportColorsTags
{
    private static readonly string[] colorPatterns =
    {
        "{DEFAULT}", "{WHITE}", "{DARKRED}", "{GREEN}", "{LIGHTYELLOW}", "{LIGHTBLUE}", "{OLIVE}", "{LIME}",
        "{RED}", "{LIGHTPURPLE}", "{PURPLE}", "{GREY}", "{YELLOW}", "{GOLD}", "{SILVER}", "{BLUE}",
        "{DARKBLUE}",
        "{BLUEGREY}", "{MAGENTA}", "{LIGHTRED}", "{ORANGE}"
    };

    private static readonly string[] colorReplacements =
    {
        $"{ChatColors.Default}", $"{ChatColors.White}", $"{ChatColors.Darkred}", $"{ChatColors.Green}",
        $"{ChatColors.LightYellow}", $"{ChatColors.LightBlue}", $"{ChatColors.Olive}", $"{ChatColors.Lime}",
        $"{ChatColors.Red}", $"{ChatColors.LightPurple}", $"{ChatColors.Purple}", $"{ChatColors.Grey}",
        $"{ChatColors.Yellow}", $"{ChatColors.Gold}", $"{ChatColors.Silver}", $"{ChatColors.Blue}",
        $"{ChatColors.DarkBlue}", $"{ChatColors.BlueGrey}", $"{ChatColors.Magenta}", $"{ChatColors.LightRed}",
        $"{ChatColors.Orange}"
    };
    
    
    public static string ReplaceColorTags(string input)
    {
        for (var i = 0; i < colorPatterns.Length; i++)
            input = input.Replace(colorPatterns[i], colorReplacements[i]);

        return input;
    }
}