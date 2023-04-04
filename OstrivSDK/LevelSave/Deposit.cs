namespace OstrivSDK
{
    public struct Deposit : ISerializable<Deposit>
    {
        public string Name;
        public Point3D Position;
        public float Rotation;
        public int Size;

        public Deposit FromBytes(BinaryReader reader, object? arg = null)
        {
            var length = reader.ReadInt32();
            Name = new string(reader.ReadChars(length));
            Position = new Point3D().FromBytes(reader);
            Rotation = reader.ReadSingle();
            Size = reader.ReadInt32();

            return this;
        }

        public void ToBytes(BinaryWriter writer, object? arg = null)
        {
            writer.Write(Name.Length);
            writer.Write(Name.ToCharArray());
            Position.ToBytes(writer);
            writer.Write(Rotation);
            writer.Write(Size);
        }
    }
}
