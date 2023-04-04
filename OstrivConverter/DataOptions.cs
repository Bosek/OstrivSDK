using CommandLine;
using OstrivSDK;

namespace OstrivConverter
{
    [Verb("data", HelpText = "Work with data.data file.")]
    internal class DataOptions
    {
        [Option('i', SetName = "Mode", HelpText = "Import mode toggle. In this mode src is data folder and dest is data.data file.")]
        public bool Import { get; set; }

        [Option('e', SetName = "Mode", HelpText = "Export mode toggle. In this mode src is data.data(other .data files don't work) file and dest is data folder.")]
        public bool Export { get; set; }

        [Option('s', HelpText = "Source file/folder name.", Required = true)]
        public string SrcPath { get; set; } = string.Empty;

        [Option('d', HelpText = "Destination file/folder name.", Required = true)]
        public string DstPath { get; set; } = string.Empty;

        public int DoExport()
        {
            Console.WriteLine($"Exporting {SrcPath} to {DstPath}");
            try
            {
                DataManager.Load(SrcPath).SaveFolder(DstPath);

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
                DataManager.LoadFolder(SrcPath).Save(DstPath);

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
