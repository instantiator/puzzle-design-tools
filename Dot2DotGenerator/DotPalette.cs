namespace Dot2DotGenerator;

public class DotPalette : List<DotColourDefinition>
{
    public DotColourDefinition SelectRandomColour(string? except = null)
    {
        var r = new Random();
        var choices = except == null ? this : this.Where(c => c.Name != except);
        var i = r.Next(0, choices.Count());
        return choices.ElementAt(i);
    }

    public DotColourDefinition ForRule(string rule)
    {
        if (rule.StartsWith("!"))
        {
            if (rule[1..].Equals("random", StringComparison.OrdinalIgnoreCase))
            {
                return GenerateRandomColour();
            }
            else
            {
                var c = this.First(c => c.Name.Equals(rule[1..], StringComparison.OrdinalIgnoreCase));
                return SelectRandomColour(c.Name);
            }
        }
        else
        {
            if (rule.Equals("random", StringComparison.OrdinalIgnoreCase))
            {
                return SelectRandomColour();
            }
            else
            {
                return this.First(c => c.Name.Equals(rule, StringComparison.OrdinalIgnoreCase));
            }
        }
    }

    public DotColourDefinition GenerateRandomColour()
    {
        var r = new Random();
        var hex = $"#{r.Next(0, 255):X2}{r.Next(0, 255):X2}{r.Next(0, 255):X2}";
        return new DotColourDefinition
        {
            Name = hex,
            Hex = hex
        };
    }
}

public class DotColourDefinition
{
    public string Name { get; set; } = null!;
    public string Hex { get; set; } = null!;
}