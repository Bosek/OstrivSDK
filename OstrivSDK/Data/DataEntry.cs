using System.Text;

namespace OstrivSDK
{
    public struct DataEntry : ISerializable<DataEntry>
    {
        public string Filename;
        public byte[] Data;
        public int Size;

        public DataEntry FromBytes(BinaryReader reader, object? arg)
        {
            if (arg == null)
                throw new ArgumentException($"{nameof(arg)} is null.");
            var dataSectionOffset = (int)arg;

            var length = reader.ReadInt32();
            StringBuilder sb = new();
            for (int y = 0; y < length; y++)
            {
                sb.Append(reader.ReadChar());
                reader.ReadBytes(3);
            }
            var entityDataOffset = reader.ReadInt32();

            Filename = sb.ToString();
            Size = reader.ReadInt32();

            var offset = reader.BaseStream.Position;
            reader.BaseStream.Seek(dataSectionOffset + entityDataOffset, SeekOrigin.Begin);
            Data = reader.ReadBytes(Size);
            reader.BaseStream.Seek(offset, SeekOrigin.Begin);

            return this;
        }

        public void ToBytes(BinaryWriter writer, object? arg)
        {
            if (arg == null)
                throw new ArgumentException($"{nameof(arg)} is null.");
            var lastEntryDataOffset = (int)arg;

            writer.Write(Filename.Length);
            foreach (var c in Filename)
            {
                writer.Write(c);
                writer.Write(new byte[] { 0, 0, 0 });
            }
            writer.Write(lastEntryDataOffset);
            writer.Write(Data.Length);
        }
    }
}
