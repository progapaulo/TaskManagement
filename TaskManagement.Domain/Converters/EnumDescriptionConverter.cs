using Newtonsoft.Json;
using System;
using System.ComponentModel;

namespace TaskManagement.Domain.Converters;

public class EnumDescriptionConverter : JsonConverter
{
    // Verifica se o tipo é enum
    public override bool CanConvert(Type objectType)
    {
        return objectType.IsEnum;
    }

    // Serializa a enum para a descrição
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        if (value == null)
        {
            writer.WriteNull();
        }
        else
        {
            var enumValue = (System.Enum)value;
            var field = enumValue.GetType().GetField(enumValue.ToString());
            var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
            var description = attribute != null ? attribute.Description : enumValue.ToString();
            writer.WriteValue(description);
        }
    }

    // Deserializa a enumeração - pode ser implementado conforme necessário
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}