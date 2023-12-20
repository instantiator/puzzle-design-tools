using System.Drawing;
using System.Text.Json;
using System.Text.Json.Serialization;
using CommandLine;
using Svg;

namespace Dot2DotGenerator;

public class Program
{
    public class Options
    {
        [Option('i', "input", Required = true, HelpText = "Input definitions path.")]
        public string InputPath { get; set; } = null!;

        [Option('o', "output", Required = true, HelpText = "Output path.")]
        public string OutputPath { get; set; } = null!;
    }

    public static void Main(string[] args)
    {
        Parser.Default.ParseArguments<Options>(args).WithParsed<Options>(GenerateSvg);
    }

    private static void GenerateSvg(Options options)
    {
        var definitions = JsonSerializer.Deserialize<InputDefinition>(File.ReadAllText(options.InputPath))!;

        var svg = Generator.Generate(definitions);
        svg.Write(options.OutputPath);
    }
}
