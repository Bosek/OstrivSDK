using System.Reflection;
using YamlDotNet.Core;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.Utilities;

namespace OstrivSDKLib
{
    [AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class, AllowMultiple = false)]
    internal class IEnumerableFieldYamlTypeAttribute : Attribute
    {
        public string FieldName { get; private set; }

        public IEnumerableFieldYamlTypeAttribute(string fieldName)
        {
            FieldName = fieldName;
        }
    }
    internal class IEnumerableYamlTypeConverter : IYamlTypeConverter
    {
        public IValueDeserializer ValueDeserializer { get; private set; }
        public Type[] Types { get; private set; }

        public IEnumerableYamlTypeConverter(IValueDeserializer valueDeserializer, Type[] types)
        {
            ValueDeserializer = valueDeserializer;
            Types = types;
        }

        public bool Accepts(Type type)
        {
            return Types.Contains(type);
        }

        public object? ReadYaml(IParser parser, Type type)
        {
            var attribute = type.GetCustomAttribute<IEnumerableFieldYamlTypeAttribute>(true) ?? throw new InvalidDataException("Invalid YAML format.");
            var field = type.GetField(attribute.FieldName) ?? throw new InvalidDataException("Invalid YAML format.");
            var data = ValueDeserializer.DeserializeValue(parser, field.FieldType, new SerializerState(), ValueDeserializer) ?? throw new InvalidDataException("Invalid YAML format."); ;

            var obj = Activator.CreateInstance(type);
            field.SetValue(obj, data);

            return obj;
        }

        public void WriteYaml(IEmitter emitter, object? value, Type type) => throw new NotImplementedException();
    }
}
