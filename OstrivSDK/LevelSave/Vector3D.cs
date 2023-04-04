namespace OstrivSDK
{
    public struct Vector3D : ISerializable<Vector3D>
    {
        public Point3D Start, End;

        public Vector3D FromBytes(BinaryReader reader, object? arg = null)
        {
            Start.FromBytes(reader);
            End.FromBytes(reader);
            return this;
        }

        public void ToBytes(BinaryWriter writer, object? arg = null)
        {
            Start.ToBytes(writer);
            End.ToBytes(writer);
        }
    }
}
