namespace OstrivSDK
{
    internal interface ISerializable<T>
    {
        public void ToBytes(BinaryWriter writer, object? arg);
        public T FromBytes(BinaryReader reader, object? arg);
    }
}
