using CommandLine;

namespace OstrivConverter
{
    internal class Program
    {
        static int Main(string[] args)
        {
            var result = Parser.Default.ParseArguments<MinimapOptions, LevelSaveOptions, DataOptions, object>(args);
            var mapped = result.MapResult((MinimapOptions opts) =>
            {
                if (opts.Export)
                    return opts.DoExport();
                if (opts.Import)
                    return opts.DoImport();
                return 1;
            }, (LevelSaveOptions opts) =>
            {
                if (opts.Export)
                    return opts.DoExport();
                if (opts.Import)
                    return opts.DoImport();
                return 1;
            }, (DataOptions opts) =>
            {
                if (opts.Export)
                    return opts.DoExport();
                if (opts.Import)
                    return opts.DoImport();
                return 1;
            }, errors => 1);

            return mapped;
        }
    }
}