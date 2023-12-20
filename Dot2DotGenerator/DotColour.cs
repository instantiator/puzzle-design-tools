using System.Text.Json.Serialization;

namespace Dot2DotGenerator;

public class DotColour
{
    public enum KnownColour
    {
        Red, Green, Blue, Yellow, Orange, Purple, Pink, Black, White, Gray, Random
    }

    public Dictionary<KnownColour, string> ColourMap { get; set; } = new()
    {
        { KnownColour.Red, "#FF0000" },
        { KnownColour.Green, "#00FF00" },
        { KnownColour.Blue, "#0000FF" },
        { KnownColour.Yellow, "#FFFF00" },
        { KnownColour.Orange, "#FFA500" },
        { KnownColour.Purple, "#800080" },
        { KnownColour.Pink, "#FFC0CB" },
        { KnownColour.Black, "#000000" },
        { KnownColour.Gray, "#888888" },
        { KnownColour.White, "#FFFFFF" }
    };

    [JsonIgnore]
    public string AsHex
    {
        get
        {
            if (Rule.StartsWith("!"))
            {
                var c = Enum.Parse<KnownColour>(Rule[1..], true);
                if (c == KnownColour.Random) { return GenerateRandomColour(); }
                switch (c)
                {
                    case KnownColour.Random:
                        return GenerateRandomColour();
                    default:
                        KnownColour nc;
                        do
                        {
                            nc = SelectRandomColour();
                        } while (nc == KnownColour.Random || nc == c);
                        return ColourMap[nc];
                }
            }
            else
            {
                var c = Enum.Parse<KnownColour>(Rule, true);
                return c switch
                {
                    KnownColour.Random => ColourMap[SelectRandomColour()],
                    _ => ColourMap[c]
                };
            }
        }
    }

    public string Rule { get; set; } = null!;

    public static KnownColour SelectRandomColour()
    {
        var random = new Random();
        var colours = Enum.GetValues<KnownColour>().ToArray();
        return colours[random.Next(0, colours.Length)];
    }

    public static string GenerateRandomColour()
    {
        var random = new Random();
        var r = random.Next(0, 255);
        var g = random.Next(0, 255);
        var b = random.Next(0, 255);
        return $"#{r:X2}{g:X2}{b:X2}";
    }
}
