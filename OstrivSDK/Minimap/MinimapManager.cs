using System.Drawing;

namespace OstrivSDK
{
    public static class MinimapManager
    {
        public static Minimap Load(string filename)
        {
            using var file = File.OpenRead(filename);
            using var reader = new BinaryReader(file);

            var minimap = new Minimap().FromBytes(reader);

            return minimap;
        }
        public static Minimap LoadBitmap(string filename)
        {
            return new Minimap { Bitmap = new Bitmap(filename) };
        }

        public static Minimap Save(this Minimap minimap, string filename)
        {
            using var file = File.OpenWrite(filename);
            using var writer = new BinaryWriter(file);

            minimap.ToBytes(writer);

            return minimap;
        }

        public static Minimap SaveBitmap(this Minimap minimap, string filename)
        {
            minimap.Bitmap.Save(filename);
            return minimap;
        }
    }
}
