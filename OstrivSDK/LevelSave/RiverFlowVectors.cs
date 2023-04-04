using OstrivSDK;
using System.Collections;

namespace OstrivSDKLib.LevelSave
{
    [IEnumerableFieldYamlType(nameof(Vectors))]
    public struct RiverFlowVectors : IEnumerable, IEnumerable<Vector3D>, ISerializable<RiverFlowVectors>
    {
        public List<Vector3D> Vectors;

        public Vector3D this[int index]
        {
            get => Vectors[index];
            set => Vectors[index] = value;
        }

        public IEnumerator<Vector3D> GetEnumerator() => Vectors.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public RiverFlowVectors FromBytes(BinaryReader reader, object? arg = null)
        {
            var length = reader.ReadInt32();
            reader.BaseStream.Seek(length, SeekOrigin.Current);

            Vectors = new List<Vector3D>();
            var vectorsCount = reader.ReadInt32();
            for (int i = 0; i < vectorsCount; i++)
            {
                Vectors.Add(new Vector3D().FromBytes(reader));
            }

            return this;
        }

        public void ToBytes(BinaryWriter writer, object? arg = null)
        {
            var name = "river_flow_vectors";
            writer.Write(name.Length);
            writer.Write(name.ToCharArray());

            writer.Write(Vectors.Count);

            foreach (var point in Vectors)
            {
                point.ToBytes(writer);
            }
        }
    }
}
