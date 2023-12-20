using System.Drawing;
using ExCSS;
using Svg;

namespace Dot2DotGenerator;

public class Generator
{
    public static SvgDocument Generate(InputDefinition inputs)
    {
        var w = inputs.Columns * inputs.ColumnWidth;
        var h = inputs.Rows * inputs.RowHeight;
        var svg = new SvgDocument()
        {
            Width = w,
            Height = h,
            ViewBox = new SvgViewBox(0, 0, w, h),
        };

        foreach (var dotset in inputs.DotSets)
        {
            var dotGroup = GenerateDotSet(dotset, inputs.Palette, inputs.ColumnWidth, inputs.RowHeight);
            svg.Children.Add(dotGroup);
        }
        var remainderGroup = GenerateRemainder(inputs);
        svg.Children.Add(remainderGroup);

        return svg;
    }

    public static SvgGroup GenerateDotSet(DotSet dotset, DotPalette palette, float columnWidth, float rowHeight)
    {
        var offsetX = columnWidth / 2;
        var offsetY = rowHeight / 2;
        var group = new SvgGroup();
        foreach (var point in dotset.Points)
        {
            var jx = GetRandomSize(dotset.MinJitter, dotset.MaxJitter);
            var jy = GetRandomSize(dotset.MinJitter, dotset.MaxJitter);
            var x = offsetX + point[0] * columnWidth + jx;
            var y = offsetY + point[1] * rowHeight + jy;
            var dot = dotset.Shape.AsSvg(x, y, dotset.MinSize, dotset.MaxSize);
            dot.Fill = new SvgColourServer(ColorTranslator.FromHtml(dotset.Colour.AsHex(palette)));
            group.Children.Add(dot);
        }
        return group;
    }

    public static SvgGroup GenerateRemainder(InputDefinition inputs)
    {
        var offsetX = inputs.ColumnWidth / 2;
        var offsetY = inputs.RowHeight / 2;
        var group = new SvgGroup();
        for (int cx = 0; cx < inputs.Columns; cx++)
        {
            for (int cy = 0; cy < inputs.Rows; cy++)
            {
                if (!inputs.DotSets.Any(ds => ds.Points.Any(p => p[0] == cx && p[1] == cy)))
                {
                    var jx = GetRandomSize(inputs.Remainder.MinJitter, inputs.Remainder.MaxJitter);
                    var jy = GetRandomSize(inputs.Remainder.MinJitter, inputs.Remainder.MaxJitter);
                    var x = offsetX + cx * inputs.ColumnWidth + jx;
                    var y = offsetY + cy * inputs.RowHeight + jy;
                    var dot = inputs.Remainder.Shape.AsSvg(x, y, inputs.Remainder.MinSize, inputs.Remainder.MaxSize);
                    dot.Fill = new SvgColourServer(ColorTranslator.FromHtml(inputs.Remainder.Colour.AsHex(inputs.Palette)));
                    group.Children.Add(dot);
                }
            }
        }
        return group;
    }

    public static float GetRandomSize(float min, float max)
    {
        var random = new Random();
        return (float)random.NextDouble() * (max - min) + min;
    }

}