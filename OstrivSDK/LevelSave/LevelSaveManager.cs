using OstrivSDKLib;
using OstrivSDKLib.LevelSave;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace OstrivSDK
{
    public static class LevelSaveManager
    {
        static readonly SerializerBuilder yamlSerializerBuilder = new SerializerBuilder()
            .WithIndentedSequences()
            .DisableAliases()
            .WithNamingConvention(LowerCaseNamingConvention.Instance);

        static readonly DeserializerBuilder yamlDeserializerBuilder = new DeserializerBuilder()
            .WithNamingConvention(LowerCaseNamingConvention.Instance);

        static readonly IEnumerableYamlTypeConverter iEnumerableYamlTypeConverter = new(yamlDeserializerBuilder.BuildValueDeserializer(), new Type[]
        {
            typeof(FishingSpots),
            typeof(RiverFlowVectors)
        });

        static readonly ISerializer yamlSerializer = yamlSerializerBuilder
            .Build();

        static readonly IDeserializer yamlDeserializer = yamlDeserializerBuilder
            .WithTypeConverter(iEnumerableYamlTypeConverter)
            .Build();

        public static LevelSave Load(string filename)
        {
            using var file = File.OpenRead(filename);
            using var reader = new BinaryReader(file);

            var levelSave = new LevelSave().FromBytes(reader);

            return levelSave;
        }

        public static LevelSave LoadYaml(string filename)
        {
            return yamlDeserializer.Deserialize<LevelSave>(File.ReadAllText(filename));
        }

        public static LevelSave Save(this LevelSave levelSave, string filename)
        {
            using var file = File.OpenWrite(filename);
            using var writer = new BinaryWriter(file);

            levelSave.ToBytes(writer);

            return levelSave;
        }

        public static LevelSave SaveYaml(this LevelSave levelSave, string filename)
        {
            File.WriteAllText(filename, yamlSerializer.Serialize(levelSave));

            return levelSave;
        }
    }
}
