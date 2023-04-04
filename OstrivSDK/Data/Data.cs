namespace OstrivSDK
{
    public struct Data : ISerializable<Data>
    {
        static int Signature { get; } = 12121212;
        public DataEntry[] Entries;

        public void ToBytes(BinaryWriter writer, object? arg = null)
        {
            writer.Write(Signature);

            var dataOffsetPosition = writer.BaseStream.Position;
            writer.Write(0);
            writer.Write(Entries.Length);

            var lastEntryDataOffset = 0;
            foreach (var entry in Entries)
            {
                entry.ToBytes(writer, lastEntryDataOffset);
                lastEntryDataOffset += entry.Data.Length;
            }

            var dataOffset = writer.BaseStream.Position;
            foreach (var entry in Entries)
                writer.Write(entry.Data);

            writer.BaseStream.Seek(dataOffsetPosition, SeekOrigin.Begin);
            writer.Write((int)dataOffset);
        }

        public Data FromBytes(BinaryReader reader, object? arg = null)
        {
            reader.ReadInt32(); //Signature
            var dataOffset = reader.ReadInt32();
            var count = reader.ReadInt32();

            var entries = new List<DataEntry>();
            for (int i = 0; i < count; i++)
                entries.Add(new DataEntry().FromBytes(reader, dataOffset));

            Entries = entries.ToArray();

            return this;
        }
    }
}
