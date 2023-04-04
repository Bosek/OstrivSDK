using OstrivSDK;
using System.Collections;

namespace OstrivSDKLib
{
    [IEnumerableFieldYamlType(nameof(Spots))]
    public struct FishingSpots : IEnumerable, IEnumerable<Point3D>, ISerializable<FishingSpots>
    {
        public List<Point3D> Spots;

        public Point3D this[int index]
        {
            get => Spots[index];
            set => Spots[index] = value;
        }

        public IEnumerator<Point3D> GetEnumerator() => Spots.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public FishingSpots FromBytes(BinaryReader reader, object? arg = null)
        {
            var length = reader.ReadInt32();
            reader.BaseStream.Seek(length, SeekOrigin.Current);

            Spots = new List<Point3D>();
            var fishingSpotsCount = reader.ReadInt32();
            for (int i = 0; i < fishingSpotsCount; i++)
            {
                Spots.Add(new Point3D().FromBytes(reader));
            }

            return this;
        }

        public void ToBytes(BinaryWriter writer, object? arg = null)
        {
            var name = "fishing_spots";
            writer.Write(name.Length);
            writer.Write(name.ToCharArray());

            writer.Write(Spots.Count);

            foreach (var point in Spots)
            {
                point.ToBytes(writer);
            }
        }    
    }
}
