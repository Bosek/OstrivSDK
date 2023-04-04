using CommandLine;
using OstrivSDK;

namespace OstrivConverter
{
    [Verb("levelsave", HelpText = "Work with .level_save file.")]
    class LevelSaveOptions
    {
        [Option('i', SetName = "Mode", HelpText = "Import mode toggle. In this mode src is .yaml file and dest is .level_save file.")]
        public bool Import { get; set; }

        [Option('e', SetName = "Mode", HelpText = "Export mode toggle. In this mode src is .level_save file and dest is .yaml file.")]
        public bool Export { get; set; }

        [Option('s', HelpText = "Source file name.", Required = true)]
        public string SrcPath { get; set; } = string.Empty;

        [Option('d', HelpText = "Destination file name.", Required = true)]
        public string DstPath { get; set; } = string.Empty;

        public int DoExport()
        {
            Console.WriteLine($"Exporting {SrcPath} to {DstPath}");
            try
            {
                LevelSaveManager.Load(SrcPath).SaveYaml(DstPath);

                Console.WriteLine("Success.");
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:");
                Console.WriteLine(ex);
                return 1;
            }
        }

        public int DoImport()
        {
            Console.WriteLine($"Importing {SrcPath} to {DstPath}");
            try
            {
                LevelSaveManager.LoadYaml(SrcPath).Save(DstPath);

                Console.WriteLine("Success.");
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:");
                Console.WriteLine(ex);
                return 1;
            }
        }
    }
}
