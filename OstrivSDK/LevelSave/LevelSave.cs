using OstrivSDKLib;
using OstrivSDKLib.LevelSave;

namespace OstrivSDK
{
    public struct LevelSave : ISerializable<LevelSave>
    {
        public Deposit[] Deposits;
        public FishingSpots FishingSpots;
        public RiverFlowVectors RiverFlowVectors;

        public LevelSave FromBytes(BinaryReader reader, object? arg = null)
        {
            Deposits = Array.Empty<Deposit>();

            var count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                var lengthOffset = reader.BaseStream.Position;
                var length = reader.ReadInt32();
                var name = new string(reader.ReadChars(length));
                reader.BaseStream.Seek(lengthOffset, SeekOrigin.Begin);

                if (name == "fishing_spots")
                    FishingSpots = new FishingSpots().FromBytes(reader);
                else if (name == "river_flow_vectors")
                    RiverFlowVectors = new RiverFlowVectors().FromBytes(reader);
                else
                {
                    var deposits = Deposits.ToList();
                    deposits.Add(new Deposit().FromBytes(reader));
                    Deposits = deposits.ToArray();
                }
            }

            return this;
        }

        public void ToBytes(BinaryWriter writer, object? arg = null)
        {
            writer.Write(Deposits.Length + 2);
            foreach (var deposit in Deposits)
                deposit.ToBytes(writer);
            FishingSpots.ToBytes(writer);
            RiverFlowVectors.ToBytes(writer);
        }
    }
}
