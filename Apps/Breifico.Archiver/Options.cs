using CommandLine;

namespace Breifico.Archiver
{
    class Options
    {
        [Option('i', "input", Required = true, HelpText = "Input file name")]
        public string InputFile { get; set; }

        [Option('o', "output", Required = true, HelpText = "Output file name")]
        public string OutputFile { get; set; }

        [Option("rle", Default = true, HelpText = "Use RLE compression")]
        public bool UseRle { get; set; }
    }
}
