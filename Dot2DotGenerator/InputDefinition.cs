using System.Text.Json.Serialization;

namespace Dot2DotGenerator;

public class InputDefinition
{
    public int Columns { get; set; }
    public int Rows { get; set; }

    public float ColumnWidth { get; set; }
    public float RowHeight { get; set; }

    public List<DotSet> DotSets { get; set; } = new();
    public DotSet? Remainder { get; set; } = null;

    public DotPalette Palette { get; set; } = new();
}