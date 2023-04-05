using CommandLine;
using OstrivSDK;

namespace OstrivConverter
{
    [Verb("minimap", HelpText = "Work with .minimap file.")]
    class MinimapOptions
    {
        [Option('i', SetName = "Mode", HelpText = "Import mode toggle. In this mode src is 1024x1024px .bmp, .gif, .jpeg, .png or .tiff image and dest is .minimap file.")]
        public bool Import { get; set; }

        [Option('e', SetName = "Mode", HelpText = "Export mode toggle. In this mode src is .minimap file and dest is 1024x1024px .bmp, .gif, .jpeg, .png or .tiff image.")]
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
                MinimapManager.Load(SrcPath).SaveBitmap(DstPath);

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
                MinimapManager.LoadBitmap(SrcPath).Save(DstPath);

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
