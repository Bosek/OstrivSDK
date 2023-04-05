using System.Drawing;

namespace OstrivSDK
{
    public struct Minimap : ISerializable<Minimap>, IDisposable
    {
        static int Signature { get; } = 20;
        public Bitmap Bitmap;

        public void Dispose()
        {
            Bitmap.Dispose();
        }

        public Minimap FromBytes(BinaryReader reader, object? obj = null)
        {
            reader.ReadInt32(); //Signature

            var bitmap = new Bitmap(1024, 1024);
            for (int y = 0; y < 1024; y++)
            {
                for (int x = 0; x < 1024; x++)
                {
                    var color = Color.FromArgb(reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
                    bitmap.SetPixel(x, y, color);
                }
            }
            bitmap.RotateFlip(RotateFlipType.Rotate270FlipX);
            Bitmap = bitmap;

            return this;
        }

        public void ToBytes(BinaryWriter writer, object? obj = null)
        {
            if (this.Bitmap.Width != 1024 || this.Bitmap.Height != 1024)
                throw new ArgumentException($"{nameof(Bitmap)} must be 1024x1024 pixels.");

            var bitmap = (Bitmap)Bitmap.Clone();
            bitmap.RotateFlip(RotateFlipType.Rotate270FlipX);

            writer.Write(Signature);

            for (int y = 0; y < 1024; y++)
            {
                for (int x = 0; x < 1024; x++)
                {
                    var color = bitmap.GetPixel(x, y);
                    writer.Write(color.R);
                    writer.Write(color.G);
                    writer.Write(color.B);
                }
            }
            bitmap.Dispose();
        }
    }
}