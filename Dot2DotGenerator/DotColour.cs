using System.Text.Json.Serialization;

namespace Dot2DotGenerator;

public class DotColour
{
    // public enum KnownColour
    // {
    //     Red, Green, Blue, Yellow, Orange, Purple, Pink, Black, White, Gray, Random
    // }

    // public Dictionary<KnownColour, string> ColourMap { get; set; } = new()
    // {
    //     { KnownColour.Red, "#FF0000" },
    //     { KnownColour.Green, "#00FF00" },
    //     { KnownColour.Blue, "#0000FF" },
    //     { KnownColour.Yellow, "#FFFF00" },
    //     { KnownColour.Orange, "#FFA500" },
    //     { KnownColour.Purple, "#800080" },
    //     { KnownColour.Pink, "#FFC0CB" },
    //     { KnownColour.Black, "#000000" },
    //     { KnownColour.Gray, "#888888" },
    //     { KnownColour.White, "#FFFFFF" }
    // };

    public string AsHex(DotPalette palette) => palette.ForRule(Rule).Hex;

    public string Rule { get; set; } = null!;

    // public static string GenerateRandomColour()
    // {
    //     var random = new Random();
    //     var r = random.Next(0, 255);
    //     var g = random.Next(0, 255);
    //     var b = random.Next(0, 255);
    //     return $"#{r:X2}{g:X2}{b:X2}";
    // }
}
