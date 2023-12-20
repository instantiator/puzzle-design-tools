namespace Dot2DotGenerator;

public class DotSet
{
    public DotShape Shape { get; set; } = null!;
    public DotColour Colour { get; set; } = null!;
    public List<int[]> Points { get; set; } = new();
    public float MinSize { get; set; }
    public float MaxSize { get; set; }
    public float MinJitter { get; set; }
    public float MaxJitter { get; set; }
}
