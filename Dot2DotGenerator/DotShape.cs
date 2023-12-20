using System.Drawing;
using System.Text.Json.Serialization;
using Svg;

namespace Dot2DotGenerator;

public class DotShape
{
    private static float STROKE_WIDTH = 0.05f;
    public enum KnownShape
    {
        Circle, Square, Triangle, Random
    }

    [JsonIgnore]
    public KnownShape AsShape =>
        Rule.Equals("random", StringComparison.OrdinalIgnoreCase)
            ? GetRandomShape(new[] { KnownShape.Random })
            : Enum.Parse<KnownShape>(Rule, true);

    public string Rule { get; set; } = null!;

    public static KnownShape GetRandomShape(KnownShape[] except)
    {
        var random = new Random();
        var shapes = Enum.GetValues<KnownShape>().Except(new[] { KnownShape.Random }).Except(except).ToArray();
        return shapes[random.Next(0, shapes.Length)];
    }

    public SvgPathBasedElement AsSvg(float x, float y, float minSize, float maxSize)
    {
        return AsSvg(AsShape, x, y, minSize, maxSize);
    }

    public static SvgPathBasedElement AsSvg(KnownShape shape, float x, float y, float minSize, float maxSize)
    {
        switch (shape)
        {
            case KnownShape.Circle:
                return new SvgCircle()
                {
                    CenterX = x,
                    CenterY = y,
                    Radius = Generator.GetRandomSize(minSize, maxSize) / 2,
                    StrokeWidth = STROKE_WIDTH,
                    Stroke = new SvgColourServer(Color.Black)
                };

            case KnownShape.Square:
                var squareSide = Generator.GetRandomSize(minSize, maxSize);
                return new SvgRectangle()
                {
                    X = x,
                    Y = y,
                    Width = squareSide,
                    Height = squareSide,
                    StrokeWidth = STROKE_WIDTH,
                    Stroke = new SvgColourServer(Color.Black)
                };

            case KnownShape.Triangle:
                var triSide = Generator.GetRandomSize(minSize, maxSize);
                return new SvgPolygon()
                {
                    Points = new SvgPointCollection()
                    {
                        x - triSide / 2, y - triSide / 2,
                        x + triSide / 2, y - triSide / 2,
                        x, y + triSide / 2
                    },
                    StrokeWidth = STROKE_WIDTH,
                    Stroke = new SvgColourServer(Color.Black)
                };

            default:
                return AsSvg(KnownShape.Circle, x, y, minSize, maxSize);

        }
    }
}
