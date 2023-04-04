namespace OstrivSDK
{
    public struct Point3D : ISerializable<Point3D>
    {
        public float X, Y, Z;

        public Point3D FromBytes(BinaryReader reader, object? arg = null)
        {
            X = reader.ReadSingle();
            Z = reader.ReadSingle();
            Y = reader.ReadSingle();
            return this;
        }

        public void ToBytes(BinaryWriter writer, object? arg = null)
        {
            writer.Write(BitConverter.GetBytes(X));
            writer.Write(BitConverter.GetBytes(Z));
            writer.Write(BitConverter.GetBytes(Y));
        }
    }
}
